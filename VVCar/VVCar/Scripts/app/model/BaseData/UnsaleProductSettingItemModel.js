Ext.define('WX.model.BaseData.UnsaleProductSettingItemModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'UnsaleProductSettingID' },
		{ name: 'ProductID' },
		{ name: 'CreatedDate' },
	],
});