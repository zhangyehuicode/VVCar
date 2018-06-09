Ext.define('WX.store.BaseData.ServicePeriodStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.ServicePeriodModel',
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/ServicePeriod',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/ServicePeriod?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + "api/ServicePeriod/BatchDelete"
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
	}
});