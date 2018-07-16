Ext.define('WX.view.Reimbursement.ReimbursementEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.ReimbursementEdit',
	title: '业务报销',
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
				name: 'Project',
				fieldLabel: '项目',
				maxLength: 50,
				allowBlank: false,
			}, {
				xtype: 'form',
				border: false,
				layout: 'hbox',
				margin: '0 0 10 0',
				items: [{
					xtype: 'filefield',
					fieldLabel: '票据图片',
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
				name: 'ImgShow',
				width: 260,
				height: 100,
				margin: '0 0 10 75',
				autoEl: {
					tag: 'img',
					src: '',
				}
			}, {
				xtype: 'textfield',
				name: 'ImgUrl',
				fieldLabel: '图片路径',
				hidden: true,
			}, {
				xtype: 'numberfield',
				name: 'Money',
				fieldLabel: '报销金额',
				minValue: 0,
				allowBlank: false,
				value: 0,
			}, {
				xtype: 'textareafield',
				name: 'Remark',
				fieldLabel: '备注',
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