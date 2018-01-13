/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.model.BaseData.PermissionFuncModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'Code', 'Name', 'PermissionType', 'IsManual', 'IsAvailable'],
});