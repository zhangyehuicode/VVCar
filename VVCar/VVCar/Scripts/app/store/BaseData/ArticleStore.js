Ext.define('WX.store.BaseData.ArticleStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.ArticleModel',
	pageSize: 20,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Article',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Article?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/Article/BatchDelete',
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