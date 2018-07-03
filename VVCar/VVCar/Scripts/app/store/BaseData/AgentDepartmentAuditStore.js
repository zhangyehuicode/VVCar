Ext.define('WX.store.BaseData.AgentDepartmentAuditStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.AgentDepartmentAuditModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartment',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartment?All=false',
			approveAgentDepartment: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartment/ApproveAgentDepartment',
		}
	},
	approveAgentDepartment: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'POST',
			url: this.proxy.api.approveAgentDepartment,
			jsonData: { IdList: ids },
			callback: cb,
		})
	},
});