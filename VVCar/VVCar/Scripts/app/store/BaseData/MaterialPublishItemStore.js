Ext.define('WX.store.BaseData.MaterialPublishItemStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.MaterialPublishItemModel',
	pageSize: 8,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/MaterialPublishItem',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/MaterialPublishItem?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/MaterialPublishItem/BatchAdd',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/MaterialPublishItem/BatchDelete',
			adjustIndex: Ext.GlobalConfig.ApiDomainUrl + 'api/MaterialPublishItem/AdjustIndex'
		}
	},
	batchAdd: function (data, success, failure) {
		Ext.Ajax.request({
			url: this.proxy.api.batchAdd,
			method: 'POST',
			jsonData: data,
			success: success,
			failure: failure
		});
	},
	batchDelete: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'DELETE',
			url: this.proxy.api.batchDelete,
			jsonData: { IdList: ids },
			callback: cb
		});
	},
	adjustIndex: function (params, success, failure) {
		Ext.Ajax.request({
			method: 'GET',
			url: this.proxy.api.adjustIndex,
			params: params,
			success: success,
			failure: failure,
		});
	},
});