Ext.define('WX.controller.UnsaleProductSetting', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.UnsaleProductSettingStore'],
	stores: ['DataDict.EnableDisableTypeStore'],
	models: ['BaseData.UnsaleProductSettingModel'],
	views: ['UnsaleProductSetting.UnsaleProductSettingList', 'UnsaleProductSetting.UnsaleProductSettingEdit', 'UnsaleProductSetting.UnsaleProductSelector'],
	refs: [{
		ref: 'unsaleProductSettingList',
		selector: 'UnsaleProductSettingList'
	}, {
		ref: 'gridUnsaleProductSetting',
		selector: 'UnsaleProductSettingList grid[name=gridUnsaleProductSetting]'
	}, {
		ref: 'gridUnsaleProductSettingItem',
		selector: 'UnsaleProductSettingList grid[name=gridUnsaleProductSettingItem]'
	}, {
		ref: 'unsaleProductSettingEdit',
		selector: 'UnsaleProductSettingEdit'
	}, {
		ref: 'unsaleProductSelector',
		selector: 'UnsaleProductSelector'
	}, {
		ref: 'gridUnsaleProductSelector',
		selector: 'UnsaleProductSelector grid[name=productList]'
	}],
	init: function () {
		var me = this;
		me.control({
			'UnsaleProductSettingList button[action=addUnsaleProductSetting]': {
				click: me.addUnsaleProductSetting
			},
			'UnsaleProductSettingList button[action=delUnsaleProductSetting]': {
				click: me.delUnsaleProductSetting
			},
			'UnsaleProductSettingList button[action=enableUnsaleProductSetting]': {
				click: me.enableUnsaleProductSetting
			},
			'UnsaleProductSettingList button[action=disableUnsaleProductSetting]': {
				click: me.disableUnsaleProductSetting
			},
			'UnsaleProductSettingList button[action=search]': {
				click: me.search
			},
			'UnsaleProductSettingList button[action=addProduct]': {
				click: me.addProduct
			},
			'UnsaleProductSettingList button[action=delProduct]': {
				click: me.delProduct
			},
			'UnsaleProductSettingList grid[name=gridUnsaleProductSetting]': {
				itemdblclick: me.editUnsaleProductSetting,
				select: me.selectUnsaleProductSetting
			},
			'UnsaleProductSettingEdit button[action=save]': {
				click: me.saveUnsaleProductSetting
			},
			'UnsaleProductSelector button[action=search]': {
				click: me.searchProduct
			},
			'UnsaleProductSelector button[action=save]': {
				click: me.saveProduct
			}
		});
	},
	selectUnsaleProductSetting: function (grid, record, index, eOpts) {
		var me = this;
		var store = this.getGridUnsaleProductSettingItem().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			UnsaleProductSettingID: record.data.ID,
			IsUnsaleProduct: true,
		});
		store.reload();
	},
	addUnsaleProductSetting: function () {
		var me = this;
		var win = Ext.widget('UnsaleProductSettingEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('新增滞销提醒配置');
		win.show();
	},
	delUnsaleProductSetting: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选择要删除的配置');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除吗?', function (optional) {
			if (optional === 'yes') {
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
							me.getGridUnsaleProductSettingItem().getStore().reload();
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
	enableUnsaleProductSetting: function () {
		Ext.Msg.alert('提示', '启用');
	},
	disableUnsaleProductSetting: function () {
		Ext.Msg.alert('提示', '禁用');
	},
	addProduct: function () {
		var me = this;
		var win = Ext.widget('UnsaleProductSelector');
		var selectedItems = me.getGridUnsaleProductSetting().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选择要添加的配置');
			return;
		} else {
			me.tasks = selectedItems;
		}
		var store = win.down('grid').getStore();
		store.proxy.extraParams = { ProductType: 1, IsUnsaleProduct: true };
		store.load();
		win.show();
	},
	delProduct: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选择要删除的产品配置');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除吗?', function (optional) {
			if (optional === 'yes') {
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
	editUnsaleProductSetting: function (grid, record) {
		var win = Ext.widget('UnsaleProductSettingEdit');
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑滞销产品提示配置');
		win.show();
	},
	saveUnsaleProductSetting: function () {
		var me = this;
		var win = me.getUnsaleProductSettingEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getGridUnsaleProductSetting().getStore();
			if (form.actionMethod == "POST") {
				store.create(formValues, {
					callback: function (records, operation, success) {
						if (!success) {
							Ext.Msg.alert('提示', operation.error);
							return;
						} else {
							store.add(records[0].data);
							store.commitChanges();
							Ext.Msg.alert('提示', '新增成功');
							win.close();
						}
					}
				})
			} else {
				if (!form.isDirty()) {
					win.close();
					return;
				}
				form.updateRecord();
				store.update({
					callback: function (records, operation, success) {
						if (!success) {
							Ext.Msg.alert('提示', operation.error);
							return;
						} else {
							Ext.Msg.alert('提示', '更新成功');
							win.close();
						}
					}
				});
			}
		}
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			me.getGridUnsaleProductSetting().getSelectionModel().clearSelections();
			me.getGridUnsaleProductSetting().getStore().load();
			me.getGridUnsaleProductSettingItem().getStore().removeAll();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	searchProduct: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.ProductType = 1;
			queryValues.IsUnsaleProduct = true;
			var store = me.getGridUnsaleProductSelector().getStore();
			store.load({ params: queryValues });
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	saveProduct: function (btn) {
		var me = this;
		var win = btn.up('window');
		var store = me.getGridUnsaleProductSettingItem().getStore();
		var selectedItems = win.down('grid[name=productList]').getSelectionModel().getSelection();
		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '未选择数据');
			return;
		}	
		me.tasks.forEach(function (item) {
			var productList = [];
			selectedItems.forEach(function (index) {
				productList.push({ UnsaleProductSettingID: item.data.ID, ProductID: index.data.ID });
			});
			store.batchAdd(productList, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				if (ajaxResult.Data == false) {
					Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
					return;
				}
			}, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
				return;
			});
		});
		store.reload();
		win.close();
	}
});