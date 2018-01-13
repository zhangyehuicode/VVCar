/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.model.BaseData.RolePermissionModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'RoleCode', 'PermissionCode'],
});