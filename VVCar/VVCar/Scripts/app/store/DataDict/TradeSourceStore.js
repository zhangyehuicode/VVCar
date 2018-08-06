/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.TradeSourceStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "微信" },
        { "DictValue": 1, "DictName": "支付宝" },
        { "DictValue": 2, "DictName": "管理后台" },
        { "DictValue": 3, "DictName": "官网自充" },
    ]
});
