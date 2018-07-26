Ext.define('WX.view.StockholderCard.StockholderCardEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.StockholderCardEdit',
	name: 'StockholderCardEdit',
	title: '编辑股东卡',
	layout: 'fit',
	width: 380,
	initComponent: function () {
		var me = this;
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 90,
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
					fieldLabel: '会员卡',
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
				fieldLabel: '会员卡ID',
				hidden: true,
			}, {
				xtype: 'form',
				layout: 'vbox',
				items: [{
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'numberfield',
						margin: '5 10 5 5',
						name: 'ConsumePointRate',
						fieldLabel: '消费返佣比例',
						minValue: 0,
						maxValue: 100,
						allowBlank: true,
					}, {
						xtype: 'label',
						text: '% (0~100)',
						margin: '10 15 0 0',
					}]
				}, {
					xtype: 'form',
					layout: 'hbox',
					items: [{
						xtype: 'numberfield',
						margin: '5 10 5 5',
						name: 'DiscountRate',
						fieldLabel: '折扣系数',
						minValue: 0,
						maxValue: 100,
						allowBlank: true,
					}, {
						xtype: 'label',
						text: '% (0~100)',
						margin: '10 15 0 0',
					}]
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