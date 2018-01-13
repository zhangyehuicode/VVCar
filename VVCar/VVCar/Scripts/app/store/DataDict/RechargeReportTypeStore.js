Ext.define('WX.store.DataDict.RechargeReportTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "门店储值业绩报表" },
        { "DictValue": 1, "DictName": "业务员储值业绩报表" },
        { "DictValue": 2, "DictName": "支付方式报表" }
    ]
});