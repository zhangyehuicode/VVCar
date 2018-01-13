/// <reference path="../../ext/ext-all-dev.js" />

Ext.define('WX.store.BaseData.UserStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.UserModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/User',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/User?All=false',
            resetPwds: Ext.GlobalConfig.ApiDomainUrl + 'api/User/resetPwds',
            deleteUsers: Ext.GlobalConfig.ApiDomainUrl + 'api/User/deleteUsers',
            changePassword: Ext.GlobalConfig.ApiDomainUrl + 'api/User/ChangePassword'
        },
    },
    resetPwds: function (ids, cb) {
        Ext.Ajax.request({
            ContentType: 'application/json',
            method: "PUT",
            url: this.proxy.api.resetPwds,
            jsonData: { IdList: ids },
            callback: cb
        });
    },
    deleteUsers: function (ids, cb) {
        Ext.Ajax.request({
            ContentType: 'application/json',
            method: "DELETE",
            url: this.proxy.api.deleteUsers,
            jsonData: { IdList: ids },
            callback: cb
        });
    },
    changePassword: function (oldPassword, newPassword, success) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.changePassword + "?oldPassword=" + oldPassword + "&newPassword=" + newPassword,
            clientValidation: true,
            success: success,
        });
    }
});