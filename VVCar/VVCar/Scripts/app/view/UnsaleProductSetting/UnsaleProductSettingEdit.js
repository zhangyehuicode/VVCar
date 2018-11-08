Ext.define('WX.view.UnsaleProductSetting.UnsaleProductSettingEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.UnsaleProductSettingEdit',
	title: '编辑滞销提醒配置',
	name: 'UnsaleProductSettingEdit',
	layout: 'fit',
	width: 350,
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
				anchor: '100%'
			},
			items: [{
				xtype: 'textfield',
				name: 'Code',
				fieldLabel: '配置编码',
				allowBlank: false,
			}, {
				xtype: 'textfield',
				name: 'Name',
				fieldLabel: '配置名称',
				allowBlank: false,
			}, {
				xtype: 'numberfield',
				name: 'UnsaleQuantity',
				fieldLabel: '滞销数量上限',
				minValue: 0,
				allowBlank: false,
			}, {
				xtype: 'numberfield',
				name: 'SaleWellQuantity',
				fieldLabel: '畅销数量下限',
				minValue: 0,
				allowBlank: false,
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
			}]
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