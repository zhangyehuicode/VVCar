Ext.define('WX.view.CarBitCoinRecord.CarBitCoinRecordList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.CarBitCoinRecordList',
	title: '车比特记录',
	name: 'gridMerchant',
	store: Ext.create('WX.store.BaseData.CarBitCoinRecordStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	initComponent: function () {
		var me = this;
		var carBitCoinRecordTypeStore = Ext.create('WX.store.DataDict.CarBitCoinRecordTypeStore');
		me.tbar = [
			{
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
					name: 'CarBitCoinRecordType',
					store: carBitCoinRecordTypeStore,
					loadMode: 'local',
					fieldLabel: '产品类型',
					displayField: 'DictName',
					valueField: 'DictValue',
					editable: true,
				}, {
					xtype: 'textfield',
					name: 'NamePhone',
					fieldLabel: '关键字',
					labelWidth: 60,
					width: 240,
					emptyText: '姓名/电话',
					//labelWidth: 140,
					margin: "0 0 0 5"
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'fa fa-search',
					cls: 'submitBtn',
					margin: '0 0 0 5',
				}, {
					action: 'export',
					xtype: 'button',
					text: '导出',
					iconCls: '',
					margin: '0 0 0 5',
				}]
			},
		];
		me.columns = [
			{ header: '车比特会员', dataIndex: 'CarBitCoinMemberName', flex: 1 },
			{ header: '手机号码', dataIndex: 'MobilePhoneNo', flex: 1 },
			{
				header: '车比特记录类型', dataIndex: 'CarBitCoinRecordType', flex: 1,
				renderer: function (value) {
					if (value == -4)
						return '<span><font>赠送币</font></span>';
					if (value == -3)
						return '<span><font>系统分配币</font></span>';
					if (value == -2)
						return '<span><font>购买比特币</font></span>';
					if (value == -1)
						return '<span><font>出售比特币</font></span>';
					if (value == 0)
						return '<span><font>未知</font></span>';
					if (value == 1)
						return '<span><font>商城下单增加马力</font></span>';
					if (value == 2)
						return '<span><font>接车单消费增加马力</font></span>';
					if (value == 3)
						return '<span><font>员工业绩增加马力</font></span>';
				}
			},
			{ header: '马力', dataIndex: 'Horsepower', flex: 1 },
			{ header: '车比特', dataIndex: 'CarBitCoin', flex: 1 },
			{ header: '交易单号', dataIndex: 'TradeNo', flex: 1 },
			{ header: '备注', dataIndex: 'Remark', flex: 1 },
			{ header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
		];
		me.dockedItems = [{
			xtype: 'pagingtoolbar',
			store: me.store,
			dock: 'bottom',
			displayInfo: true
		}];
		me.callParent();
	},
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
		me.getStore().load();
	}
});