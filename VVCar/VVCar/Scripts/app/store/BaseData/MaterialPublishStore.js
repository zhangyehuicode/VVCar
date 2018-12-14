Ext.define('WX.store.BaseData.MaterialPublishStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.MaterialPublishModel',
	pageSize: 20,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/MaterialPublish',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/MaterialPublish?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/MaterialPublish/BatchDelete',
			batchHandMaterialPublish: Ext.GlobalConfig.ApiDomainUrl + 'api/MaterialPublish/BatchHandMaterialPublish',
			batchHandCancelMaterialPublish: Ext.GlobalConfig.ApiDomainUrl + 'api/MaterialPublish/BatchHandCancelMaterialPublish',
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
	batchHandMaterialPublish: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchHandMaterialPublish,
			method: 'POST',
			jsonData: { IdList: ids },
			callback: cb
		});
	},
	batchHandCancelMaterialPublish: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchHandCancelMaterialPublish,
			method: 'POST',
			jsonData: { IdList: ids },
			callback: cb
		});
	}
});