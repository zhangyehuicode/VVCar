/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.SexStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 1, "DictName": "男" },
        { "DictValue": 2, "DictName": "女" }
    ]
});
