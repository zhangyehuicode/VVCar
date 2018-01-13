/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.PaymentTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "现金" },
        { "DictValue": 1, "DictName": "支付宝" },
        { "DictValue": 2, "DictName": "微信" },
        { "DictValue": 3, "DictName": "银行卡" },
        { "DictValue": 4, "DictName": "初始余额储值" },
        { "DictValue": 5, "DictName": "百度糯米" },
        { "DictValue": 6, "DictName": "营销活动" },
        { "DictValue": 7, "DictName": "余额调整" },
        { "DictValue": 8, "DictName": "买赠" },
    ]
});
