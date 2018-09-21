Ext.define('WX.store.BaseData.ProductRetailStatisticsStore', {
	extend: "Ext.data.Store",
	model: "WX.model.BaseData.ProductRetailStatisticsModel",
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: "rest",
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/ProductRetailStatistics?All=false',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/ProductRetailStatistics?All=false',
			export: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/ExportProductRetailStatistics?All=false',
			unsaleNotify: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/UnsaleProductNotify?All=false',
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
	unsaleNotify: function (params, success, failure) {
		Ext.Ajax.request({
			method: 'GET',
			url: this.proxy.api.unsaleNotify,
			params: params,
			success: success,
			failure: failure,
		})
	},
});