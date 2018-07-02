Ext.define('WX.model.BaseData.CarBitCoinRecordModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'CarBitCoinMemberID' },
		{ name: 'CarBitCoinMemberName' },
		{ name: 'NamePhone' },
		{ name: 'MobilePhoneNo' },
		{ name: 'CarBitCoinRecordType' },
		{ name: 'Horsepower' },
		{ name: 'CarBitCoin' },
		{ name: 'TradeNo' },
		{ name: 'Remark' },
		{ name: 'CreatedDate' },
	]
});