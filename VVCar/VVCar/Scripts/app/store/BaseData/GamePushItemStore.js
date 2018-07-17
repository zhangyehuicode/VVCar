Ext.define('WX.store.BaseData.GamePushItemStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.GamePushItemModel',
	pageSize: 8,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePushItem',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePushItem?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePushItem/BatchAdd',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePushItem/BatchDelete'
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
	}
});