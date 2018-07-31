Ext.define('WX.controller.AgentDepartmentAudit', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.AgentDepartmentAuditStore'],
	models: ['BaseData.AgentDepartmentAuditModel'],
	views: ['AgentDepartmentAudit.AgentDepartmentAuditList', 'AgentDepartmentAudit.ManageUserAuditSelector', 'AgentDepartmentAudit.AgentDepartmentAuditEdit'],
	refs: [{
		ref: 'agentDepartmentAuditList',
		selector: 'AgentDepartmentAuditList'
	}, {
		ref: 'agentDepartmentAuditEdit',
		selector: 'AgentDepartmentAuditEdit'
	}, {
		ref: 'manageUserAuditSelector',
		selector: 'ManageUserAuditSelector'
	}],
	init: function () {
		var me = this;
		me.control({
			'AgentDepartmentAuditList button[action=auditAgentDepartment]': {
				click: me.auditAgentDepartment
			},
			'AgentDepartmentAuditList button[action=antiAuditAgentDepartment]': {
				click: me.antiAuditAgentDepartment
			},
			'AgentDepartmentAuditList button[action=importAgentDepartment]': {
				click: me.importAgentDepartment
			},
			'AgentDepartmentAuditList button[action=search]': {
				click: me.search
			},
			'AgentDepartmentAuditList': {
				editActionClick: me.editAgentDepartment,
				itemdblclick: me.editAgentDepartmentAudit,
				afterrender: me.afterrender, 
			},
			'AgentDepartmentAuditEdit button[action=selectManageAuditUser]': {
				click: me.selectManageAuditUser
			},
			'AgentDepartmentAuditEdit button[action=uploadLicensePic]': {
				click: me.uploadLicensePic
			},
			'AgentDepartmentAuditEdit button[action=uploadIDCardFrontPic]': {
				click: me.uploadIDCardFrontPic
			},
			'AgentDepartmentAuditEdit button[action=uploadIDCardBehindPic]': {
				click: me.uploadIDCardBehindPic
			},
			'AgentDepartmentAuditEdit button[action=save]': {
				click: me.save
			},
			'ManageUserAuditSelector grid[name=manageUserAuditList]': {
				itemdblclick: me.chooseManageAuditUser
			}
		});
	},
	selectManageAuditUser: function () {
		var win = Ext.widget('ManageUserAuditSelector');
		var grid = win.down('grid');
		var store = grid.getStore();
		store.load();
		win.show();
	},
	chooseManageAuditUser: function (grid, record) {
		var win = Ext.ComponentQuery.query('AgentDepartmentAuditEdit')[0];
		win.down('textfield[name=UserName]').setValue(record.data.Name);
		win.down('textfield[name=UserID]').setValue(record.data.ID);
		grid.up('window').close();
	},
	editAgentDepartmentAudit: function (grid, record) {
		var win = Ext.widget('AgentDepartmentAuditEdit');
		win.form.loadRecord(record);
		win.down('box[name=ImgLicenseShow]').autoEl.src = record.data.BusinessLicenseImgUrl;
		win.down('box[name=ImgIDCardFrontShow]').autoEl.src = record.data.LegalPersonIDCardFrontImgUrl;
		win.down('box[name=ImgIDCardBehindShow]').autoEl.src = record.data.LegalPersonIDCardBehindImgUrl;
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑代理商门店');
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
	auditAgentDepartment: function () {
		var me = this;
		var store = me.getAgentDepartmentAuditList().getStore();
		var selectedItems = me.getAgentDepartmentAuditList().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
			return;
		}
		var ids = [];
		var approvedData = false;
		selectedItems.forEach(function (item) {
			if (item.data.ApproveStatus != 0) {
				approvedData = true;
				return;
			} else {
				ids.push(item.data.ID);
			}
		});
		if (approvedData) {
			Ext.Msg.alert('提示', '请选择未审核的数据!');
			return;
		}
		Ext.Msg.confirm('询问', '确定通过审核?', function (operational) {
			if (operational == 'yes') {
				store.approveAgentDepartment(ids,
					function success(response, request, c) {
						var result = Ext.decode(c.responseText);
						if (result.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '审核成功!');
						} else {
							Ext.Msg.alert('提示', result.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', '审核失败!');
					}
				);
			}
		});
	},
	antiAuditAgentDepartment: function () {
		var me = this;
		var store = me.getAgentDepartmentAuditList().getStore();
		var selectedItems = me.getAgentDepartmentAuditList().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
			return;
		}
		var ids = [];
		var approvedData = false;
		selectedItems.forEach(function (item) {
			if (item.data.ApproveStatus != 1) {
				approvedData = true;
				return;
			} else {
				ids.push(item.data.ID);
			}
		});
		if (approvedData) {
			Ext.Msg.alert('提示', '请选择已审核的数据!');
			return;
		}
		Ext.Msg.confirm('询问', '确定要反审核?', function (operational) {
			if (operational == 'yes') {
				store.rejectAgentDepartment(ids,
					function success(response, request, c) {
						var result = Ext.decode(c.responseText);
						if (result.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '反审核成功!');
						} else {
							Ext.Msg.alert('提示', result.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', '反审核失败!');
					}
				);
			}
		});
	},
	importAgentDepartment: function () {
		var me = this;
		var store = me.getAgentDepartmentAuditList().getStore();
		var selectedItems = me.getAgentDepartmentAuditList().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
			return;
		}
		var ids = [];
		var approvedData = false;
		selectedItems.forEach(function (item) {
			if (item.data.ApproveStatus != 1) {
				approvedData = true;
				return;
			} else {
				ids.push(item.data.ID);
			}
		});
		if (approvedData) {
			Ext.Msg.alert('提示', '请选择已审核的数据!');
			return;
		}
		Ext.Msg.confirm('询问', '确定要导入?', function (operational) {
			if (operational == 'yes') {
				store.importAgentDepartment(ids,
					function success(response, request, c) {
						var result = Ext.decode(c.responseText);
						if (result.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '导入成功!');
						} else {
							Ext.Msg.alert('提示', result.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', '导入失败!');
					}
				);
			}
		});
	},
	save: function () {
		var me = this;
		var win = me.getAgentDepartmentAuditEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getAgentDepartmentAuditList().getStore();
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
				var selectedItems = me.getAgentDepartmentAuditList().getSelectionModel().getSelection();
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
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.Type = 0;
			var store = me.getAgentDepartmentAuditList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	afterrender: function () {
		var me = this;
		var store = me.getAgentDepartmentAuditList().getStore();
		var params = {
			Type: 0,
			All: true,
		}
		Ext.apply(store.proxy.extraParams, params);
		store.load();
	}
});