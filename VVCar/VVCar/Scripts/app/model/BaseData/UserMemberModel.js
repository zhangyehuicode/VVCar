Ext.define('WX.model.BaseData.UserMemberModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'MemberGroup' },
		{ name: 'MemberName' },
		{ name: 'Sex' },
		{ name: 'CreatedDate' },
	],
});