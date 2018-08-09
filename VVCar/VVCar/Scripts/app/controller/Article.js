Ext.define('WX.controller.Article', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.ArticleStore'],
	models: ['BaseData.ArticleModel'],
	views: ['Article.ArticleList', 'Article.ArticleEdit', 'Article.ArticleItemEdit'],
	refs: [{
		ref: 'articleList',
		selector: 'ArticleList',
	}, {
		ref: 'gridArticle',
		selector: 'ArticleList grid[name=gridArticle]',
	}, {
		ref: 'gridArticleItem',
		selector: 'ArticleList grid[name=gridArticleItem]',
	}, {
		ref: 'articleEdit',
		selector: 'ArticleEdit',
	}, {
		ref: 'articleItemEdit',
		selector: 'ArticleItemEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'ArticleList button[action=addArticle]': {
				click: me.addArticle
			},
			'ArticleList button[action=delArticle]': {
				click: me.delArticle
			},
			'ArticleList button[action=addArticleItem]': {
				click: me.addArticleItem
			},
			'ArticleList button[action=delArticleItem]': {
				click: me.delArticleItem
			},
			'ArticleList button[action=batchHandArticle]': {
				click: me.batchHandArticle
			},
			'ArticleList grid[name=gridArticle]': {
				select: me.gridArticleSelect,
				itemdblclick: me.editArticle
			},
			'ArticleList grid[name=gridArticleItem]': {
				itemdblclick: me.editArticleItem
			},
			'ArticleEdit button[action=save]': {
				click: me.saveArticle
			},
			'ArticleItemEdit button[action=uploadpic]': {
				click: me.uploadpic
			},
			'ArticleItemEdit button[action=save]': {
				click: me.saveArticleItem
			}
		});
	},
	gridArticleSelect: function (grid, record, index, eOpts) {
		var me = this;
		var articleItemStore = me.getGridArticleItem().getStore();
		Ext.apply(articleItemStore.proxy.extraParams, {
			All: false,
			ArticleID: record.data.ID
		});
		articleItemStore.reload();
	},
	addArticle: function () {
		var win = Ext.widget('ArticleEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加图文消息');
		win.show();
	},
	editArticle: function (grid, record) {
		var win = Ext.widget('ArticleEdit');
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑图文消息');
		win.show();
	},
	batchHandArticle: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要推送的数据!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要手动推送吗?', function (optional) {
			if (optional === 'yes') {
				var store = btn.up('grid').getStore();
				var ids = [];
				selectedItems.forEach(function (item) {
					ids.push(item.data.ID);
				});
				store.batchHandArticle(ids,
					function success(response, request, c) {
						var ajaxResult = JSON.parse(c.responseText);
						if (ajaxResult.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '手动推送成功');
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
	editArticleItem: function (grid, record) {
		var win = Ext.widget('ArticleItemEdit');
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑图文消息子项');
		win.show();
	},
	delArticle: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的数据');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除图文消息吗?', function (optional) {
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
				)
			}
		})
	},
	addArticleItem: function () {
		var me = this;
		var selectedItems = me.getGridArticle().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选择图文消息');
			return;
		}
		if (selectedItems.length > 1) {
			Ext.Msg.alert('提示', '一次只能选择一条图文消息');
			return;
		}
		var win = Ext.widget('ArticleItemEdit');
		win.down('textfield[name=ArticleID]').setValue(selectedItems[0].data.ID);
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加公告子项');
		win.show();
	},
	delArticleItem: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选择要删除的图文消息');
			return;
		}
		var me = this;
		Ext.Msg.confirm('询问', '您确定要删除吗', function (optional) {
			if (optional == 'yes') {
				var store = me.getGridArticleItem().getStore();
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
		});
	},
	saveArticle: function () {
		var me = this;
		var win = me.getArticleEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getGridArticle().getStore();
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
	uploadpic: function (btn) {
		var form = btn.up('form').getForm();
		var win = btn.up('window');
		if (form.isValid()) {
			form.submit({
				url: '/api/UploadFile/UploadArticle',
				method: 'POST',
				waitMsg: '正在上传...',
				success: function (fp, o) {
					if (o.result.success) {
						Ext.Msg.alert('提示', '上传成功！');
						win.down('textfield[name=CoverPicUrl]').setValue(o.result.FileUrl);
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
	saveArticleItem: function () {
		var me = this;
		var win = me.getArticleItemEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (formValues.CoverPicUrl == '') {
			Ext.Msg.alert('提示', '请先上传封面');
			return;
		}
		if (form.isValid()) {
			var store = me.getGridArticleItem().getStore();
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
							Ext.MessageBox.alert("提示", operation.error);
							return;
						} else {
							Ext.MessageBox.alert("提示", "更新成功");
							store.reload();
							win.close();
						}
					}
				});
			}
		}
	}
})