Ext.define('WX.model.BaseData.CouponPushItemModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: ['ID',
		'CouponTemplateID',
		'TemplateCode',
		'CouponTemplateTitle'
	],
});