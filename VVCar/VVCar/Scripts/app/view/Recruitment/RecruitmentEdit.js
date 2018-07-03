Ext.define('WX.view.Recruitment.RecruitmentEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.RecruitmentEdit',
	title: '编辑人才需求信息',
	layout: 'fit',
	width: 1100,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
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
						name: 'Recruiter',
						fieldLabel: '招聘单位',
						maxLength: 100,
						allowBlank: false,
					}, {
							xtype: 'textfield',
							margin: '5 10 5 5',
							name: 'Recruiter',
							fieldLabel: '招聘单位',
							maxLength: 100,
							allowBlank: false,
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
						allowBlank: true,

					}, {
						xtype: 'textfield',
						margin: '5 5 5 10',
						name: 'CompanyAddress',
						fieldLabel: '公司地址',
						maxLength: 50,
						allowBlank: true,

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
						xtype: 'form',
						layout: 'hbox',
						items: [{
							xtype: 'textfield',
							margin: '5 5 5 10',
							name: 'WeChatMchKey',
							fieldLabel: '微信商户Key',
							maxLength: 50,
							allowBlank: true,
						}, {
							xtype: 'textfield',
							margin: '5 5 5 10',
							name: 'MeChatMchPassword',
							fieldLabel: '商户号密码',
							maxLength: 20,
						}]
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