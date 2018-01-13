/// <reference path="../../ext/ext-all-dev.js" />

Ext.define('WX.store.BaseData.PointExchangeCouponStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.PointExchangeCouponModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/PointExchangeCoupon',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/PointExchangeCoupon',
            savePointExchangeCoupon: Ext.GlobalConfig.ApiDomainUrl + 'api/PointExchangeCoupon',
        },
    },
    savePointExchangeCoupon: function (entities, cb) {
        Ext.Ajax.request({
            method: "POST",
            timeout: 300000,
            url: this.proxy.api.savePointExchangeCoupon,
            jsonData: entities,
            callback: cb
        });
    },
    updatePointExchangeCoupon: function (entities, cb) {
        Ext.Ajax.request({
            method: "PUT",
            timeout: 300000,
            url: this.proxy.api.savePointExchangeCoupon,
            jsonData: entities,
            callback: cb
        });
    }
});