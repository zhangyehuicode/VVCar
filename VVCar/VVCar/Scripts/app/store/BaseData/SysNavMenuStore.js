
Ext.define('WX.store.BaseData.SysNavMenuStore', {
    extend: 'Ext.data.TreeStore',
    model: 'WX.model.BaseData.SysNavMenuModel',
    nodeParam: 'ParentID',
    defaultRootId: '00000000-0000-0000-0000-000000000000',
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/SysMenu',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/SysMenu/NavMenu',
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
