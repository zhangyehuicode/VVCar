Ext.define('WX.controller.CouponPush', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.CouponPushStore', 'WX.view.selector.MemberSelector'],
	models: ['BaseData.CouponPushModel'],
	views: ['CouponPush.CouponPushList', 'CouponPush.CouponPushEdit', 'CouponPush.CouponPushItemEdit'],
	refs: [{
		ref: 'couponPushList',
		selector: 'CouponPushList'
	}, {
		ref: 'gridCouponPush',
		selector: 'CouponPushList grid[name=gridCouponPush]'
	}, {
		ref: 'gridCouponPushItem',
		selector: 'CouponPushList grid[name=gridCouponPushItem]'
	}, {
		ref: 'gridCouponPushMember',
		selector: 'CouponPushList grid[name=gridCouponPushMember]'
	}, {
		ref: 'gridCouponPushItemEdit',
		selector: 'CouponPushItemEdit grid[name=couponTemplate]'
	}, {
		ref: 'couponPushEdit',
		selector: 'CouponPushEdit'
	}, {
		ref: 'couponPushItemEdit',
		selector: 'CouponPushItemEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'CouponPushList button[action=addCouponPush]': {
				click: me.addCouponPush
			},
			'CouponPushList button[action=deleteCouponPush]': {
				click: me.deleteCouponPush
			},
			'CouponPushList button[action=addCouponPushItem]': {
				click: me.addCouponPushItem
			},
			'CouponPushList button[action=addCouponPushCardItem]': {
				click: me.addCouponPushCardItem
			},
			'CouponPushList button[action=deleteCouponPush]': {
				click: me.deleteCouponPush
			},
			'CouponPushList button[action=deleteCouponPushItem]': {
				click: me.deleteCouponPushItem
			},
			'CouponPushList button[action=batchHandCouponPush]': {
				click: me.batchHandCouponPush
			},
			'CouponPushList button[action=addCouponPushMember]': {
				click: me.addCouponPushMember
			},
			'CouponPushList button[action=deleteCouponPushMember]': {
				click: me.deleteCouponPushMember
			},
			'CouponPushList button[action=search]': {
				click: me.searchData
			},
			'CouponPushList grid[name=gridCouponPush]': {
				select: me.gridCouponPushSelect,
				itemdblclick: me.editCouponPush,
			},
			'CouponPushList': {
				updateActionClick: me.editCouponPush
			},
			'CouponPushEdit button[action=save]': {
				click: me.saveCouponPush
			},
			'CouponPushItemEdit button[action=save]': {
				click: me.saveCouponPushItem
			},
			'MemberSelector button[action=save]': {
				click: me.saveCouponPushMember
			}
		});
	},
	gridCouponPushSelect: function (grid, record, index, eOpts) {
		var me = this;
		var couponPushItemStore = this.getGridCouponPushItem().getStore();
		Ext.apply(couponPushItemStore.proxy.extraParams, {
			All: false,
			CouponPushID: record.data.ID
		});
		couponPushItemStore.reload();

		var couponPushMemberStore = this.getGridCouponPushMember().getStore();
		Ext.apply(couponPushMemberStore.proxy.extraParams, {
			All: false,
			CouponPushID: record.data.ID
		});
		couponPushMemberStore.reload();
	},
	addCouponPush: function () {
		var win = Ext.widget('CouponPushEdit');
		win.form.getForm().actionMethod = 'POST';
		win.setTitle('添加任务');
		win.show();
	},
	editCouponPush: function (grid, record) {
		var win = Ext.widget('CouponPushEdit');
		record.data.PushDate = record.data.PushDate.substring(0, 10);
		win.form.loadRecord(record);
		win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑任务');
		win.show();
	},
	addCouponPushItem: function () {
		var me = this;
		var couponPushGrid = me.getGridCouponPush();
		var selectedItems = couponPushGrid.getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要添加的任务!');
			return;
		} else {
			me.tasks = selectedItems;
		}
		var win = Ext.widget('CouponPushItemEdit');
		var store = me.getGridCouponPushItemEdit().getStore();
		var params = {
			CouponType: -1,
			AproveStatus: 2,
			Nature: 0,
		}
		store.limit = 10;
		store.pageSize = 10;
		Ext.apply(store.proxy.extraParams, params);
		store.load();
		win.setTitle('添加优惠券');
		win.show();
	},
	addCouponPushCardItem: function () {
		var me = this;
		var couponPushGrid = me.getGridCouponPush();
		var selectedItems = couponPushGrid.getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要添加的任务!');
			return;
		} else {
			me.tasks = selectedItems;
		}
		var win = Ext.widget('CouponPushItemEdit');
		var store = me.getGridCouponPushItemEdit().getStore();
		var params = {
			CouponType: -1,
			AproveStatus: 2,
			Nature: 1,
		}
		store.limit = 10;
		store.pageSize = 10;
		Ext.apply(store.proxy.extraParams, params);
		store.load();
		win.setTitle('添加会员卡');
		win.show();
	},
	deleteCouponPush: function (btn) {
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
							me.getGridCouponPushItem().getStore().reload();
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
	deleteCouponPushItem: function (btn) {
		var selectedItems = btn.up('grid').getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要删除的卡券!');
			return;
		}
		Ext.Msg.confirm('提示', '确定要删除卡券吗?', function (optional) {
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
	addCouponPushMember: function () {
		var me = this;
		var couponPushGrid = me.getGridCouponPush();
		var selectedItems = couponPushGrid.getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '请先选择要添加的任务!');
			return;
		} else {
			me.tasks = selectedItems;
		}
		me.memberSelector = Ext.create('WX.view.selector.MemberSelector').show();
	},
	deleteCouponPushMember: function (btn) {
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
	saveCouponPush: function () {
		var me = this;
		var win = me.getCouponPushEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var couponPushStore = this.getGridCouponPush().getStore();
			if (form.actionMethod == 'POST') {
				formValues.PushDate += ' 00:00:00';
				couponPushStore.create(formValues, {
					callback: function (records, operation, success) {
						if (!success) {
							Ext.MessageBox.alert('提示', operation.error);
							return;
						} else {
							couponPushStore.add(records[0].data);
							couponPushStore.commitChanges();
							Ext.MessageBox.alert('提示', '新增成功');
							couponPushStore.reload();
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
				couponPushStore.update({
					callback: function (records, operation, success) {
						if (!success) {
							Ext.Msg.alert('提示', operation.error);
							return;
						} else {
							Ext.Msg.alert('提示', '更新成功');
							couponPushStore.reload();
							win.close();
						}

					}
				});
			}
		}
	},
	saveCouponPushItem: function () {
		var me = this;
		var win = me.getCouponPushItemEdit();
		var store = me.getGridCouponPushItem().getStore();
		var selectedItems = me.getGridCouponPushItemEdit().getSelectionModel().getSelection();

		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '未选择操作数据');
			return;
		};
		me.tasks.forEach(function (item) {
			var couponPushItem = [];
			selectedItems.forEach(function (index) {
				couponPushItem.push({ CouponPushID: item.data.ID, CouponTemplateID: index.data.ID, CouponTemplateTitle: index.data.Title });
			});
			store.batchAdd(couponPushItem, function (response, opts) {
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
			var store = me.getGridCouponPush().getStore();
			me.getGridCouponPush().getSelectionModel().clearSelections();
			store.proxy.extraParams = queryValues;
			store.load();
			me.getGridCouponPushItem().getStore().removeAll();
		} else {
			Ext.MessageBox.alert('系统提示', '请输入过滤条件!');
		}
	},
	batchHandCouponPush: function (btn) {
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
				store.batchHandCouponPush(ids,
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
	saveCouponPushMember: function (btn) {
		var me = this;
		var win = btn.up('window');
		var gridMember = win.down('grid[name=gridMember]');
		var store = me.getGridCouponPushMember().getStore();
		var selectedItems = gridMember.getSelectionModel().getSelection();
		if (selectedItems.length === 0) {
			Ext.Msg.alert('提示', '未选择操作数据');
			return;
		};
		me.tasks.forEach(function (item) {
			var couponPushMember = [];
			selectedItems.forEach(function (index) {
				couponPushMember.push({ CouponPushID: item.data.ID, MemberID: index.data.ID });
			});
			store.batchAdd(couponPushMember, function (response, opts) {
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