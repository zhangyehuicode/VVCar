/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.RechargePlanTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 1, "DictName": "活动" },
        { "DictValue": 2, "DictName": "满送" }
    ]
});
