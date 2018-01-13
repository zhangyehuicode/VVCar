Ext.define('WX.store.DataDict.CouponTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "代金券" },
        { "DictValue": 1, "DictName": "抵用券" },
        { "DictValue": 2, "DictName": "兑换券" },
        { "DictValue": 3, "DictName": "折扣券" },
    ]
});