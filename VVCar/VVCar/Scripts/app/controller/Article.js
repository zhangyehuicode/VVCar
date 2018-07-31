Ext.define('WX.controller.Article', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.ArticleStore'],
	models: ['BaseData.ArticleModel'],
	views: ['Article.ArticleList', 'Article.ArticleEdit'],
	refs: [{
		ref: 'articleList',
		selector: 'ArticleList',
	}, {
			ref: 'gridArticle',
			selector: 'ArticleList grid',
		}, {
			ref: 'articleEdit',
			selector: 'ArticleEdit',
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
			'ArticleEdit button[action=save]': {
				click: me.saveArticle
			}
		});
	},
	addArticle: function () {
		var win = Ext.widget('ArticleEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加公告');
		win.show();
	},
	delArticle: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的数据');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除公告吗?', function (optional) {
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
})