Ext.define('WX.view.StockholderCard.StockholderCardList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.StockholderCardList',
	title: '股东卡',
	store: Ext.create('WX.store.BaseData.CouponTemplateInfoStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	selType: 'checkboxmodel',
	selModel: {
		selection: 'rowmodel',
		mode: 'single'
	},
	initComponent: function () {
		var me = this;
		this.tbar = [
			{
				action: 'setConsumePointRate',
				xtype: 'button',
				text: '设置消费返积分比例',
				scope: this,
				iconCls: 'fa fa-plus-circle',
			},
		];
		this.columns = [
			//{ header: 'ID', dataIndex: 'ID', flex: 1 },
			//{
			//	header: '卡券类型', dataIndex: 'Nature', flex: 1,
			//	renderer: function (value) {
			//		if (value == 0) return '优惠券';
			//		if (value == 1) return '会员卡';
			//	}
			//},
			{ header: '优惠类型', dataIndex: 'CouponTypeName', flex: 1, },
			{ header: '编号', dataIndex: 'TemplateCode', flex: 1 },
			{ header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
			{ header: '投放时间', dataIndex: 'PutInDate', flex: 1 },
			{ header: '标题', dataIndex: 'Title', flex: 1 },
			{ header: '有效时间', dataIndex: 'Validity', flex: 1 },
			{ header: '状态', dataIndex: 'AproveStatusText', flex: 1 },
			{ header: '发行量', dataIndex: 'Stock', flex: 1 },
			{
				header: '分享与赠送', dataIndex: 'CanGiveToPeople', flex: 1,
				renderer: function (value) {
					return value ? '分享' : '无';
				}
			},
			//{ header: '库存', dataIndex: 'FreeStock', flex: 1 },
			{ header: '消费返积分比例', dataIndex: 'ConsumePointRate', flex: 1 },
			{ header: '备注', dataIndex: 'Remark', flex: 1 },
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
		var store = this.getStore();
		var params = {
			Nature: 1,
			CouponType: -1,
			AproveStatus: -2,
		}
		Ext.apply(store.proxy.extraParams, params);
		store.load();
	}
});
