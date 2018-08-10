Ext.define('WX.store.BaseData.AnnouncementPushMemberStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.AnnouncementPushMemberModel',
	pageSize: 10,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/AnnouncementPushMember',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/AnnouncementPushMember?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/AnnouncementPushMember/BatchAdd',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/AnnouncementPushMember/BatchDelete'
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
})