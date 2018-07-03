﻿Ext.define('WX.model.BaseData.ManageUserModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Code' },
		{ name: 'Name' },
		{ name: 'DepartmentID' },
		{ name: 'Sex' },
		{ name: 'Birthday' },
		{ name: 'IDNumber' },
		{ name: 'PhoneNo' },
		{ name: 'MobilePhoneNo' },
		{ name: 'EmailAddress' },
		{ name: 'ContactPerson' },
		{ name: 'ContactPhone' },
		{ name: 'Remark' },
		{ name: 'Password' },
		{ name: 'WeChatOpenID' },
		{ name: 'AuthorityCard' },
		{ name: 'CreatedUser' },
		{ name: 'CreatedDate' },
		{ name: 'IsAvailable', type: 'boolean' },
		{ name: 'CanLoginPos', type: 'boolean' },
		{ name: 'CanLoginAdminPortal', type: 'boolean' },
		{ name: 'DutyTime' },
		{ name: 'Age' },
		{ name: "BasicSalary" },
	],
});