Ext.define('WX.controller.AdvisementSetting', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.AdvisementSettingStore'],
	models: ['BaseData.ArticleModel'],
	views: ['CustomerFinder.AdvisementSettingList', 'CustomerFinder.AdvisementSettingEdit'],
	refs: [{
		ref: 'advisementSettingList',
		selector: 'AdvisementSettingList',
	}, {
		ref: 'advisementSettingEdit',
		selector: 'AdvisementSettingEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'AdvisementSettingList button[action=addAdvisementSetting]': {
				click: me.addAdvisementSetting
			},
			'AdvisementSettingList button[action=search]': {
				click: me.search
			},
			'AdvisementSettingList': {
				deleteActionClick: me.delAdvisementSetting,
				itemdblclick: me.editAdvisementSetting
			},
			'AdvisementSettingEdit button[action=uploadpic]': {
				click: me.uploadpic
			},
			'AdvisementSettingEdit button[action=save]': {
				click: me.save
			}
		});
	},
	addAdvisementSetting: function () {
		var win = Ext.widget('AdvisementSettingEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('新增广告');
		win.show();
	},
	editAdvisementSetting: function (grid, record) {
		var win = Ext.widget('AdvisementSettingEdit');
		win.form.loadRecord(record);
		win.down('box[name=ImgShow]').autoEl.src = record.data.ImgUrl;
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑广告');
		win.show();
	},
	delAdvisementSetting: function (grid, record) {
		var me = this;
		Ext.Msg.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据,请稍后...', '状态提示');
				var store = me.getAdvisementSettingList().getStore();
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
	uploadpic: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: 'api/UploadFile/UploadAdvisementImg',
				method: 'POST',
				waitMsg: '正在上传',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功!');
						win.down('textfield[name=ImgUrl]').setValue(o.result.FileUrl);
						win.down('box[name=ImgShow]').el.dom.src = o.result.FileUrl;
					} else {
						Ext.Msg.alert('提示', o.result.errorMessage);
					}
				},
				failure: function (fp, o) {
					Ext.Msg.alert('提示', o.result.errorMessage);
				}
			});
		}
	},
	save: function () {
		var me = this;
		var win = me.getAdvisementSettingEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getAdvisementSettingList().getStore();
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
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getAdvisementSettingList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	}
})