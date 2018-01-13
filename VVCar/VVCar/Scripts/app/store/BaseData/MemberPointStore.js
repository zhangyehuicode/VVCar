Ext.define('WX.store.BaseData.MemberPointStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.MemberPointModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberPoint',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberPoint/GetMemberPoints',
            save: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberPoint/SaveRange',
        },
    },
    getMemberPoints: function (success, failure) {
        Ext.Ajax.request({
            method: 'GET',
            url: this.proxy.api.read,
            success: success,
            failure: failure,
        });
    },
    saveMemberPoints: function (data, success, failure) {
        Ext.Ajax.request({
            method: 'POST',
            url: this.proxy.api.save,
            jsonData: data,
            success: success,
            failure: failure,
        });
    }
});