Ext.define('WX.model.BaseData.UnsaleAnalyseModel', {
	extend: 'Ext.data.Model',
	fields: [
		{ name: 'Code' },
		{ name: 'ProductTypeText' },
		{ name: 'ProductCode' },
		{ name: 'ProductName' },
		{ name: 'UnsaleQuantity' },
		{ name: 'SaleWellQuantity' },
		{ name: 'Quantity' },
		{ name: 'StatusText'},
	],
});