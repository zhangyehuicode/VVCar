Ext.define('WX.controller.Reimbursement', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.ReimbursementStore'],
	models: ['BaseData.MerchantModel'],
	views: ['Reimbursement.ReimbursementList', 'Reimbursement.ReimbursementEdit'],
	refs: [{
		ref: 'reimbursementList',
		selector: 'ReimbursementList'
	}, {
		ref: 'reimbursementEdit',
		selector: 'ReimbursementEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'ReimbursementList button[action=addReimbursement]': {
				click: me.addReimbursement
			},
			'ReimbursementList button[action=delReimbursement]': {
				click: me.delReimbursement
			},
			'ReimbursementList button[action=approveReimbursement]': {
				click: me.approveReimbursement
			},
			'ReimbursementList button[action=antiApproveReimbursement]': {
				click: me.antiApproveReimbursement
			},
			'ReimbursementList button[action=search]': {
				click: me.search
			},
			'ReimbursementList': {
				itemdblclick: me.editReimbursement,
			},
			'ReimbursementEdit button[action=uploadpic]': {
				click: me.uploadpic
			},
			'ReimbursementEdit button[action=save]': {
				click: me.save
			},
		});
	},
	addReimbursement: function () {
		var win = Ext.widget('ReimbursementEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加报销单');
		win.show();
	},
	uploadpic: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: '/api/UploadFile/UploadReimbursement',
				waitMsg: '正在上传...',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功');
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
		var win = me.getReimbursementEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getReimbursementList().getStore();
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
	editReimbursement: function (grid, record) {
		var win = Ext.widget('ReimbursementEdit');
		win.form.loadRecord(record);
		win.down('box[name=ImgShow]').autoEl.src = record.data.ImgUrl;
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑业务报销单');
		win.show();
	},
	delReimbursement: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的数据!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除任务吗', function (optional) {
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
				);
			}
		})
	},
	approveReimbursement: function () {
		var me = this;
		var store = me.getReimbursementList().getStore();
		var selectedItems = me.getReimbursementList().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
		} else {
			var ids = [];
			var noApprovedData = false;
			selectedItems.forEach(function (item) {
				if (item.data.Status == 1) {
					noApprovedData = true;
					return;
				} else {
					ids.push(item.data.ID);
				}
			});
			if (noApprovedData) {
				Ext.Msg.alert('提示', '请选择未审核的数据!');
				return;
			}
			Ext.Msg.confirm('询问', '确定要审核吗?', function (operational) {
				if (operational == 'yes') {
					store.approveReimbursement(ids,
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
		}
	},
	antiApproveReimbursement: function () {
		var me = this;
		var store = me.getReimbursementList().getStore();
		var selectedItems = me.getReimbursementList().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
		} else {
			var ids = [];
			var approvedData = false;
			selectedItems.forEach(function (item) {
				if (item.data.Status == 0) {
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
			Ext.Msg.confirm('询问', '确定要反审核吗?', function (operational) {
				if (operational == 'yes') {
					store.antiApproveReimbursement(ids,
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
		}
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getReimbursementList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
});