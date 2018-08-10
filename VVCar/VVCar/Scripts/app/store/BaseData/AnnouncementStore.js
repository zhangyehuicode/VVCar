Ext.define('WX.store.BaseData.AnnouncementStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.AnnouncementModel',
	pageSize: 20,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Announcement',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Announcement?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/Announcement/BatchDelete',
			batchHandPush: Ext.GlobalConfig.ApiDomainUrl + 'api/Announcement/BatchHandPush',
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
	batchHandPush: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchHandPush,
			method: 'POST',
			jsonData: { IdList: ids },
			callback: cb
		})
	}
})