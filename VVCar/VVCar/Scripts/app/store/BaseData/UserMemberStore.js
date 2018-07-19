Ext.define('WX.store.BaseData.UserMemberStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.UserMemberModel',
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/UserMember',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/UserMember?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/UserMember/BatchAdd',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/UserMember/BatchDelete',
		},
	},
	batchAdd: function (data, success, failure) {
		Ext.Ajax.request({
			url: this.proxy.api.batchAdd,
			method: 'POST',
			jsonData: data,
			success: success,
			failure: failure,
		});
	},
	batchDelete: function (data, success, failure) {
		Ext.Ajax.request({
			url: this.proxy.api.batchDelete,
			method: 'DELETE',
			jsonData: data,
			success: success,
			failure: failure,
		});
	},
});