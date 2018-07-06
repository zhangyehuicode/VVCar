Ext.define('WX.store.BaseData.DepartmentPerformanceStatisticsStore', {
	extend: "Ext.data.Store",
	model: "WX.model.BaseData.DepartmentPerformanceStatisticsModel",
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: "rest",
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/DepartmentPerformanceStatistics?All=false',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/DepartmentPerformanceStatistics?All=false',
			export: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/ExportDepartmentPerformanceStatistics?All=false',
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