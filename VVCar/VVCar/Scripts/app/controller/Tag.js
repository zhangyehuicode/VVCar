Ext.define('WX.controller.Tag', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.TagStore'],
	models: ['BaseData.TagModel'],
	views: ['Tag.TagList', 'Tag.TagEdit'],
	refs: [{
		ref: 'tagList',
		selector: 'TagList'
	}, {
		ref: 'tagEdit',
		selector: 'TagEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'TagList button[action=addTag]': {
				click: me.addTag
			},
			'TagList button[action=batchDelete]': {
				click: me.batchDelete
			},
			'TagList': {
				itemdblclick: me.editTag
			},
			'TagList button[action=search]': {
				click: me.search
			},
			'TagEdit button[action=save]': {
				click: me.save
			},
		});
	},
	addTag: function () {
		var win = Ext.widget('TagEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle("添加客户标签");
		win.show();
	},
	editTag: function (grid, record) {
		var win = Ext.widget('TagEdit');
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑标签');
		win.show();
	},
	save: function () {
		var me = this;
		var win = me.getTagEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getTagList().getStore();
			if (form.actionMethod == "POST") {
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
				})
			}
		}
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getTagList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	batchDelete: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请选择要删除的数据');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除数据吗?', function (optional) {
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
	}
})