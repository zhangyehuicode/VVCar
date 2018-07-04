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
		var win = Ext.widget('RecruitmentEdit');
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑人才需求');
		win.show();
	},
	deleteRecruitment: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的人才需求!');
			return;
		}
		Ext.Msg.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				var ids = [];
				selectedItems.forEach(function (item) {
					ids.push(item.data.ID);
				});
				var store = me.getRecruitmentList().getStore();
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
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getRecruitmentList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
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