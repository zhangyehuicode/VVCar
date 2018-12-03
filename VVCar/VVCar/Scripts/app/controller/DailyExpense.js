Ext.define('WX.controller.DailyExpense', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.DailyExpenseStore'],
	models: ['BaseData.DailyExpenseModel'],
	views: ['DailyExpense.DailyExpense', 'DailyExpense.DailyExpenseEdit'],
	refs: [{
		ref: 'dailyExpense',
		selector: 'DailyExpense',
	}, {
		ref: 'dailyExpenseEdit',
		selector: 'DailyExpenseEdit',
	}],
	init: function () {
		var me = this;
		me.control({
			'DailyExpense': {
				itemdblclick: me.editDailyExpense,
			},
			'DailyExpense button[action=addDailyExpense]': {
				click: me.addDailyExpense,
			},
			'DailyExpense button[action=delDailyExpense]': {
				click: me.delDailyExpense
			},
			'DailyExpenseEdit button[action=save]': {
				click: me.save
			}
		});
	},
	addDailyExpense: function () {
		var me = this;
		var win = Ext.widget('DailyExpenseEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加日常开支');
		win.show();
	},
	editDailyExpense: function (grid, record) {
		var win = Ext.widget('DailyExpenseEdit');
		win.down('textfield[name=StaffCount]').show();
		record.data.ExpenseDate = record.data.ExpenseDate.substring(0, 10);
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑日常开支');
		win.show();
	},
	delDailyExpense: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的任务!');
			return;
		} 
		Ext.Msg.confirm('提示', '确定要删除任务吗?', function (optional) {
			if (optional === 'yes') {
				var ids = [];
				selectedItems.forEach(function (item) {
					ids.push(item.data.ID);
				});
				var store = me.getDailyExpense().getStore();
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
	save: function (btn) {
		var me = this;
		var win = me.getDailyExpenseEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getDailyExpense().getStore();
			if (form.actionMethod == 'POST') {
				formValues.ExpenseDate += ' 00:00:00';
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
	}
})