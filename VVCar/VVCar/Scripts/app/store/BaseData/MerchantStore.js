Ext.define('WX.store.BaseData.MerchantStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.MerchantModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Merchant',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Merchant?All=false',
		}
	},
});