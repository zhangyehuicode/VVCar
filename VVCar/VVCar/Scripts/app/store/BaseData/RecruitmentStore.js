Ext.define('WX.store.BaseData.RecruitmentStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.RecruitmentModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Recruitment',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Recruitment?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/Recruitment/BatchDelete',
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
});