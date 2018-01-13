Ext.define('WX.store.BaseData.MemberCardThemeStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.MemberCardThemeModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCardTheme',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCardTheme?All=false',
            setIndex: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCardTheme/setIndex',
            setAvailable: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCardTheme/SetAvailable',
        }
    },
    setIndex: function (params, success, failure) {
        Ext.Ajax.request({
            method: 'POST',
            url: this.proxy.api.setIndex,
            jsonData: params,
            success: success,
            failure: failure,
        });
    },
    setAvailable: function (params, success, failure) {
        Ext.Ajax.request({
            method: 'POST',
            url: this.proxy.api.setAvailable,
            jsonData: params,
            success: success,
            failure: failure,
        });
    },
});