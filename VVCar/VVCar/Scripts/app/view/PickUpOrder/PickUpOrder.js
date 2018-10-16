Ext.define('WX.view.PickUpOrder.PickUpOrder', {
	extend: 'Ext.container.Container',
	alias: 'widget.PickUpOrder',
	title: '接车单',
	layout: { type: 'vbox', align: 'stretch' },
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var statusStore = Ext.create('Ext.data.Store', {
			fields: ['Name', 'Value'],
			data: [
				{ 'Name': '未付款', 'Value': 0 },
				{ 'Name': '已付款', 'Value': 1 },
				{ 'Name': '收款不足', 'Value': 2 },
			]
		});
		me.items = [{
			xtype: 'panel',
			name: 'openorder',
			border: false,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 80,
				anchor: '100%',
				readOnly: true,
				margin: '0 0 8 0',
				tabIndex: 0,
				fieldStyle: 'font-size: 16px; line-height: normal',
			},
			items: [{
				xtype: 'displayfield',
				value: '接车单开单',
				fieldStyle: 'font-weight:bold;font-size:22px',
			}, {
				xtype: 'fieldcontainer',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					name: 'PlateNumber',
					fieldLabel: '车牌号',
					emptyText: '请输入车牌号',
					allowBlank: false,
					readOnly: false,
					tabIndex: 1,
					labelStyle: 'font-weight: bold; font-size: 22px;',
					fieldStyle: 'font-weight; bold; font-siez: 22px;line-height: normal',
					height: 30,
				}, {
					xtype: 'textfield',
					fieldLabel: '会员ID',
					name: 'MemberID',
					hidden: true,
				}, {
					xtype: 'textfield',
					fieldLabel: '姓名',
					name: 'MemberName',
					labelAlign: 'right',
					flex: 1,
				}, {
					xtype: 'textfield',
					fieldLabel: '手机号码',
					name: 'MobilePhoneNo',
					labelAlign: 'right',
					flex: 1,
				}, {
					action: 'openorder',
					xtype: 'button',
					text: '开单',
					cls: 'submitBtn',
					margin: '0 0 0 10'
				}]
			}]
		}, {
			xtype: 'gridpanel',
			name: 'pickuporder',
			store: Ext.create('WX.store.BaseData.PickUpOrderStore'),
			flex: 1,
			tbar: [{
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
					name: 'Code',
					fieldLabel: '订单号',
					width: 280,
					labelWidth: 60,
					margin: '5 5 5 10',
				}, {
					xtype: 'textfield',
					name: 'PlateNumber',
					fieldLabel: '车牌号',
					width: 180,
					labelWidth: 60,
					margin: '5 5 5 10',
				}, {
					xtype: 'textfield',
					name: 'StaffName',
					fieldLabel: '开单店员',
					width: 180,
					labelWidth: 60,
					margin: '5 5 5 10',
				}, {
					xtype: 'combobox',
					name: 'Status',
					margin: '5 5 5 5',
					store: statusStore,
					displayField: 'Name',
					valueField: 'Value',
					fieldLabel: '接车单状态',
					labelWidth: 80,
					width: 200,
					editable: false,
					value: false,
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'fa fa-search',
					cls: 'submitBtn',
					margin: '5 5 5 10'
				}]
			}],
			columns: [
				{ header: '订单号', dataIndex: 'Code', flex: 2 },
				{ header: '会员名称', dataIndex: 'MemberName', flex: 1 },
				{ header: '会员手机号', dataIndex: 'MemberMobilePhoneNo', flex: 1 },
				{ header: '车牌号', dataIndex: 'PlateNumber', flex: 1 },
				{ header: '开单店员', dataIndex: 'StaffName', flex: 1 },
				{ header: '订单总额', dataIndex: 'Money', width: 80 },
				{ header: '已收金额', dataIndex: 'ReceivedMoney', width: 80 },
				{ header: '尚欠金额', dataIndex: 'StillOwedMoney', width: 80 },
				{
					header: '接车单状态', dataIndex: 'Status', width: 100,
					renderer: function (value) {
						if (value == 0)
							return "<span style='color:green;'>未付款</span>";
						else if (value == 1)
							return "<span style='color:red;'>已付款</span>";
						else if (value == 2)
							return "<span style='color:green;'>收款不足</span>";
					}
				},
				{ header: '订单日期', dataIndex: 'CreatedDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', width: 150 },
				{
					text: '操作',
					xtype: 'actioncolumn',
					width: 100,
					sortable: false,
					menuDisabled: true,
					height: 30,
					align: 'center',
					items: [{
						action: 'pickuporderdetails',
						iconCls: 'x-fa fa-reorder',
						tooltip: '详情',
						scope: this,
						margin: '10 10 10 10',
						handler: function (grid, rowIndex, colIndex) {
							var record = grid.getStore().getAt(rowIndex);
							this.fireEvent('pickuporderdetailsClick', grid, record);
						},
					},
					{ scope: this },
					{
						action: 'deleteItem',
						iconCls: 'x-fa fa-close',
						tooltip: '删除',
						scope: this,
						margin: '10 10 10 10',
						handler: function (grid, rowIndex, colIndex) {
							var record = grid.getStore().getAt(rowIndex);
							this.fireEvent('deleteActionClick', grid, record);
						},
					}]
				},
			],
			dockedItems: [{
				xtype: 'pagingtoolbar',
				store: me.store,
				dock: 'bottom',
				displayInfo: true
			}],
		}]
		this.callParent();
	},
});