/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.AdjustTypeDicStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 1, "DictName": "增加余额" },
        { "DictValue": 2, "DictName": "减少余额" }
    ]
});
