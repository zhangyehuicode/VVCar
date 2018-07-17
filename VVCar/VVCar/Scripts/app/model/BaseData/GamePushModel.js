Ext.define('WX.model.BaseData.GamePushModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Title' },
		{ name: 'PushDate' },
		{ name: 'Status' },
		{ name: 'PushAllMembers' },
		{ name: 'CreatedDate' },
	],
});