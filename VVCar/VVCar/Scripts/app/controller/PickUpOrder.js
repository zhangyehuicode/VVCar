Ext.define('WX.controller.PickUpOrder', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.PickUpOrderStore','WX.store.BaseData.PickUpOrderTaskDistributionStore'],
	models: ['BaseData.PickUpOrderModel', 'BaseData.PickUpOrderTaskDistributionModel'],
	views: ['PickUpOrder.PickUpOrder', 'PickUpOrder.PickUpOrderDetails', 'PickUpOrder.ProductSelector', 'PickUpOrder.UserSelector'],
	refs: [{
		ref: 'pickUpOrderList',
		selector: 'PickUpOrder grid[name=pickuporder]',
	}, {
		ref: 'gridProductSelector',
		selector: 'ProductSelector grid[name=productList]',
	}, {
		ref: 'productSelector',
		selector: 'ProductSelector',
	}, {
		ref: 'formPickUpOrder',
		selector: 'PickUpOrderDetails form[name=pickuporderformpanel]',
	}, {
		ref: 'gridPickUpOrderItem',
		selector: 'PickUpOrderDetails grid[name=pickuporderitemgrid]',
	}, {
		ref: 'gridPickUpOrderItemUser',
		selector: 'PickUpOrderDetails grid[name=pickuporderitemusergrid]',
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
			'PickUpOrderDetails textfield[name=PlateNumber]': {
				blur: me.blur
			},
			'PickUpOrderDetails button[action=checkOpenOrder]': {
				click: me.checkOpenOrder
			},
			'PickUpOrder': {
				deleteActionClick: me.deletepickuporder,
				pickuporderdetailsClick: me.pickuporderdetails,
			},
			'PickUpOrder grid': {
				itemdblclick: me.pickuporderdetails,
			},
			'PickUpOrder grid[name=pickuporder]': {
				afterrender: me.pickuporderafterrender,
			},
			'PickUpOrderDetails grid[name=pickuporderitemgrid]': {
				edit: me.edit,
				select: me.gridPickUpOrderItemSelect,
			},
			'PickUpOrderDetails button[action=addPickUpOrderItem]': {
				click: me.addPickUpOrderItem
			},
			'PickUpOrderDetails button[action=delPickUpOrderItem]': {
				click: me.delPickUpOrderItem
			},
			'PickUpOrderDetails button[action=addPickUpOrderItemCrew]': {
				click: me.addPickUpOrderItemCrew
			},
			'PickUpOrderDetails button[action=addPickUpOrderItemSalesman]': {
				click: me.addPickUpOrderItemSalesman
			},
			'PickUpOrderDetails button[action=delPickUpOrderItemUser]': {
				click: me.delPickUpOrderItemUser
			},
			'PickUpOrderDetails button[action=payorder]': {
				click: me.payorder
			},
			'ProductSelector button[action=save]': {
				click: me.saveItem
			},
			'UserSelector button[action=save]': {
				click: me.saveUser
			}
		});
	},
	itemdblclick: function () {
		Ext.Msg.alert('hello', 'hei');
	},
	payorder: function (btn) {
		var me = this;
		var win = btn.up('window');
		var count = me.getGridPickUpOrderItem().getStore().getTotalCount();
		if (count < 1) {
			Ext.Msg.alert('提示', '请先添加项目!');
			return;
		}
		var payMoney = win.down('numberfield[name=PayMoney]').getValue();
		if (payMoney == null || payMoney == '') {
			Ext.Msg.alert('提示', '请输入金额');
			return;
		}
		if (payMoney > win.down('numberfield[name=StillOwedMoney]').getValue()) {
			Ext.Msg.alert('提示', '结算金额不要大于尚欠金额!');
			return;
		}

		var payType = win.down('radiogroup[name=PayType]').getValue();
		var code = win.down('textfield[name=Code]').getValue();
		var store = me.getPickUpOrderList().getStore();
		if (payType.type == 2) {
			Ext.Msg.confirm('提示', '确定要<span><font color="red">现金</font></span>支付吗?', function (opt) {
				if (opt === 'yes') {
					//现金支付
					var pickUpOrderDetail = {};
					pickUpOrderDetail.PickUpOrderID = win.down('textfield[name=ID]').getValue();
					pickUpOrderDetail.PickUpOrderCode = code;
					pickUpOrderDetail.PayType = payType;
					pickUpOrderDetail.PayInfo = '现金支付:' + payMoney + '元';
					pickUpOrderDetail.payMoney = payMoney;
					pickUpOrderDetail.MemberInfo = win.down('textfield[name=MemberName]').getValue();
					pickUpOrderDetail.StaffID = win.down('textfield[name=StaffID]').getValue();
					pickUpOrderDetail.StaffName = win.down('textfield[name=StaffName]').getValue();
					store.payorder(pickUpOrderDetail, function (response, opts) {
						var ajaxResult = JSON.parse(response.responseText);
						if (ajaxResult.IsSuccessful) {
							var receivedMoney = win.down('numberfield[name=ReceivedMoney]').getValue();
							win.down('numberfield[name=ReceivedMoney]').setValue(Number(receivedMoney) + Number(payMoney));
							var stillOwedMoney = win.down('numberfield[name=StillOwedMoney]').getValue();
							win.down('numberfield[name=StillOwedMoney]').setValue(Number(stillOwedMoney) - Number(payMoney));
							if (win.down('numberfield[name=StillOwedMoney]').getValue() == 0) {
								win.down('textfield[name=Status]').setValue('已付款');
							} else {
								win.down('textfield[name=Status]').setValue('付款不足');
							}
							store.load();
							Ext.Msg.alert('提示', '现金支付' + payMoney + '元成功');
						} else {
							Ext.Msg.alert("提示", ajaxResult.ErrorMessage);
							return;
						}
					}, function (response, opts) {
						var ajaxResult = JSON.parse(response.responseText);
						Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
					});
				}
			});
		}
		if (payType.type == 6) {
			Ext.Msg.confirm('提示', '确定要<span><font color="red">储值卡</font></span>支付吗?', function (opt) {
				if (opt === 'yes') {
					//储值卡支付处理
					var verificationByMemberCardParam = {};
					var cardBalance = win.down('numberfield[name=CardBalance]').getValue();
					if (cardBalance < payMoney) {
						Ext.Msg.alert('提示', '储值卡余额不足!');
						return;
					}
					verificationByMemberCardParam.PickUpOrderID = win.down('textfield[name=ID]').getValue();
					verificationByMemberCardParam.Code = code;
					verificationByMemberCardParam.MemberID = win.down('textfield[name=MemberID]').getValue();
					verificationByMemberCardParam.PayMoney = payMoney;
					verificationByMemberCardParam.StaffID = win.down('textfield[name=StaffID]').getValue();
					verificationByMemberCardParam.StaffName = win.down('textfield[name=StaffName]').getValue();
					store.verificationByMemberCard(verificationByMemberCardParam, function (response, opts) {
						var ajaxResult = JSON.parse(response.responseText);
						if (ajaxResult.IsSuccessful) {
							var receivedMoney = win.down('numberfield[name=ReceivedMoney]').getValue();
							win.down('numberfield[name=ReceivedMoney]').setValue(Number(receivedMoney) + Number(payMoney));
							var stillOwedMoney = win.down('numberfield[name=StillOwedMoney]').getValue();
							win.down('numberfield[name=StillOwedMoney]').setValue(Number(stillOwedMoney) - Number(payMoney));
							var cardBalance = win.down('numberfield[name=CardBalance]').getValue(); 
							win.down('numberfield[name=CardBalance]').setValue(Number(cardBalance) - Number(payMoney));
							if (win.down('numberfield[name=StillOwedMoney]').getValue() == 0) {
								win.down('textfield[name=Status]').setValue('已付款');
							} else {
								win.down('textfield[name=Status]').setValue('付款不足');
							}
							store.load();
							Ext.Msg.alert('提示', '储值卡支付' + payMoney + '元成功');
						} else {
							Ext.Msg.alert("提示", ajaxResult.ErrorMessage);
							return;
						}
					}, function (response, opts) {
						var ajaxResult = JSON.parse(response.responseText);
						Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
					});
				}
			});			
		} 
	},
	checkOpenOrder: function (btn) {
		var me = this;
		var win = btn.up('window');
		var store = me.getPickUpOrderList().getStore();
		var pickuporder = {};
		pickuporder.PlateNumber = win.down('textfield[name=PlateNumber]').getValue();
		pickuporder.MemberID = win.down('textfield[name=MemberID]').getValue();
		store.create(pickuporder, {
			callback: function (records, operation, success) {
				if (!success) {
					Ext.MessageBox.alert("提示", operation.error);
					return;
				} else {
					store.add(records[0].data);
					store.commitChanges();
					store.reload();
					win.down('button[action=checkOpenOrder]').hide();
					win.down('textfield[name=Code]').show();
					win.down('form[name=form4]').show();
					win.down('form[name=form5]').show();
					win.down('form[name=form6]').show();
					//值更新
					win.down('textfield[name=PlateNumber]').setReadOnly(true);
					win.down('textfield[name=ID]').setValue(records[0].data.ID)
					win.down('textfield[name=Code]').setValue(records[0].data.Code);
					win.down('textfield[name=StaffID]').setValue(records[0].data.StaffID);
					win.down('textfield[name=StaffName]').setValue(records[0].data.StaffName);
					win.down('numberfield[name=Money]').setValue(records[0].data.Money);
					win.down('numberfield[name=ReceivedMoney]').setValue(records[0].data.ReceivedMoney);
					win.down('numberfield[name=StillOwedMoney]').setValue(records[0].data.StillOwedMoney);
					win.down('textfield[name=Status]').setValue(records[0].data.Status);
					win.down('textfield[name=CreatedDate]').setValue(records[0].data.CreatedDate);
					var pickUpOrderItemStore = win.down('grid[name=pickuporderitemgrid]').getStore();
					var statusdesc = "";
					switch (records[0].data.Status) {
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
					pickUpOrderItemStore.proxy.extraParams = { PickUpOrderID: records[0].data.ID };
					pickUpOrderItemStore.load();
				}
			}
		});
	},
	edit: function (editor, context, eOpts) {
		var me = this;
		var pickorder = me.getFormPickUpOrder();
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
						context.store.load();
					}
				}
			});
		} else {
			if (!context.record.dirty)
				return;
			if(context.record.data.IsReduce == 'on') {
				context.record.data.IsReduce = 'true';
			}
			context.store.updatepickuporder(context.record.data, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				if (ajaxResult.IsSuccessful) {
					if (ajaxResult.Data != null) {
						pickorder.down('textfield[name=Money]').setValue(ajaxResult.Data.Money);
						pickorder.down('textfield[name=ReceivedMoney]').setValue(ajaxResult.Data.ReceivedMoney);
						pickorder.down('textfield[name=StillOwedMoney]').setValue(ajaxResult.Data.StillOwedMoney);
					}
				}
				store.load();
				me.getGridPickUpOrderItem().getStore().load();
			}, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
			});
		}
	},
	addPickUpOrderItem: function (btn) {
		var win = Ext.widget('ProductSelector');
		var id = btn.up('window').down('textfield[name=ID]').getValue();
		if (id == '') {
			Ext.Msg.alert('提示', '请先开单!');
			return;
		}
		win.down('textfield[name=PickUpOrderID]').setValue(id);
		win.show();
	},
	delPickUpOrderItem: function (btn) {
		var me = this;
		var pickorder = me.getFormPickUpOrder();
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选中要删除的项目!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除吗?', function (opt) {
			if (opt === 'yes') {
				var store = btn.up('grid').getStore();
				var ids = [];
				selectedItems.forEach(function (item) {
					ids.push(item.data.ID);
				});
				store.batchDelete(ids,
					function success(response, request, c) {
						var ajaxResult = JSON.parse(c.responseText);
						if (ajaxResult.IsSuccessful) {
							if (ajaxResult.Data != null) {
								pickorder.down('textfield[name=Money]').setValue(ajaxResult.Data.Money);
								pickorder.down('textfield[name=ReceivedMoney]').setValue(ajaxResult.Data.ReceivedMoney);
								pickorder.down('textfield[name=StillOwedMoney]').setValue(ajaxResult.Data.StillOwedMoney);
							}
							store.reload();
							Ext.Msg.alert('提示', '删除成功');
						} else {
							Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
					}
				);
			}
		});
	},
	addPickUpOrderItemCrew: function () {
		var me = this;
		var win = Ext.widget('UserSelector');
		var selectedItems = me.getGridPickUpOrderItem().getSelectionModel().getSelection();
		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '请先选中项目');
			return;
		}
		win.down('textfield[name=PeopleType]').setValue(0);
		win.show();
	},
	addPickUpOrderItemSalesman: function () {
		var me = this;
		var win = Ext.widget('UserSelector');
		var selectedItems = me.getGridPickUpOrderItem().getSelectionModel().getSelection();
		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '请先选中项目');
			return;
		}
		win.down('textfield[name=PeopleType]').setValue(1);
		win.show();
	},
	delPickUpOrderItemUser: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选中要删除的人员!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除吗?', function (opt) {
			if (opt === 'yes') {
				var store = btn.up('grid').getStore();
				var ids = [];
				selectedItems.forEach(function (item) {
					ids.push(item.data.ID);
				});
				store.batchDelete(ids,
					function success(response, request, c) {
						var ajaxResult = JSON.parse(c.responseText);
						if (ajaxResult.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '删除成功');
						} else {
							Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
					}
				);
			}
		});
	},
	saveUser: function (btn) {
		var me = this;
		var win = btn.up('window');
		var peopleType = win.down('textfield[name=PeopleType]').getValue();
		var selectedItems = win.down('grid[name=gridUser]').getSelectionModel().getSelection();
		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '请先选中用户');
			return;
		}
		var pickUpOrderID = me.getFormPickUpOrder().down('textfield[name=ID]').getValue();
		var pickUpOrderItemID = me.getGridPickUpOrderItem().getSelectionModel().getSelection()[0].data.ID;
		var pickUpOrderTaskDistributions = [];
		selectedItems.forEach(function (item) {
			pickUpOrderTaskDistributions.push({ PickUpOrderID: pickUpOrderID, PickUpOrderItemID: pickUpOrderItemID, UserID: item.data.ID, peopleType: peopleType });
		});
		var store = me.getGridPickUpOrderItemUser().getStore();
		store.batchAdd(pickUpOrderTaskDistributions, function (response, opts) {
			var ajaxResult = JSON.parse(response.responseText);
			if (ajaxResult.Data == false) {
				Ext.Msg.alert("提示", ajaxResult.ErrorMessage);
				return;
			}
			store.reload();
			win.close();
		}, function (response, opts) {
			var ajaxResult = JSON.parse(response.responseText);
			Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
		});
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
		var pickorder = me.getFormPickUpOrder();
		var gridProduct = win.down('grid[name=gridProduct]');
		var selectedItems = gridProduct.getSelectionModel().getSelection();
		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '未选择操作数据');
			return;
		}

		var pickupOrderID = win.down('textfield[name=PickUpOrderID]').getValue();
		var store = me.getGridPickUpOrderItem().getStore();
		var pickuporderitems = [];
		selectedItems.forEach(function (item) {
			pickuporderitems.push({ PickUpOrderID: pickupOrderID, ProductID: item.data.ID, ProductCode: item.data.Code, ProductName: item.data.Name, PriceSale: item.data.PriceSale, ImgUrl: item.data.ImgUrl, Quantity: 1, });
		});
		store.batchAdd(pickuporderitems, function (response, opts) {
			var ajaxResult = JSON.parse(response.responseText);
			if (ajaxResult.IsSuccessful) {
				if (ajaxResult.Data != null) {
					pickorder.down('textfield[name=Money]').setValue(ajaxResult.Data.Money);
					pickorder.down('textfield[name=ReceivedMoney]').setValue(ajaxResult.Data.ReceivedMoney);
					pickorder.down('textfield[name=StillOwedMoney]').setValue(ajaxResult.Data.StillOwedMoney);
				}
				store.load();
			}
			win.close();
		}, function (response, opts) {
			var ajaxResult = JSON.parse(response.responseText);
			Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
		});
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
		win.form.loadRecord(record);
		var pickUpOrderItemStore = win.down('grid[name=pickuporderitemgrid]').getStore();

		var statusdesc = "";
		switch (record.data.Status) {
			case 0:
				statusdesc = "未付款";
				break;
			case 1:
				statusdesc = "已付款";
				win.down('grid[name=pickuporderitemgrid]').down('button[action=addPickUpOrderItem]').hide();
				win.down('grid[name=pickuporderitemgrid]').down('button[action=delPickUpOrderItem]').hide();
				win.down('grid[name=pickuporderitemusergrid]').down('button[action=addPickUpOrderItemCrew]').hide();
				win.down('grid[name=pickuporderitemusergrid]').down('button[action=addPickUpOrderItemSalesman]').hide();
				win.down('grid[name=pickuporderitemusergrid]').down('button[action=delPickUpOrderItemUser]').hide();
				break;
			case 2:
				statusdesc = "付款不足";
				win.down('grid[name=pickuporderitemgrid]').down('button[action=addPickUpOrderItem]').hide();
				win.down('grid[name=pickuporderitemgrid]').down('button[action=delPickUpOrderItem]').hide();
				win.down('grid[name=pickuporderitemusergrid]').down('button[action=addPickUpOrderItemCrew]').hide();
				win.down('grid[name=pickuporderitemusergrid]').down('button[action=addPickUpOrderItemSalesman]').hide();
				win.down('grid[name=pickuporderitemusergrid]').down('button[action=delPickUpOrderItemUser]').hide();
				break;
		}
		win.down('textfield[name=Status]').setValue(statusdesc);
		pickUpOrderItemStore.proxy.extraParams = { PickUpOrderID: record.data.ID };
		pickUpOrderItemStore.limit = 3;
		pickUpOrderItemStore.pageSize = 3;
		pickUpOrderItemStore.load();

		plateNumber = win.down('textfield[name=PlateNumber]').getValue();
		var memberStore = Ext.create('WX.store.BaseData.MemberStore');
		memberStore.getMemberByPlateNumber(plateNumber,
			function success(response, request, c) {
				var ajaxResult = JSON.parse(c.responseText);
				if (ajaxResult.IsSuccessful) {
					if (ajaxResult.Data != null) {
						var data = [];
						ajaxResult.Data.forEach(function (item) {
							data.push({ 'Name': item.Name, 'Value': item.ID, 'MobilePhoneNo': item.MobilePhoneNo, CardNumber: item.CardNumber, CardBalance: item.CardBalance, CardStatus: item.CardStatus, EffectiveDate: item.EffectiveDate });
						});
						if (data.length > 1) {
							Ext.Msg.alert('提示', '该车牌号绑定了多个会员，请联系管理员');
							return;
						}
						win.down("textfield[name=MemberID]").setValue(data[0].Value);
						win.down("textfield[name=MemberName]").setValue(data[0].Name);
						win.down("textfield[name=MemberMobilePhoneNo]").setValue(data[0].MobilePhoneNo);
						win.down("textfield[name=CardNumber]").setValue(data[0].CardNumber);
						win.down("textfield[name=CardBalance]").setValue(data[0].CardBalance);
						win.down("textfield[name=EffectiveDate]").setValue(data[0].EffectiveDate);
						win.down("textfield[name=CardStatus]").setValue(data[0].CardStatus);
					} else {
						Ext.Msg.alert('该车牌号尚未绑定会员');
						win.down("textfield[name=MemberID]").setValue('');
						win.down("textfield[name=MemberName]").setValue('');
						win.down("textfield[name=MemberMobilePhoneNo]").setValue('');
						win.down("textfield[name=CardNumber]").setValue('');
						win.down("textfield[name=CardBalance]").setValue('');
						win.down("textfield[name=CardStatus]").setValue('');
						win.down("textfield[name=EffectiveDate]").setValue('');
					}
				} else {
					Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
				}
			},
			function failure(a, b, c) {
				Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
			}
		);


		win.show();
	},
	gridPickUpOrderItemSelect: function (grid, record, index, eOpts) {
		var me = this;
		var win = Ext.widget('PickUpOrderDetails');
		var store = me.getGridPickUpOrderItemUser().getStore();
		store.proxy.extraParams = { PickUpOrderItemID: record.data.ID };
		store.load();
	},
	openorder: function (btn) {
		var win = Ext.widget('PickUpOrderDetails');
		//win.down('textfield[name=Code]').hide();
		win.down('button[action=checkOpenOrder]').show();
		win.down('form[name=form4]').hide();
		win.down('form[name=form5]').hide();
		win.down('form[name=form6]').hide();
		win.down('textfield[name=PlateNumber]').setReadOnly(false);
		win.setTitle('接车单开单');
		win.show();
	},
	blur: function (field, e, eOpts) {
		var me = this;
		var plateNumber = field.getValue();
		if (plateNumber.length == 0) {
			return;
		}
		if (plateNumber.length != 7 && plateNumber.length != 8) {
			Ext.Msg.alert('提示', '车牌号必须为7位或者八位');
			return;
		}
		var memberStore = Ext.create('WX.store.BaseData.MemberStore');
		memberStore.getMemberByPlateNumber(plateNumber,
			function success(response, request, c) {
				var ajaxResult = JSON.parse(c.responseText);
				if (ajaxResult.IsSuccessful) {
					if (ajaxResult.Data != null) {
						var data = [];
						ajaxResult.Data.forEach(function (item) {
							data.push({ Name: item.Name, MemberID: item.ID, MobilePhoneNo: item.MobilePhoneNo, CardNumber: item.CardNumber, CardBalance: item.CardBalance, CardStatus: item.CardStatus, EffectiveDate: item.EffectiveDate });
						});
						if (data.length > 1) {
							Ext.Msg.alert('提示', '该车牌号绑定了多个会员，请联系管理员');
							return;
						}
						field.up("window").down("textfield[name=MemberID]").setValue(data[0].MemberID);
						field.up("window").down("textfield[name=MemberName]").setValue(data[0].Name);
						field.up("window").down("textfield[name=MemberMobilePhoneNo]").setValue(data[0].MobilePhoneNo);
						field.up("window").down("textfield[name=CardNumber]").setValue(data[0].CardNumber);
						field.up("window").down("textfield[name=CardBalance]").setValue(data[0].CardBalance);
						field.up("window").down("textfield[name=EffectiveDate]").setValue(data[0].EffectiveDate);
						field.up("window").down("textfield[name=CardStatus]").setValue(data[0].CardStatus);
					} else {
						Ext.Msg.alert('该车牌号尚未绑定会员');
						field.up("window").down("textfield[name=MemberID]").setValue('');
						field.up("window").down("textfield[name=MemberName]").setValue('');
						field.up("window").down("textfield[name=MemberMobilePhoneNo]").setValue('');
						field.up("window").down("textfield[name=CardNumber]").setValue('');
						field.up("window").down("textfield[name=CardBalance]").setValue('');
						field.up("window").down("textfield[name=CardStatus]").setValue('');
						field.up("window").down("textfield[name=EffectiveDate]").setValue('');
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