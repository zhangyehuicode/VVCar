Ext.define('WX.view.Merchant.MerchantEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.MerchantEdit',
	title: '编辑商户信息',
	layout: 'fit',
	width: 950,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 110,
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
						name: 'Name',
						fieldLabel: '名称',
						maxLength: 100,
						allowBlank: false,
					}, {
						xtype: 'textfield',
						name: 'Email',
						fieldLabel: '注册邮箱',
						maxLength: 20,
						allowBlank: false,

					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						name: 'LegalPerson',
						fieldLabel: '法人(负责人)',
						maxLength: 20,
						allowBlank: false,

					}, {
						xtype: 'textfield',
						name: 'IDNumber',
						fieldLabel: '法人身份证编号',
						vtype: 'IDNumber',
						maxLength: 18,
						allowBlank: false,

					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						name: 'MobilePhoneNo',
						fieldLabel: '手机号码',
						vtype: 'mobilephone',
						maxLength: 11,
						allowBlank: false,

					}, {
						xtype: 'textfield',
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
						name: 'WeChatAppID',
						fieldLabel: '公众号AppID',
						maxLength: 50,
						allowBlank: false,
					}, {
						xtype: 'textfield',
						name: 'WeChatAppSecret',
						fieldLabel: '公众号AppSecret',
						maxLength: 50,
						allowBlank: false,
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						name: 'WeChatMchID',
						fieldLabel: '微信商户号',
						maxLength: 20,
						allowBlank: false,
					}, {
						xtype: 'textfield',
						name: 'WeChatMchKey',
						fieldLabel: '微信商户Key',
						maxLength: 50,
						allowBlank: false,
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'textfield',
						name: 'Bank',
						fieldLabel: '开户行',
						maxLength: 20,
						allowBlank: false,
					}, {
						xtype: 'textfield',
						name: 'BankCard',
						fieldLabel: '账号',
						maxLength: 32,
						allowBlank: false,
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'form',
						border: false,
						layout: 'hbox',
						margin: '0 0 10 0',
						items: [{
							xtype: 'filefield',
							fieldLabel: '营业执照',
							labelWidth: 60,
							allowBlank: true,
							buttonText: '选择图片',
						}, {
							xtype: 'button',
							text: '上传',
							margin: '5 0 0 0',
							action: 'uploadLicensePic',
						}]
					}, {
						xtype: 'form',
						border: false,
						layout: 'hbox',
						margin: '0 0 10 0',
						items: [{
							xtype: 'filefield',
							fieldLabel: '身份证(正)',
							labelWidth: 70,
							allowBlank: true,
							buttonText: '选择图片',
						}, {
							xtype: 'button',
							text: '上传',
							margin: '5 0 0 0',
							action: 'uploadIDCardFrontPic',
						}]
					}, {
						xtype: 'form',
						border: false,
						layout: 'hbox',
						margin: '0 0 10 0',
						items: [{
							xtype: 'filefield',
							fieldLabel: '身份证(反)',
							labelWidth: 70,
							allowBlank: true,
							buttonText: '选择图片',
						}, {
							xtype: 'button',
							text: '上传',
							margin: '5 0 0 0',
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
						margin: '70 5 5 70',
						autoEl: {
							tag: 'img',
							src: '',
						},
					}, {
						xtype: 'box',
						name: 'ImgIDCardFrontShow',
						width: 200,
						height: 290,
						margin: '70 5 5 70',
						autoEl: {
							tag: 'img',
							src: '',
						}
					}, {
						xtype: 'box',
						name: 'ImgIDCardBehindShow',
						width: 200,
						height: 290,
						margin: '70 5 5 70',
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