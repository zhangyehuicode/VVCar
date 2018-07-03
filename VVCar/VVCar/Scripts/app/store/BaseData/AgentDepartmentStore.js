Ext.define('WX.store.BaseData.AgentDepartmentStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.AgentDepartmentModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartment',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartment?All=false',
		}
	},
});