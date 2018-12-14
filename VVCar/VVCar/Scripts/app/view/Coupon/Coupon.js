Ext.define('WX.view.Coupon.Coupon', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.Coupon',
	title: '卡券列表',
	store: Ext.create('WX.store.BaseData.CouponStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	//selType: 'checkboxmodel',
	initComponent: function () {
		var me = this;
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
			autoScroll: false,
			columnWidth: 1,
			items: [{
				xtype: 'combobox',
				fieldLabel: '卡券类型',
				name: 'Nature',
				width: 190,
				labelWidth: 85,
				margin: '0 0 0 5',
				store: [
					[-1, '全部'],
					[0, '优惠券'],
					[1, '会员卡'],
				]
			},{
				xtype: 'combobox',
				fieldLabel: '优惠类型',
				name: 'CouponType',
				width: 190,
				labelWidth: 85,
				margin: '0 0 0 5',
				store: [
					[-1, '全部'],
					[1, '抵用'],
					[3, '折扣'],
				],
				defaultValue: '1',
			}, {
				xtype: 'combobox',
				fieldLabel: '状态',
				name: 'AproveStatus',
				width: 200,
				labelWidth: 85,
				margin: '0 0 0 5',
				store: [
					[-2, '全部状态'],
					[1, '待投放'],
					[2, '已投放'],
				]
			}, {
				action: 'search',
				xtype: 'button',
				text: '搜索',
				iconCls: 'fa fa-search',
				cls: 'submitBtn',
				margin: '0 0 0 5',
			}]
		}];
		me.columns = [
			{
				header: '卡券类型', dataIndex: 'Nature', width: 80 ,
				renderer: function (value) {
					if (value == 0) {
						return '优惠券';
					}
					if (value == 1) {
						return '会员卡';
					}
				}
			},
			{ header: '优惠类型', dataIndex: 'CouponTypeName', width: 80},
			{ header: '编号', dataIndex: 'TemplateCode', width: 110 },
			{
				header: '创建时间', dataIndex: 'CreatedDate', width: 90,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
			{ header: '投放时间', dataIndex: 'PutInDate', width: 170 },
			{ header: '标题', dataIndex: 'Title', flex: 1 },
			{ header: '有效期', dataIndex: 'Validity', width:180 },
			{ header: '状态', dataIndex: 'AproveStatusText', flex: 1 },
			{ header: '发行量', dataIndex: 'Stock', width: 70 },
			{
				header: '赠送', dataIndex: 'CanGiveToPeople', width: 50 ,
				renderer: function (value) {
					if (value) {
						return '<span><font color="green">是</font></span>';
					} else {
						return '<span color="red"><font color="red">否</font></span>';
					}
				}
			},
			{
				header: '分享', dataIndex: 'CanShareByPeople', width: 50,
				renderer: function (value) {
					if (value) {
						return '<span><font color="green">是</font></span>';
					} else {
						return '<span color="red"><font color="red">否</font></span>';
					}
				}
			},
			{ header: '库存', dataIndex: 'FreeStock', width: 70 },
			{ header: '消费返积分比例', dataIndex: 'ConsumePointRate',width:　120 },
			{ header: '抽成比例', dataIndex: 'CommissionRate', width: 80 },
			{ header: '备注', dataIndex: 'Remark', flex: 1 },
			{
				text: '操作功能',
				xtype: 'actioncolumn',
				width: 160,
				sortable: false,
				menuDisabled: true,
				height: 30,
				align: 'center',
				items: [{
					action: 'details',
					iconCls: 'x-fa fa-stack-exchange',
					tooltip: '详情',
					scope: this,
					margin: '10 10 10 10',
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('detailsActionClick', grid, record);
					}
				}, { scope: this }, {
					action: 'immediatepush',
					iconCls: 'x-fa fa-arrow-right',
					tooltip: '立即推送',
					scope: this,
					margin: '10 10 10 10',
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus == 2) {
							return 'x-fa fa-arrow-right';
						} else {
							return 'x-hidden';
						}
					},
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('immediatePushActionClick', grid, record);
					}
				}, {
					scope: this,
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus == 2) {
							return '';
						} else {
							return 'x-hidden';
						}
					},
				}, {
					action: 'qrcode',
					tooltip: '查看二维码',
					scope: this,
					margin: '10 10 10 10',
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus == 2) {
							return 'x-fa fa-qrcode';
						} else {
							return 'x-hidden';
						}
					},
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('qrCodeActionClick', grid, record);
					}
				}, {
					scope: this,
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus == 2) {
							return '';
						} else {
							return 'x-hidden';
						}
					},
				}, {
					action: 'receivehistory',
					tooltip: '领取记录',
					scope: this,
					margin: '10 10 10 10',
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus == 2) {
							return 'x-fa fa-book';
						} else {
							return 'x-hidden';
						}
					},
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('receiveHistoryActionClick', grid, record);
					}
				}, {
					scope: this,
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus == 2) {
							return '';
						} else {
							return 'x-hidden';
						}
					},
				}, {
					action: 'publish',
					tooltip: '投放',
					scope: this,
					margin: '10 10 10 10',
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus != 2) {
							return 'x-fa fa-hand-pointer-o ';
						} else {
							return 'x-hidden';
						}
					},
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('publishActionClick', grid, record);
					}
				}, {
					scope: this,
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus != 2) {
							return '';
						} else {
							return 'x-hidden';
						}
					},
				}, {
					action: 'updateItem',
					tooltip: '修改',
					scope: this,
					margin: '10 10 10 10',
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus != 2) {
							return 'x-fa fa-pencil';
						} else {
							return 'x-hidden';
						}
					},
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('updateItemActionClick', grid, record);
					}
				}, {
					scope: this,
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus != 2) {
							return '';
						} else {
							return 'x-hidden';
						}
					},
				}, {
					action: 'deleteItem',
					tooltip: '删除',
					scope: this,
					margin: '10 10 10 10',
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus != 2) {
							return 'x-fa fa-close';
						} else {
							return 'x-hidden';
						}
					},
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('deleteItemActionClick', grid, record);
					}
				}, {
					scope: this,
					getClass: function (v, metadata, record) {
						if (record.data.AproveStatus != 2) {
							return '';
						} else {
							return 'x-hidden';
						}
					},
				}]
			}
		];
		me.dockedItems = [{
			xtype: 'pagingtoolbar',
			store: me.store,
			dock: 'bottom',
			displayInfo: true
		}];
		me.callParent();
	},

});
