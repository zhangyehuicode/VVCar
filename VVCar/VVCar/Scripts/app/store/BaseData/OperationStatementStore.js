﻿Ext.define('WX.store.BaseData.OperationStatementStore', {
	extend: "Ext.data.Store",
	model: "WX.model.BaseData.OperationStatementModel",
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: "rest",
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/GetOperationStatement?All=false',
			export: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/ExtportOperationStatement'
		},
	},
	export: function (p, cb) {
		Ext.Ajax.request({
			method: 'GET',
			url: this.proxy.api.export,
			params: p,
			callback: cb,
		});
	}
});