Ext.define('WX.store.BaseData.PickUpOrderPaymentDetailsStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.PickUpOrderPaymentDetailsModel',
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderPaymentDetails',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderPaymentDetails?All=false',
		},
	},
});