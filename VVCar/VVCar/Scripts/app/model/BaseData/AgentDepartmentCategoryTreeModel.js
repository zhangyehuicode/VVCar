Ext.define('WX.model.BaseData.AgentDepartmentCategoryTreeModel', {
	extend: 'WX.model.BaseData.AgentDepartmentCategoryModel',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'leaf' },
		{ name: 'expanded' },
		{ name: 'Children' },
	]
})