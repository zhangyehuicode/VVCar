Ext.define('WX.model.BaseData.ServicePeriodModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'ProductID' },
		{ name: 'ProductCode' },
		{ name: 'ProductName' },
		{ name: 'PeriodDays' },
		{ name: 'ExpirationNotice'},
		{ name: 'IsAvailable' },
	],
});