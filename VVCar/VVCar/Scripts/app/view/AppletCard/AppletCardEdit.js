Ext.define('WX.view.AppletCard.AppletCardEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.AppletCardEdit',
	name: 'AppletCardEdit',
	title: '编辑小程序卡券',
	layout: 'fit',
	width: 300,
	initComponent: function () {
		var me = this;
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 30,
				anchor: '100%',
				flex: 1,
				margin: 5,
			},
			items: [{
				xtype: 'panel',
				layout: 'hbox',
				border: false,
				padding: '0 0 5 0',
				items: [{
					xtype: 'textfield',
					name: 'Title',
					fieldLabel: '卡券',
					readOnly: true,
					allowBlank: false,
				}, {
					action: 'selectCard',
					xtype: 'button',
					text: '查找',
					cls: 'submitBtn',
					margin: '5 5 0 5',
				}]
			}, {
				xtype: 'textfield',
				name: 'ID',
				fieldLabel: '卡券ID',
				hidden: true,
			}]
		});
		me.items = [me.form];
		me.buttons = [{
			text: '保存',
			cls: 'submitBtn',
			action: 'save',
		}, {
			text: '取消',
			scope: me,
			handler: me.close
		}];
		me.callParent(arguments);
	}
})