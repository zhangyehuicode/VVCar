/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.model.BaseData.RoleModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'Code', 'Name', 'RoleType', 'CreatedUser', 'CreatedDate',
        { name: 'IsAvailable', type: 'boolean', defaultValue: true }
    ],
});