﻿Ext.define('WX.model.BaseData.MerchantBargainOrderModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Name' },
		{ name: 'ProductID' },
		{ name: 'ProductName' },
		{ name: 'PriceSale' },
		{ name: 'Stock' },
		{ name: 'Price' },
		{ name: 'PeopleCount' },
		{ name: 'IsAvailable' },
		{ name: 'PutawayTime' },
		{ name: 'SoleOutTime' },
		{ name: 'CreatedDate' },
	]
})