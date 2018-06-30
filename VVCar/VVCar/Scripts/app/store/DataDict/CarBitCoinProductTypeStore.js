Ext.define('WX.store.DataDict.CarBitCoinProductTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "引擎" },
        { "DictValue": 1, "DictName": "商品" },
    ]
});
