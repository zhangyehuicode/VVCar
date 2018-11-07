Ext.define('WX.view.OrderDividend.OrderDividend', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.OrderDividend',
	title: '分红结算',
	name: 'gridMerchant',
	store: Ext.create('WX.store.BaseData.OrderDividendStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	selType: 'checkboxmodel',
	initComponent: function () {
		var me = this;
		me.tbar = [
			{
				action: 'balance',
				xtype: 'button',
				text: '结算',
				scope: this,
				iconCls: 'fa fa-money',
			}, {
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
					xtype: 'textfield',
					name: 'Keyword',
					fieldLabel: '关键词',
					emptyText: '用户编码/名称',
					width: 200,
					labelWidth: 50,
					margin: '0 0 0 5',
				}, {
					xtype: 'textfield',
					fieldLabel: '交易单号',
					name: 'TradeNo',
					width: 200,
					labelWidth: 60,
					margin: '0 0 0 5',
					store: [
						[false, '否'],
						[true, '是'],
					]
				}, {
					xtype: 'combobox',
					fieldLabel: '交易类型',
					name: 'OrderType',
					width: 175,
					labelWidth: 60,
					margin: '0 0 0 5',
					store: [
						[0, '其他'],
						[1, '商城订单'],
						[2, '接车单'],
					]
				}, {
					xtype: 'combobox',
					fieldLabel: '结算状态',
					name: 'IsBalance',
					width: 175,
					labelWidth: 60,
					margin: '0 0 0 5',
					store: [
						[false, '未结算'],
						[true, '已结算'],
					]
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'fa fa-search',
					cls: 'submitBtn',
					margin: '0 0 0 5',
				}]
			},
		];
		me.columns = [
			{ header: '店员编码', dataIndex: 'UserCode', flex: 1 },
			{ header: '店员名称', dataIndex: 'UserName', flex: 1 },
			{ header: '交易单号', dataIndex: 'TradeNo', flex: 1 },
			{ header: '交易类型', dataIndex: 'OrderTypeText', flex: 1 },
			{ header: '人员类型', dataIndex: 'PeopleTypeText', flex: 1 },
			{ header: '业绩', dataIndex: 'Money', flex: 1 },
			{ header: '分红', dataIndex: 'Commission', flex: 1 },
			{
				header: '创建时间', dataIndex: 'CreatedDate', width: 100,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
			{
				header: '是否已结算', dataIndex: 'IsBalance', width: 140,
				renderer: function (value) {
					if (value == true) {
						return '<div style="color:green">已结算</div>';
					}
					if (value == false) {
						return '<div style="color:red">未结算</div>';
					}
				}
			},
			{ header: '结算者名称', dataIndex: 'BalanceUserName', width: 100 },
			{
				header: '结算时间', dataIndex: 'BalanceDate', width: 100,
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