Ext.define('WX.view.GoodsLandingClass.GoodsLandingClassEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.GoodsLandingClassEdit',
	title: '编辑商品落地课程',
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
					fieldLabel: '视频',
					allowBlank: true,
					buttonText: '选择视频',
				}, {
					xtype: 'button',
					text: '上传',
					margin: '0 0 0 5',
					action: 'uploadVideo',
				}]
			},
			{
				xtype: 'box',
				name: 'VideoShow',
				margin: '0 0 10 75',
				width: 200,
				height: 200,
				autoEl: {
					tag: 'video',
					src: '',
					controls: '',
				}
			},
			{
				xtype: 'textfield',
				name: 'VideoUrl',
				fieldLabel: '图片路径',
				hidden: true,
			}, {
				xtype: 'textareafield',
				name: 'Description',
				fieldLabel: '视频简介',
				maxLength: 200,
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