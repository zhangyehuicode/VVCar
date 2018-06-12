Ext.define('WX.model.BaseData.StaffPerformanceStatisticsModel', {
	extend: 'Ext.data.Model',
	idProperty: 'StaffID',
	fields: [
		{ name: 'StaffID' },
		{ name: 'StaffName' },
		{ name: 'StaffCode' },
		{ name: 'TotalPerformance' },
		{ name: 'TotalCommission' },
		{ name: 'MonthPerformance' },
		{ name: 'MonthCommission' },
		{ name: 'BasicSalary' },
		{ name: 'Subsidy' },
		{ name: 'CustomerServiceCount' },
		{ name: 'MonthCustomerServiceCount' },
		{ name: 'CurrentPerformance' },
		{ name: 'CurrentCommission' },
		{ name: 'CurrentCustomerServiceCount' },
		{ name: 'MonthIncome' },
	]
})