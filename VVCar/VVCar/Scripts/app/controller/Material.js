Ext.define('WX.controller.Material', {
	extend: 'Ext.app.Controller',
	views: ['Material.Material', 'Material.MaterialEdit'],
	refs: [{
		ref: 'material',
		selector: 'Material'
	}, {
		ref: 'materialEdit',
		selector: 'MaterialEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'Material button[action=addMaterial]': {
				click: me.addMaterial
			},
			'Material button[action=editMaterial]': {
				click: me.editMaterialBtn
			},
			'Material button[action=delMaterial]': {
				click: me.delMaterial
			},
			'Material button[action=search]': {
				click: me.search
			},
			'Material': {
				afterrender: me.afterrender,
				itemdblclick: me.editMaterial,
			},
			'MaterialEdit button[action=uploadMaterial]': {
				click: me.uploadMaterial
			},
			'MaterialEdit button[action=save]': {
				click: me.saveMaterial
			}
		});
	},
	editMaterial: function (grid, record) {
		var win = Ext.widget('MaterialEdit');
		win.form.loadRecord(record);
		win.down('box[name=MaterialImgShow]').autoEl.src = record.data.Url;
		win.down('box[name=MaterialVideoShow]').autoEl.src = record.data.Url;
		var extension = record.data.Url.substring(record.data.Url.lastIndexOf("."), record.data.Url.length);
		var reg = new RegExp('.jpg|.gif|.png|.jpeg');
		if (reg.test(extension)) {
			win.down('box[name=MaterialImgShow]').show();
			win.down('box[name=MaterialVideoShow]').hide();
		} else {
			win.down('box[name=MaterialImgShow]').hide();
			win.down('box[name=MaterialVideoShow]').show();
		}
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑素材');
		win.show();
	},
	editMaterialBtn: function (btn) {
		var me = this;
		var selectedItems = me.getMaterial().getSelectionModel().getSelection();
		if (selectedItems < 1) {
			Ext.Msg.alert('提示', '请先选中需要编辑的数据');
			return;
		}
		if (selectedItems > 1) {
			Ext.Msg.alert('提示', '一次只能编辑一条数据');
			return;
		}
		this.editMaterial(null, selectedItems[0]);
	},
	addMaterial: function () {
		var win = Ext.widget('MaterialEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加素材');
		win.show();
	},
	delMaterial: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的素材!');
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
	uploadMaterial: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: '/api/UploadFile/UploadMaterial',
				waitMsg: '正在上传...',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功！');
						win.down('textfield[name=Url]').setValue(o.result.FileUrl);
						win.down('box[name=MaterialImgShow]').el.dom.src = o.result.FileUrl;
						win.down('box[name=MaterialVideoShow]').el.dom.src = o.result.FileUrl;
						var extension = o.result.FileUrl.substring(o.result.FileUrl.lastIndexOf("."), o.result.FileUrl.length);
						var reg = new RegExp('.jpg|.gif|.png|.jpeg');
						if (reg.test(extension)) {
							win.down('box[name=MaterialImgShow]').show();
							win.down('box[name=MaterialVideoShow]').hide();
						} else {
							win.down('box[name=MaterialImgShow]').hide();
							win.down('box[name=MaterialVideoShow]').show();
						}
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
	saveMaterial: function () {
		var me = this;
		var win = me.getMaterialEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (formValues.Url == '') {
			Ext.Msg.alert('提示', '请先上传素材');
			return;
		}
		if (Ext.getCmp('file1').getValue() != '') {
			Ext.Msg.alert('提示', '请先点击上传');
			return;
		} 
		if (form.isValid()) {
			var store = me.getMaterial().getStore();
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
			queryValues.VideoType = 0;
			var store = me.getMaterial().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	afterrender: function () {
		var me = this;
		var store = me.getMaterial().getStore();
		store.load();
	}
});