Ext.define("WX.store.BaseData.WeChatFansStore", {
    extend: "Ext.data.Store",
    model: "WX.model.BaseData.WeChatFansModel",
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: "rest",
        url: Ext.GlobalConfig.ApiDomainUrl + "api/WeChatFans",
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/WeChatFans/Search?All=false',
            setFansTag: Ext.GlobalConfig.ApiDomainUrl + 'api/WeChatFans/SetFansTag',
            exportWeChatFans: Ext.GlobalConfig.ApiDomainUrl + 'api/WeChatFans/exportWeChatFans',
        },
    },
    setFansTag: function (paramData, successHandler) {
        Ext.Ajax.request({
            url: this.proxy.api.setFansTag,
            method: 'POST',
            ContentType: 'application/json',
            jsonData: paramData,
            clientValidation: true,
            success: successHandler,
        });
    },
    exportWeChatFans: function (filter, success, failure) {
        Ext.Ajax.request({
            method: "POST",
            timeout: 3600000,
            url: this.proxy.api.exportWeChatFans,
            jsonData: filter,
            success: success,
            failure: failure
        });
    },
});