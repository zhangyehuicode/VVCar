Ext.define('WX.store.BaseData.CardThemeCategoryStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.CardThemeCategoryModel',
    autoLoad: true,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeCategory',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeCategory?All=false',
        },
    },
});