Ext.define('WX.store.BaseData.AdvisementSettingStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.AdvisementSettingModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/AdvisementSetting',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/AdvisementSetting?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/AdvisementSetting/BatchDelete',
		}
	},
	batchDelete: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchDelete,
			method: 'DELETE',
			jsonData: { IdList: ids },
			callback: cb,
		});
	}
})