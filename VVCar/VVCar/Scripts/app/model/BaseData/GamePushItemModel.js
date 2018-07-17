Ext.define('WX.model.BaseData.GamePushItemModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'GameType' },
		{ name: 'StartTime' },
		{ name: 'EndTime' },
		{ name: 'PeriodDays' },
		{ name: 'PeriodCounts' },
		{ name: 'Limit' },
	],
});