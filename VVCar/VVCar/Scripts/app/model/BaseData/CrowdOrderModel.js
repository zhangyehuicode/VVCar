Ext.define('WX.model.BaseData.CrowdOrderModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Name' },
		{ name: 'ProductID' },
		{ name: 'ProductName' },
		{ name: 'Price' },
		{ name: 'PeopleCount' },
		{ name: 'IsAvailable' },
		{ name: 'PutawayTime' },
		{ name: 'SoleOutTime' },
		{ name: 'CreatedDate' },
	]
})