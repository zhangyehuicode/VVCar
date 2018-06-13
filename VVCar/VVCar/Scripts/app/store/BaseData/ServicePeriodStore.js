﻿Ext.define('WX.store.BaseData.ServicePeriodStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.ServicePeriodModel',
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/ServicePeriod',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/ServicePeriod?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + "api/ServicePeriod/BatchDelete",
			enableServicePeriod: Ext.GlobalConfig.ApiDomainUrl + "api/ServicePeriod/EnableServicePeriod",
			disableServicePeriod: Ext.GlobalConfig.ApiDomainUrl + "api/ServicePeriod/DisableServicePeriod",
		}
	},
	batchDelete: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchDelete,
			method: 'DELETE',
			jsonData: { IdList: ids },
			callback: cb
		});
	},
	enableServicePeriod: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.enableServicePeriod,
			method: 'POST',
			jsonData: { IdList: ids },
			callback: cb
		});
	},
	disableServicePeriod: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.disableServicePeriod,
			method: 'POST',
			jsonData: { IdList: ids },
			callback: cb
		});
	},
});