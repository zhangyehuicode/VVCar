Ext.define('WX.view.Recruitment.RecruitmentList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.RecruitmentList',
	title: '商户管理',
	name: 'gridRecruitment',
	store: Ext.create('WX.store.BaseData.RecruitmentStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	selType: 'checkboxmodel',
	initComponent: function () {
		var me = this;
		me.tbar = [
			{
				action: 'addRecruitment',
				xtype: 'button',
				text: '添加需求',
				scope: this,
				iconCls: 'fa fa-plus-circle',
			}, {
				action: 'deleteRecruitment',
				xtype: 'button',
				text: '删除需求',
				scope: this,
				iconCls: 'fa fa-close',
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
					name: 'Name',
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
			{ header: 'ID', dataIndex: 'id', flex: 1 },
			{ header: '招聘单位', dataIndex: 'Recruiter', flex: 1 },
			{ header: '招聘岗位', dataIndex: 'Position', flex: 1 },
			{ header: '招聘人数', dataIndex: 'RecruitNumber', flex: 1 },
			{ header: '开始时间', dataIndex: 'StartDate', flex: 1 },
			{ header: '结束时间', dataIndex: 'EndDate', flex: 1 },
			{ header: '职位职责要求', dataIndex: 'Requirement', flex: 1 },
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
					tooltip: '修改',
					scope: this,
					margin: '10 10 10 10',
					handler: function (grid, rowIndex, colIndex) {
						var record = grid.getStore().getAt(rowIndex);
						this.fireEvent('editActionClick', grid, record);
					}
				},
					// { scope: this }, {
					//	action: 'deleteItem',
					//	iconCls: 'x-fa fa-close',
					//	tooltip: '删除',
					//	scope: this,
					//	margin: '10 10 10 10',
					//	handler: function (grid, rowIndex, colIndex) {
					//		var record = grid.getStore().getAt(rowIndex);
					//		this.fireEvent('deleteActionClick', grid, record);
					//	},
					//}
				]
			}
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