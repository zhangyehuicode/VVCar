Ext.define('WX.store.BaseData.GameCouponStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.GameCouponModel',
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/GameCoupon',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/GameCoupon?All=false',
			addGameCoupon: Ext.GlobalConfig.ApiDomainUrl + 'api/GameCoupon/AddGameCoupon'
		}
	},
	addGameCoupon: function (templateIds, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'POST',
			url: this.proxy.api.addGameCoupon,
			jsonData: { IdList: templateIds },
			callback: cb
		});
	}
});