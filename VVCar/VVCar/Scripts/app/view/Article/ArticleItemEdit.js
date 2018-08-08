Ext.define('WX.view.Article.ArticleItemEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.ArticleItemEdit',
	title: '编辑图文消息子项',
	layout: 'fit',
	width: 640,
	height: 600,
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
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'Title',
					fieldLabel: '文章标题',
					maxLength: 50,
					allowBlank: false,
				}, {
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'Author',
					fieldLabel: '作者',
					maxLength: 20,
					allowBlank: true,
				}, {
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'ArticleID',
					fieldLabel: '图文消息ID',
					maxLength: 50,
					allowBlank: false,
					hidden: true,
				},]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'form',
					border: false,
					layout: 'hbox',
					margin: '10 10 0 0',
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
				name: 'CoverPicUrl',
				fieldLabel: '图片路径',
				hidden: true,
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'Digest',
					fieldLabel: '摘要',
					maxLength: 50,
					allowBlank: true,
				}, {
					xtype: 'combobox',
					margin: '5 10 5 5',
					name: 'IsShowCoverPic',
					store: yesNoDictStore,
					displayField: 'DictName',
					valueField: 'DictValue',
					fieldLabel: '是否显示封面',
					editable: false,
					value: false,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'Content',
					fieldLabel: '具体内容',
					maxLength: 50,
					allowBlank: true,
				}, {
					xtype: 'textfield',
					margin: '5 10 5 5',
					name: 'ContentSourceUrl',
					fieldLabel: '原文地址',
					maxLength: 50,
					allowBlank: true,
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
})