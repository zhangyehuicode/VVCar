Ext.define('WX.model.BaseData.AdvisementSettingModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Title' },
		{ name: 'SubTitle' },
		{ name: 'ImgUrl' },
		{ name: 'Content' },
		{ name: 'CreatedDate' },
	]
});