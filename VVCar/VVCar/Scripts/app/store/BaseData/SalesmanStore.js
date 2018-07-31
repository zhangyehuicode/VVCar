Ext.define('WX.store.BaseData.SalesmanStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.ManageUserModel',
	autoLoad: false,
	pageSize: 10,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/User',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/User/GetSaleUser?All=false',
		},
	},
});