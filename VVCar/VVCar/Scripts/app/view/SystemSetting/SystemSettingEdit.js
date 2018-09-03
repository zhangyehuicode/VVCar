Ext.define('WX.view.SystemSetting.SystemSettingEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.SystemSettingEdit',
	name: 'SystemSettingEdit',
	title: '编辑系统参数',
	layout: 'fit',
	width: 380,
	initComponent: function () {
		var me = this;
		var merchantStore = Ext.create('WX.store.BaseData.MerchantStore');
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
				xtype: 'combobox',
				margin: '5 5 0 5',
				name: 'MerchantID',
				store: merchantStore,
				displayField: 'Name',
				valueField: 'ID',
				fieldLabel: '商户',
				editable: false,
				value: false,
			}, {
				xtype: 'textfield',
				margin: '5 5 0 5',
				name: 'Caption',
				fieldLabel: '模板名称',
				maxLength: 10,
				allowBlank: true,
			}, {
				xtype: 'textfield',
				margin: '5 5 0 5',
				name: 'Name',
				fieldLabel: '模板编码',
				maxLength: 10,
				allowBlank: true,
			}, {
				xtype: 'textfield',
				margin: '5 5 0 5',
				name: 'SettingValue',
				fieldLabel: '模板数值',
				maxLength: 10,
				allowBlank: true,
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