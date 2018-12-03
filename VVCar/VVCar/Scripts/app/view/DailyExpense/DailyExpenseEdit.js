Ext.define('WX.view.DailyExpense.DailyExpenseEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.DailyExpenseEdit',
	title: '日常开支编辑',
	layout: 'fit',
	width: 300,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 60,
				anchor: '100%'
			},
			items: [{
				xtype: 'datefield',
				name: 'ExpenseDate',
				fieldLabel: '支出时间',
				margin: '5 0 0 5',
				allowBlank: true,
				format: 'Y-m-d',
				editable: false,
				allowBlank: false,
				value: new Date()
			}, {
				xtype: 'textfield',
				name: 'Money',
				fieldLabel: '支出金额',
				margin: '5 0 0 5',
				maxLength: 18,
				allowBlank: false,
			}, {
				xtype: 'textfield',
				name: 'StaffCount',
				fieldLabel: '工作人员数量',
				margin: '5 0 0 5',
				maxLength: 18,
				allowBlank: true,
				hidden: true,
			}, {
				xtype: 'textfield',
				name: 'Remark',
				fieldLabel: '备注',
				margin: '5 0 0 5',
				maxLength: 18,
				allowBlank: false,
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