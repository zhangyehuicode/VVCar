Ext.define('WX.view.Material.MaterialEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.MaterialEdit',
	title: '素材',
	layout: 'fit',
	width: 350,
	modal: true,
	bodyPadding: 5,
	initComponent: function () {
		var me = this;
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 70,
				anchor: '100%'
			},
			items: [{
				xtype: 'textfield',
				name: 'Name',
				fieldLabel: '名称',
				maxLength: 50,
				allowBlank: false,
			}, {
				xtype: 'form',
				border: false,
				layout: 'hbox',
				margin: '0 0 10 0',
				items: [{
					xtype: 'filefield',
					id: 'file1',
					fieldLabel: '素材',
					allowBlank: true,
					buttonText: '选择素材',
				}, {
					xtype: 'button',
					text: '上传',
					margin: '0 0 0 5',
					action: 'uploadMaterial',
				}]
			},
			{
				xtype: 'box',
				name: 'MaterialImgShow',
				margin: '0 0 10 75',
				width: 200,
				height: 200,
				autoEl: {
					tag: 'img',
					src: '',
					controls: '',
				}
			},
			{
				xtype: 'box',
				name: 'MaterialVideoShow',
				margin: '0 0 10 75',
				width: 200,
				height: 200,
				hidden: true,
				autoEl: {
					tag: 'video',
					src: '',
					controls: '',
				}
			},
			{
				xtype: 'textfield',
				name: 'Url',
				fieldLabel: '图片路径',
				hidden: true,
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