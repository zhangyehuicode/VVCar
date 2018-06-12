Ext.define('WX.store.BaseData.StaffPerformanceStatisticsStore', {
	extend: "Ext.data.Store",
	model: "WX.model.BaseData.StaffPerformanceStatisticsModel",
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: "rest",
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/StaffPerformanceStatistics?All=false',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/StaffPerformanceStatistics?All=false',
			export: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/ExportStaffPerformanceStatistics?All=false',
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