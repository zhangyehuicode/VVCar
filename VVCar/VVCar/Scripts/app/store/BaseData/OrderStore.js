Ext.define('WX.store.BaseData.OrderStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.OrderModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/Order',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/Order?All=false',
        },
    },
    adjustIndex: function (params, success, failure) {
        Ext.Ajax.request({
            method: 'GET',
            url: '',
            params: params,
            success: success,
            failure: failure,
        });
    },
});