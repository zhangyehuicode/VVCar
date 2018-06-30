Ext.define('WX.store.BaseData.CarBitCoinProductStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.CarBitCoinProductModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProduct',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProduct?All=false',
            adjustIndex: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProduct/AdjustIndex',
            publishSoldOut: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProduct/ChangePublishStatus',
            stockOutIn: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProduct/StockOutIn',
        },
    },
    adjustIndex: function(params, success, failure) {
        Ext.Ajax.request({
            method: 'GET',
            url: this.proxy.api.adjustIndex,
            params: params,
            success: success,
            failure: failure,
        });
    },
    publishSoldOut: function(id, success, failure) {
        Ext.Ajax.request({
            method: 'GET',
            url: this.proxy.api.publishSoldOut + '?id=' + id,
            success: success,
            failure: failure,
        });
    },
    stockOutIn: function(data, success, failure) {
        Ext.Ajax.request({
            method: 'POST',
            url: this.proxy.api.stockOutIn,
            jsonData: data,
            success: success,
            failure: failure,
        });
    },
});