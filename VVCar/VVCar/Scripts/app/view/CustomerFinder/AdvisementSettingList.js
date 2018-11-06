Ext.define('WX.view.CustomerFinder.AdvisementSettingList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.AdvisementSettingList',
	title: '广告配置',
	store: Ext.create('WX.store.BaseData.AdvisementSettingStore'),
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
			action: 'addAdvisementSetting',
			xtype: 'button',
			text: '新增广告',
			scope: this,
			iconCls: 'fa fa-plus-circle',
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
			{
				header: '封面图片', dataIndex: 'ImgUrl', width: 100,
				renderer: function (value) {
					if (value != "" && value != null) {
						return '<img src="' + value + '" style="width: 80px; height: 50px;" />';
					}
				}
			},
			{ header: '广告标题', dataIndex: 'Title', flex: 1, },
			{ header: '广告内容', dataIndex: 'Content', flex: 1, },
			{ header: '作者', dataIndex: 'CreatedUser', width: 100 },
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