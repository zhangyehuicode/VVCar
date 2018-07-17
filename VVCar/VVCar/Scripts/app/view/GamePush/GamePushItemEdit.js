Ext.define('WX.view.GamePush.GamePushItemEdit', {
	extend: 'Ext.window.Window',
	alias: 'widget.GamePushItemEdit',
	title: '游戏推送子项编辑',
	layout: 'fit',
	width: 600,
	bodyPadding: 5,
	modal: true,
	initComponent: function () {
		var me = this;
		var couponTemplateInfoStore = Ext.create('WX.store.BaseData.GameSettingStore');
		me.grid = Ext.create('Ext.grid.Panel', {
			name: "gameTemplate",
			flex: 1,
			emptyText: '没有数据',
			store: couponTemplateInfoStore,
			stripeRows: true,
			selModel: Ext.create('Ext.selection.CheckboxModel', { model: 'SIMPLE' }),
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
				{ header: '周期(天)', dataIndex: 'PeriodDays', flex: 1},
				{ header: '周期(次)', dataIndex: 'PeriodCounts', flex: 1 },
				{ header: '上限', dataIndex: 'Limit', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		});
		me.items = [me.grid];
		me.buttons = [
			{
				text: '保存',
				action: 'save',
				cls: 'submitBtn',
				scope: me
			},
			{
				text: '取消',
				scope: me,
				handler: me.close
			}
		];
		me.callParent(arguments);
	}
})