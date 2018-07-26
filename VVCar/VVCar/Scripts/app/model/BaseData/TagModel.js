Ext.define('WX.model.BaseData.TagModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Code' },
		{ name: 'Name' },
		{ name: 'CreatedDate' },
	]
})