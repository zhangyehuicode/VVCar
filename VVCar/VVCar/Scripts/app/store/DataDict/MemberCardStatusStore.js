/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.MemberCardStatusStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "未激活" },
        { "DictValue": 10, "DictName": "已激活" },
        { "DictValue": -1, "DictName": "挂失" },
    ]
});
