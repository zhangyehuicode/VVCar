/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.model.BaseData.UserModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'Code', 'Name', 'DepartmentID', 'Sex', 'Birthday', 'IDNumber', 'PhoneNo', 'MobilePhoneNo',
        'EmailAddress', 'ContactPerson', 'ContactPhone', 'Remark', 'Password',
        'WeChatOpenID', 'AuthorityCard', 'CreatedUser', 'CreatedDate',
        { name: 'IsAvailable', type: 'boolean' },
        { name: 'CanLoginPos', type: 'boolean' },
        { name: 'CanLoginAdminPortal', type: 'boolean' },
        { name: 'DutyTime' },
        { name: 'Age' },
        { name: "BasicSalary" },
    ],
});