Ext.define('WX.view.CustomerFinder.AdvisementBrowseHistoryList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.AdvisementBrowseHistoryList',
	title: '寻客侠广告浏览记录',
	store: Ext.create('WX.store.BaseData.AdvisementBrowseHistoryStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	selType: 'checkboxmodel',
	selModel: {
		selection: 'rowmodel',
		model: 'single',
	},
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
				xtype: 'textfield',
				name: 'Title',
				fieldLabel: '标题',
				width: 170,
				labelWidth: 30,
				margin: '0 0 0 5',
			}, {
				xtype: 'textfield',
				name: 'NickName',
				fieldLabel: '会员昵称',
				width: 170,
				labelWidth: 60,
				margin: '0 0 0 5',
			}, {
				xtype: 'numberfield',
				name: 'Period',
				fieldLabel: '大于间隔时间',
				width: 170,
				labelWidth: 90,
				value: 0,
				minValue: 0,
				margin: '0 0 0 5',
			},{
				action: 'search',
				xtype: 'button',
				text: '搜索',
				iconCls: 'fa fa-search',
				cls: 'submitBtn',
				margin: '0 0 0 5',
			}]
		}];
		me.columnLines = true;
		me.columns = [
			{ header: '广告标题', dataIndex: 'Title', flex: 1, },
			{ header: '分享者', dataIndex: 'ShareNickName', flex: 1 },
			{ header: '会员名称', dataIndex: 'NickName', flex: 1 },
			{ header: '开始浏览时间', dataIndex: 'StartDate', flex: 1,},
			{ header: '结束浏览时间', dataIndex: 'EndDate', flex:1,},
			{ header: '时间间隔(秒)', dataIndex: 'Period', flex: 1, },
			{
				header: '创建时间', dataIndex: 'CreatedDate', width: 100,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
			{
				text: '操作',
				xtype: 'actioncolumn',
				width: 100,
				sortable: false,
				menuDisabled: true,
				height: 30,
				align: 'center',
				items: [{
					action: 'deleteItem',
					iconCls: 'x-fa fa-close',
					tooltip: '删除',
					scope: this,
					margin: '5 5 5 5',
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('deleteActionClick', grid, record);
					}
				}]
			}
		];
		me.dockedItems = [{
			xtype: 'pagingtoolbar',
			store: me.store,
			dock: 'bottom',
			displayInfo: true,
		}];
		me.callParent();
	},
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
		var store = this.getStore();
		store.load();
	}
});