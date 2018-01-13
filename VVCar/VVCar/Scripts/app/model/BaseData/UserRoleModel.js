/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.model.BaseData.UserRoleModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'UserID', 'RoleID', 'CreatedUser', 'CreatedDate',
        { name: 'UserCode', mapping: 'User.Code' },
        { name: 'UserName', mapping: 'User.Name' },
    ],
});