Ext.define('WX.store.BaseData.RechargeHistoryReportStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.RechargeHistoryModel',
    pageSize: 25,
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePaging: false,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/RechargeHistory/RechargeHistorySearch',
    }
});