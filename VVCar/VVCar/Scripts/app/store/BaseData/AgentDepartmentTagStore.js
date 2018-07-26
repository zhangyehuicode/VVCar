Ext.define('WX.store.BaseData.AgentDepartmentTagStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.AgentDepartmentTagModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartmentTag',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartmentTag?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartmentTag/BatchAdd',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartmentTag/BatchDelete',
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
			url: this.proxy.api.batchDelete,
			method: 'DELETE',
			jsonData: { IdList: ids },
			callback: cb
		});
	},
});