Ext.define('WX.model.BaseData.AdvisementSettingModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Title' },
		{ name: 'ImgUrl' },
		{ name: 'Content' },
		{ name: 'CreatedDate' },
	]
});