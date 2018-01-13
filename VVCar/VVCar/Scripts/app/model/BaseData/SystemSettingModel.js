/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.model.BaseData.SystemSettingModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'Index', 'Name', 'Type', 'Caption', 'DefaultValue', 'SettingValue',
        { name: 'IsAvailable', type: 'boolean' }
    ],
});