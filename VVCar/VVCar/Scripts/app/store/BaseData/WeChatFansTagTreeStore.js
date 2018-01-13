Ext.define("WX.store.BaseData.WeChatFansTagTreeStore", {
    extend: "Ext.data.Store",
    model: "WX.model.BaseData.IDCodeNameModel",
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: "rest",
        url: Ext.GlobalConfig.ApiDomainUrl + "api/WeChatFansTag",
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/WeChatFansTag/TreeData',
        },
    }
});