/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.ConsumeReportTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "门店储值消费报表" },
    ]
});
