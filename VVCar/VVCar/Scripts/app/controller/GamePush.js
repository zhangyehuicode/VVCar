Ext.define('WX.controller.GamePush', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.GamePushStore'],
	models: ['BaseData.GamePushModel'],
	views: ['GamePush.GamePushList', 'GamePush.GamePushEdit', 'GamePush.GamePushItemEdit', 'GamePush.GameMemberSelector'],
	refs: [{
		ref: 'gamePushList',
		selector: 'GamePushList'
	}, {
		ref: 'gridGamePush',
		selector: 'GamePushList grid[name=gridGamePush]'
	}, {
		ref: 'gridGamePushItem',
		selector: 'GamePushList grid[name=gridGamePushItem]'
	}, {
		ref: 'gridGamePushMember',
		selector: 'GamePushList grid[name=gridGamePushMember]'
	}, {
		ref: 'gridGamePushItemEdit',
		selector: 'GamePushItemEdit grid[name=gameTemplate]'
	}, {
		ref: 'gamePushEdit',
		selector: 'GamePushEdit'
	}, {
		ref: 'gamePushItemEdit',
		selector: 'GamePushItemEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'GamePushList button[action=addGamePush]': {
				click: me.addGamePush
			},
			'GamePushList button[action=deleteGamePush]': {
				click: me.deleteGamePush
			},
			'GamePushList button[action=addGamePushItem]': {
				click: me.addGamePushItem
			},
			'GamePushList button[action=deleteGamePush]': {
				click: me.deleteGamePush
			},
			'GamePushList button[action=deleteGamePushItem]': {
				click: me.deleteGamePushItem
			},
			'GamePushList button[action=batchHandGamePush]': {
				click: me.batchHandGamePush
			},
			'GamePushList button[action=addGamePushMember]': {
				click: me.addGamePushMember
			},
			'GamePushList button[action=deleteGamePushMember]': {
				click: me.deleteGamePushMember
			},
			'GamePushList button[action=search]': {
				click: me.searchData
			},
			'GamePushList grid[name=gridGamePush]': {
				select: me.gridGamePushSelect,
				itemdblclick: me.editGamePush,
			},
			'GamePushList': {
				updateActionClick: me.editGamePush
			},
			'GamePushEdit button[action=save]': {
				click: me.saveGamePush
			},
			'GamePushItemEdit button[action=save]': {
				click: me.saveGamePushItem
			},
			'GameMemberSelector button[action=save]': {
				click: me.saveGamePushMember
			}
		});
	},
	gridGamePushSelect: function (grid, record, index, eOpts) {
		var me = this;
		var gamePushItemStore = this.getGridGamePushItem().getStore();
		Ext.apply(gamePushItemStore.proxy.extraParams, {
			All: false,
			GamePushID: record.data.ID
		});
		gamePushItemStore.reload();

		var gamePushMemberStore = this.getGridGamePushMember().getStore();
		Ext.apply(gamePushMemberStore.proxy.extraParams, {
			All: false,
			GamePushID: record.data.ID
		});
		gamePushMemberStore.reload();
	},
	addGamePush: function () {
		var win = Ext.widget('GamePushEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加任务');
		win.show();
	},
	editGamePush: function (grid, record) {
		var win = Ext.widget('GamePushEdit');
		record.data.PushDate = record.data.PushDate.substring(0, 10);
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑任务');
		win.show();
	},
	addGamePushItem: function () {
		var me = this;
		var gamePushGrid = me.getGridGamePush();
		var selectedItems = gamePushGrid.getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要添加的任务!');
			return;
		} else {
			me.tasks = selectedItems;
		}
		var win = Ext.widget('GamePushItemEdit');
		var store = me.getGridGamePushItemEdit().getStore();
		store.limit = 10;
		store.pageSize = 10;
		store.load();
		win.setTitle('添加游戏');
		win.show();
	},
	deleteGamePush: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的任务!');
			return;
		} else {
			selectedItems.forEach(function (item) {
				if (item.data.Status != 0) {
					Ext.Msg.alert('提示', '请选择未推送的任务');
					return;
				}
			})
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
							me.getGridGamePushItem().getStore().reload();
							me.getGridGamePushMember().getStore().reload();
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
	deleteGamePushItem: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的游戏!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除游戏吗?', function (optional) {
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
	addGamePushMember: function () {
		var me = this;
		var gamePushGrid = me.getGridGamePush();
		var selectedItems = gamePushGrid.getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要添加的任务!');
			return;
		} else {
			me.tasks = selectedItems;
		}
		me.gameMemberSelector = Ext.widget('GameMemberSelector').show();
	},
	deleteGamePushMember: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的会员!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除选中的会员吗?', function (optional) {
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
	saveGamePush: function () {
		var me = this;
		var win = me.getGamePushEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var gamePushStore = this.getGridGamePush().getStore();
			if (form.actionMethod == 'POST') {
				formValues.PushDate += ' 00:00:00';
				gamePushStore.create(formValues, {
					callback: function (records, operation, success) {
						if (!success) {
							Ext.MessageBox.alert('提示', operation.error);
							return;
						} else {
							gamePushStore.add(records[0].data);
							gamePushStore.commitChanges();
							Ext.MessageBox.alert('提示', '新增成功');
							gamePushStore.reload();
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
				gamePushStore.update({
					callback: function (records, operation, success) {
						if (!success) {
							Ext.Msg.alert('提示', operation.error);
							return;
						} else {
							Ext.Msg.alert('提示', '更新成功');
							gamePushStore.reload();
							win.close();
						}

					}
				});
			}
		}
	},
	saveGamePushItem: function () {
		var me = this;
		var win = me.getGamePushItemEdit();
		var store = me.getGridGamePushItem().getStore();
		var selectedItems = me.getGridGamePushItemEdit().getSelectionModel().getSelection();

		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '未选择操作数据');
			return;
		};
		me.tasks.forEach(function (item) {
			var gamePushItem = [];
			selectedItems.forEach(function (index) {
				gamePushItem.push({ GamePushID: item.data.ID, GameSettingID: index.data.ID });
			});
			store.batchAdd(gamePushItem, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				if (ajaxResult.Data == false) {
					Ext.Msg.alert("提示", ajaxResult.ErrorMessage);
					return;
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
			var store = me.getGridGamePush().getStore();
			me.getGridGamePush().getSelectionModel().clearSelections();
			store.proxy.extraParams = queryValues;
			store.load();
			me.getGridGamePushItem().getStore().removeAll();
		} else {
			Ext.MessageBox.alert('系统提示', '请输入过滤条件!');
		}
	},
	batchHandGamePush: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要推送的数据!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要手动推送数据吗?', function (optional) {
			if (optional === 'yes') {
				var store = btn.up('grid').getStore();
				var ids = [];
				selectedItems.forEach(function (item) {
					ids.push(item.data.ID);
				});
				store.batchHandGamePush(ids,
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
	saveGamePushMember: function (btn) {
		var me = this;
		var win = btn.up('window');
		var gridMember = win.down('grid[name=gridMember]');
		var store = me.getGridGamePushMember().getStore();
		var selectedItems = gridMember.getSelectionModel().getSelection();
		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '未选择操作数据');
			return;
		};
		me.tasks.forEach(function (item) {
			var gamePushMember = [];
			selectedItems.forEach(function (index) {
				gamePushMember.push({ GamePushID: item.data.ID, MemberID: index.data.ID });
			});
			store.batchAdd(gamePushMember, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				if (ajaxResult.Data == false) {
					Ext.Msg.alert("提示", ajaxResult.ErrorMessage);
					return;
				}
			}, function (response, opts) {
				var ajaxResult = JSON.parse(response.responseText);
				Ext.Msg.alert('提示', ajaxResult.ErrorMessage);
			});
		});
		store.reload();
		win.close();
	}
});