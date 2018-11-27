Ext.define('WX.model.BaseData.DataAnalyseModel', {
	extend: 'Ext.data.Model',
	fields: [
		{ name: 'MemberID' },
		{ name: 'MemberName' },
		{ name: 'MemberMobilePhone' },
		{ name: 'TotalQuantity' },
		{ name: 'TotalMoney' },
		{ name: 'DataAnalyseItemDtos' },
		{ name: 'PeopleCount' },
		{ name: 'IsAvailable' },
		{ name: 'PutawayTime' },
		{ name: 'SoleOutTime' },
		{ name: 'CreatedDate' },
	]
})