Ext.define('WX.store.BaseData.MaterialStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.MaterialModel',
	pageSize: 10,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Material',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Material?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/Material/BatchDelete',
		}
	},
	batchDelete: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchDelete,
			method: 'DELETE',
			jsonData: { IdList: ids },
			callback: cb
		})
	},
})