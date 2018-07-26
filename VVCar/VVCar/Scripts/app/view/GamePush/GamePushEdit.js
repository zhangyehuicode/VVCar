﻿Ext.define('WX.view.GamePush.GamePushEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.GamePushEdit',
	title: '编辑卡券推送',
	layout: 'fit',
	width: 300,
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
				labelWidth: 60,
				anchor: '100%'
			},
			items: [
				{
					xtype: 'textfield',
					name: 'Title',
					fieldLabel: '标题',
					margin: '5 0 0 5',
					maxLength: 18,
					allowBlank: false
				}, {
					xtype: 'combobox',
					name: 'PushAllMembers',
					store: yesNoDictStore,
					displayField: 'DictName',
					valueField: 'DictValue',
					fieldLabel: '是否推送所有会员',
					margin: '5 0 0 5',
					labelWidth: 60,
					editable: false,
					allowBlank: false,
				}, {
					xtype: 'datefield',
					name: 'PushDate',
					fieldLabel: '推送时间',
					margin: '5 0 0 5',
					allowBlank: true,
					minValue: new Date(),
					format: 'Y-m-d',
					editable: false,
					allowBlank: false,
					value: new Date()
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
		];
		me.callParent(arguments);
	}
});