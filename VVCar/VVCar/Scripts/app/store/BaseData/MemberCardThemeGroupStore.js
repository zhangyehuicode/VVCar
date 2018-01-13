Ext.define('WX.store.BaseData.MemberCardThemeGroupStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.MemberCardThemeGroupModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeGroup',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeGroup?All=false',
            newAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeGroup/newAdd/',
            newUpp: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeGroup/newUpp/',
            setIndex: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeGroup/setIndex',
            UpIndex: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeGroup/UpIndex',
            SearchTime: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeGroup/SearchTime/',
            SearchImg: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeGroup/SearchImg/',
            setAvailable: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeGroup/setAvailable/',
            UpAvailable: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeGroup/UpAvailable/',
            DeleteGiftCardTem: Ext.GlobalConfig.ApiDomainUrl + 'api/CardThemeGroup/DeleteGiftCardTem/',
        }
    },
    newAdd: function (data, success, failure) {
        Ext.Ajax.request({
            method: 'POST',
            url: this.proxy.api.newAdd,
            jsonData: data,
            success: success,
            failure: failure
        });
    },
    newUpp: function (data, success, failure) {
        Ext.Ajax.request({
            method: 'PUT',
            url: this.proxy.api.newUpp,
            jsonData: data,
            success: success,
            failure: failure
        });
    },
    SearchTime: function (data, success, failure) {
        Ext.Ajax.request({
            method: 'PUT',
            url: this.proxy.api.SearchTime,
            jsonData: data,
            success: success,
            failure: failure
        });
    },
    SearchImg: function (data, successImg, failure) {
        Ext.Ajax.request({
            method: 'PUT',
            url: this.proxy.api.SearchImg,
            jsonData: data,
            success: successImg,
            failure: failure
        });
    },
    setIndex: function (Data, success, failure) {
        Ext.Ajax.request({
            method: 'PUT',
            url: this.proxy.api.setIndex,
            jsonData: Data,
            success: success,
            failure: failure,
        });
    },
    UpIndex: function (Data, success, failure) {
        Ext.Ajax.request({
            method: 'PUT',
            url: this.proxy.api.UpIndex,
            jsonData: Data,
            success: success,
            failure: failure,
        });
    },
    setAvailable: function (Data, success, failure) {
        Ext.Ajax.request({
            method: 'PUT',
            url: this.proxy.api.setAvailable,
            jsonData: Data,
            success: success,
            failure: failure,
        });
    },
    UpAvailable: function (Data, success, failure) {
        Ext.Ajax.request({
            method: 'PUT',
            url: this.proxy.api.UpAvailable,
            jsonData: Data,
            success: success,
            failure: failure,
        });
    },
    DeleteGiftCardTem: function (Data, success, failure) {
        Ext.Ajax.request({
            method: 'PUT',
            url: this.proxy.api.DeleteGiftCardTem,
            jsonData: Data,
            success: success,
            failure: failure,
        });
    },

});