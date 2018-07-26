Ext.define('WX.store.BaseData.AgentDepartmentCategoryLiteStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.IDCodeNameModel',
	pageSize: 25,
	autoLoad: false,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartmentCategory',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartmentCategory/LiteData?All=false',
		},
		reader: {
			type: 'json',
			root: 'Data',
			successProperty: 'IsSuccessful',
			messageProperty: 'ErrorMessage',
		}
	}
})