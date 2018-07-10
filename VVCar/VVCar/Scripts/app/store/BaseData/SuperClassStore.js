Ext.define('WX.store.BaseData.SuperClassStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.SuperClassModel',
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/SuperClass',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/SuperClass?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/SuperClass/BatchDelete',
		},
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
});