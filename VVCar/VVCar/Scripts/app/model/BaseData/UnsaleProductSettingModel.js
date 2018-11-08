Ext.define('WX.model.BaseData.UnsaleProductSettingModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Code' },
		{ name: 'Name' },
		{ name: 'UnsaleQuantity' },
		{ name: 'SaleWellQuantity' },
		{ name: 'IsAvailable', type: 'boolean' },
		{ name: 'CreatedDate' },
	],
});