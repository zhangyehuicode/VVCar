Ext.define('WX.model.BaseData.GameCouponRecordModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'GameType' },
		{ name: 'ReceiveOpenID' },
		{ name: 'CouponTemplateID' },
		{ name: 'CouponTitle' },
		{ name: 'ReceiveCount' },
		{ name: 'NickName' },
		{ name: 'OutTradeNo' },
		{ name: 'CreatedDate' },
	],
});