Ext.define('WX.model.BaseData.AgentDepartmentTagModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'TagCode' },
		{ name: 'TagName' },
		{ name: 'CreatedDate' },
	]
})