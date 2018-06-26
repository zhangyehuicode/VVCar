Ext.define('WX.model.BaseData.GameSettingModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: ['ID',
		'GameType',
		'StartTime',
		'EndTime',
		'PeriodDays',
		'PeriodCounts',
		'Limit',
		'IsShare',
		'ShareTitle',
		'IsOrderShow',
		'IsAvailable',
	]
});

