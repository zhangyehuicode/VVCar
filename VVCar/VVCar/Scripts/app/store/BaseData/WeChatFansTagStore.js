Ext.define("WX.store.BaseData.WeChatFansTagStore", {
    extend: "Ext.data.Store",
    model: "WX.model.BaseData.WeChatFansTagModel",
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: "rest",
        url: Ext.GlobalConfig.ApiDomainUrl + "api/WeChatFansTag",
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/WeChatFansTag/Search?All=false',
        },
    }
});