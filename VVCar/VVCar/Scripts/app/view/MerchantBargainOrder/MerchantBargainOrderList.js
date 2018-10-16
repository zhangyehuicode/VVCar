Ext.define('WX.view.MerchantBargainOrder.MerchantBargainOrderList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.MerchantBargainOrderList',
	title: '拼单',
	store: Ext.create('WX.store.BaseData.MerchantBargainOrderStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	selType: 'checkboxmodel',
	selModel: {
		selection: 'rowmodel',
		//mode: 'single'
	},
	initComponent: function () {
		var me = this;
		this.tbar = [{
			xtype: 'form',
			layout: 'column',
			border: false,
			frame: false,
			labelAlign: 'left',
			buttonAlign: 'right',
			labelWidth: 100,
			padding: 5,
			autoWidth: 1,
			items: [{
				xtype: 'textfield',
				name: 'Name',
				fieldLabel: '标题',
				width: 170,
				labelWidth: 30,
				margin: '0 0 0 5'
			}, {
				action: 'search',
				xtype: 'button',
				text: '搜索',
				iconCls: 'fa fa-search',
				cls: 'submitBtn',
				margin: '0 0 0 5'
			}]
		}, {
			action: 'addMerchantBargainOrder',
			xtype: 'button',
			text: '新增',
			scope: this,
			iconCls: 'fa fa-plus-circle',
		}, {
			action: 'delMerchantBargainOrder',
			xtype: 'button',
			text: '删除',
			scope: this,
			iconCls: 'fa fa-close',
		}];
		me.columnLines = true;
		me.columns = [
			{ header: '砍价标题', dataIndex: 'Name', flex: 1 },
			{ header: '产品', dataIndex: 'ProductName', flex: 1 },
			{ header: '库存', dataIndex: 'Stock', width: 80 },
			{ header: '销售单价', dataIndex: 'PriceSale', flex: 1 },
			{ header: '砍价价格', dataIndex: 'Price', flex: 1 },
			{ header: '砍价人数', dataIndex: 'PeopleCount', flex: 1 },
			{
				header: '是否启用', dataIndex: 'IsAvailable', flex: 1,
				renderer: function (value) {
					if (value == 0)
						return '<span>否</span>';
					if (value == 1)
						return '<span style="color:green;">是</span>';
				}
			},
			{
				header: '上架时间', dataIndex: 'PutawayTime', flex: 1,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
			{
				header: '下架时间', dataIndex: 'SoleOutTime', flex: 1,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
			{
				header: '创建时间', dataIndex: 'CreatedDate', flex: 1,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
		];
		me.dockedItems = [{
			xtype: 'pagingtoolbar',
			store: me.store,
			dock: 'bottom',
			displayInfo: true,
		}];
		this.callParent();
	},
	afterRender: function () {
		this.callParent(arguments);
		var store = this.getStore();
		store.load();
	}
})