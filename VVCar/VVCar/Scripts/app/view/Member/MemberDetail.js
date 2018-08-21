Ext.define('WX.view.Member.MemberDetail', {
	extend: 'Ext.window.Window',
	alias: 'widget.MemberDetail',
	title: '会员明细',
	width: 550,
	height: 280,
	bodyPadding: 5,
	modal: true,
	autoScroll: true,
	bodyStyle: 'overflow-y: auto; overflow-x: hidden;',
	initComponent: function () {
		var me = this;
		var yesNoDictStore = Ext.create('WX.store.DataDict.YesNoTypeStore');
		var membergroupStore = Ext.create('WX.store.BaseData.MemberGroupStore');
		me.form = Ext.create('Ext.form.Panel', {
			width: 500,
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 60,
				anchor: '100%',
				flex: 1,
				margin: '5',
			},
			items: [{
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					margin: '5 5 5 5',
					name: 'CardNumber',
					fieldLabel: '会员卡号',
					maxLength: 100,
					readOnly: true,
				}, {
					xtype: 'numberfield',
					margin: '5 5 5 5',
					name: 'CardBalance',
					fieldLabel: '余额/元',
					maxLength: 25,
					readOnly: true,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					margin: '5 5 5 5',
					name: 'MemberGroup',
					fieldLabel: '会员分组',
					maxLength: 25,
					readOnly: true,

				}, {
					xtype: 'datefield',
					margin: '5 5 5 5',
					name: 'CreatedDate',
					fieldLabel: '注册时间',
					format: 'Y-m-d',
					readOnly: true,

				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					margin: '5 5 5 5',
					name: 'Name',
					fieldLabel: '姓名',
					maxLength: 25,
					readOnly: true,
				}, {
					xtype: "combobox",
					margin: '5 5 5 5',
					name: "Sex",
					displayField: "DictName",
					valueField: "DictValue",
					store: Ext.create("WX.store.DataDict.SexStore"),
					fieldLabel: "性别",
					readOnly: true
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: "textfield",
					margin: '5 5 5 5',
					name: "MobilePhoneNo",
					fieldLabel: "手机号码",
					allowBlank: false,
					vtype: 'mobilephone',
					readOnly: true,
				}, {
					xtype: 'numberfield',
					margin: '5 5 5 5',
					name: 'Point',
					fieldLabel: '剩余积分',
					maxLength: 20,
					readOnly: true,
				}]
			}]
		});
		me.items = [me.form];
		me.buttons = [
			{
				text: '关闭',
				scope: me,
				handler: me.close
			}
		]
		me.callParent(arguments);
	}
});