Ext.define('WX.view.AgentDepartmentAudit.AgentDepartmentAuditList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.AgentDepartmentAuditList',
	title: '商户管理',
	name: 'gridAgentDepartment',
	store: Ext.create('WX.store.BaseData.AgentDepartmentAuditStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
	selType: 'checkboxmodel',
	selModel: {
		selection: 'rowmodel',
		mode: 'single'
	},
	initComponent: function () {
		var me = this;
		me.tbar = [
			{
				action: 'auditAgentDepartment',
				xtype: 'button',
				text: '商户门店审核',
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
						[0, '未激活'],
						[1, '已激活'],
						[-1, '已冻结'],
					]
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
			{ header: '名称', dataIndex: 'Name', width: 150 },
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
			{ header: '注册邮箱', dataIndex: 'Email', flex: 3 },
			{ header: '法人(负责人)', dataIndex: 'LegalPerson', width: 100 },
			{ header: '法人身份证编号', dataIndex: 'IDNumber', width: 160 },
			{ header: '联系电话', dataIndex: 'MobilePhoneNo', width: 110 },
			{ header: '开户行', dataIndex: 'Bank', flex: 4 },
			{ header: '账号', dataIndex: 'BankCard', width: 170 },
			{ header: '销售经理', dataIndex: 'UserName', width: 100 },
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
				header: '创建时间', dataIndex: 'CreatedDate', width: 100,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
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