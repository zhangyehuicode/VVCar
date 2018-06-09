Ext.define('WX.store.BaseData.OrderItemStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.OrderItemModel',
    autoLoad: false,
    pageSize: 5,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/OrderItem',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/OrderItem?All=false',
        },
    },
});