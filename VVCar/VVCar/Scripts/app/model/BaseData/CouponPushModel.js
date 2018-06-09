Ext.define('WX.model.BaseData.CouponPushModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Title' },
		{ name: 'PushDate' },
		{ name: 'Status' },
		{ name: 'CreatedDate' },
	],
});