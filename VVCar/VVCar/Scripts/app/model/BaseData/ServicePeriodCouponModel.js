Ext.define('WX.model.BaseData.ServicePeriodCouponModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'CouponTemplateID' },
		{ name: 'CouponTemplateTitle' },
	]
})