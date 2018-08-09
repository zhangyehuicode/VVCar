﻿Ext.define('WX.model.BaseData.MerchantModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Code' },
		{ name: 'Name' },
		{ name: 'IsHQ' },
		{ name: 'IsAgent' },
		{ name: 'IsGeneralMerchant' },
		{ name: 'LegalPerson' },
		{ name: 'IDNumber' },
		{ name: 'Email' },
		{ name: 'WeChatOAPassword' },
		{ name: 'MobilePhoneNo' },
		{ name: 'WeChatQRCodeImgUrl' },
		{ name: 'BusinessLicenseImgUrl' },
		{ name: 'LegalPersonIDCardFrontImgUrl' },
		{ name: 'LegalPersonIDCardBehindImgUrl' },
		{ name: 'CompanyAddress' },
		{ name: 'WeChatAppID' },
		{ name: 'WeChatAppSecret' },
		{ name: 'WeChatMchID' },
		{ name: 'WeChatMchKey' },
		{ name: "MeChatMchPassword" },
		{ name: 'Bank' },
		{ name: 'BankCard' },
		{ name: 'DataSource' },
		{ name: 'CreatedDate' },
	]
});