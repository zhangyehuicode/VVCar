Ext.define('WX.store.BaseData.GamePushMemberStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.GamePushMemberModel',
	pageSize: 10,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePushMember',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePushMember?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePushMember/BatchAdd',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/GamePushMember/BatchDelete'
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