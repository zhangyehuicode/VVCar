Ext.define('WX.controller.Merchant', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.MerchantStore'],
	models: ['BaseData.MerchantModel'],
	views: ['Merchant.MerchantList', 'Merchant.MerchantEdit'],
	refs: [{
		ref: 'merchantList',
		selector: 'MerchantList'
	}, {
		ref: 'merchantEdit',
		selector: 'MerchantEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'MerchantList button[action=addMerchant]': {
				click: me.addMerchant
			},
			'MerchantList button[action=activateMerchant]': {
				click: me.activateMerchant
			},
			'MerchantList button[action=freezeMerchant]': {
				click: me.freezeMerchant
			},
			'MerchantList button[action=search]': {
				click: me.search
			},
			'MerchantList button[action=export]': {
				click: me.export
			},
			'MerchantList': {
				editActionClick: me.editMerchant,
				deleteActionClick: me.deleteMerchant,
				itemdblclick: me.editMerchant,
			},
			'MerchantEdit button[action=uploadLicensePic]': {
				click: me.uploadLicensePic
			},
			'MerchantEdit button[action=uploadIDCardFrontPic]': {
				click: me.uploadIDCardFrontPic
			},
			'MerchantEdit button[action=uploadIDCardBehindPic]': {
				click: me.uploadIDCardBehindPic
			},
			'MerchantEdit button[action=save]': {
				click: me.save
			},
			'MerchantEdit image[name=download]': {
				link: me.downloadLicenseImg
			},
		});
	},
	addMerchant: function () {
		var win = Ext.widget('MerchantEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加商户');
		win.show();
	},
	activateMerchant: function () {
		var me = this;
		var store = me.getMerchantList().getStore();
		var selectedItems = me.getMerchantList().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
		} else {
			var ids = [];
			var noActivationData = false;
			selectedItems.forEach(function (item) {
				if (item.data.Status != 0 && item.data.Status != -1) {
					noActivationData = true;
					return;
				} else {
					ids.push(item.data.ID);
				}
			});
			if (noActivationData) {
				Ext.Msg.alert('提示', '请选择未激活或者冻结的数据!');
				return;
			}
			Ext.Msg.confirm('询问', '确定要激活所选商户吗?', function (operational) {
				if (operational == 'yes') {
					store.activateMerchant(ids,
						function success(response, request, c) {
							var result = Ext.decode(c.responseText);
							if (result.IsSuccessful) {
								store.reload();
								Ext.Msg.alert('提示', '激活成功!');
							} else {
								Ext.Msg.alert('提示', result.ErrorMessage);
							}
						},
						function failure(a, b, c) {
							Ext.Msg.alert('提示', '激活失败!');
						}
					);
				}
			});
		}
	},
	freezeMerchant: function () {
		var me = this;
		var store = me.getMerchantList().getStore();
		var selectedItems = me.getMerchantList().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
		} else {
			var ids = [];
			var activationData = false;
			selectedItems.forEach(function (item) {
				if (item.data.Status != 1) {
					activationData = true;
					return;
				} else {
					ids.push(item.data.ID);
				}
			});
			if (activationData) {
				Ext.Msg.alert('提示', '请选择已激活的数据!');
				return;
			}
			Ext.Msg.confirm('询问', '确定要冻结所选商户吗?', function (operational) {
				if (operational == 'yes') {
					store.freezeMerchant(ids,
						function success(response, request, c) {
							var result = Ext.decode(c.responseText);
							if (result.IsSuccessful) {
								store.reload();
								Ext.Msg.alert('提示', '冻结成功!');
							} else {
								Ext.Msg.alert('提示', result.ErrorMessage);
							}
						},
						function failure(a, b, c) {
							Ext.Msg.alert('提示', '冻结失败!');
						}
					);
				}
			});
		}
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
		var win = me.getMerchantEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getMerchantList().getStore();
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
	editMerchant: function (grid, record) {
		var win = Ext.widget('MerchantEdit');
		win.form.loadRecord(record);
		win.down('box[name=ImgLicenseShow]').autoEl.src = record.data.BusinessLicenseImgUrl;
		win.down('box[name=ImgIDCardFrontShow]').autoEl.src = record.data.LegalPersonIDCardFrontImgUrl;
		win.down('box[name=ImgIDCardBehindShow]').autoEl.src = record.data.LegalPersonIDCardBehindImgUrl;
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑商户');
		win.show();
	},
	deleteMerchant: function (grid, record) {
		var me = this;
		Ext.Msg.confirm('询问', '您确定要删除吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据,请稍后...', '状态显示');
				var store = me.getMerchantList().getStore();
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
	downloadLicenseImg: function () {
		Ext.Msg.alert('提示', 'hello');
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getMerchantList().getStore();
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
		var store = me.getMerchantList().getStore();
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