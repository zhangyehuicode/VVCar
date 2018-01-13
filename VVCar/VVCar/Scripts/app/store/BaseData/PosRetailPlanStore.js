Ext.define('WX.store.BaseData.PosRetailPlanStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.IDCodeNameModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/YunPos',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/YunPos/VIPRetailPlanList',
        },
    }
});