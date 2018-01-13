Ext.define('WX.store.BaseData.MemberGroupStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.MemberGroupModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberGroup',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberGroup/Search?All=false',
        },
    }
});