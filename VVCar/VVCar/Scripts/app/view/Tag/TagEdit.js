Ext.define('WX.view.Tag.TagEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.TagEdit',
	title: '编辑客户标签',
	layout: 'fit',
	width: 350,
	bodyPadding: 5,
	model: true,
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
				margin: 5,
			},
			items: [{
				xtype: 'textfield',
				margin: '5 10 5 5',
				name: 'Code',
				fieldLabel: '标签编码',
				maxLength: 20,
				allowBlank: false,
			}, {
				xtype: 'textfield',
				margin: '5 10 5 5',
				name: 'Name',
				fieldLabel: '标签名称',
				maxLength: 25,
				allowBlank: false,
			}]
		});
		me.items = [me.form];
		me.buttons = [{
			text: '保存',
			action: 'save',
			cls: 'submitBtn',
			scope: me
		}, {
			text: '取消',
			scope: me,
			handler: me.close
		}];
		me.callParent(arguments);
	}

})