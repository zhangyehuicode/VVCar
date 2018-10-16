Ext.define('WX.controller.PickUpOrder', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.PickUpOrderStore'],
	models: ['BaseData.PickUpOrderModel'],
	views: ['PickUpOrder.PickUpOrder', 'PickUpOrder.PickUpOrderDetails', 'PickUpOrder.ProductSelector'],
	refs: [{
		ref: 'pickUpOrderList',
		selector: 'PickUpOrder grid[name=pickuporder]',
	}, {
		ref: 'gridProductSelector',
		selector: 'ProductSelector grid[name=productList]',
	}, {
		ref: 'productSelector',
		selector: 'ProductSelector',
	}],
	init: function () {
		var me = this;
		me.control({
			'PickUpOrder button[action=search]': {
				click: me.search
			},
			'PickUpOrder button[action=openorder]': {
				click: me.openorder
			},
			'PickUpOrder textfield[name=PlateNumber]': {
				blur: me.blur
			},
			'PickUpOrderDetails grid[name=pickuporderitemgrid]': {
				edit: me.edit,
			},
			'PickUpOrder': {
				deleteActionClick: me.deletepickuporder,
				pickuporderdetailsClick: me.pickuporderdetails,
			},
			'PickUpOrder grid[name=pickuporder]': {
				edit: me.edit,
				afterrender: me.pickuporderafterrender,
			},
			'ProductSelector button[action=save]': {
				click: me.saveItem
			}
		});
	},
	edit: function (editor, context, eOpts) {
		var me = this;
		var win = me.win;
		var store = this.getPickUpOrderList().getStore();
		if (context.record.phantom) {
			context.store.create(context.record.data, {
				callback: function (records, operation, success) {
					if (!success) {
						Ext.Msg.alert('提示', operation.error);
						return;
					} else {
						context.record.copyFrom(records[0]);
						context.record.commit();
						Ext.Msg.alert('提示', '新增成功');
						context.store.reload();
						store.reload();
						win.form.loadRecord(me.record);
					}
				}
			});
		} else {
			if (!context.record.dirty)
				return;
			context.store.update({
				callback: function (records, operation, success) {
					if (!success) {
						Ext.Msg.alert("提示", operation.error);
						return;
					} else {
						Ext.Msg.alert('提示', '更新成功');
						context.store.reload();
						store.reload();
						store.data.items.forEach(function (item) {
							if (item.data.Code == me.record.data.Code) {
								me.record.Money = item.data.Money;
							}
						});
						win.form.loadRecord(me.record);
					}
				}
			})
		}
	},
	search: function (btn) {
		var store = this.getPickUpOrderList().getStore();
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.All = true;
			store.load({ params: queryValues });
		}
	},
	saveItem: function (btn) {
		var me = this;
		var win = btn.up('window');
		var gridProduct = win.down('grid[name=gridProduct]');
		var store = me.getPickUpOrderList().getStore();
		var selectedItems = gridProduct.getSelectionModel().getSelection();
		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '未选择操作数据');
			return;
		}
		var pickuporder = {};
		var pickUpOrderItemList = [];
		pickuporder.PlateNumber = me.plateNumber;
		pickuporder.MemberID = me.memberid;
		selectedItems.forEach(function (item) {
			pickUpOrderItemList.push({ ProductID: item.data.ID, ProductName: item.data.Name, ProductCode: item.data.Code, PriceSale: item.data.PriceSale, ImgUrl: item.data.ImgUrl, Quantity: 1 }); 
		});
		pickuporder.PickUpOrderItemList = pickUpOrderItemList;
		store.create(pickuporder, {
			callback: function (records, operation, success) {
				if (!success) {
					Ext.MessageBox.alert("提示", operation.error);
					return;
				} else {
					store.add(records[0].data);
					store.commitChanges();
					Ext.MessageBox.alert("提示", "新增成功");
					store.reload();
					win.close();
				}
			}
		})
	},
	deletepickuporder: function (grid, record) {
		var me = this;
		Ext.Msg.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据，请稍候...', '状态提示');
				var store = me.getPickUpOrderList().getStore();
				store.remove(record);
				store.sync({
					callback: function (batch, options) {
						Ext.Msg.hide();
						if (batch.hasException()) {
							Ext.Msg.alert("提示", batch.exceptions[0].error);
							store.rejectChanges();
						} else {
							Ext.Msg.alert("提示", "删除成功");
						}
					}
				});
			}
		});
	},
	pickuporderdetails: function (grid, record) {
		var me = this;
		var win = Ext.widget('PickUpOrderDetails');
		me.win = win;
		me.record = record;
		win.form.loadRecord(record);
		var pickUpOrderItemStore = win.down('grid[name=pickuporderitemgrid]').getStore();

		var statusdesc = "";
		switch (record.data.Status) {
			case 0:
				statusdesc = "未付款";
				break;
			case 1:
				statusdesc = "已付款";
				break;
			case 2:
				statusdesc = "付款不足";
				break;
		}
		win.down('textfield[name=Status]').setValue(statusdesc);
		pickUpOrderItemStore.proxy.extraParams = { PickUpOrderID: record.data.ID };
		pickUpOrderItemStore.load();
		win.show();
	},
	openorder: function (btn) {
		var me = this;
		if (me.plateNumber == null || me.plateNumber == '') {
			Ext.Msg.alert('提示', '请先输入车牌号');
			return;
		}
		var win = Ext.widget('ProductSelector');
		win.show();
	},
	blur: function (field, e, eOpts) {
		var me = this;
		var win = field.up("fieldcontainer").down("combobox[name=member]");
		var plateNumber = field.getValue();
		if (plateNumber.length == 0) {
			return;
		}
		if (plateNumber.length != 7 && plateNumber.length != 8) {
			Ext.Msg.alert('提示', '车牌号必须为7位或者八位');
			return;
		}
		me.plateNumber = plateNumber;
		var memberStore = Ext.create('WX.store.BaseData.MemberStore');
		memberStore.getMemberByPlateNumber(plateNumber,
			function success(response, request, c) {
				var ajaxResult = JSON.parse(c.responseText);
				if (ajaxResult.IsSuccessful) {
					if (ajaxResult.Data != null) {
						var data = [];
						ajaxResult.Data.forEach(function (item) {
							data.push({ 'Name': item.Name, 'Value': item.ID, 'MobilePhoneNo': item.MobilePhoneNo });
						});
						if (data.length > 1) {
							Ext.Msg.alert('提示', '该车牌号绑定了多个会员，请联系管理员');
							return;
						}
						me.memberid = data[0].Value;
						field.up("fieldcontainer").down("textfield[name=MemberID]").setValue(data[0].Value);
						field.up("fieldcontainer").down("textfield[name=MemberName]").setValue(data[0].Name);
						field.up("fieldcontainer").down("textfield[name=MobilePhoneNo]").setValue(data[0].MobilePhoneNo);
					} else {
						Ext.Msg.alert('该车牌号尚未绑定会员');
						field.up("fieldcontainer").down("textfield[name=MemberID]").setValue('');
						field.up("fieldcontainer").down("textfield[name=MemberName]").setValue('');
						field.up("fieldcontainer").down("textfield[name=MobilePhoneNo]").setValue('');
					}
				} else {
					Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
				}
			},
			function failure(a, b, c) {
				Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
			}
		);
	},
	pickuporderafterrender: function () {
		var me = this;
		var store = me.getPickUpOrderList().getStore();
		store.load();
	}
})