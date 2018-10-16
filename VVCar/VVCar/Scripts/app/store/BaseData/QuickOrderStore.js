Ext.define('WX.store.BaseData.QuickOrderStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.QuickOrderModel',
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/QuickOrder'
	}
})