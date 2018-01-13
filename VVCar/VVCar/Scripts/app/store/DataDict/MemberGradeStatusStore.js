Ext.define('WX.store.DataDict.MemberGradeStatusStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "禁用" },
        { "DictValue": 1, "DictName": "启用" }
    ]
});
