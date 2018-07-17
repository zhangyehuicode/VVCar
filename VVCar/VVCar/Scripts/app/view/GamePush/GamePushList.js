Ext.define("WX.view.GamePush.GamePushList", {
	extend: 'Ext.container.Container',
	alias: 'widget.GamePushList',
	title: '游戏推送',
	layout: 'hbox',
	align: 'stretch',
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var gamePushStore = Ext.create('WX.store.BaseData.GamePushStore');
		gamePushStore.load();
		var gamePushItemStore = Ext.create('WX.store.BaseData.GamePushItemStore');
		var gamePushMemberStore = Ext.create('WX.store.BaseData.GamePushMemberStore');
		me.items = [{
			xtype: 'grid',
			name: 'gridGamePush',
			title: '游戏推送任务',
			flex: 6,
			height: "100%",
			store: gamePushStore,
			stripeRow: true,
			selType: 'checkboxmodel',
			selModel: {
				selection: 'rowmodel',
				mode: 'single'
			},
			tbar: [
				{
					action: 'addGamePush',
					xtype: 'button',
					text: '添加任务',
					iconCls: 'x-fa fa-plus-circle'
				}, {
					action: 'deleteGamePush',
					xtype: 'button',
					text: '删除任务',
					iconCls: 'x-fa fa-close'
				}, {
					action: 'batchHandGamePush',
					xtype: 'button',
					text: '手动推送',
					scope: this,
					iconCls: 'fa fa-arrow-up',
				}, {
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
						name: 'Title',
						fieldLabel: '标题',
						width: 170,
						labelWidth: 30,
						margin: '0 0 0 5'
					}, {
						xtype: 'combobox',
						fieldLabel: '推送状态',
						name: 'Status',
						width: 175,
						labelWidth: 60,
						margin: '0 0 0 5',
						store: [
							[0, '未推送'],
							[1, '已推送'],
							[-1, '终止推送'],
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
						margin: '0 0 0 5'
					}]
				}
			],
			columns: [
				{ header: '标题', dataIndex: 'Title', flex: 1 },
				{
					header: '推送时间', dataIndex: 'PushDate', flex: 1,
					renderer: Ext.util.Format.dateRenderer('Y-m-d'),
				},
				{
					header: '推送状态', dataIndex: 'Status', flex: 1,
					renderer: function (value) {
						if (value == 0)
							return '<span><font>未推送</font></span>';
						if (value == 1)
							return '<span><font color="green">已推送</font></span>';
						if (value == -1)
							return '<span><font color="red">终止推送</font></span>';
					}
				},
				{
					header: '是否推送所有会员', dataIndex: 'PushAllMembers', flex: 1,
					renderer: function (value) {
						if (value == 0)
							return '<span><font color="red">否</font></span>';
						if (value == 1)
							return '<span><font color="green">是</font></span>';
					}
				},
				{ header: '创建日期', dataIndex: 'CreatedDate', flex: 1 },
				{
					text: '操作功能',
					xtype: 'actioncolumn',
					width: 80,
					sortable: false,
					menuDisabled: true,
					height: 30,
					align: 'center',
					items: [{
						action: 'updateItem',
						iconCls: 'x-fa fa-pencil',
						tooltip: '编辑',
						scope: this,
						margin: '10 10 10 10',
						handler: function (grid, rowIndex, colIndex) {
							var record = grid.getStore().getAt(rowIndex);
							this.fireEvent('updateActionClick', grid, record);
						}
					}]
				}
			],
			bbar: {
				xtype: "pagingtoolbar",
				store: gamePushStore,
				dock: "bottom",
				displayInfo: true
			}
		}, {
			xtype: 'splitter',
		}, {
			xtype: 'container',
			flex: 4,
			layout: { type: 'vbox', align: 'stretch' },
			height: '100%',
			autoScroll: true,
			items: [{
				xtype: 'grid',
				name: 'gridGamePushItem',
				title: '游戏',
				flex: 1,
				stripeRows: true,
				store: gamePushItemStore,
				selType: 'checkboxmodel',
				tbar: [
					{
						action: 'addGamePushItem',
						xtype: 'button',
						text: '添加游戏',
						iconCls: 'x-fa fa-plus-circle',
						margin: '5 5 5 5',
					}, {
						action: 'deleteGamePushItem',
						xtype: 'button',
						text: '删除游戏',
						iconCls: 'x-fa fa-close',
						margin: '5 5 5 5',
					}
				],
				columns: [
					{
						header: '游戏类型', dataIndex: 'GameType', flex: 1,
						renderer: function (value) {
							if (value == 0)
								return "<span>拓客转盘</span>";
							if (value == 1)
								return "<span>活动转盘</span>";
						}
					},
					{
						header: '游戏开始时间', dataIndex: 'StartTime', width: 100,
						renderer: Ext.util.Format.dateRenderer('Y-m-d'),
					},
					{
						header: '游戏结束时间', dataIndex: 'EndTime', width: 100,
						renderer: Ext.util.Format.dateRenderer('Y-m-d'),
					},
					{ header: '周期(天)', dataIndex: 'PeriodDays', flex: 1 },
					{ header: '周期(次)', dataIndex: 'PeriodCounts', flex: 1 },
					{ header: '上限', dataIndex: 'Limit', flex: 1 },
				],
				bbar: {
					xtype: "pagingtoolbar",
					store: gamePushItemStore,
					dock: "bottom",
					displayInfo: true
				}
			}, {
				xtype: 'splitter',
			}, {
				xtype: 'grid',
				name: 'gridGamePushMember',
				title: '会员',
				flex: 1,
				stripeRows: true,
				store: gamePushMemberStore,
				selType: 'checkboxmodel',
				tbar: [
					{
						action: 'addGamePushMember',
						xtype: 'button',
						text: '添加推送会员',
						iconCls: 'x-fa fa-plus-circle',
						margin: '5 5 5 5',
					}, {
						action: 'deleteGamePushMember',
						xtype: 'button',
						text: '删除推送会员',
						iconCls: 'x-fa fa-close',
						margin: '5 5 5 5',
					}
				],
				columns: [
					{ header: '会员名称', dataIndex: 'Name', flex: 1 },
					{ header: '手机号', dataIndex: 'MobilePhoneNo', flex: 1 },
					{ header: '车牌号', dataIndex: 'PlateList', flex: 1 },
				],
				dockedItems: {
					xtype: "pagingtoolbar",
					store: gamePushMemberStore,
					dock: "bottom",
					displayInfo: true
				}
			}]
		}]
		this.callParent();
	}
});