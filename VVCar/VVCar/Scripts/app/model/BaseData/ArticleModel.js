Ext.define('WX.model.BaseData.ArticleModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Name' },
		{ name: 'IsPushAllMembers' },
		{ name: 'PushDate' },
		{ name: 'Status' },
		{ name: 'CreatedUser' },
		{ name: 'CreatedDate' },
	],
});