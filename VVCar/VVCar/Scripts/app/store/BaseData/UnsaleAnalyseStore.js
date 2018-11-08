Ext.define('WX.store.BaseData.UnsaleAnalyseStore', {
	extend: "Ext.data.Store",
	model: "WX.model.BaseData.UnsaleAnalyseModel",
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: "rest",
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/UnsaleProductHistory?All=false',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/UnsaleProductHistory?All=false',
		},
	},
});