Ext.define('WX.store.BaseData.AgentDepartmentCategoryTreeStore', {
	extend: 'Ext.data.TreeStore',
	model: 'WX.model.BaseData.AgentDepartmentCategoryTreeModel',
	nodeParam: 'ParentId',
	defaultRootId: '00000000-0000-0000-0000-000000000001',
	defaultRootProperty: 'Children',
	rootProperty: 'Children',
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartmentCategory',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartmentCategory/GetTree?parentID=',
			create: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartmentCategory',
			update: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartmentCategory',
			destroy: Ext.GlobalConfig.ApiDomainUrl + 'api/AgentDepartmentCategory',
		},
		reader: {
			type: 'json',
			root: 'Children',
			successProperty: 'IsSuccessful',
			messageProperty: 'ErrorMessage',
		}
	},
	root: {
		Text: '全部分类',
		id: '00000000-0000-0000-0000-000000000001',
		expanded: true,
	},
	addAgentDepartmentCategory: function (entity, cb) {
		Ext.Ajax.request({
			method: 'POST',
			url: this.proxy.api.create,
			jsonData: entity,
			callback: cb
		});
	},
	updateAgentDepartmentCategory: function (entity, cb) {
		Ext.Ajax.request({
			method: 'PUT',
			url: this.proxy.api.update,
			jsonData: entity,
			callback: cb
		});
	},
	deleteAgentDepartmentCategory: function (id, cb) {
		Ext.Ajax.request({
			method: 'DELETE',
			url: this.proxy.api.destroy + "/" + id,
			callback: cb
		});
	},
})