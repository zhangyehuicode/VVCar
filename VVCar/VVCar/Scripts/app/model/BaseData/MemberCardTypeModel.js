/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.model.BaseData.MemberCardTypeModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'Name',
        { name: 'AllowStoreActivate', type: 'boolean', defaultValue: false },
        { name: 'AllowDiscount', type: 'boolean', defaultValue: false },
        { name: 'AllowRecharge', type: 'boolean', defaultValue: false },
        { name: 'MaxRecharge' }
    ],
});