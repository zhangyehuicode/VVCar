Ext.define('WX.view.Report.ConsumeHistoryList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.ConsumeHistoryList',
	title: '会员消费记录',
	store: Ext.create('WX.store.BaseData.ConsumeHistoryStore'),
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
						xtype: 'textfield',
						name: 'Name',
						labelWidth: 45,
						fieldLabel: '姓名',
					}, {
						xtype: 'textfield',
						name: 'MobilePhoneNo',
						labelWidth: 45,
						fieldLabel: '手机号',
					}, {
						xtype: 'textfield',
						name: 'PlateNumber',
						labelWidth: 45,
						fieldLabel: '车牌号',
					}, {
						xtype: 'combobox',
						name: 'Source',
						fieldLabel: '类型',
						width: 140,
						labelWidth: 30,
						margin: '0 0 0 5',
						store: [
							[0, '接车单'],
							[1, '商城订单'],
							[2, '导入数据'],
						]
					}, {
						xtype: 'datefield',
						name: 'StartTime',
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
						name: 'EndTime',
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
					}]
				}]
			}, {
				xtype: 'toolbar',
				items: [{
					xtype: 'button',
					action: 'export',
					text: '导出',
					iconCls: '',
					margin: '0 0 0 5',
				}, {
					action: 'importConsumeHistoryDataTemplate',
					xtype: 'button',
					margin: "0 0 0 25",
					text: '下载模板',
					//permissionCode: 'Member.Member.ImportMember',
				}, {
					xtype: 'form',
					layout: 'fit',
					border: false,
					frame: false,
					width: 60,
					//permissionCode: 'Member.Member.ImportMember',
					items: [{
						name: 'importConsumeHistoryData',
						xtype: 'fileuploadfield',
						buttonOnly: true,
						hideLabel: true,
						buttonText: '导入',
						margin: "0 0 0 5",
						allowBlank: false,
					}]
				}]
			}]
		}];
		this.columns = [
			{ header: '交易单号', dataIndex: 'TradeNo', flex: 1 },
			{ header: '姓名', dataIndex: 'Name', flex: 1 },
			{ header: '手机号', dataIndex: 'MobilePhoneNo', flex: 1 },
			{ header: '车牌号', dataIndex: 'PlateNumber', flex: 1 },
			{ header: '消费项目', dataIndex: 'Consumption', flex: 1 },
			{ header: '单位', dataIndex: 'Unit', flex: 1 },
			{ header: '数量', dataIndex: 'TradeCount', flex: 1 },
			{ header: '单价', dataIndex: 'Price', flex: 1 },
			{ header: '交易金额', dataIndex: 'TradeMoney', flex: 1 },
			{
				header: '类型', dataIndex: 'Source', flex: 1,
				renderer: function (value) {
					if (value == 0)
						return "接车单";
					if (value == 1)
						return "商城订单";
					if (value == 2)
						return "导入数据";
				}
			},
			{ header: '商品成本', dataIndex: 'BasePrice', flex: 1 },
			{ header: '毛利', dataIndex: 'GrossProfit', flex: 1 },
			{ header: '备注', dataIndex: 'Remark', flex: 1 },
			{ header: '门店', dataIndex: 'DepartmentName', flex: 1 },
			{ header: '交易时间', dataIndex: 'CreatedDate', flex: 1 },
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