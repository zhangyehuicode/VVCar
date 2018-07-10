Ext.define('WX.model.BaseData.ReimbursementModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'UserName' },
		{ name: 'Project' },
		{ name: 'ImgUrl' },
		{ name: 'Money' },
		{ name: 'Status' },
		{ name: 'Remark' },
		{ name: 'CreatedDate' },
	]
});