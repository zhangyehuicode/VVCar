/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.model.BaseData.MakeCodeRuleModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: [
        { name: 'ID' },
        { name: 'Code' },
        { name: 'Name' },
        { name: 'IsAvailable', type: 'boolean' },
        { name: 'IsManualMake', type: 'boolean' },
        { name: 'Length' },
        { name: 'Prefix1Rule' },
        { name: 'Prefix1' },
        { name: 'Prefix1Length' },
        { name: 'Prefix2Rule' },
        { name: 'Prefix2' },
        { name: 'Prefix2Length' },
        { name: 'Prefix3Rule' },
        { name: 'Prefix3' },
        { name: 'Prefix3Length' }
    ]
});