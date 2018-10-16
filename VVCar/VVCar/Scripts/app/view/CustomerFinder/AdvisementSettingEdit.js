Ext.define('WX.view.CustomerFinder.AdvisementSettingEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.AdvisementSettingEdit',
	title: '编辑广告',
	//layout: 'fit',
	width: 500,
	height: 600,
	bodyPadding: 5,
	modal: true,
	resizable: false,
	autoScroll: true,
	bodyStyle: 'overflow-y:auto; overflow-x:hidden;',
	initComponent: function () {
		var me = this;
		var yesNoDictStore = Ext.create('WX.store.DataDict.YesNoTypeStore');
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 60,
				anchor: '100%',
			},
			items: [{
				xtype: 'textfield',
				name: 'Title',
				fieldLabel: '标题',
				margin: '5 5 5 5',
				maxLength: 18,
				allowBlank: false,
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'form',
					border: false,
					layout: 'hbox',
					margin: '5 10 0 0',
					items: [{
						xtype: 'filefield',
						fieldLabel: '封面图片',
						width: 217,
						allowBlank: true,
						buttonText: '选择图片',
					}, {
						xtype: 'button',
						text: '上传',
						margin: '0 0 0 5',
						action: 'uploadpic',
					}]
				}, {
					xtype: 'box',
					margin: '5 10 0 5',
					name: 'ImgShow',
					width: 83,
					height: 50,
					margin: '0 0 5 72',
					autoEl: {
						tag: 'img',
						src: '',
					}
				}]
			}, {
				xtype: 'textfield',
				name: 'ImgUrl',
				fieldLabel: '图片路径',
				hidden: true,
			}, {
				xtype: 'htmleditor',
				margin: '5 10 5 5',
				name: 'Content',
				fieldLabel: '内容',
				height: 600,
				maxLength: 3000,
				allowBlank: true,
				enableAlignments: true,
				enableColors: true,
				enableFont: true,
				enableFontSize: true,
				enableFormat: true,
				enableLinks: false,
				enableLists: true,
				enableSourceEdit: false,
				fontFamilies: ['宋体', '隶书', '黑体'],
				plugins: [
					Ext.create('WX.view.Shop.HtmlEditorImage')
				],
			}, {
				html: ' <span style="font-size:13px;margin:5px 10px 5px 65px;" >（建议上传345* 200的图片）</span>',
			}]
		});
		me.items = [me.form];
		me.buttons = [{
			text: '保存',
			cls: 'submitBtn',
			action: 'save',
		}, {
			text: '取消',
			scope: me,
			handler: me.close
		}];
		me.callParent(arguments);
	}
})