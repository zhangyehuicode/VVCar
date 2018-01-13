Ext.define('WX.store.BaseData.CardThemeCategoryMenuStore', {
    extend: 'Ext.data.TreeStore',
    model: 'WX.model.BaseData.CardThemeCategoryMenuModel',
    nodeParam: 'ParentID',
    defaultRootId: '00000000-0000-0000-0000-000000000000',
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeCategory',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeCategory/GetCardThemeCategoryMenu',
        },
        reader: {
            type: 'json',
            rootProperty: 'Children',
            successProperty: 'IsSuccessful',
            messageProperty: 'ErrorMessage',
        },
    },
    root: {
        expanded: true,
    }
});
