Ext.define('WX.store.BaseData.SysNavManageMenuStore', {
    extend: 'Ext.data.TreeStore',
    model: 'WX.model.BaseData.SysNavMenuModel',
    autoLoad: true,
    root: {
        Name: "菜单目录",
        ID: "00000000-0000-0000-0000-000000000000"
    },
    nodeParam: 'ParentID',
    defaultRootId: '00000000-0000-0000-0000-000000000000',
    defaultRootText: '菜单目录',
    defaultRootProperty: 'Children',
    rootProperty: 'Children',
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/SysMenu',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/SysMenu/NavManageMenu',
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