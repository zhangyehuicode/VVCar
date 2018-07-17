Ext.define('WX.model.BaseData.CouponPushItemModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'CouponTemplateID' },
		{ name: 'TemplateCode'},
		{ name: 'CouponTemplateTitle' },
		{ name: 'PutInStartDate' },
		{ name: 'PutInEndDate' },
	],
});