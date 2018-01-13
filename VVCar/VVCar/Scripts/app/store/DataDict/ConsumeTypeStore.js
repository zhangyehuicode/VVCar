/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.ConsumeTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "扣减余额" },
        { "DictValue": 1, "DictName": "打折" }
    ]
});
