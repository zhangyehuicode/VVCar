Ext.define('WX.view.Announcement.AnnouncementEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.AnnouncementEdit',
	title: '编辑公告',
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
				anchor: '100%',
			},
			items: [{
				xtype: 'textfield',
				name: 'Title',
				fieldLabel: '标题',
				margin: '5 0 0 5',
				maxLength: 20,
				allowBlank: false,
			}, {
				xtype: 'combobox',
				name: 'PushAllMembers',
				store: yesNoDictStore,
				displayField: 'DictName',
				valueField: 'DictValue',
				fieldLabel: '是否推送所有会员',
				margin: '5 0 0 5',
				labelWidth: 60,
				editable: false,
				allowBlank: false,
			}, {
				xtype: 'textfield',
				name: 'Name',
				fieldLabel: '项目名称',
				margin: '5 0 0 5',
				maxLength: 20,
				allowBlank: false,
			}, {
				xtype: 'textfield',
				name: 'Process',
				fieldLabel: '项目进展',
				margin: '5 0 0 5',
				maxLength: 20,
				allowBlank: false,
			}, {
				xtype: 'textfield',
				name: 'Content',
				fieldLabel: '详细内容',
				margin: '5 0 0 5',
				maxLength: 20,
				allowBlank: false,
			}, {
				xtype: 'textfield',
				name: 'Remark',
				fieldLabel: '备注',
				margin: '5 0 0 5',
				maxLength: 20,
				allowBlank: true,
			}]
		});
		me.items = [me.form];
		me.buttons = [{
			text: '保存',
			action: 'save',
			cls: 'submitBtn',
			scope: me,
		}, {
			text: '取消',
			scope: me,
			handler: me.close,
		}];
		me.callParent(arguments);
	}
});