/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.Reporting.ConsumeReportModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: "TradeDepartmentName" },
        { name: "TradeBillCount" },
        { name: "TradeAmount" }
    ]
});

Ext.define('WX.store.Reporting.ConsumeReportStore', {
    extend: 'Ext.data.Store',
    model: 'WX.store.Reporting.ConsumeReportModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/ConsumeReport?All=false',
            exportConsumeReport: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/exportConsumeReport',
        },
    },
    exportConsumeReport: function (queryValues, success, failure) {
        Ext.Ajax.request({
            method: "Get",
            url: this.proxy.api.exportConsumeReport,
            params: queryValues,
            success: success,
            failure: failure
        });
    }
});