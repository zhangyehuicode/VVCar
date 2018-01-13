/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.model.BaseData.DepartmentModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'Code', 'Name', 'ContactPerson', 'ContactPhoneNo', 'MobilePhoneNo', 'EmailAddress', 'Address',
        'DistrictRegion', 'AdministrationRegion', 'Remark', 'CreatedUser', 'CreatedDate', 'LastUpdateUser', 'LastUpdateDate',
        { name: 'IsDeleted', type: 'boolean', defaultValue: false }
    ],
});