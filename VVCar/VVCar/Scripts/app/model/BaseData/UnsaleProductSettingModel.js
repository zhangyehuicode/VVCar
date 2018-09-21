Ext.define('WX.model.BaseData.UnsaleProductSettingModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Code' },
		{ name: 'Name' },
		{ name: 'PeriodDays' },
		{ name: 'Quantities' },
		{ name: 'Performence' },
		{ name: 'IsAvailable', type: 'boolean' },
		{ name: 'CreatedDate' },
	],
});