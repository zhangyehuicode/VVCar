Ext.define('WX.model.BaseData.LogisticsModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'OrderCode' },
		{ name: 'RevisitDate' },
		{ name: 'RevisitTips' },
		{ name: 'RevisitStatus' },
		{ name: 'DeliveryTips' },
		{ name: 'LinkMan' },
		{ name: 'Phone' },
		{ name: 'Address' },
		{ name: 'ExpressNumber' },
		{ name: 'LogisticsCompany' },
		{ name: 'CreatedDate'},
	]
});