Ext.define('WX.view.GameCouponRecord.GameCouponRecordList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.GameCouponRecordList',
	title: '游戏卡券领取记录',
	store: Ext.create('WX.store.BaseData.GameCouponRecordStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	//selType: 'checkboxmodel',
	//selModel: {
	//	selection: 'rowmodel',
	//	mode: 'single'
	//},
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
			autoWidth: true,
			autoScroll: false,
			columnWidth: 1,
			items: [{
				xtype: 'combobox',
				fieldLabel: '游戏类型',
				name: 'GameType',
				width: 175,
				labelWidth: 60,
				margin: '0 0 0 5',
				store: [
					[0, '扩客转盘'],
					[1, '活动转盘'],
				],
				queryMode: 'local',
				displayField: 'DictName',
				valueField: 'DictValue',
			}, {
				xtype: 'textfield',
				name: 'NickName',
				fieldLabel: '昵称',
				width: 170,
				labelWidth: 30,
				margin: '0 0 0 5',
			}, {
				xtype: 'textfield',
				name: 'GameCouponTitle',
				fieldLabel: '卡券标题',
				width: 170,
				labelWidth: 60,
				margin: '0 0 0 5',
			}, {
				xtype: 'datefield',
				name: 'StartTime',
				fieldLabel: '开始时间',
				labelWidth: 60,
				allowBlank: true,
				editable: true,
				width: 190,
				format: 'Y-m-d',
				margin: '0 0 0 5',
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
				labelWidth: 60,
				allowBlank: true,
				editable: true,
				width: 190,
				format: 'Y-m-d',
				margin: '0 0 0 5',
				value: '',
			}, {
				action: 'search',
				xtype: 'button',
				text: '搜索',
				iconCls: 'fa fa-search',
				cls: 'submitBtn',
				margin: '0 0 0 5',
			}]
		},];
		this.columns = [
			{
				header: '游戏类型', dataIndex: 'GameType', flex: 1,
				renderer: function (value) {
					if (value == 0)
						return '<span><font>拓客转盘</font></span>';
					if (value == 1)
						return '<span><font>活动转盘</font></span>';
				}
			},
			{ header: '昵称', dataIndex: 'NickName', flex: 1 },
			//{ header: '领券人OpenID', dataIndex: 'ReceiveOpenID', flex: 1 },
			//{ header: '卡券ID', dataIndex: 'CouponTemplateID', flex: 1 },
			{ header: '卡券标题', dataIndex: 'CouponTitle', flex: 1 },
			{ header: '领取数量', dataIndex: 'ReceiveCount', flex: 1 },
			{ header: '订单号', dataIndex: 'OutTradeNo', flex: 1 },
			{ header: '领取时间', dataIndex: 'CreatedDate', flex: 1 },
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
		store.load();
	}
});
