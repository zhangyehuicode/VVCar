/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.BusinessTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "普通交易" },
        { "DictValue": 1, "DictName": "余额调整" }
    ]
});
