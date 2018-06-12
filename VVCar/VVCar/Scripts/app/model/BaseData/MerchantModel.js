Ext.define('WX.model.BaseData.MerchantModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'Code' },
		{ name: 'Name' },
		{ name: 'IsHQ' },
		{ name: 'LegalPerson' },
		{ name: 'IDNumber' },
		{ name: 'Email' },
		{ name: 'WeChatOAPassword' },
		{ name: 'MobilePhoneNo' },
		{ name: 'BusinessLicenseImgUrl' },
		{ name: 'LegalPersonIDCardFrontImgUrl' },
		{ name: 'LegalPersonIDCardBehindImgUrl' },
		{ name: 'CompanyAddress' },
		{ name: 'WeChatAppID' },
		{ name: 'WeChatAppSercret' },
		{ name: 'WeChatMchKey' },
		{ name: 'Bank' },
		{ name: 'BankCard' },
	]
});