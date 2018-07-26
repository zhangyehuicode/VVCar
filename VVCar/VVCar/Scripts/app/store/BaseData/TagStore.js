Ext.define('WX.store.BaseData.TagStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.TagModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Tag',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Tag?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/Tag/BatchDelete',
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
});