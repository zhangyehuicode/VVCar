Ext.define('WX.model.BaseData.ProductRetailStatisticsModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ProductID',
	fields: [
		{ name: 'ProductID' },
		{ name: 'ProductType' },
		{ name: 'ProductCategoryName' },
		{ name: 'ProductName' },
		{ name: 'ProductCode' },
		{ name: 'Quantity' },
		{ name: 'Unit' },
		{ name: 'Money' },
		{ name: 'OrderType' },
	]
})