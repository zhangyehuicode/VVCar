/// <reference path="../../ext/ext-all-dev.js" />

Ext.define('WX.store.BaseData.CouponTemplateInfoStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.CouponTemplateInfoModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate/GetCouponTemplateInfo',
            getCouponTemplateDto: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate/GetCouponTemplateDto',
        },
    },
    getCouponTemplateDto: function (id, cb) {
        Ext.Ajax.request({
            method: "GET",
            timeout: 300000,
            url: this.proxy.api.getCouponTemplateDto + '/' + id,
            // jsonData: entities,
            callback: cb
        });
    }
});