/// <reference path="../../ext/ext-all-dev.js" />

Ext.define('WX.store.BaseData.UserRoleStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.UserRoleModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/UserRole',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/UserRole?All=false',
            batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/UserRole/BatchAdd',
            batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/UserRole/BatchDelete',
        },
    },
    batchAdd: function (data, success, failure) {
        Ext.Ajax.request({
            url: this.proxy.api.batchAdd,
            method: 'POST',
            jsonData: data,
            success: success,
            failure: failure,
        });
    },
    batchDelete: function (data, success, failure) {
        Ext.Ajax.request({
            url: this.proxy.api.batchDelete,
            method: 'DELETE',
            jsonData: data,
            success: success,
            failure: failure,
        });
    },
});