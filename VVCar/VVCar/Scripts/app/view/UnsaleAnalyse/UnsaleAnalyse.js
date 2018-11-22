Ext.define('WX.view.UnsaleAnalyse.UnsaleAnalyse', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.UnsaleAnalyse',
	title: '畅销滞销分析',
	store: Ext.create('WX.store.BaseData.UnsaleAnalyseStore'),
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
						name: 'Code',
						fieldLabel: '开始时间',
						allowBlank: true,
						editable: true,
						width: 190,
						format: 'Ym',
						margin: '0 0 0 20',
						value: '',
					}, {
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
						name: 'Keyword',
						labelWidth: 45,
						fieldLabel: '关键词',
						emptyText: '产品名称/编码',
					}, {
						xtype: 'combobox',
						fieldLabel: '销售状态',
						name: 'Status',
						width: 190,
						labelWidth: 85,
						margin: '0 0 0 5',
						store: [
							[0, '一般'],
							[1, '滞销'],
							[2, '畅销'],
						]
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
					}]
				}]
			}]
		}];
		this.columns = [
			{ header: '时间', dataIndex: 'Code', flex: 1 },
			{
				header: '产品类别', dataIndex: 'ProductTypeText', flex: 1,
				renderer: function (value) {
					if (value != null)
						return '<span><font>' + value + '</font></span>';
					return;
				}
			},
			{ header: '产品编码', dataIndex: 'ProductCode', flex: 1 },
			{ header: '产品名称', dataIndex: 'ProductName', flex: 1 },
			{ header: '畅销下限', dataIndex: 'UnsaleQuantity', flex: 1 },
			{ header: '滞销上限', dataIndex: 'SaleWellQuantity', flex: 1 },
			{ header: '销售数量', dataIndex: 'Quantity', flex: 1 },
			{
				header: '销售状况', dataIndex: 'StatusText', flex: 1,
				renderer: function (value) {
					if (value == '一般') {
						return '<span><font>' + value + '</font></span>';
					}
					if (value == '滞销') {
						return '<span><font color="red">' + value + '</font></span>';
					}
					if (value == '畅销') {
						return '<span><font color="green">' + value + '</font></span>';
					}
				}
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