Ext.define('WX.store.BaseData.DataAnalyseStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.DataAnalyseModel',
	pageSize: 10,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/DataAnalyseList',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/DataAnalyseList?All=false',
		}
	},
});