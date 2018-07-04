Ext.define('WX.store.BaseData.CarBitCoinOrderStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.CarBitCoinOrderModel',
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinOrder',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinOrder?All=false',
		},
	},
	adjustIndex: function (params, success, failure) {
		Ext.Ajax.request({
			method: 'GET',
			url: '',
			params: params,
			success: success,
			failure: failure,
		});
	},
});