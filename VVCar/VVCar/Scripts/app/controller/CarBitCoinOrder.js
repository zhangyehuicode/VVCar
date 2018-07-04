Ext.define('WX.controller.CarBitCoinOrder', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.CarBitCoinOrderStore'],
	models: ['BaseData.CarBitCoinOrderModel'],
	views: ['CarBitCoinOrder.CarBitCoinOrder', 'CarBitCoinOrder.CarBitCoinOrderEdit', 'CarBitCoinOrder.CarBitCoinOrderDetails'],
	refs: [{
		ref: 'carBitCoinOrder',
		selector: 'CarBitCoinOrder'
	}, {
		ref: 'carBitCoinOrderEdit',
		selector: 'CarBitCoinOrderEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'CarBitCoinOrder button[action=search]': {
				click: me.search
			},
			'CarBitCoinOrder': {
				itemdblclick: me.edit,
				editActionClick: me.edit,
				deleteActionClick: me.deleteOrder,
				orderdetailsClick: me.orderdetails,
			},
			'CarBitCoinOrderEdit button[action=save]': {
				click: me.save
			},
		});
	},
	orderdetails: function (grid, record) {
		var win = Ext.widget("CarBitCoinOrderDetails");
		win.form.loadRecord(record);
		var orderItemStore = win.down('grid[name=orderitemgrid]').getStore();

		var statusdesc = "";
		switch (record.data.Status) {
			case -1:
				statusdesc = "未付款";
				break;
			case 0:
				statusdesc = "未发货";
				break;
			case 1:
				statusdesc = "已发货";
				break;
			case 2:
				statusdesc = "已完成";
				break;
		}
		win.down('textfield[name=Status]').setValue(statusdesc);

		orderItemStore.proxy.extraParams = { OrderID: record.data.ID };
		orderItemStore.load();
		win.show();
	},
	edit: function (grid, record) {
		var win = Ext.widget("CarBitCoinOrderEdit");
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑订单');
		win.show();
	},
	save: function (btn) {
		var me = this;
		var win = me.getCarBitCoinOrderEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getCarBitCoinOrder().getStore();
			if (form.actionMethod == 'POST') {
				store.create(formValues, {
					callback: function (records, operation, success) {
						if (!success) {
							Ext.MessageBox.alert("提示", operation.error);
							return;
						} else {
							store.add(records[0].data);
							store.commitChanges();
							Ext.MessageBox.alert("提示", "新增成功");
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
							Ext.MessageBox.alert("提示", operation.error);
							return;
						} else {
							Ext.MessageBox.alert("提示", "更新成功");
							win.close();
						}
					}
				});
			}
		}
	},
	search: function (btn) {
		var store = this.getCarBitCoinOrder().getStore();
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.All = true;
			store.load({ params: queryValues });
		}
	},
	deleteOrder: function (grid, record) {
		var me = this;
		Ext.MessageBox.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据，请稍候……', '状态提示');
				var store = me.getCarBitCoinOrder().getStore();
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
});
