Ext.define('WX.store.BaseData.ArticleItemStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.ArticleItemModel',
	pageSize: 20,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/ArticleItem',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/ArticleItem?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/ArticleItem/BatchDelete',
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
})