Ext.define('WX.view.Merchant.MerchantEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.MerchantEdit',
	title: '编辑商户信息',
	layout: 'fit',
	width: 1100,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		var yesNoDictStore = Ext.create('WX.store.DataDict.YesNoTypeStore');
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 80,
				anchor: '100%',
				flex: 1,
				margin: '5',
			},
			items: [
				{
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'Name',
						fieldLabel: '名称',
						maxLength: 100,
						allowBlank: false,
					}, {
						xtype: 'textfield',
						margin: '5 8 5 10',
						name: 'Email',
						fieldLabel: '注册邮箱',
						maxLength: 25,
						allowBlank: true,
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'LegalPerson',
						fieldLabel: '法人(负责人)',
						maxLength: 20,
						allowBlank: false,

					}, {
						xtype: 'textfield',
						margin: '5 5 5 10',
						name: 'IDNumber',
						fieldLabel: '身份证编号',
						vtype: 'IDNumber',
						maxLength: 18,
						allowBlank: true,

					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'MobilePhoneNo',
						fieldLabel: '手机号码',
						vtype: 'mobilephone',
						maxLength: 11,
						allowBlank: false,

					}, {
						xtype: 'textfield',
						margin: '5 5 5 10',
						name: 'CompanyAddress',
						fieldLabel: '公司地址',
						maxLength: 50,
						allowBlank: false,

					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'WeChatAppID',
						fieldLabel: '公众号AppID',
						maxLength: 50,
						allowBlank: true,
					}, {
						xtype: 'textfield',
						margin: '5 5 5 10',
						name: 'WeChatAppSecret',
						fieldLabel: '公众号Secret',
						maxLength: 50,
						allowBlank: true,
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'WeChatMchID',
						fieldLabel: '微信商户号',
						maxLength: 20,
						allowBlank: true,
					}, {
						xtype: 'textfield',
						margin: '5 8 5 10',
						name: 'WeChatMchKey',
						fieldLabel: '微信商户Key',
						maxLength: 50,
						allowBlank: true,
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'WeChatOAPassword',
						fieldLabel: '微信公众平台登录密码',
						emptyText: '微信公众平台登录密码',
						maxLength: 20,

					}, {
						xtype: 'textfield',
						margin: '5 5 5 10',
						name: 'MeChatMchPassword',
						fieldLabel: '微信商户平台操作密码',
						emptyText: '微信商户平台操作密码',
						maxLength: 20,

					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						margin: '5 10 5 5',
						name: 'Bank',
						fieldLabel: '开户行',
						maxLength: 20,
						allowBlank: true,
					}, {
						xtype: 'textfield',
						margin: '5 5 5 10',
						name: 'BankCard',
						fieldLabel: '账号',
						maxLength: 32,
						allowBlank: true,
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					width: 300,
					items: [{
						xtype: "checkboxgroup",
						fieldLabel: "商户性质",
						id: 'merchantType',
						labelWidth: 70,
						columns: 2,
						items: [
							{ boxLabel: '代理商', name: 'IsAgent', inputValue: 1 },
							{ boxLabel: '普通商户', name: 'IsGeneralMerchant', inputValue: 2 },
						]
					}, {

					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'form',
						border: false,
						layout: 'hbox',
						items: [{
							xtype: 'filefield',
							fieldLabel: '营业执照',
							labelWidth: 60,
							allowBlank: true,
							buttonText: '选择图片',
						}, {
							xtype: 'button',
							text: '上传',
							margin: '5 60 0 5',
							action: 'uploadLicensePic',
						}]
					}, {
						xtype: 'form',
						border: false,
						layout: 'hbox',
						items: [{
							xtype: 'filefield',
							fieldLabel: '身份证(正)',
							labelWidth: 70,
							allowBlank: true,
							buttonText: '选择图片',
						}, {
							xtype: 'button',
							text: '上传',
							margin: '5 60 0 5',
							action: 'uploadIDCardFrontPic',
						}]
					}, {
						xtype: 'form',
						border: false,
						layout: 'hbox',
						items: [{
							xtype: 'filefield',
							fieldLabel: '身份证(反)',
							labelWidth: 70,
							allowBlank: true,
							buttonText: '选择图片',
						}, {
							xtype: 'button',
							text: '上传',
							margin: '5 60 0 5',
							action: 'uploadIDCardBehindPic',
						}]
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'box',
						name: 'ImgLicenseShow',
						width: 200,
						height: 290,
						margin: '5 5 5 100',
						autoEl: {
							tag: 'img',
							src: '',
						},
					}, {
						xtype: 'box',
						name: 'ImgIDCardFrontShow',
						width: 200,
						height: 290,
						margin: '5 5 5 100',
						autoEl: {
							tag: 'img',
							src: '',
						}
					}, {
						xtype: 'box',
						name: 'ImgIDCardBehindShow',
						width: 200,
						height: 290,
						margin: '5 5 5 180',
						autoEl: {
							tag: 'img',
							src: '',
						}
					}]
				}, {
					xtype: 'textfield',
					name: 'BusinessLicenseImgUrl',
					fieldLabel: '营业执照图片路径',
					hidden: true,
				}, {
					xtype: 'textfield',
					name: 'LegalPersonIDCardFrontImgUrl',
					fieldLabel: '法人身份证正面图片路径',
					hidden: true,
				}, {
					xtype: 'textfield',
					name: 'LegalPersonIDCardBehindImgUrl',
					fieldLabel: '法人身份证背面图片路径',
					hidden: true,
				}
			]
		});
		me.items = [me.form];
		me.buttons = [
			{
				text: '保存',
				action: 'save',
				cls: 'submitBtn',
				scope: me
			},
			{
				text: '取消',
				scope: me,
				handler: me.close
			}
		]
		me.callParent(arguments);
	}
});