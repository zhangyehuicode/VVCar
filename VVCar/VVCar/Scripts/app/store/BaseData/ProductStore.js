Ext.define('WX.store.BaseData.ProductStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.ProductModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/Product',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/Product?All=false',
            adjustIndex: Ext.GlobalConfig.ApiDomainUrl + 'api/Product/AdjustIndex',
            publishSoldOut: Ext.GlobalConfig.ApiDomainUrl + 'api/Product/ChangePublishStatus',
        },
    },
    adjustIndex: function (params, success, failure) {
        Ext.Ajax.request({
            method: 'GET',
            url: this.proxy.api.adjustIndex,
            params: params,
            success: success,
            failure: failure,
        });
    },
    publishSoldOut: function (id, success, failure) {
        Ext.Ajax.request({
            method: 'GET',
            url: this.proxy.api.publishSoldOut + '?id=' + id,
            success: success,
            failure: failure,
        });
    },
});