Ext.define('WX.store.BaseData.GamePushStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.GamePushModel',
	pageSize: 20,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePush',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePush?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePush/deleteGamePushs',
			batchHandGamePush: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePush/batchHandGamePush',
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
	batchHandGamePush: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchHandGamePush,
			method: 'POST',
			jsonData: { IdList: ids },
			callback: cb
		});
	}
});