Ext.define('WX.view.ServicePeriod.ServicePeriodEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.ServicePeriodEdit',
	title: '编辑服务周期配置',
	name: 'ServicePeriodEdit',
	layout: 'fit',
	width: 350,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 80,
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
						fieldLabel: '服务',
						readOnly: true,
						allowBlank: false,
					}, {
						action: 'selectService',
						xtype: 'button',
						text: '查找',
						cls: 'submitBtn',
						margin: '0 0 5 0',
					}]
				}, {
					xtype: 'textfield',
					name: 'ProductID',
					fieldLabel: '产品ID',
					hidden: true,
				}, {
					xtype: 'numberfield',
					name: 'PeriodDays',
					fieldLabel: '服务周期(天)',
					minValue: 0,
					allowBlank: false,
				}, {
					xtype: 'textfield',
					name: 'ExpirationNotice',
					fieldLabel: '到期提示语',
					allowBlank: true,
				}, {
					xtype: 'combobox',
					name: 'IsAvailable',
					fieldLabel: '启用',
					store: Ext.getStore('DataDict.EnableDisableTypeStore'),
					queryMode: 'local',
					displayField: 'DictName',
					valueField: 'DictValue',
					emptyText: '请选择...',
					blankText: '请选择是否启用',
					editable: false,
					allowBlank: false,
					value: true,
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