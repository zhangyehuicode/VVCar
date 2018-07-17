Ext.define('WX.store.BaseData.CouponPushTemplateInfoStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.CouponTemplateInfoModel',
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate/GetValidCouponTemplateInfo',
		},
	},
});