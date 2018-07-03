Ext.define('WX.store.BaseData.ManageUserAuditStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.ManageUserAuditModel',
	autoLoad: false,
	pageSize: 10,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/User',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/User/GetManagerUser?All=false',
		},
	},
});