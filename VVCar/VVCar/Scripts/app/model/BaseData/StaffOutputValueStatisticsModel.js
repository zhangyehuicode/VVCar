Ext.define('WX.model.BaseData.StaffOutputValueStatisticsModel', {
	extend: 'Ext.data.Model',
	idProperty: 'StaffID',
	fields: [
		{ name: 'StaffID' },
		{ name: 'StaffName' },
		{ name: 'StaffCode' },
		{ name: 'TotalPerformance' },
		{ name: 'TotalCostMoney' },
		{ name: 'TotalProfit' },
		{ name: 'DailyExpense' },
		{ name: 'AverageDailyExpense' },
		{ name: 'TotalRetaainedProfit' },
	]
})