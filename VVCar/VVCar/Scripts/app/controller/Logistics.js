Ext.define('WX.controller.Logistics', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.OrderStore'],
	models: ['BaseData.OrderModel'],
	views: ['Logistics.Logistics', 'Logistics.LogisticsEdit', 'Logistics.LogisticsDetails', 'Logistics.SalesmanSelector'],
	refs: [{
		ref: 'logistics',
		selector: 'Logistics'
	}, {
		ref: 'logisticsEdit',
		selector: 'LogisticsEdit'
	}, {
		ref: 'salesmanSelector',
		selector: 'SalesmanSelector'
	}, {
		ref: 'gridSalesmanSelector',
		selector: 'SalesmanSelector grid'
	}],
	init: function () {
		var me = this;
		me.control({
			'Logistics button[action=search]': {
				click: me.search
			},
			'Logistics button[action=delivery]': {
				click: me.delivery
			},
			'Logistics button[action=antiDelivery]': {
				click: me.antiDelivery
			},
			'Logistics button[action=revisitTips]': {
				click: me.revisitTips
			},
			'Logistics': {
				itemdblclick: me.edit,
				editActionClick: me.edit,
				deleteActionClick: me.deleteOrder,
				logisticsdetailsClick: me.logisticsdetailsClick,
			},
			'LogisticsEdit button[action=selectSalesman]': {
				click: me.selectSalesman
			},
			'LogisticsEdit button[action=save]': {
				click: me.save
			},
			'SalesmanSelector grid[name=salesmanList]': {
				itemdblclick: me.choosseSalesman
			},
			'SalesmanSelector button[action=search]': {
				click: me.searchSalesman
			}
		});
	},
	logisticsdetailsClick: function (grid, record) {
		var win = Ext.widget("LogisticsDetails");
		win.form.loadRecord(record);
		var orderItemStore = win.down('grid[name=logisticsitemgrid]').getStore();

		var statusdesc = "";
		switch (record.data.Status) {
			case -1:
				statusdesc = '付款不足';
				break;
			case 0:
				statusdesc = '未付款';
				break;
			case 1:
				statusdesc = '已付款未发货';
				break;
			case 2:
				statusdesc = '已发货';
				break;
			case 3:
				statusdesc = '已完成';
				break;
		}
		win.down('textfield[name=Status]').setValue(statusdesc);

		orderItemStore.proxy.extraParams = { OrderID: record.data.ID };
		orderItemStore.load();
		win.show();
	},
	edit: function (grid, record) {
		var win = Ext.widget("LogisticsEdit");
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('发货');
		win.show();
	},
	save: function (btn) {
		var me = this;
		var win = me.getLogisticsEdit();
		var form = win.form.getForm();
		var order = form.getValues();
		var store = me.getLogistics().getStore();
		store.delivery(order,
			function success(response, request, c) {
				var result = Ext.decode(c.responseText);
				if (result.IsSuccessful) {
					store.reload();
					Ext.Msg.alert('提示', '发货成功!');
					win.close();
				} else {
					Ext.Msg.alert('提示', result.ErrorMessage);
				}
			},
			function failure(a, b, c) {
				Ext.Msg.alert('提示', '发货失败!');
			}
		);
	},
	search: function (btn) {
		var store = this.getLogistics().getStore();
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.All = true;
			store.load({ params: queryValues });
		}
	},
	searchSalesman: function (btn) {
		var me = this;
		var store = me.getGridSalesmanSelector().getStore();
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.All = true;
			store.load({ params: queryValues });
		}
	},
	choosseSalesman: function (grid, record) {
		var win = Ext.ComponentQuery.query('LogisticsEdit')[0];
		win.down('textfield[name=UserName]').setValue(record.data.Name);
		win.down('textfield[name=UserID]').setValue(record.data.ID);
		grid.up('window').close();
	},
	deleteOrder: function (grid, record) {
		var me = this;
		Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
				var store = me.getOrder().getStore();
				store.remove(record);
				store.sync({
					callback: function (batch, options) {
						Ext.Msg.hide();
						if (batch.hasException()) {
							Ext.MessageBox.alert("提示", batch.exceptions[0].error);
							store.rejectChanges();
						} else {
							Ext.MessageBox.alert("提示", "删除成功");
						}
					}
				});
			}
		});
	},
	delivery: function (btn) {
		var selectedItems = this.getLogistics().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选中需要编辑的数据');
			return;
		} else if (selectedItems.length > 1) {
			Ext.Msg.alert('提示', '一次只能选择一条数据');
			return;
		} else {
			if (selectedItems[0].data.Status != 1) {
				Ext.Msg.alert('提示', '请选择已付款未发货的数据!');
				return;
			}
		}
		this.edit(null, selectedItems[0]);
	},
	antiDelivery: function () {
		var me = this;
		var store = me.getLogistics().getStore();
		var selectedItems = me.getLogistics().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
		} else if (selectedItems.length > 1) {
			Ext.Msg.alert('提示', '一次只能选择一条数据');
		} else {
			if (selectedItems[0].data.Status != 2) {
				Ext.Msg.alert('提示', '请选择已发货的数据!');
				return;
			}
			var id = selectedItems[0].data.ID;
			Ext.Msg.confirm('询问', '确定要取消发货吗?', function (operational) {
				if (operational == 'yes') {
					store.antiDelivery(id,
						function success(response, request, c) {
							var result = Ext.decode(c.responseText);
							if (result.IsSuccessful) {
								store.reload();
								Ext.Msg.alert('提示', '取消发货成功!');
							} else {
								Ext.Msg.alert('提示', result.ErrorMessage);
							}
						},
						function failure(a, b, c) {
							Ext.Msg.alert('提示', '取消发货失败!');
						}
					);
				}
			});
		}
	},
	revisitTips: function () {
		var me = this;
		var store = me.getLogistics().getStore();
		var selectedItems = me.getLogistics().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
		} else if (selectedItems.length > 1) {
			Ext.Msg.alert('提示', '一次只能选择一条数据');
		} else {
			if (selectedItems[0].data.Status != 2) {
				Ext.Msg.alert('提示', '请选择已发货的数据!');
				return;
			}
			if (selectedItems[0].data.RevisitStatus != 0) {
				Ext.Msg.alert('提示', '请选择未回访的数据!');
				return;
			}
			var id = selectedItems[0].data.ID;
			Ext.Msg.confirm('询问', '确定要发送回访吗?', function (operational) {
				if (operational == 'yes') {
					store.revisitTips(id,
						function success(response, request, c) {
							var result = Ext.decode(c.responseText);
							if (result.IsSuccessful) {
								store.reload();
								Ext.Msg.alert('提示', '发送回访成功!');
							} else {
								Ext.Msg.alert('提示', result.ErrorMessage);
							}
						},
						function failure(a, b, c) {
							Ext.Msg.alert('提示', '发送回访失败!');
						}
					);
				}
			});
		}
	},
	selectSalesman: function () {
		var win = Ext.widget('SalesmanSelector');
		var grid = win.down('grid');
		var store = grid.getStore();
		store.load();
		win.show();
	}
});
