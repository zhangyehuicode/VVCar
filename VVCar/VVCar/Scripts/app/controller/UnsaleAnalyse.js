Ext.define('WX.controller.UnsaleAnalyse', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.UnsaleAnalyseStore'],
	models: ['BaseData.UnsaleAnalyseModel'],
	views: ['UnsaleAnalyse.UnsaleAnalyse'],
	refs: [{
		ref: 'unsaleAnalyse',
		selector: 'UnsaleAnalyse'
	}],
	init: function () {
		var me = this;
		me.control({
			'UnsaleAnalyse button[action=search]': {
				click: me.search
			},
		});
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getUnsaleAnalyse().getStore();
			store.proxy.extraParams = queryValues;
			store.currentPage = 1;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
});