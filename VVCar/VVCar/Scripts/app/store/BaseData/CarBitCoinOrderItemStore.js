Ext.define('WX.store.BaseData.CarBitCoinOrderItemStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.CarBitCoinOrderItemModel',
	autoLoad: false,
	pageSize: 5,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinOrderItem',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinOrderItem?All=false',
		},
	},
});