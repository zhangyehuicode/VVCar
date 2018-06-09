Ext.define('WX.store.BaseData.StockRecordStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.OrderModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/StockRecord',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/StockRecord?All=false',
        },
    },
});