/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.SystemSetting', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.SystemSettingStore'],
	models: ['BaseData.SystemSettingModel'],
	views: ['SystemSetting.SystemSetting', 'SystemSetting.SystemSettingEdit'],
	refs: [{
		ref: "grdSystemSetting",
		selector: "SystemSetting"
	}, {
		ref: 'systemSettingEdit',
		selector: 'SystemSettingEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'SystemSetting': {
				edit: me.onSystemSettingEdit,
				deleteActionClick: me.delSystemSetting
			},
			'SystemSetting button[action=search]': {
				click: me.search
			},
			'SystemSetting button[action=addSystemSetting]': {
				click: me.addSystemSetting
			},
			'SystemSetting button[action=editSystemSetting]': {
				click: me.editSystemSetting
			},
			'SystemSettingEdit button[action=save]': {
				click: me.save
			},
		});
	},
	onSystemSettingEdit: function (editor, context, eOpts) {
		if (context.record.phantom) {//表示新增
			context.store.create(context.record.data, {
				callback: function (records, operation, success) {
					if (!success) {
						Ext.MessageBox.alert("提示", operation.error);
						return;
					} else {
						context.record.copyFrom(records[0]);
						context.record.commit();
						Ext.MessageBox.alert("提示", "新增成功");
					}
				}
			});
		} else {
			if (!context.record.dirty)
				return;
			context.store.update({
				callback: function (records, operation, success) {
					if (!success) {
						Ext.MessageBox.alert("提示", operation.error);
						return;
					} else {
						Ext.MessageBox.alert("提示", "更新成功");
					}
				}
			});
		}
	},
	delSystemSetting: function (grid, record) {
		var me = this;
		Ext.Msg.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据, 请稍后...', '状态提示');
				var store = me.getGrdSystemSetting().getStore();
				store.remove(record);
				store.sync({
					callback: function (batch, options) {
						Ext.Msg.hide();
						if (batch.hasException()) {
							Ext.Msg.alert('提示', batch.exceptions[0].error);
							store.rejectChanges();
						} else {
							Ext.Msg.alert('提示', '删除成功');
						}
					}
				});
			}
		});
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getGrdSystemSetting().getStore();
			me.getGrdSystemSetting().getSelectionModel().clearSelections();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.MessageBox.alert('系统提示', '请输入过滤条件!');
		}
	},
	addSystemSetting: function () {
		var win = Ext.widget('SystemSettingEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加系统配置');
		win.show();
	},
	save: function (btn) {
		var me = this;
		var win = me.getSystemSettingEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		formValues.Type = 9;
		formValues.IsVisible = true;
		formValues.IsAvailable = true;
		if (form.isValid()) {
			var store = me.getGrdSystemSetting().getStore();
			if (form.actionMethod == 'POST') {
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
});
