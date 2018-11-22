Ext.define('WX.view.Report.ProductRetailStatisticsList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.ProductRetailStatisticsList',
	title: '产品销售汇总',
	store: Ext.create('WX.store.BaseData.ProductRetailStatisticsStore'),
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
						xtype: 'combobox',
						name: 'ProductType',
						fieldLabel: '产品类别',
						width: 170,
						labelWidth: 60,
						margin: '0 0 0 5',
						store: [
							[0, '服务'],
							[1, '商品'],
						]
					}, {
						xtype: 'textfield',
						name: 'ProductCodeName',
						labelWidth: 45,
						fieldLabel: '关键词',
						emptyText: '产品名称/编码',
					}, {
						xtype: 'textfield',
						name: 'PlateNumber',
						labelWidth: 45,
						fieldLabel: '车牌号',
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
						xtype: 'combobox',
						name: 'OrderType',
						fieldLabel: '排序类型',
						width: 180,
						labelWidth: 60,
						margin: '0 0 0 5',
						store: [
							[0, '销售总额'],
							[1, '销售总数'],
						],
						value: 0,
					},{
						xtype: 'button',
						action: 'search',
						text: '搜索',
						iconCls: 'search',
						cls: 'submitBtn',
						margin: '0 0 0 5',
					}, 
					//, {
					//	xtype: 'button',
					//	action: 'searchSellWell',
					//	text: '畅销',
					//	margin: '0 0 0 5',
					//}, {
					//	xtype: 'button',
					//	action: 'searchUnsalable',
					//	text: '滞销',
					//	margin: '0 0 0 5',
					//}, {
					//	xtype: 'button',
					//	action: 'unsaleNotify',
					//	text: '滞销提醒',
					//	iconCls: '',
					//	margin: '0 0 0 5'
					//}
					,{
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
				}]
			}]
		}];
		this.columns = [
			{
				header: '产品类别', dataIndex: 'ProductType', flex: 1,
				renderer: function (value) {
					if (value != null)
						return '<span><font>' + value + '</font></span>';
					return;
				}
			},
			{ header: '产品类别名称', dataIndex: 'ProductCategoryName', flex: 1 },
			{ header: '产品名称', dataIndex: 'ProductName', flex: 1 },
			{ header: '产品编码', dataIndex: 'ProductCode', flex: 1 },
			{ header: '销售数量', dataIndex: 'Quantity', flex: 1 },
			{ header: '单位', dataIndex: 'Unit', flex: 1 },
			{ header: '销售总额', dataIndex: 'Money', flex: 1 },
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