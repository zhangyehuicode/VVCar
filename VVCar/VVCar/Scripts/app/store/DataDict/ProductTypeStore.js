Ext.define('WX.store.DataDict.ProductTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "服务" },
        { "DictValue": 1, "DictName": "商品" },
    ]
});
