Ext.define('WX.view.GameSetting.GameSettingList', {
	extend: 'Ext.container.Container',
	alias: 'widget.GameSettingList',
	title: '游戏设置',
	layout: 'hbox',
	align: 'stretch',
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var gameSettingStore = Ext.create('WX.store.BaseData.GameSettingStore');
		gameSettingStore.load();
		var gameCouponStore = Ext.create('WX.store.BaseData.GameCouponStore')
		me.items = [{
			xtype: 'grid',
			name: 'gridGameSetting',
			title: '游戏设置',
			flex: 6,
			height: '100%',
			store: gameSettingStore,
			stripeRow: true,
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
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'fa fa-search',
					cls: 'submitBtn',
					margin: '0 0 0 5',
				}]
			}],
			columns: [
				{
					header: '游戏类型', dataIndex: 'GameType', flex: 1,
					renderer: function (value) {
						if (value == 0)
							return '<span><font>拓客转盘</font></span>';
						if (value == 1)
							return '<span><font>活动转盘</font></span>';
					}
				},
				{
					header: '游戏开始', dataIndex: 'StartTime', flex: 1,
					renderer: Ext.util.Format.dateRenderer('Y-m-d'),
				},
				{
					header: '游戏结束', dataIndex: 'EndTime', flex: 1,
					renderer: Ext.util.Format.dateRenderer('Y-m-d'),
				},
				{ header: '周期(天)', dataIndex: 'PeriodDays', flex: 1 },
				{ header: '周期(次)', dataIndex: 'PeriodCounts', flex: 1 },
				{ header: '上限', dataIndex: 'Limit', flex: 1 },
				{
					header: '是否可分享', dataIndex: 'IsShare', flex: 1,
					renderer: function (value) {
						if (value == true)
							return '<span><font color="green">是</font></span>';
						else
							return '<span><font color="red">否</font></span>';
					}
				},
				{ header: '分享标题', dataIndex: 'ShareTitle', flex: 1 },
				{
					header: '显示游戏链接', dataIndex: 'IsOrderShow', flex: 1,
					renderer: function (value) {
						if (value == true)
							return '<span><font color="green">是</font></span>';
						else
							return '<span><font color="red">否</font></span>';
					}
				},
				{
					header: '是否启用', dataIndex: 'IsAvailable', flex: 1,
					renderer: function (value) {
						if (value == true)
							return '<span><font color="green">是</font></span>';
						else
							return '<span><font color="red">否</font></span>';
					}
				},
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true,
			}
		}, {
			xtype: 'splitter',
		}, {
			xtype: 'grid',
			name: 'gridGameCoupon',
			title: '游戏卡券',
			flex: 4,
			height: '100%',
			stripeRows: true,
			store: gameCouponStore,
			selType: 'checkboxmodel',
			tbar: [{
				action: 'addGameCoupon',
				xtype: 'button',
				text: '添加游戏卡券',
				iconCls: 'x-fa fa-plus-circle',
				margin: '5 5 5 5',
			}],
			columns: [
				{ header: '卡券模板编号', dataIndex: 'TemplateCode', width: 110 },
				{ header: '标题', dataIndex: 'Title', flex: 1 },
				{
					header: '卡券类型', dataIndex: 'Nature', width: 80,
					renderer: function (value) {
						if (value == 0) return '优惠券';
						if (value == 1) return '会员卡';
					}
				},
				{
					header: '优惠类型', dataIndex: 'CouponType', width: 80,
					renderer: function (value) {
						if (value == 0) return '代金';
						if (value == 1) return '抵用';
						if (value == 2) return '兑换';
						if (value == 3) return '折扣';
					}
				},
				{
					header: '创建时间', dataIndex: 'CreatedDate', width: 90,
					renderer: Ext.util.Format.dateRenderer('Y-m-d'),
				},
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
			],
		}];
		this.callParent();
	}
});
