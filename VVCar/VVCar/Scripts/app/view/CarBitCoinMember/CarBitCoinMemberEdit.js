Ext.define('WX.view.CarBitCoinMember.CarBitCoinMemberEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.CarBitCoinMemberEdit',
	title: '赠送车比特',
	layout: 'fit',
	width: 300,
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
			items: [{
				xtype: 'form',
				layout: 'vbox',
				items: [{
					xtype: 'numberfield',
					name: 'CarBitCoin',
					fieldLabel: '车比特',
					minValue: 0,
					allowBlank: false,
					value: 0,
					step: 0.01,
				}, {
					xtype: 'textareafield',
					name: 'Remark',
					fieldLabel: '备注',
					maxLength: 20,
					blankText: '请输入赠送原因!',
					allowBlank: true,
				}]
			}],
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