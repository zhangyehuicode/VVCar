Ext.define("WX.store.BaseData.MemberGroupTreeStore", {
    extend: "Ext.data.Store",
    model: "WX.model.BaseData.IDCodeNameModel",
    autoLoad: false,
    pageSize: 100,
    proxy: {
        type: "rest",
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberGroup/TreeData',
        },
    }
});