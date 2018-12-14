Ext.define('WX.view.MaterialPublish.MaterialPublishEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.MaterialPublishEdit',
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
					name: 'Name',
					fieldLabel: '标题',
					margin: '5 0 0 5',
					maxLength: 18,
					allowBlank: false
				},
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