Ext.define('WX.controller.AdvisementBrowseHistory', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.AdvisementBrowseHistoryStore'],
	models: ['BaseData.AdvisementBrowseHistoryModel'],
	views: ['CustomerFinder.AdvisementBrowseHistoryList'],
	refs: [{
		ref: 'advisementBrowseHistoryList',
		selector: 'AdvisementBrowseHistoryList',
	}],
	init: function () {
		var me = this;
		me.control({
			'AdvisementBrowseHistoryList button[action=search]': {
				click: me.search
			}
		});
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getAdvisementBrowseHistoryList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	}
})