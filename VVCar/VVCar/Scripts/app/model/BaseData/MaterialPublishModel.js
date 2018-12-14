Ext.define('WX.model.BaseData.MaterialPublishModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Code' },
		{ name: 'Name' },
		{ name: 'Status' },
		{ name: 'PublishDate' },
		{ name: 'CreatedUser' },
		{ name: 'CreatedDate' },
	]
})