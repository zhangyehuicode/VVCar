Ext.define('WX.view.Report.OperationStatement', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.OperationStatement',
	title: '营业报表',
	store: Ext.create('WX.store.BaseData.OperationStatementStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		this.tbar = [{
			xtype: 'container',
			border: false,
			items: [{
				xtype: 'toolbar',
				items: [{
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
					}]
				}]
			}]
		}];
		this.columns = [
			{ header: '时间', dataIndex: 'Code', flex: 1 },
			{ header: '总收入', dataIndex: 'TotalInCome', flex: 1 },
			{ header: '总支出', dataIndex: 'TotalOutCome', flex: 1 },
			{
				text: '操作',
				xtype: 'actioncolumn',
				width: 200,
				sortable: false,
				menuDisabled: true,
				height: 30,
				align: 'center',
				items: [{
					action: 'editItem',
					iconCls: 'x-fa fa-reorder',
					tooltip: '详情',
					scope: this,
					margin: '10 10 10 10',
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('detailClick', grid, record);
					},
				}]
			},
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