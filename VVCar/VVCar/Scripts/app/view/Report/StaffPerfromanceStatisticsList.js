Ext.define('WX.view.Report.StaffPerfromanceStatisticsList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.StaffPerfromanceStatisticsList',
	title: '员工业绩汇总',
	store: Ext.create('WX.store.BaseData.StaffPerformanceStatisticsStore'),
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
					fieldLabel: '员工姓名',
					emptyText: '请输入...',
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
			{ header: '总抽成', dataIndex: 'TotalCommission', flex: 1 },
			{ header: '当前业绩', dataIndex: 'CurrentPerformance', flex: 1 },
			{ header: '当月业绩', dataIndex: 'MonthPerformance', flex: 1 },
			{ header: '当前抽成', dataIndex: 'CurrentCommission', flex: 1 },
			{ header: '当月抽成', dataIndex: 'MonthCommission', flex: 1 },
			{ header: '客户服务量', dataIndex: 'CustomerServiceCount', flex: 1 },
			{ header: '当前客户服务量', dataIndex: 'CurrentCustomerServiceCount', flex: 1 },
			{ header: '月客户服务量', dataIndex: 'MonthCustomerServiceCount', flex: 1 },
			//{ header: '底薪', dataIndex: 'BasicSalary', flex: 1 },
			//{ header: '奖励/补贴', dataIndex: 'Subsidy', flex: 1 },
			//{ header: '月总收入', dataIndex: 'MonthIncome', flex: 1 },
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