Ext.define('WX.model.BaseData.DepartmentPerformanceStatisticsModel', {
	extend: 'Ext.data.Model',
	idProperty: 'StaffID',
	fields: [
		{ name: 'StaffID' },
		{ name: 'StaffName' },
		{ name: 'StaffCode' },
		{ name: 'TotalDepartmentNumber' },
		{ name: 'CurrentDepartmentNumber' },
		{ name: 'MonthDepartmentNumber' },
	]
})