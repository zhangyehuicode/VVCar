Ext.define('WX.store.BaseData.AdvisementBrowseHistoryStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.AdvisementBrowseHistoryModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/AdvisementBrowseHistory',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/AdvisementBrowseHistory?All=false',
		}
	},
});