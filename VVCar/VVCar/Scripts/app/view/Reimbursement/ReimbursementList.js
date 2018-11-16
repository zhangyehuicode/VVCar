Ext.define('WX.view.Reimbursement.ReimbursementList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.ReimbursementList',
	title: '商户管理',
	name: 'gridReimbursement',
	store: Ext.create('WX.store.BaseData.ReimbursementStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	selType: 'checkboxmodel',
	initComponent: function () {
		var me = this;
		me.tbar = [
			{
				action: 'addReimbursement',
				xtype: 'button',
				text: '添加报销单',
				scope: this,
				iconCls: 'fa fa-plus-circle',
			}, {
				action: 'delReimbursement',
				xtype: 'button',
				text: '删除报销单',
				scope: this,
				iconCls: 'fa fa-key',
				permissionCode: 'Reimbursement.Delete',
			}, {
				action: 'approveReimbursement',
				xtype: 'button',
				text: '审核',
				scope: this,
				iconCls: 'fa fa-key',
				permissionCode: 'Reimbursement.Approve',
			}, {
				action: 'antiApproveReimbursement',
				xtype: 'button',
				text: '反审核',
				scope: this,
				iconCls: 'fa fa-key',
				permissionCode: 'Reimbursement.Approve',
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
					name: 'Project',
					fieldLabel: '项目',
					width: 170,
					labelWidth: 30,
					margin: '0 0 0 5',
				}, {
					xtype: 'combobox',
					fieldLabel: '审核状态',
					name: 'Status',
					width: 175,
					labelWidth: 60,
					margin: '0 0 0 5',
					store: [
						[0, '未审核'],
						[1, '已审核'],
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
			},
		];
		me.columns = [
			{ header: '编号', dataIndex: 'Code', flex: 1},
			{ header: '项目', dataIndex: 'Project', flex: 1 },
			{
				header: '审核状态', dataIndex: 'Status', width: 100,
				renderer: function (value) {
					if (value == 0)
						return '<span><font color="red">未审核</font></span>';
					if (value == 1)
						return '<span><font color="green">已审核</font></span>';
				}
			},
			{
				header: '票据', dataIndex: 'ImgUrl', width: 180,
				renderer: function (value) {
					if (value != "" && value != null) {
						return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 150px; height: 50px;" /></a>';
					}
				},
			},
			{ header: '报销人', dataIndex: 'UserName', flex: 1 },
			{ header: '报销金额', dataIndex: 'Money', width: 150 },
			{ header: '备注', dataIndex: 'Remark', flex: 2 },
			{
				header: '创建时间', dataIndex: 'CreatedDate', width: 100,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
			//{
			//	text: '操作功能',
			//	xtype: 'actioncolumn',
			//	width: 80,
			//	sortable: false,
			//	menuDisabled: true,
			//	height: 30,
			//	align: 'center',
			//	items: [{
			//		action: 'updateItem',
			//		iconCls: 'x-fa fa-pencil',
			//		tooltip: '修改',
			//		scope: this,
			//		margin: '10 10 10 10',
			//		handler: function (grid, rowIndex, colIndex) {
			//			var record = grid.getStore().getAt(rowIndex);
			//			this.fireEvent('editActionClick', grid, record);
			//		}
			//	},
			//		 { scope: this }, {
			//			action: 'deleteItem',
			//			iconCls: 'x-fa fa-close',
			//			tooltip: '删除',
			//			scope: this,
			//			margin: '10 10 10 10',
			//			handler: function (grid, rowIndex, colIndex) {
			//				var record = grid.getStore().getAt(rowIndex);
			//				this.fireEvent('deleteActionClick', grid, record);
			//			},
			//		}
			//	]
			//}
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