Ext.define('WX.view.DailyExpense.DailyExpense', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.DailyExpense',
	title: '日常开支',
	name: 'gridMerchant',
	store: Ext.create('WX.store.BaseData.DailyExpenseStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	selType: 'checkboxmodel',
	initComponent: function () {
		var me = this;
		me.tbar = [{
			action: 'addDailyExpense',
			xtype: 'button',
			text: '添加开支',
			scope: this,
			iconCls: 'fa fa-plus-circle',
		}, {
			action: 'delDailyExpense',
			xtype: 'button',
			text: '删除开支',
			scope: this,
			iconCls: 'fa fa-close',
		},{
			xtype: 'form',
			layout: 'column',
			border: false,
			frame: false,
			labelAlign: 'left',
			buttonAlign: 'right',
			labelWidth: 100,
			padding: 5,
			autoWidth: true,
			autoScroll: false,
			columnWidth: 1,
			items: [{
				xtype: 'datefield',
				name: 'ExpenseDate',
				fieldLabel: '日期',
				width: 170,
				labelWidth: 30,
				margin: '0 0 0 5',
				format: 'Y-m-d',
			}, {
				action: 'search',
				xtype: 'button',
				text: '搜索',
				iconCls: 'fa fa-search',
				cls: 'submitBtn',
				margin: '0 0 0 5',
			}]
		}];
		me.columns = [
			{
				header: '支出日期', dataIndex: 'ExpenseDate', flex: 1,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
			{ header: '支出金额', dataIndex: 'Money', flex: 1 },
			{ header: '支出备注', dataIndex: 'Remark', flex: 1 },
			{ header: '工作人员数量', dataIndex: 'StaffCount', flex: 1 },
			{ header: '创建人', dataIndex: 'CreatedUser', flex: 1 },
			{
				header: '创建时间', dataIndex: 'CreatedDate', flex: 1,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
		];
		me.dockedItems = [{
			xtype: 'pagingtoolbar',
			store: me.store,
			dock: 'bottom',
			displayInfo: true
		}];
		me.callParent();
	},
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
		me.getStore().load();
	}
});