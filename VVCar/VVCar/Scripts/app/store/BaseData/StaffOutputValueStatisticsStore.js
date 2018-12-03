Ext.define('WX.store.BaseData.StaffOutputValueStatisticsStore', {
	extend: "Ext.data.Store",
	model: "WX.model.BaseData.StaffOutputValueStatisticsModel",
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: "rest",
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/StaffOutputValueStatistics?All=false',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/StaffOutputValueStatistics?All=false',
			export: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/ExportStaffOutputValueStatistics?All=false',
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