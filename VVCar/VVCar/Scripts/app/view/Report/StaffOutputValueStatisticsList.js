Ext.define('WX.view.Report.StaffOutputValueStatisticsList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.StaffOutputValueStatisticsList',
	title: '员工个人产值报表',
	store: Ext.create('WX.store.BaseData.StaffOutputValueStatisticsStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		this.tbar = [
			{
				xtype: 'form',
				layout: 'column',
				border: false,
				frame: false,
				labelAlign: 'left',
				buttonAlign: 'right',
				autoWidth: true,
				autoScroll: false,
				columnWidth: 1,
				fieldDefaults: {
					labelAlign: 'left',
					labelWidth: 60,
					width: 170,
					margin: '0 0 0 10',
				},
				columnWidth: 1,
				items: [{
					xtype: 'textfield',
					name: 'StaffName',
					labelWidth: 60,
					fieldLabel: '关键字',
					emptyText: '员工姓名/编码',
				}, {
					xtype: 'datefield',
					name: 'StartDate',
					fieldLabel: '开始时间',
					allowBlank: true,
					editable: true,
					width: 190,
					format: 'Y-m-d',
					margin: '0 0 0 20',
					value: '',
				}, {
					xtype: "displayfield",
					value: '-',
					width: 5,
					margin: '0 0 0 5',
				}, {
					xtype: 'datefield',
					name: 'EndDate',
					fieldLabel: '结束时间',
					allowBlank: true,
					editable: true,
					width: 190,
					format: 'Y-m-d',
					margin: '0 0 0 5',
					value: '',
				}, {
					xtype: 'button',
					action: 'search',
					text: '搜索',
					iconCls: 'search',
					cls: 'submitBtn',
					margin: '0 0 0 5',
				}, {
					xtype: 'button',
					action: 'reset',
					text: '重置',
					iconCls: 'reset',
					margin: '0 0 0 5',
				}, {
					xtype: 'button',
					action: 'export',
					text: '导出',
					iconCls: '',
					margin: '0 0 0 5',
				}]
			}
		];
		this.columns = [
			{ header: '员工姓名', dataIndex: 'StaffName', flex: 1 },
			{ header: '员工编码', dataIndex: 'StaffCode', flex: 1 },
			{ header: '总业绩', dataIndex: 'TotalPerformance', flex: 1 },
			{ header: '总成本', dataIndex: 'TotalCostMoney', flex: 1 },
			{ header: '总利润', dataIndex: 'TotalProfit', flex: 1 },
			{ header: '总日常支出', dataIndex: 'DailyExpense', flex: 1 },
			{ header: '平摊日常支出', dataIndex: 'AverageDailyExpense', flex: 1, },
			{ header: '产值(净利润)', dataIndex: 'TotalRetaainedProfit', flex: 1, },
		];
		me.dockedItems = [{
			xtype: 'pagingtoolbar',
			store: me.store,
			dock: 'bottom',
			displayInfo: true
		}];
		this.callParent();
	},
	afterRender: function () {
		this.callParent(arguments);
		this.getStore().load();
	}
});