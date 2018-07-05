Ext.define('WX.view.AgentDepartment.AgentDepartmentList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.AgentDepartmentList',
	title: '商户管理',
	name: 'gridAgentDepartment',
	store: Ext.create('WX.store.BaseData.AgentDepartmentStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	selType: 'checkboxmodel',
	initComponent: function () {
		var me = this;
		me.tbar = [
			{
				action: 'addAgentDepartment',
				xtype: 'button',
				text: '添加门店',
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
					name: 'Name',
					fieldLabel: '名称',
					width: 170,
					labelWidth: 30,
					margin: '0 0 0 5',
				}, {
					xtype: 'combobox',
					fieldLabel: '审核状态',
					name: 'ApproveStatus',
					width: 175,
					labelWidth: 60,
					margin: '0 0 0 5',
					store: [
						[0, '未审核'],
						[1, '已审核'],
						[2, '已导入'],
					]
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'fa fa-search',
					cls: 'submitBtn',
					margin: '0 0 0 5',
				}, {
					//action: 'export',
					//xtype: 'button',
					//text: '导出',
					//iconCls: '',
					//margin: '0 0 0 5',
				}]
			},
		];
		me.columns = [
			{ header: '名称', dataIndex: 'Name', width: 150, },
			{
				header: '审核状态', dataIndex: 'ApproveStatus', width: 100,
				renderer: function (value) {
					if (value == 0) {
						return "<span><font>未审核</font></span>";
					}
					if (value == 1) {
						return "<span><font color='green'>已审核</font></span>";
					}
					if (value == 2) {
						return "<span><font color='red'>已导入</font></span>";
					}
				}
			},
			{ header: '注册邮箱', dataIndex: 'Email', width: 140 },
			{ header: '法人(负责人)', dataIndex: 'LegalPerson', width: 100 },
			{ header: '法人身份证编号', dataIndex: 'IDNumber', width: 150 },
			{ header: '联系电话', dataIndex: 'MobilePhoneNo', width: 110 },
			//{ header: '开户行', dataIndex: 'Bank', flex: 4 },
			//{ header: '账号', dataIndex: 'BankCard', width: 170 },
			{ header: '销售经理', dataIndex: 'UserName', width: 80 },
			{
				header: '营业执照', dataIndex: 'BusinessLicenseImgUrl', width: 100,
				renderer: function (value) {
					if (value != "" && value != null) {
						return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 80px; height: 50px;" /></a>';
					}
				},
			},
			{
				header: '法人身份证(正)', dataIndex: 'LegalPersonIDCardFrontImgUrl', width: 110,
				renderer: function (value) {
					if (value != "" && value != null) {
						return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 80px; height: 50px;" /></a>';
					}
				},
			},
			{
				header: '法人身份证(反)', dataIndex: 'LegalPersonIDCardBehindImgUrl', width: 110,
				renderer: function (value) {
					if (value != "" && value != null) {
						return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 80px; height: 50px;" /></a>';
					}
				},
			},
			{ header: '公司地址', dataIndex: 'CompanyAddress', flex: 2 },
			{
				header: '创建时间', dataIndex: 'CreatedDate', width: 90,
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