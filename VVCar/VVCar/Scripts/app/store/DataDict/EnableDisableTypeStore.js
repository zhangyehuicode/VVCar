/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.EnableDisableTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": true, "DictName": "启用" },
        { "DictValue": false, "DictName": "禁用" }
    ]
});
