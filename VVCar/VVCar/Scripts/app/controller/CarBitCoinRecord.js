Ext.define('WX.controller.CarBitCoinRecord', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.CarBitCoinRecordStore'],
	models: ['BaseData.CarBitCoinRecordModel'],
	views: ['CarBitCoinRecord.CarBitCoinRecordList'],
	refs: [{
		ref: 'carBitCoinRecordList',
		selector: 'CarBitCoinRecordList'
	}],
	init: function () {
		var me = this;
		me.control({
			'CarBitCoinRecordList button[action=search]': {
				click: me.search
			},
		});
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getCarBitCoinRecordList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
});