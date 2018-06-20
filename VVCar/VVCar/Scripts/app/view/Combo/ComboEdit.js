Ext.define('WX.view.Combo.ComboEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.ComboEdit',
	title: '编辑套餐配置',
	name: 'ComboEdit',
	layout: 'fit',
	width: 280,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 40,
				anchor: '100%'
			},
			items: [
				{
					xtype: 'panel',
					layout: 'hbox',
					border: false,
					padding: '0 0 5 0',
					items: [{
						xtype: 'textfield',
						name: 'ProductName',
						fieldLabel: '产品',
						readOnly: true,
						allowBlank: false,
					}, {
						action: 'selectProduct',
						xtype: 'button',
						text: '查找',
						cls: 'submitBtn',
						margin: '0 0 0 5',
					}]
				}, {
					xtype: 'textfield',
					name: 'ProductID',
					fieldLabel: '产品ID',
					hidden: true,
				}, {
					xtype: 'textfield',
					name: 'ProductCode',
					fieldLabel: '产品编号',
					readOnly: true,
					allowBlank: false,
					hidden: false,
				}, {
					xtype: 'textfield',
					name: 'BasePrice',
					fieldLabel: '原单价',
					hidden: true,
				}, {
					xtype: 'textfield',
					name: 'PriceSale',
					fieldLabel: '销售单价',
					hidden: true,
				}, {
					xtype: 'numberfield',
					name: 'Quantity',
					fieldLabel: '数量',
					minValue: 1,
					value: 1,
					allowBlank: false,
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