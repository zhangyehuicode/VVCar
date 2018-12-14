Ext.define('WX.model.BaseData.MaterialModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Name' },
		{ name: 'Url' },
		{ name: 'CreatedUser' },
		{ name: 'CreatedDate' },
	]
});