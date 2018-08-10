Ext.define('WX.controller.Announcement', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.AnnouncementStore', 'WX.view.selector.MemberSelector'],
	models: ['BaseData.AnnouncementModel'],
	views: ['Announcement.AnnouncementList', 'Announcement.AnnouncementEdit'],
	refs: [{
		ref: 'announcementList',
		selector: 'AnnouncementList',
	}, {
		ref: 'announcementEdit',
		selector: 'AnnouncementEdit',
	}, {
		ref: 'gridAnnouncement',
		selector: 'AnnouncementList grid[name=gridAnnouncement]',
	}, {
		ref: 'gridAnnouncementPushMember',
		selector: 'AnnouncementList grid[name=gridAnnouncementPushMember]'
	}],
	init: function () {
		var me = this;
		me.control({
			'AnnouncementList button[action=addAnnouncement]': {
				click: me.addAnnouncement
			},
			'AnnouncementList button[action=delAnnouncement]': {
				click: me.delAnnouncement
			},
			'AnnouncementList button[action=batchHandPush]': {
				click: me.batchHandPush
			},
			'AnnouncementList button[action=addAnnouncementPushMember]': {
				click: me.addAnnouncementPushMember
			},
			'AnnouncementList button[action=delAnnouncementPushMember]': {
				click: me.delAnnouncementPushMember
			},
			'AnnouncementList grid[name=gridAnnouncement]': {
				select: me.gridAnnouncementSelect,
				itemdblclick: me.editAnnouncement,
			},
			'AnnouncementList button[action=search]': {
				click: me.search
			},
			'AnnouncementEdit button[action=save]': {
				click: me.save
			},
			'MemberSelector button[action=save]': {
				click: me.saveAnnouncementPushMember
			}
		})
	},
	gridAnnouncementSelect: function (grid, record, index, eOpts) {
		var me = this;
		var announcementPushMemberStore = this.getGridAnnouncementPushMember().getStore();
		Ext.apply(announcementPushMemberStore.proxy.extraParams, {
			All: false,
			AnnouncementID: record.data.ID
		});
		announcementPushMemberStore.reload();
	},
	addAnnouncementPushMember: function () {
		var me = this;
		var announcementGrid = me.getGridAnnouncement();
		var selectedItems = announcementGrid.getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要添加的任务!');
			return;
		} else {
			me.tasks = selectedItems;
		}
		me.memberSelector = Ext.create('WX.view.selector.MemberSelector').show();
	},
	delAnnouncementPushMember: function (btn) {
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
	saveAnnouncementPushMember: function (btn) {
		var me = this;
		var win = btn.up('window');
		var gridMember = win.down('grid[name=gridMember]');
		var store = me.getGridAnnouncementPushMember().getStore();
		var selectedItems = gridMember.getSelectionModel().getSelection();
		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '未选择操作数据');
			return;
		};
		me.tasks.forEach(function (item) {
			var announcementPushMember = [];
			selectedItems.forEach(function (index) {
				announcementPushMember.push({ AnnouncementID: item.data.ID, MemberID: index.data.ID });
			});
			store.batchAdd(announcementPushMember, function (response, opts) {
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
	addAnnouncement: function () {
		var win = Ext.widget('AnnouncementEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加公告');
		win.show();
	},
	delAnnouncement: function (btn) {
		var me = this;
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的数据');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除吗?', function (optional) {
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
	save: function () {
		var me = this;
		var win = me.getAnnouncementEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = me.getGridAnnouncement().getStore();
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
	batchHandPush: function (btn) {
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
				store.batchHandPush(ids,
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
	editAnnouncement: function (grid, record) {
		var win = Ext.widget('AnnouncementEdit');
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑公告');
		win.show();
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getGridAnnouncement().getStore();
			me.getGridAnnouncement().getSelectionModel().clearSelections();
			store.proxy.extraParams = queryValues;
			store.load();
			me.getGridCouponPushItem().getStore().removeAll();
		} else {
			Ext.MessageBox.alert('系统提示', '请输入过滤条件!');
		}
	}
})