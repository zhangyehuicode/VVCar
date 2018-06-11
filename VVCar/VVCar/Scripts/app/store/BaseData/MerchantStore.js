Ext.define('WX.store.BaseData.MerchantStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.MerchantModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Merchant',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Merchant?All=false',
			activateMerchant: Ext.GlobalConfig.ApiDomainUrl + 'api/Merchant/activateMerchant',
			freezeMerchant: Ext.GlobalConfig.ApiDomainUrl + 'api/Merchant/freezeMerchant',
		}
	},
	activateMerchant: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'POST',
			url: this.proxy.api.activateMerchant,
			jsonData: { IdList: ids },
			callback: cb,
		})
	},
	freezeMerchant: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'POST',
			url: this.proxy.api.freezeMerchant,
			jsonData: { IdList: ids },
			callback: cb,
		})
	}
});