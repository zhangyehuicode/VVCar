Ext.define('WX.model.BaseData.PosProductModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'Code', 'Name', 'ProductCategoryID', 'Unit', 'PriceSale'],
});

Ext.define('WX.store.BaseData.PosProductStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.PosProductModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/YunPos',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/YunPos/SearchMemberProduct?All=false',
        },
    }
});