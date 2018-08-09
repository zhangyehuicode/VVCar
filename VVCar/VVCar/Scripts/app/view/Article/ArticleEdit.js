Ext.define('WX.view.Article.ArticleEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.ArticleEdit',
	title: '编辑图文消息',
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
			items: [{
				xtype: 'textfield',
				name: 'Code',
				fieldLabel: '编码',
				margin: '5 0 0 5',
				maxLength: 18,
				allowBlank: false,
			}, {
				xtype: 'textfield',
				name: 'Name',
				fieldLabel: '标题',
				margin: '5 0 0 5',
				maxLength: 18,
				allowBlank: false,
			}, {
				xtype: 'combobox',
				name: 'IsPushAllMembers',
				store: yesNoDictStore,
				displayField: 'DictName',
				valueField: 'DictValue',
				fieldLabel: '是否推送所有会员',
				margin: '5 0 0 5',
				labelWidth: 60,
				editable: false,
				allowBlank: false,
			}, {
				xtype: 'datefield',
				name: 'PushDate',
				fieldLabel: '推送时间',
				margin: '5 0 0 5',
				allowBlank: true,
				minValue: new Date(),
				format: 'Y-m-d',
				editable: false,
				allowBlank: false,
				value: new Date()
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