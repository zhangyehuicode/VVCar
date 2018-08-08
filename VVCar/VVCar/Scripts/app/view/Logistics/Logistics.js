Ext.define('WX.view.Logistics.Logistics', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.Logistics',
	title: '物流',
	store: Ext.create('WX.store.BaseData.OrderStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var statusStore = Ext.create('Ext.data.Store', {
			fields: ['Name', 'Value'],
			data: [
				{ 'Name': '未付款', 'Value': 0 },
				{ 'Name': '已发货', 'Value': 2 },
				{ 'Name': '已完成', 'Value': 3 },
				{ 'Name': '已付款未发货', 'Value': 1 },
				{ 'Name': '付款不足', 'Value': -1 },
			]
		});
		me.tbar = [{
			xtype: 'form',
			layout: 'column',
			border: false,
			frame: false,
			labelAlign: 'left',
			buttonAlign: 'right',
			labelWidth: 100,
			padding: 5,
			autoWidth: true,
			autoScroll: true,
			columnWidth: 1,
			items: [{
				xtype: 'textfield',
				name: 'TNoLMPAddEN',
				fieldLabel: '关键字',
				width: 275,
				labelWidth: 60,
				margin: '0 5 5 10',
				emptyText: '订单号/联系人/联系电话/收货地址/快递单号',
			}, {
				xtype: 'combobox',
				name: 'Status',
				margin: '0 5 5 5',
				store: statusStore,
				displayField: 'Name',
				valueField: 'Value',
				fieldLabel: '发货状态',
				labelWidth: 60,
				width: 200,
				editable: false,
				value: false,
			}, {
				action: 'search',
				xtype: 'button',
				text: '搜索',
				iconCls: 'fa fa-search',
				cls: 'submitBtn',
				margin: '0 0 0 10'
			}, {
				action: 'delivery',
				xtype: 'button',
				text: '发货',
				scope: this,
				iconCls: 'fa fa-plus-circle',
				margin: '0 5 5 5',
			}, {
				action: 'antiDelivery',
				xtype: 'button',
				text: '取消发货',
				scope: this,
				iconCls: 'fa fa-close',
				margin: '0 5 5 5',
				}, {
				action: 'revisitTips',
				xtype: 'button',
				text: '手动回访',
				scope: this,
				iconCls: 'fa fa-close',
				margin: '0 5 5 5',
				}]
		}];
		me.columns = [
			{ header: '序号', dataIndex: 'Index', width: 60 },
			{ header: '订单号', dataIndex: 'Code', width: 150 },
			{
				header: '发货状态', dataIndex: 'Status', width: 100,
				renderer: function (value) {
					if (value == 2)
						return "已发货";
					else if (value == 1)
						return "已付款未发货";
					else if (value == 0)
						return "未付款";
					else if (value == 3)
						return "已完成";
					else if (value == -1)
						return "付款不足";
				}
			},
			{ header: '回访时间', dataIndex: 'RevisitDays', width: 90 },
			{
				header: '回访状态', dataIndex: 'RevisitStatus', width: 80,
				renderer: function (value) {
					if (value == 0) {
						return '<span><font color="red">未回访</font></span>';
					}
					if (value == 1) {
						return '<span><font color="green">已回访</font></span>';
					}
				}
			},
			{ header: '回访提示', dataIndex: 'RevisitTips', width: 80 },
			{ header: '发货提醒', dataIndex: 'DeliveryTips', width: 80 },
			{ header: '联系人', dataIndex: 'LinkMan', width: 90 },
			{ header: '联系电话', dataIndex: 'Phone', width: 120 },
			{ header: '收货地址', dataIndex: 'Address', flex: 1 },
			{ header: '快递单号', dataIndex: 'ExpressNumber', flex: 1 },
			{ header: '物流公司', dataIndex: 'LogisticsCompany', flex: 1 },
			{ header: '业务员', dataIndex: 'UserName', flex: 1 },
			{ header: '发货员', dataIndex: 'Consigner', flex: 1 },
			{ header: '发货时间', dataIndex: 'DeliveryDate', xtype: 'datecolumn', format: 'Y-m-d', width: 100 },
			//{ header: '创建时间', dataIndex: 'CreatedDate', xtype: 'datecolumn', format: 'Y-m-d', width: 100 },
			{
				text: '操作',
				xtype: 'actioncolumn',
				width: 150,
				sortable: false,
				menuDisabled: true,
				height: 30,
				align: 'center',
				items: [{
					action: 'orderdetails',
					iconCls: 'x-fa fa-reorder',
					tooltip: '详情',
					scope: this,
					margin: '10 10 10 10',
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('logisticsdetailsClick', grid, record);
					},
				}, { scope: this }, {
					action: 'editItem',
					iconCls: 'x-fa fa-pencil',
					tooltip: '编辑',
					scope: this,
					margin: '10 10 10 10',
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('editActionClick', grid, record);
					},
				},
				//{ scope: this }, {
				//	action: 'deleteItem',
				//	iconCls: 'x-fa fa-close',
				//	tooltip: '删除',
				//	scope: this,
				//	margin: '10 10 10 10',
				//	handler: function (grid, rowIndex, colIndex) {
				//		var record = grid.getStore().getAt(rowIndex);
				//		this.fireEvent('deleteActionClick', grid, record);
				//	},
				//}
				]
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
		this.callParent();
		var params = {
			IsLogistics: true,
			All: true,
		}
		Ext.apply(this.store.proxy.extraParams, params);
		this.store.load();
	},
});