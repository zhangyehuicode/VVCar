Ext.define('WX.model.BaseData.AdvisementBrowseHistoryModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Title' },
		{ name: 'NickName' },
		{ name: 'ShareNickName' },
		{ name: 'StartDate' },
		{ name: 'EndDate' },
		{ name: 'Period' },
		{ name: 'CreatedDate' },
	]
});