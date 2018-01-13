/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.YesNoTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": true, "DictName": "是" },
        { "DictValue": false, "DictName": "否" }
    ]
});
