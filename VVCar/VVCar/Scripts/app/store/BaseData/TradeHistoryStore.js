Ext.define('WX.store.BaseData.TradeHistoryStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.TradeHistoryModel',
    pageSize: 25,
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePaging: false,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/TradeHistory',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/TradeHistory?All=false',
            exportTradeHistory: Ext.GlobalConfig.ApiDomainUrl + "api/TradeHistory/exportTradeHistory?All=false"
        },
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