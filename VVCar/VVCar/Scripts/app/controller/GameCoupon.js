Ext.define('WX.controller.GameCoupon', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.GameCouponStore'],
	models: ['BaseData.GameCouponModel', 'BaseData.DepartmentModel'],
	views: ['GameCoupon.GameCouponList', 'GameCoupon.GameCouponEdit'],
	refs: [{
		ref: 'gameCouponList',
		selector: 'GameCouponList'
	}, {
		ref: 'gameCouponEdit',
		selector: 'GameCouponEdit'
	}],
	init: function () {
		var me = this;
		me.control({
			'GameCouponList button[action=addGameCoupon]': {
				click: me.addGameCoupon
			},
			'GameCouponList': {
				deleteActionClick: me.deleteItem
			},
			'GameCouponEdit button[action=save]': {
				click: me.saveGameCoupon
			}
		});
	},
	addGameCoupon: function (button) {
		var win = Ext.widget("GameCouponEdit");
		win.setTitle('添加游戏卡券配置');
		win.show();
	},
	saveGameCoupon: function (btn) {
		var me = this;
		var win = me.getGameCouponEdit();
		var grid = win.grid;
		var selectedItems = grid.getSelectionModel().getSelection();
		if (selectedItems.length === 0)
			Ext.Msg.alert("提示", "未选择操作数据");
		var store = me.getGameCouponList().getStore();
		var templateIds = [];
		selectedItems.forEach(function (item) {
			templateIds.push(item.data.ID);
		});
		store.addGameCoupon(templateIds,
			function success(response, request, c) {
				var result = Ext.decode(c.responseText);
				if (result.IsSuccessful) {
					store.reload();
					Ext.Msg.alert("提示", "新增功");
					win.close();
				} else {
					Ext.Msg.alert("提示", result.ErrorMessage);
				}
			},
			function failure(a, b, c) {
				Ext.Msg.alert("提示", "新增失败.");
			}
		);
	},
	deleteItem: function (grid, record) {
		var me = this;
		Ext.MessageBox.confirm('询问', '您确定要删除数据吗?', function (opt) {
			if (opt == 'yes') {
				Ext.Msg.wait('正在处理数据,请稍后...', '状态提示');
				var gameCouponStore = me.getGameCouponList().getStore();
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
	}
});