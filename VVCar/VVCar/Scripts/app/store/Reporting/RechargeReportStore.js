/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.Reporting.RechargeReportModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: "PaymentType" },
        { name: "TradeUser" },
        { name: "TradeDepartment" },
        { name: "TradeBillCount" },
        { name: "TotalAmount" },
        { name: "TotalRechargeAmount" },
        { name: "TotalGiveAmount" },
        { name: "TotalInvoiceAmount" },
    ]
});

Ext.define('WX.store.Reporting.RechargeReportStore', {
    extend: 'Ext.data.Store',
    model: 'WX.store.Reporting.RechargeReportModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/RechargeReport?All=false',
            exportRechargeReport: Ext.GlobalConfig.ApiDomainUrl + 'api/Reporting/exportRechargeReport',
        },
    },
    exportRechargeReport: function (queryValues, success, failure) {
        Ext.Ajax.request({
            method: "Get",
            url: this.proxy.api.exportRechargeReport,
            params: queryValues,
            success: success,
            failure: failure
        });
    },
});