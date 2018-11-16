Ext.define('WX.store.BaseData.OperationStatementDetailStore', {
	extend: "Ext.data.Store",
	model: "WX.model.BaseData.OperationStatementDetailModel",
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: "rest",
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/GetOperationStatementDetail?All=false',
		},
	},
});