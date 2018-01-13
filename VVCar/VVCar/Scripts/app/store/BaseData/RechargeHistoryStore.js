Ext.define('WX.store.BaseData.RechargeHistoryStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.RechargeHistoryModel',
    pageSize: 25,
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePaging: false,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/RechargeHistory',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/RechargeHistory?All=false',
            drawReceipt: Ext.GlobalConfig.ApiDomainUrl + "api/RechargeHistory/DrawReceipt",
            exportTradeHistory: Ext.GlobalConfig.ApiDomainUrl + "api/RechargeHistory/exportTradeHistory?All=false"
        },
    },
    drawReceipt: function (entity, cb) {
        Ext.Ajax.request({
            method: "PUT",
            url: this.proxy.api.drawReceipt,
            jsonData: entity,
            callback: cb
        });
    },
    exportTradeHistory: function (p, cb) {
        Ext.Ajax.request({
            method: "GET",
            url: this.proxy.api.exportTradeHistory,
            params: p,
            callback: cb
        });
    }
});