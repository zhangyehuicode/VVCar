﻿Ext.define('WX.model.BaseData.PickUpOrderModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'MemberName' },
		{ name: 'MemberMobilePhoneNo' },
		{ name: 'Code' },
		{ name: 'PlateNumber' },
		{ name: 'CardNumber' },
		{ name: 'CardStatus'},
		{ name: 'EffectiveDate' },
		{ name: 'CardBalance'},
		{ name: 'StaffName' },
		{ name: 'Money' },
		{ name: 'ReceivedMoney' },
		{ name: 'StillOwedMoney' },
		{ name: 'Status' },
		{ name: 'CreatedDate' },
	]
})