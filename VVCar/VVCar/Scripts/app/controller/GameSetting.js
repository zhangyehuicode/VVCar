Ext.define('WX.controller.GameSetting', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.GameSettingStore', 'WX.store.BaseData.GameCouponStore'],
	models: ['BaseData.GameCouponModel', 'BaseData.DepartmentModel'],
	views: ['GameSetting.GameSettingList', 'GameSetting.GameSettingEdit', 'GameSetting.GameCouponEdit'],
	refs: [{
		ref: 'gameSettingList',
		selector: 'GameSettingList'
	}, {
		ref: 'gameSettingEdit',
		selector: 'GameSettingEdit'
	}, {
		ref: 'gridGameSetting',
		selector: 'GameSettingList grid[name=gridGameSetting]'
	}, {
		ref: 'gridGameCoupon',
		selector: 'GameSettingList grid[name=gridGameCoupon]'
	}, {
		ref: 'gameCouponEdit',
		selector: 'GameCouponEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'GameSettingList button[action=addGameCoupon]': {
				click: me.addGameCoupon
			},
			'GameSettingList button[action=search]': {
				click: me.search
			},
			'GameSettingList grid[name=gridGameSetting]': {
				select: me.gridGameSettingSelect,
				itemdblclick: me.editGameSetting,
			},
			'GameSettingList': {
				deleteActionClick: me.deleteItem
			},
			'GameSettingEdit button[action=save]': {
				click: me.saveGameSetting
			},
			'GameCouponEdit button[action=save]': {
				click: me.saveGameCoupon
			}
		});
	},
	gridGameSettingSelect: function (grid, record, index, eOpts) {
		var store = this.getGridGameCoupon().getStore();
		Ext.apply(store.proxy.extraParams, {
			All: false,
			GameSettingID: record.data.ID
		});
		store.reload();
	},
    editGameSetting: function(gird, record) {
		var win = Ext.widget('GameSettingEdit');
        if (record.data.GameType == 0) {
            win.down('combobox[name=IsOrderShow]').hidden = true;
            win.down('label[name=tip]').hidden = true;
        }
		win.form.loadRecord(record);
        win.form.getForm().actionMethod = 'PUT';
		win.setTitle('编辑游戏设置');
		win.show();
	},
	saveGameSetting: function () {
		var win = this.getGameSettingEdit();
		var form = win.form.getForm();
		var formValues = form.getValues();
		if (form.isValid()) {
			var store = this.getGridGameSetting().getStore();
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
	addGameCoupon: function () {
		var win = Ext.widget("GameCouponEdit");
		win.setTitle('添加游戏卡券配置');
		win.show();
	},
	saveGameCoupon: function (btn) {
		var me = this;
		var win = me.getGameCouponEdit();
		var grid = win.grid;
		var gameSettingId = me.getGridGameSetting().getSelectionModel().getSelection();
		if (gameSettingId.length != 1) {
			Ext.Msg.alert("提示", "游戏配置选择错误");
			return;
		}
		var selectedItems = grid.getSelectionModel().getSelection();
		if (selectedItems.length === 0)
			Ext.Msg.alert("提示", "未选择操作数据");

		var store = me.getGridGameCoupon().getStore();
		var gameCoupons = [];
		selectedItems.forEach(function (item) {
			gameCoupons.push({ CouponTemplateID: item.data.ID, GameSettingID: gameSettingId[0].data.ID });
		});

		store.batchAdd(gameCoupons, function (response, opts) {
			var ajaxResult = Ext.decode(response.responseText);
			if (ajaxResult.Data == false) {
				Ext.Msg.alert("提示", ajaxResult.ErrorMessage);
				return;
			}
		}, function (response, opts) {
			var ajaxResult = JSON.parse(response.responseText);
			Ext.Msg.alert("提示", ajaxResult.ErrorMessage);
		});
		store.reload();
		win.close();
	},
	deleteItem: function (grid, record) {
		var me = this;
		Ext.MessageBox.confirm('询问', '您确定要删除数据吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据,请稍后...', '状态提示');
				var gameCouponStore = me.getGridGameCoupon().getStore();
				gameCouponStore.remove(record);
				gameCouponStore.sync({
					callback: function (batch, options) {
						Ext.Msg.hide();
						if (batch.hasException()) {
							Ext.MessageBox.alert('提示', batch.exception[0].error);
							gameCouponStore.rejectChanges();
						} else {
							Ext.MessageBox.alert('提示', '删除成功');
						}
					}
				})
			}
		});
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getGridGameSetting().getStore();
			me.getGridGameSetting().getSelectionModel().clearSelections();
			store.proxy.extraParams = queryValues;
			store.load();
			me.getGridGameCoupon().getStore().removeAll();
		} else {
			Ext.MessageBox.alert('系统提示', '请输入过滤条件!');
		}
	},
});