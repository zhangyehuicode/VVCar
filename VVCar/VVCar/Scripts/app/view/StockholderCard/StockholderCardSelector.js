Ext.define('WX.view.StockholderCard.StockholderCardSelector', {
	extend: 'Ext.window.Window',
	alias: 'widget.StockholderCardSelector',
	title: '选择会员卡',
	layout: 'fit',
	width: 600,
	height: 500,
	bodyPadding: 5,
	autoShow: false,
	modal: true,
	buttonAlign: 'center',
	initComponent: function () {
		var me = this;
		var couponTemplateInfoStore = Ext.create('WX.store.BaseData.CouponTemplateInfoStore');
		me.items = [{
			xtype: 'grid',
			name: 'stockholderCardList',
			stripeRows: true,
			loadMask: true,
			store: couponTemplateInfoStore,
			tbar: {
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
					name: 'TemplateCode',
					xtype: 'textfield',
					fieldLabel: '编号',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					name: 'Title',
					xtype: 'textfield',
					fieldLabel: '标题',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'submitBtn',
					margin: '0 0 0 5',
				}]
			},
			columns: [
				{ header: '编号', dataIndex: 'TemplateCode', flex: 1 },
				{ header: '优惠类型', dataIndex: 'CouponTypeName', flex: 1 },
				{ header: '标题', dataIndex: 'Title', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		}]
		me.callParent(arguments);
	}
});