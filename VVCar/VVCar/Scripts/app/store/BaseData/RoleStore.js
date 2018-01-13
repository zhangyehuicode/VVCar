Ext.define('WX.store.BaseData.RoleStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.RoleModel',
    pageSize: 25,
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePaging: false,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/Role',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/Role?All=false',
        },
    }
});