Ext.define('WX.controller.Recruitment', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.RecruitmentStore'],
	models: ['BaseData.RecruitmentModel'],
	views: ['Recruitment.RecruitmentList', 'Recruitment.RecruitmentEdit'],
	refs: [{
		ref: 'recruitmentList',
		selector: 'RecruitmentList'
	}, {
		ref: 'recruitmentEdit',
		selector: 'RecruitmentEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'RecruitmentList button[action=addRecruitment]': {
				click: me.addRecruitment
			},
			'RecruitmentList button[action=deleteRecruitment]': {
				click: me.deleteRecruitment
			},
			'RecruitmentList button[action=search]': {
				click: me.search
			},
			'RecruitmentList button[action=export]': {
				click: me.export
			},
			'RecruitmentList': {
				editActionClick: me.editRecruitment,
				deleteActionClick: me.deleteRecruitment,
				itemdblclick: me.editRecruitment,
			},
			'RecruitmentEdit button[action=save]': {
				click: me.save
			},
		});
	},
	addRecruitment: function () {
		var win = Ext.widget('RecruitmentEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加人才需求');
		win.show();
	},
	editRecruitment: function (grid, record) {
		var win = Ext.widget('Recruitmentdit');
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑人才需求');
		win.show();
	},
	deleteMerchant: function (grid, record) {
		var me = this;
		Ext.Msg.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据,请稍后...', '状态显示');
				var store = me.getRecruitmentList().getStore();
				store.remove(record);
				store.sync({
					callback: function (batch, options) {
						Ext.Msg.hide();
						if (batch.hasException()) {
							Ext.Msg.alert('操作失败', batch.exceptions[0].error);
							store.rejectChanges();
						} else {
							Ext.Msg.alert('操作成功', '删除成功');
						}
					}
				});
			}
		});
	},
	save: function () {
		var me = this;
		var win = me.getRecruitmentEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getRecruitmentList().getStore();
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
							win.close();
						}
					}
				});
			}
		}
	},
	export: function (btn) {
		var me = this;
		Ext.Msg.show({
			msg: '正在生成数据...,请稍候',
			progressText: '正在生成数据...',
			width: 300,
			wait: true,
		});
		var queryValues = btn.up('form').getValues();
		var store = me.getRecruitmentList().getStore();
		store.export(queryValues, function (req, success, res) {
			Ext.Msg.hide();
			if (res.status === 200) {
				var response = JSON.parse(res.responseText);
				if (response.IsSuccessful) {
					window.location.href = response.Data;
				} else {
					Ext.Msg.alert('提示', response.ErrorMessage);
				}
			} else {
				Ext.Msg.alert('提示', '网络请求异常:' + res.status);
			}
		});
	}
});