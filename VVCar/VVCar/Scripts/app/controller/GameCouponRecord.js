Ext.define('WX.controller.GameCouponRecord', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.GameCouponRecordStore'],
	models: ['BaseData.GameCouponRecordModel'],
	views: ['GameCouponRecord.GameCouponRecordList'],
	refs: [{
		ref: 'gameCouponRecordList',
		selector: 'GameCouponRecordList'
	}],
	init: function () {
		var me = this;
		me.control({
			'GameCouponRecordList button[action=search]': {
				click: me.search
			},
		});
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getGameCouponRecordList().getStore();
			me.getGameCouponRecordList().getSelectionModel().clearSelections();
			store.proxy.extraParams = queryValues;
			store.load();
			me.getGameCouponRecordList().getStore().removeAll();
		} else {
			Ext.MessageBox.alert('系统提示', '请输入过滤条件!');
		}
	},
});