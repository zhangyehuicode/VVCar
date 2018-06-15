Ext.define('WX.store.BaseData.ConsumeHistoryStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.ConsumeHistoryModel',
	pageSize: 25,
	autoLoad: false,
	proxy: {
		type: 'rest',
		enablePaging: false,
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/GetConsumeHistory',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/GetConsumeHistory?All=false',
			getConsumeHistory: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/GetConsumeHistory',
			export: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/ExportConsumeHistory?All=false',
		},
	},
	export: function (p, cb) {
		Ext.Ajax.request({
			method: "GET",
			url: this.proxy.api.export,
			params: p,
			callback: cb
		});
	},
});