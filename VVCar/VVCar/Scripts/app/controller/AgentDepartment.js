Ext.define('WX.controller.AgentDepartment', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.AgentDepartmentStore'],
	models: ['BaseData.AgentDepartmentModel'],
	views: ['AgentDepartment.AgentDepartmentList', 'AgentDepartment.ManageUserSelector', 'AgentDepartment.AgentDepartmentEdit'],
	refs: [{
		ref: 'agentDepartmentList',
		selector: 'AgentDepartmentList'
	}, {
		ref: 'agentDepartmentEdit',
		selector: 'AgentDepartmentEdit'
	}, {
		ref: 'manageUserSelector',
		selector: 'ManageUserSelector'
	}],
	init: function () {
		var me = this;
		me.control({
			'AgentDepartmentList button[action=addAgentDepartment]': {
				click: me.addAgentDepartment
			},
			'AgentDepartmentList button[action=search]': {
				click: me.search
			},
			'AgentDepartmentList': {
				editActionClick: me.editAgentDepartment,
				deleteActionClick: me.deleteAgentDepartment,
				itemdblclick: me.editAgentDepartment,
			},
			'AgentDepartmentEdit button[action=selectManageUser]': {
				click: me.selectManageUser
			},
			'AgentDepartmentEdit button[action=uploadLicensePic]': {
				click: me.uploadLicensePic
			},
			'AgentDepartmentEdit button[action=uploadIDCardFrontPic]': {
				click: me.uploadIDCardFrontPic
			},
			'AgentDepartmentEdit button[action=uploadIDCardBehindPic]': {
				click: me.uploadIDCardBehindPic
			},
			'AgentDepartmentEdit button[action=save]': {
				click: me.save
			},
			'ManageUserSelector grid[name=manageUserList]': {
				itemdblclick: me.chooseManageUser
			}
		});
	},
	selectManageUser: function () {
		var win = Ext.widget('ManageUserSelector');
		var grid = win.down('grid');
		var store = grid.getStore();
		store.load();
		win.show();
	},
	chooseManageUser: function (grid, record) {
		var win = Ext.ComponentQuery.query('AgentDepartmentEdit')[0];
		win.down('textfield[name=UserName]').setValue(record.data.Name);
		win.down('textfield[name=UserID]').setValue(record.data.ID);
		grid.up('window').close();
	},
	addAgentDepartment: function () {
		var win = Ext.widget('AgentDepartmentEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加门店');
		win.show();
	},
	uploadLicensePic: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: '/api/UploadFile/UploadLicense',
				waitMsg: '正在上传...',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功');
						win.down('textfield[name=BusinessLicenseImgUrl]').setValue(o.result.FileUrl);
						win.down('box[name=ImgLicenseShow]').el.dom.src = o.result.FileUrl;
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
	uploadIDCardFrontPic: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: '/api/UploadFile/UploadIDCard',
				waitMsg: '正在上传...',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功');
						win.down('textfield[name=LegalPersonIDCardFrontImgUrl]').setValue(o.result.FileUrl);
						win.down('box[name=ImgIDCardFrontShow]').el.dom.src = o.result.FileUrl;
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
	uploadIDCardBehindPic: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: '/api/UploadFile/UploadIDCard',
				waitMsg: '正在上传...',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功');
						win.down('textfield[name=LegalPersonIDCardBehindImgUrl]').setValue(o.result.FileUrl);
						win.down('box[name=ImgIDCardBehindShow]').el.dom.src = o.result.FileUrl;
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
		var win = me.getAgentDepartmentEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getAgentDepartmentList().getStore();
			if (form.actionMethod == 'POST') {
				store.create(formValues, {
					callback: function (records, operation, success) {
						if (!success) {
							Ext.Msg.alert('提示', operation.error);
							store.rejectChanges();
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
				var selectedItems = me.getAgentDepartmentList().getSelectionModel().getSelection();
				if (selectedItems[0].data.ApproveStatus != 0) {
					Ext.Msg.alert('提示', '请选择未审核的数据进行修改');
					return;
				}
				form.updateRecord();
				store.update({
					callback: function (records, operation, success) {
						if (!success) {
							Ext.Msg.alert('提示', operation.error);
							store.rejectChanges();
							return;
						} else {
							Ext.Msg.alert('提示', '更新成功');
							store.commitChanges();
							store.reload();
							win.close();
						}
					}
				});
			}
		}
	},
	editAgentDepartment: function (grid, record) {
		var win = Ext.widget('AgentDepartmentEdit');
		win.form.loadRecord(record);
		win.down('box[name=ImgLicenseShow]').autoEl.src = record.data.BusinessLicenseImgUrl;
		win.down('box[name=ImgIDCardFrontShow]').autoEl.src = record.data.LegalPersonIDCardFrontImgUrl;
		win.down('box[name=ImgIDCardBehindShow]').autoEl.src = record.data.LegalPersonIDCardBehindImgUrl;
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑代理商门店');
		win.show();
	},
	deleteAgentDepartment: function (grid, record) {
		var me = this;
		Ext.Msg.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据,请稍后...', '状态显示');
				var store = me.getAgentDepartmentList().getStore();
				store.remove(record);
				store.sync({
					callback: function (batch, options) {
						Ext.Msg.hide();
						if (batch.hasException()) {
							Ext.Msg.alert('操作失败', batch.exceptions[0].error);
							store.rejectChanges();
						} else {
							Ext.Msg.alert('操作成功', '删除成功');
							store.reload();
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
			var store = me.getAgentDepartmentList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
});