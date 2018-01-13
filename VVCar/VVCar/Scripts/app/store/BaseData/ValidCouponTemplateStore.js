Ext.define('WX.store.BaseData.ValidCouponTemplateStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.ValidCouponTemplateModel',
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