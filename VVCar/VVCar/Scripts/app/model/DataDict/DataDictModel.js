Ext.define('WX.model.DataDict.DataDictModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'DictType', 'DictValue', 'DictName', 'Index', 'Remark',
        { name: 'IsAvailable', type: 'boolean', defaultValue: true }],
});