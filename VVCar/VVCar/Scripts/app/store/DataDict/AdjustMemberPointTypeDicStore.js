Ext.define('WX.store.DataDict.AdjustMemberPointTypeDicStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 1, "DictName": "增加积分" },
        { "DictValue": 2, "DictName": "减少积分" }
    ]
});
