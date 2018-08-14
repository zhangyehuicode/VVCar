﻿Ext.define('WX.view.Merchant.MerchantList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.MerchantList',
	title: '商户管理',
	name: 'gridMerchant',
	store: Ext.create('WX.store.BaseData.MerchantStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	viewConfig: { enableTextSelection: true },
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
					xtype: 'combobox',
					fieldLabel: '是否代理商',
					name: 'IsAgent',
					width: 175,
					labelWidth: 70,
					margin: '0 0 0 5',
					store: [
						[0, '否'],
						[1, '是'],
					]
				}, {
					xtype: 'combobox',
					fieldLabel: '激活状态',
					name: 'Status',
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
			{ header: '商户号', dataIndex: 'Code', width: 90 },
			{ header: '名称', dataIndex: 'Name', width: 160 },
			{
				header: '代理商', dataIndex: 'IsAgent', width: 70,
				renderer: function (value) {
					if (value == true)
						return '<span><font color="green">是</font></span>';
					if (value == false)
						return '<span><font color="red">否</font></span>';
				}
			},
			{
				header: '普通商户', dataIndex: 'IsGeneralMerchant', width: 80,
				renderer: function (value) {
					if (value == true)
						return '<span><font color="green">是</font></span>';
					if (value == false)
						return '<span><font color="red">否</font></span>';
				}
			},
			{
				header: '商户状态', dataIndex: 'Status', width: 80,
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
			{ header: '注册邮箱', dataIndex: 'Email', width: 140 },
			{ header: '法人(负责人)', dataIndex: 'LegalPerson', width: 100 },
			//{ header: '法人身份证编号', dataIndex: 'IDNumber', width: 150 },
			{ header: '联系电话', dataIndex: 'MobilePhoneNo', width: 110 },
			//{ header: '开户行', dataIndex: 'Bank', flex: 4 },
			//{ header: '账号', dataIndex: 'BankCard', width: 170 },
			//{
			//	header: '二维码', dataIndex: 'WeChatQRCodeImgUrl', width: 80,
			//	renderer: function (value) {
			//		if (value != "" && value != null) {
			//			return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 50px; height: 50px;" /></a>';
			//		}
			//	},
			//},
			{
				header: '营业执照', dataIndex: 'BusinessLicenseImgUrl', width: 100,
				renderer: function (value) {
					if (value != "" && value != null) {
						return '<a href="' + value + '"download="' + value + '"><img src="' + value + '" style="width: 80px; height: 50px;" /></a>';
					}
				},
			},
			{
				header: '门店照片', dataIndex: 'DepartmentImgUrl', width: 100,
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
				header: '认证到期', dataIndex: 'ExpireDate', width: 100,
				renderer: function (value) {
					if (value == null || value == '')
						return '未知';
					var now = new Date();
					var expireDate = new Date(value);
					//var year = value.substring(0, 4);
					//var month = value.substring(5, 7) < 10 ? value.substring(6, 7) : value.substring(5, 7);
					//var day = value.substring(8, 10) < 10 ? value.substring(9, 10) : value.substring(8, 10);
					var year = expireDate.getFullYear();
					var month = expireDate.getMonth() + 1;
					var day = expireDate.getDate();
					if (now.getFullYear() == year) {
						if ((now.getMonth() + 1) == month) {
							if ((now.getDate()) < day) {
								return '<span><font color="green">' + year + '-' + month + '-' + day + '</color></span>';
							} else {
								return '<span><font color="red">' + year + '-' + month + '-' + day + '</color></span>';
							}
						} else if ((now.getMonth() + 1) < month) {
							return '<span><font color="green">' + year + '-' + month + '-' + day + '</color></span>';
						} else {
							return '<span><font color="red">' + year + '-' + month + '-' + day + '</color></span>';
						}
					} else if (now.getFullYear() < year) {
						return '<span><font color="green">' + year + '-' + month + '-' + day + '</color></span>';
					} else {
						return '<span><font color="red">' + year + '-' + month + '-' + day + '</color></span>';
					}
				}
			},
			{
				header: '激活时间', dataIndex: 'ActivateDate', width: 100,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
			{
				header: '创建时间', dataIndex: 'CreatedDate', width: 100,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
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