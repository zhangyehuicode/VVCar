Ext.define('WX.controller.MaterialPublish', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.MaterialPublishStore'],
	models: ['BaseData.MaterialPublishModel'],
	views: ['MaterialPublish.MaterialPublish', 'MaterialPublish.MaterialPublishEdit', 'MaterialPublish.MaterialPublishItemEdit'],
	refs: [{
		ref: 'materialPublish',
		selector: 'MaterialPublish'
	}, {
		ref: 'gridMaterialPublish',
		selector: 'MaterialPublish grid[name=gridMaterialPublish]'
	}, {
		ref: 'gridMaterialPublishItem',
		selector: 'MaterialPublish grid[name=gridMaterialPublishItem]'
	}, {
		ref: 'gridMaterialPublishItemEdit',
		selector: 'MaterialPublishItemEdit grid[name=couponTemplate]'
	}, {
		ref: 'materialPublishEdit',
		selector: 'MaterialPublishEdit'
	}, {
		ref: 'materialPublishItemEdit',
		selector: 'MaterialPublishItemEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'MaterialPublish button[action=addMaterialPublish]': {
				click: me.addMaterialPublish
			},
			'MaterialPublish button[action=deleteMaterialPublish]': {
				click: me.deleteMaterialPublish
			},
			'MaterialPublish button[action=addMaterialPublishItem]': {
				click: me.addMaterialPublishItem
			},
			'MaterialPublish button[action=addMaterialPublishCardItem]': {
				click: me.addMaterialPublishCardItem
			},
			'MaterialPublish button[action=deleteMaterialPublish]': {
				click: me.deleteMaterialPublish
			},
			'MaterialPublish button[action=deleteMaterialPublishItem]': {
				click: me.deleteMaterialPublishItem
			},
			'MaterialPublish button[action=batchHandMaterialPublish]': {
				click: me.batchHandMaterialPublish
			},
			'MaterialPublish button[action=batchHandCancelMaterialPublish]': {
				click: me.batchHandCancelMaterialPublish
			},
			'MaterialPublish combobox[name=Status]': {
				select: me.searchData
			},
			'MaterialPublish button[action=search]': {
				click: me.searchData
			},
			'MaterialPublish button[action=fleshMaterialPublishItem]': {
				click: me.fleshMaterialPublishItem,
			},
			'MaterialPublish grid[name=gridMaterialPublish]': {
				select: me.gridMaterialPublishSelect,
				itemdblclick: me.editMaterialPublish,
			},
			'MaterialPublish': {
				escActionClick: me.escActionClick,
				descActionClick: me.descActionClick,
			},
			'MaterialPublishEdit button[action=save]': {
				click: me.saveMaterialPublish
			},
			'MaterialPublishItemEdit button[action=save]': {
				click: me.saveMaterialPublishItem
			},
		});
	},
	gridMaterialPublishSelect: function (grid, record, index, eOpts) {
		var me = this;
		var MaterialPublishItemStore = this.getGridMaterialPublishItem().getStore();
		Ext.apply(MaterialPublishItemStore.proxy.extraParams, {
			All: false,
			MaterialPublishID: record.data.ID
		});
		MaterialPublishItemStore.reload();
	},
	addMaterialPublish: function () {
		var win = Ext.widget('MaterialPublishEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加广告');
		win.show();
	},
	editMaterialPublish: function (grid, record) {
		var win = Ext.widget('MaterialPublishEdit');
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑广告');
		win.show();
	},
	addMaterialPublishItem: function () {
		var me = this;
		var MaterialPublishGrid = me.getGridMaterialPublish();
		var selectedItems = MaterialPublishGrid.getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要添加的广告!');
			return;
		} else {
			me.tasks = selectedItems;
		}
		var win = Ext.widget('MaterialPublishItemEdit');
		var store = me.getGridMaterialPublishItemEdit().getStore();
		store.load();
		win.setTitle('添加素材');
		win.show();
	},
	addMaterialPublishCardItem: function () {
		var me = this;
		var MaterialPublishGrid = me.getGridMaterialPublish();
		var selectedItems = MaterialPublishGrid.getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要添加的广告!');
			return;
		} else {
			me.tasks = selectedItems;
		}
		var win = Ext.widget('MaterialPublishItemEdit');
		var store = me.getGridMaterialPublishItemEdit().getStore();
		var params = {
			CouponType: -1,
			AproveStatus: 2,
			Nature: 1,
			HiddenExpirePutInDate: true,
		}
		store.limit = 10;
		store.pageSize = 10;
		Ext.apply(store.proxy.extraParams, params);
		store.load();
		win.setTitle('添加会员卡');
		win.show();
	},
	deleteMaterialPublish: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的广告!');
			return;
		} else {
			selectedItems.forEach(function (item) {
				if (item.data.Status != 0) {
					Ext.Msg.alert('提示', '请选择未发布的广告');
					return;
				}
			})
		}
		Ext.Msg.confirm('提示', '确定要删除广告吗', function (optional) {
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
							me.getGridMaterialPublishItem().getStore().reload();
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
	deleteMaterialPublishItem: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的素材!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除素材吗?', function (optional) {
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
		});
	},
	saveMaterialPublish: function () {
		var me = this;
		var win = me.getMaterialPublishEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var MaterialPublishStore = this.getGridMaterialPublish().getStore();
			if (form.actionMethod == 'POST') {
				MaterialPublishStore.create(formValues, {
					callback: function (records, operation, success) {
						if (!success) {
							Ext.MessageBox.alert('提示', operation.error);
							return;
						} else {
							MaterialPublishStore.add(records[0].data);
							MaterialPublishStore.commitChanges();
							Ext.MessageBox.alert('提示', '新增成功');
							MaterialPublishStore.reload();
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
				MaterialPublishStore.update({
					callback: function (records, operation, success) {
						if (!success) {
							Ext.Msg.alert('提示', operation.error);
							return;
						} else {
							Ext.Msg.alert('提示', '更新成功');
							MaterialPublishStore.reload();
							win.close();
						}

					}
				});
			}
		}
	},
	saveMaterialPublishItem: function () {
		var me = this;
		var win = me.getMaterialPublishItemEdit();
		var store = me.getGridMaterialPublishItem().getStore();
		var selectedItems = me.getGridMaterialPublishItemEdit().getSelectionModel().getSelection();

		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '未选择操作数据');
			return;
		};
		me.tasks.forEach(function (item) {
			var MaterialPublishItem = [];
			selectedItems.forEach(function (index) {
				MaterialPublishItem.push({ MaterialPublishID: item.data.ID, MaterialID: index.data.ID });
			});
			store.batchAdd(MaterialPublishItem, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				if (ajaxResult.Data == false) {
					Ext.Msg.alert("提示", ajaxResult.ErrorMessage);
					return;
				} else {
					store.reload();
				}
			}, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
			});
		});
		store.reload();
		win.close();
	},
	searchData: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getGridMaterialPublish().getStore();
			me.getGridMaterialPublish().getSelectionModel().clearSelections();
			store.proxy.extraParams = queryValues;
			store.load();
			me.getGridMaterialPublishItem().getStore().removeAll();
		} else {
			Ext.MessageBox.alert('系统提示', '请输入过滤条件!');
		}
	},
	fleshMaterialPublishItem: function () {
		var me = this;
		var MaterialPublishItemStore = this.getGridMaterialPublishItem().getStore();
		var record = me.getGridMaterialPublish().getSelectionModel().getSelection();
		Ext.apply(MaterialPublishItemStore.proxy.extraParams, {
			All: false,
			MaterialPublishID: record[0].data.ID
		});
		MaterialPublishItemStore.reload();
	},
	batchHandMaterialPublish: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要发布的广告!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要发布吗?', function (optional) {
			if (optional === 'yes') {
				var store = btn.up('grid').getStore();
				var ids = [];
				selectedItems.forEach(function (item) {
					ids.push(item.data.ID);
				});
				store.batchHandMaterialPublish(ids,
					function success(response, request, c) {
						var ajaxResult = JSON.parse(c.responseText);
						if (ajaxResult.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '发布成功');
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
	batchHandCancelMaterialPublish: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要取消发布的广告!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要取消发布吗?', function (optional) {
			if (optional === 'yes') {
				var store = btn.up('grid').getStore();
				var ids = [];
				selectedItems.forEach(function (item) {
					ids.push(item.data.ID);
				});
				store.batchHandCancelMaterialPublish(ids,
					function success(response, request, c) {
						var ajaxResult = JSON.parse(c.responseText);
						if (ajaxResult.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '取消发布成功');
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
	escActionClick: function (grid, record) {
		this.adjustIndexAction(grid, record, 1);
	},
	descActionClick: function (grid, record) {
		this.adjustIndexAction(grid, record, 2);
	},
	adjustIndexAction: function (grid, record, direction) {
		var store = grid.getStore();
		var data = {
			ID: record.data.ID,
			Direction: direction,
		}
		function success(response) {
			Ext.Msg.hide();
			response = JSON.parse(response.responseText);
			if (!response.IsSuccessful) {
				Ext.Msg.alert('提示', response.ErrorMessage);
				return;
			}
			if (!response.Data) {
				Ext.Msg.alert('提示', direction != 1 ? '降序' : '升序' + '失败！');
				return;
			}
			store.load();
		};
		function failure(response) {
			Ext.Msg.hide();
			Ext.Msg.alert('提示', response.responseText);
		};
		store.adjustIndex(data, success, failure);
	},
});