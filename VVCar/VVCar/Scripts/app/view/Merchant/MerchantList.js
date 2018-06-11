Ext.define('WX.view.Merchant.MerchantList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.MerchantList',
	title: '商户管理',
	name: 'gridMerchant',
	store: Ext.create('WX.store.BaseData.MerchantStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	selType: 'checkboxmodel',
	initComponent: function () {
		var me = this;
		me.tbar = [
			{
				action: 'addMerchant',
				xtype: 'button',
				text: '添加商户',
				scope: this,
				iconCls: 'fa fa-plus-circle',
			}, {
				action: 'activateMerchant',
				xtype: 'button',
				text: '激活商户',
				scope: this,
				iconCls: 'fa fa-key',
				permissionCode: 'Merchant.ActivateMerchant',
			}, {
				action: 'freezeMerchant',
				xtype: 'button',
				text: '冻结商户',
				scope: this,
				iconCls: 'fa fa-lock',
				permissionCode: 'Merchant.ActivateMerchant',
			},
		];
		me.columns = [
			{ header: '商户号', dataIndex: 'Code', flex: 1 },
			{ header: '名称', dataIndex: 'Name', flex: 2 },
			{
				header: '商户状态', dataIndex: 'Status', flex: 2,
				renderer: function (value) {
					if (value == 0) {
						return "<span><font>未激活</font></span>";
					}
					if (value == 1) {
						return "<span><font color='green'>已激活</font></span>";
					}
					if (value == -1) {
						return "<span><font color='red'>已冻结</font></span>";
					}
				}
			},
			{ header: '注册邮箱', dataIndex: 'Email', flex: 2 },
			{ header: '法人(负责人)', dataIndex: 'LegalPerson', flex: 2 },
			{ header: '法人身份证编号', dataIndex: 'IDNumber', flex: 2 },
			{ header: '联系电话', dataIndex: 'MobilePhoneNo', flex: 2 },
			{ header: '开户行', dataIndex: 'Bank', flex: 2 },
			{ header: '账号', dataIndex: 'BankCard', flex: 2 },
			{
				header: '营业执照', dataIndex: 'BusinessLicenseImgUrl', width: 120,
				renderer: function (value) {
					if (value != "" && value != null) {
						return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 80px; height: 50px;" /></a>';
					}
				},
			},
			{
				header: '法人身份证(正)', dataIndex: 'LegalPersonIDCardFrontImgUrl', width: 120,
				renderer: function (value) {
					if (value != "" && value != null) {
						return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 80px; height: 50px;" /></a>';
					}
				},
			},
			{
				header: '法人身份证(反)', dataIndex: 'LegalPersonIDCardBehindImgUrl', width: 120,
				renderer: function (value) {
					if (value != "" && value != null) {
						return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 80px; height: 50px;" /></a>';
					}
				},
			},
			{ header: '公司地址', dataIndex: 'CompanyAddress', flex: 2 },
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