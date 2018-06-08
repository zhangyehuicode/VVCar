Ext.define('WX.model.BaseData.CouponPushModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: ['ID',
		'Title',
		'PushDate',
		'Status',
		'CreatedDate',
	],
});