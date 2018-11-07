Ext.define('WX.model.BaseData.OrderDividendModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'TradeNo' },
		{ name: 'Code' },
		{ name: 'Name' },
		{ name: 'UserCode' },
		{ name: 'UserName' },
		{ name: 'UserID' },
		{ name: 'Money' },
		{ name: 'Commission' },
		{ name: 'OrderType' },
		{ name: 'OrderTypeText' },
		{ name: 'PeopleType' },
		{ name: 'PeopleTypeText' },
		{ name: 'CreatedDate' },
		{ name: 'IsBalance' },
		{ name: 'BalanceUserName' },
		{ name: 'BalanceDate' },
	]
});