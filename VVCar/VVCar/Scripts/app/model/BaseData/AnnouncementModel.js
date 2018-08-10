Ext.define('WX.model.BaseData.AnnouncementModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Title' },
		{ name: 'Name' },
		{ name: 'Process'},
		{ name: 'Content' },
		{ name: 'PushDate' },
		{ name: "PushAllMembers" },
		{ name: 'Status' },
		{ name: 'Remark'},
		{ name: 'CreatedDate' },
	]
})