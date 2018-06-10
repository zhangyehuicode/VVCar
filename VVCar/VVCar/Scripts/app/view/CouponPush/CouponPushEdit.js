Ext.define('WX.view.CouponPush.CouponPushEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.CouponPushEdit',
	title: '编辑卡券推送',
	layout: 'fit',
	width: 300,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
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
					maxLength: 18,
					allowBlank: false
				}, {
					xtype: 'datefield',
					name: 'PushDate',
					fieldLabel: '推送时间',
					allowBlank: true,
					minValue: new Date(),
					format: 'Y-m-d 17:25:00',
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