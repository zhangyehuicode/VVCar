Ext.define('WX.store.BaseData.GameCouponStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.GameCouponModel',
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/GameCoupon',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/GameCoupon?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/GameCoupon/BatchAdd'
		}
	},
	batchAdd: function (data, success, failure) {
		Ext.Ajax.request({
			url: this.proxy.api.batchAdd,
			method: 'POST',
			jsonData: data,
			success: success,
			failure: failure,
		});
	}
});