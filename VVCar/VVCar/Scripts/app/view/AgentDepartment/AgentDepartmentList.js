Ext.define('WX.view.AgentDepartment.AgentDepartmentList', {
	extend: 'Ext.container.Container',
	alias: 'widget.AgentDepartmentList',
	title: '客户资料管理',
	layout: 'hbox',
	align: 'stretch',
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var agentDepartmentStore = Ext.create('WX.store.BaseData.AgentDepartmentStore');
		var treeAgentDepartmentCategoryStore = Ext.create('WX.store.BaseData.AgentDepartmentCategoryTreeStore');
		me.items = [{
			name: 'treeAgentDepartmentCategory',
			xtype: 'treepanel',
			title: '客户资料类别',
			height: '100%',
			width: 200,
			useArrows: true,
			animate: true,
			displayField: 'Text',
			store: treeAgentDepartmentCategoryStore,
			bbar: ['->', {
				action: 'manageAgentDepartmentCategory',
				xtype: 'button',
				text: '分类管理',
				tooltip: '分类管理',
				width: 100,
				margin: '0 0 2 0',
			}, '->']
		}, {
			xtype: 'grid',
			name: 'gridAgendDepartment',
			title: '客户资料列表',
			flex: 1,
			store: agentDepartmentStore,
			stripeRows: true,
			height: '100%',
			tbar: [{
				action: 'addAgentDepartment',
				xtype: 'button',
				text: '添加客户',
				scope: this,
				iconCls: 'fa fa-plus-circle',
			}, {
				action: 'manageTag',
				xtype: 'button',
				text: '客户标签',
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
					fieldLabel: '客户类型',
					name: 'Type',
					width: 175,
					labelWidth: 60,
					margin: '0 0 0 5',
					store: [
						[0, '开发客户'],
						[1, '意向客户'],
					]
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
						action: 'changeCategory',
						xtype: 'button',
						text: '移动至其他分类',
						margin: '0 0 0 5',
						scope: this,
				}]
			}],
			columns: [
				{ header: '名称', dataIndex: 'Name', width: 120, flex: 1 },
				{
					header: '客户类型', dataIndex: 'Type', width: 75,
					renderer: function (value) {
						if (value == 0) {
							return "<span><font>开发客户</font></span>";
						}
						if (value == 1) {
							return "<span><font>意向客户</font></span>";
						}
					}
				},
				{
					header: '审核状态', dataIndex: 'ApproveStatus', width: 75,
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
				//{ header: '注册邮箱', dataIndex: 'Email', width: 140 },
				{ header: '法人(负责人)', dataIndex: 'LegalPerson', width: 100 },
				//{ header: '法人身份证编号', dataIndex: 'IDNumber', width: 150 },
				{ header: '联系电话', dataIndex: 'MobilePhoneNo', width: 110 },
				//{ header: '开户行', dataIndex: 'Bank', flex: 4 },
				//{ header: '账号', dataIndex: 'BankCard', width: 170 },
				{ header: '销售经理', dataIndex: 'UserName', width: 80 },
				//{
				//	header: '营业执照', dataIndex: 'BusinessLicenseImgUrl', width: 100,
				//	renderer: function (value) {
				//		if (value != "" && value != null) {
				//			return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 80px; height: 50px;" /></a>';
				//		}
				//	},
				//},
				//{
				//	header: '门店照片', dataIndex: 'DepartmentImgUrl', width: 100,
				//	renderer: function (value) {
				//		if (value != "" && value != null) {
				//			return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 80px; height: 50px;" /></a>';
				//		}
				//	},
				//},
				//{
				//	header: '法人身份证(正)', dataIndex: 'LegalPersonIDCardFrontImgUrl', width: 110,
				//	renderer: function (value) {
				//		if (value != "" && value != null) {
				//			return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 80px; height: 50px;" /></a>';
				//		}
				//	},
				//},
				//{
				//	header: '法人身份证(反)', dataIndex: 'LegalPersonIDCardBehindImgUrl', width: 110,
				//	renderer: function (value) {
				//		if (value != "" && value != null) {
				//			return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 80px; height: 50px;" /></a>';
				//		}
				//	},
				//},
				{ header: '公司地址', dataIndex: 'CompanyAddress', width: 100 },
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
						action: 'editItem',
						iconCls: 'x-fa fa-pencil',
						tooltip: '编辑',
						scope: this,
						margin: '10 10 10 10',
						handler: function (grid, rowIndex, colIndex) {
							var record = grid.getStore().getAt(rowIndex);
							this.fireEvent('editActionClick', grid, record);
						}
					}, { scopre: this }, {
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
			dockedItems: [{
				xtype: 'pagingtoolbar',
				store: agentDepartmentStore,
				dock: 'bottom',
				displayInfo: true
			}]
		}];
		this.callParent(arguments);
	}
})