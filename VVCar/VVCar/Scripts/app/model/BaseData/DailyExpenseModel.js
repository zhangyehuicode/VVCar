Ext.define('WX.model.BaseData.DailyExpenseModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'ExpenseDate' },
		{ name: 'Money' },
		{ name: 'StaffCount' },
		{ name: 'Remark' },
		{ name: 'CreatedUserID' },
		{ name: 'CreatedUser' },
		{ name: 'CreatedDate' },
		{ name: 'LastUpdateUserID' },
		{ name: 'LastUpdateUser' },
		{ name: 'LastUpdateDate' },
	]
})