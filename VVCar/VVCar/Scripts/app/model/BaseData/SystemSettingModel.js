Ext.define('WX.model.BaseData.SystemSettingModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'MerchantCode' },
		{ name: 'MerchantName' },
		{ name: 'Index' },
		{ name: 'Name' },
		{ name: 'TemplateName' },
		{ name: 'Type' },
		{ name: 'Caption' },
		{ name: 'DefaultValue' },
		{ name: 'SettingValue' },
		{ name: 'IsAvailable', type: 'boolean' }
	],
});