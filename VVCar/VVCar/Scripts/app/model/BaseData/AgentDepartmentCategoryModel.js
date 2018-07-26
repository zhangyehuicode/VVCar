Ext.define('WX.model.BaseData.AgentDepartmentCategoryModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'ParentId' },
		{ name: 'Index' },
		{ name: 'Code' },
		{ name: 'Name' },
	]
})