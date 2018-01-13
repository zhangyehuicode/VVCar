/// <reference path="../../ext/ext-all-dev.js" />

Ext.define('WX.store.BaseData.MakeCodeRuleStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.MakeCodeRuleModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/MakeCodeRule',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/MakeCodeRule/AllData',
        },
    },
    generateCode: function (codeType, successHandler) {
        Ext.Ajax.request({
            url: this.proxy.url + "/GenerateCode/" + codeType,
            method: 'GET',
            clientValidation: true,
            success: successHandler,
        });
    },
    getCode: function (codeType, successHandler) {
        Ext.Ajax.request({
            url: this.proxy.url + "/GetCode/" + codeType,
            method: 'GET',
            clientValidation: true,
            success: successHandler,
        });
    },
});