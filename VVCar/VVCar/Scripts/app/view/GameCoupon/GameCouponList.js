Ext.define('WX.view.GameCoupon.GameCouponList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.GameCouponList',
	title: '游戏卡券配置',
	store: Ext.create('WX.store.BaseData.GameCouponStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	initComponent: function () {
		this.tbar = [
			{
				action: 'addGameCoupon',
				xtype: 'button',
				text: '添加',
				scope: this,
				iconCls: 'fa fa-plus-circle',
			},
		];
		this.columns = [
			{ header: '卡券模板编号', dataIndex: 'TemplateCode', flex: 1 },
			{
				header: '卡券类型', dataIndex: 'Nature', flex: 1,
				renderer: function (value) {
					if (value == 0) return '优惠券';
					if (value == 1) return '会员卡';
				}
			},
			{
				header: '优惠类型', dataIndex: 'CouponType', flex: 1,
				renderer: function (value) {
					if (value == 0) return '代金';
					if (value == 1) return '抵用';
					if (value == 2) return '兑换';
					if (value == 3) return '折扣';
				}
			},
			{ header: '标题', dataIndex: 'Title', flex: 1 },
			{ header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
			{
				text: '操作功能',
				xtype: 'actioncolumn',
				width: 80,
				sortable: false,
				menuDisabled: true,
				height: 30,
				align: 'center',
				items: [{
					action: 'deleteItem',
					iconCls: 'x-fa fa-close',
					tooltip: '删除',
					scope: this,
					margin: '10 10 10 10',
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('deleteActionClick', grid, record);
					}
				}]
			}
		];
		this.callParent();
	},
	afterRender: function () {
		this.callParent(arguments);
		this.getStore().load();
	}
});
