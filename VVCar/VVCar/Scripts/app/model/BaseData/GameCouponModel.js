Ext.define('WX.model.BaseData.GameCouponModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: ['ID', 'Nature', 'CouponType', 'CouponTemplateID', 'TemplateCode', 'Title', 'CreatedDate']
});

