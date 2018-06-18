Ext.define('WX.view.StockholderCard.StockholderCardEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.StockholderCardEdit',
	title: '编辑消费返积分比例',
	layout: 'fit',
	width: 320,
	initComponent: function () {
		var me = this;
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 100,
				anchor: '100%',
				flex: 1,
				margin: 5,
			},
			items: [{
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'id',
					allowBlank: false,
					hidden: true,

				}, {
					xtype: 'numberfield',
					margin: '5 10 5 5',
					name: 'rate',
					fieldLabel: '消费返积分比例',
					minValue: 0,
					width: 50,
					maxValue: 100,
					allowBlank: true,
				}, {
					xtype: 'label',
					text: '% (0~100)',
					margin: '10 15 0 0',
				}]
			}]
		});
		me.items = [me.form];
		me.buttons = [{
			text: '保存',
			cls: 'submitBtn',
			action: 'save'
		}, {
			text: '取消',
			scope: me,
			handler: me.close
		}];
		me.callParent(arguments);
	}
});