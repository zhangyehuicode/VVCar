Ext.define('WX.view.StockholderCard.StockholderCardSelector', {
	extend: 'Ext.window.Window',
	alias: 'widget.StockholderCardSelector',
	title: '选择会员',
	layout: 'fit',
	width: 600,
	height: 500,
	bodyPadding: 5,
	autoShow: false,
	modal: true,
	buttonAlign: 'center',
	initComponent: function () {
		var me = this;
		var memberStore = Ext.create('WX.store.BaseData.MemberStore');
		me.items = [{
			xtype: 'grid',
			name: 'stockholderCardList',
			stripeRows: true,
			loadMask: true,
			store: memberStore,
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
					xtype: 'textfield',
					name: 'Keyword',
					fieldLabel: '关键字',
					labelWidth: 60,
					width: 240,
					emptyText: '电话/姓名',
					margin: "0 0 0 5"
				},{
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'submitBtn',
					margin: '0 0 0 5',
				}]
			},
			columns: [
				{ header: "会员卡号", dataIndex: "CardNumber", width: 80 },
				{ header: "姓名", dataIndex: "Name", minWidth: 80, flex: 1 },
				{ header: "手机号码", dataIndex: "MobilePhoneNo", width: 105 },
				{ header: "车牌号", dataIndex: "PlateList", minWidth: 80, flex: 1, permissionCode: 'Member.Member.DepartmentColumn' },
				{ header: "注册时间", dataIndex: "CreatedDate", xtype: "datecolumn", format: "Y-m-d", minWidth: 90 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		}]
		me.callParent(arguments);
	}
});