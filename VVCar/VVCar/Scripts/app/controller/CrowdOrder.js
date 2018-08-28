Ext.define('WX.controller.CrowdOrder', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.CrowdOrderStore'],
	models: ['BaseData.CrowdOrderModel'],
	views: ['CrowdOrder.CrowdOrderList', 'CrowdOrder.CrowdOrderEdit', 'CrowdOrder.ProductSelector'],
	refs: [{
		ref: 'crowdOrderList',
		selector: 'CrowdOrderList'
	}, {
		ref: 'crowdOrderEdit',
		selector: 'CrowdOrderEdit'
	}, {
		ref: 'productSelector',
		selector: 'ProductSelector'
	}],
	init: function () {
		var me = this;
		me.control({
			'CrowdOrderList button[action=addCrowdOrder]': {
				click: me.addCrowdOrder
			},
			'CrowdOrderList button[action=delCrowdOrder]': {
				click: me.delCrowdOrder
			},
			'CrowdOrderList button[action=search]': {
				click: me.search
			},
			'CrowdOrderList': {
				itemdblclick: me.editCrowdOrder
			},
			'CrowdOrderEdit button[action=selectProduct]': {
				click: me.selectProduct
			},
			'CrowdOrderEdit button[action=save]': {
				click: me.save
			},
			'ProductSelector grid[name=productList]': {
				itemdblclick: me.chooseProduct
			}
		});
	},
	editCrowdOrder: function (grid, record) {
		var win = Ext.widget('CrowdOrderEdit');
		record.data.PutawayTime = record.data.PutawayTime.substring(0, 10);
		record.data.SoleOutTime = record.data.SoleOutTime.substring(0, 10);
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑拼单');
		win.show();
	},
	addCrowdOrder: function () {
		var me = this;
		var win = Ext.widget('CrowdOrderEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('新增拼单');
		win.show();
	},
	delCrowdOrder: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选择要删除的数据');
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
				)
			}
		});
	},
	selectProduct: function () {
		var win = Ext.widget('ProductSelector');
		var store = win.down('grid').getStore();
		store.proxy.extraParams = {
			ProductType: 0
		};
		store.load();
		win.show();
	},
	chooseProduct: function (grid, record) {
		var win = Ext.ComponentQuery.query('window[name=CrowdOrderEdit]')[0];
		win.down('textfield[name=ProductID]').setValue(record.data.ID);
		win.down('textfield[name=ProductName]').setValue(record.data.Name);
		grid.up('window').close();
	},
	save: function () {
		var me = this;
		var win = me.getCrowdOrderEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getCrowdOrderList().getStore();
			if (form.actionMethod == 'POST') {
				formValues.PutawayTime += ' 00:00:00';
				formValues.SoleOutTime += ' 00:00:00';
				store.create(formValues, {
					callback: function (records, operation, success) {
						if (!success) {
							Ext.Msg.alert('提示', operation.error);
							return;
						} else {
							store.add(records[0].data);
							store.commitChanges();
							Ext.Msg.alert('提示', '新增成功');
							store.reload();
							win.close();
						}
					}
				});
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
							store.reload();
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
			var store = me.getCrowdOrderList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('系统提示', '请输入过滤条件!');
		}
	},
})