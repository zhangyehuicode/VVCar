Ext.define('WX.controller.SuperClass', {
	extend: 'Ext.app.Controller',
	views: ['SuperClass.SuperClassList', 'SuperClass.SuperClassEdit'],
	refs: [{
		ref: 'superClassList',
		selector: 'SuperClassList'
	}, {
		ref: 'superClassEdit',
		selector: 'SuperClassEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'SuperClassList button[action=addVideo]': {
				click: me.addVideo
			},
			'SuperClassList button[action=editVideo]': {
				click: me.editSuperClassBtn
			},
			'SuperClassList button[action=delVideo]': {
				click: me.delVideo
			},
			'SuperClassList button[action=search]': {
				click: me.search
			},
			'SuperClassList': {
				//itemdblclick: me.editSuperClass
			},
			'SuperClassEdit button[action=uploadVideo]': {
				click: me.uploadVideo
			},
			'SuperClassEdit button[action=save]': {
				click: me.saveVideo
			}
		});
	},
	editSuperClass: function (grid, record) {
		var win = Ext.widget('SuperClassEdit');
		win.form.loadRecord(record);
		win.down('box[name=VideoShow]').autoEl.src = record.data.VideoUrl;
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑商户');
		win.show();
	},
	editSuperClassBtn: function (btn) {
		var me = this;
		var selectedItems = me.getSuperClassList().getSelectionModel().getSelection();
		if (selectedItems < 1) {
			Ext.Msg.alert('提示', '请先选中需要编辑的数据');
			return;
		}
		if (selectedItems > 1) {
			Ext.Msg.alert('提示', '一次只能编辑一条数据');
			return;
		}
		this.editSuperClass(null, selectedItems[0]);
	},
	addVideo: function () {
		var win = Ext.widget('SuperClassEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加视频');
		win.show();
	},
	delVideo: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的任务!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除吗', function (optional) {
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
	uploadVideo: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: '/api/UploadFile/UploadSuperClass',
				waitMsg: '正在上传...',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功！');
						win.down('textfield[name=VideoUrl]').setValue(o.result.FileUrl);
						win.down('box[name=VideoShow]').el.dom.src = o.result.FileUrl;
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
	saveVideo: function () {
		var me = this;
		var win = me.getSuperClassEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (formValues.ImgUrl == '') {
			Ext.Msg.alert('提示', '请先上传视频');
			return;
		}
		if (form.isValid()) {
			var store = me.getSuperClassList().getStore();
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
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getSuperClassList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	}
});