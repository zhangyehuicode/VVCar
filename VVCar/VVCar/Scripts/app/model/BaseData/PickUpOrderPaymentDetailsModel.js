Ext.define('WX.model.BaseData.PickUpOrderPaymentDetailsModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'PickUpOrderID' },
		{ name: 'PayType' },
		{ name: 'PayMoney' },
		{ name: 'VoucherAmount' },
		{ name: 'PayInfo' },
		{ name: 'MemberInfo' },
		{ name: 'StaffName' },
		{ name: 'CreatedDate' },
	]
})