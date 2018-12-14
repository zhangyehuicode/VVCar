Ext.define('WX.view.Coupon.CouponPush', {
	extend: 'Ext.container.Container',
	alias: 'widget.CouponPush',
	title: '卡券立即推送',
	layout: 'vbox',
	align: 'stretch',
	width: '100%',
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var memberStore = Ext.create('WX.store.BaseData.MemberStore');
		var memberGroupStore = Ext.create('WX.store.BaseData.MemberGroupTreeStore');
		var couponStore = Ext.create('WX.store.BaseData.CouponStore');
		me.items = [{
			xtype: 'grid',
			name: 'coupontemplate',
			title: '卡券信息',
			width: '100%',
			store: couponStore,
			stripeRows: true,
			height: '15%',
			columns: [
				{
					header: '卡券类型', dataIndex: 'Nature', width: 80,
					renderer: function (value) {
						if (value == 0) {
							return '优惠券';
						}
						if (value == 1) {
							return '会员卡';
						}
					}
				},
				{ header: '优惠类型', dataIndex: 'CouponTypeName', width: 80 },
				{ header: '编号', dataIndex: 'TemplateCode', width: 110 },
				{
					header: '创建时间', dataIndex: 'CreatedDate', width: 90,
					renderer: Ext.util.Format.dateRenderer('Y-m-d'),
				},
				{ header: '投放时间', dataIndex: 'PutInDate', width: 170 },
				{ header: '标题', dataIndex: 'Title', flex: 1 },
				{ header: '有效期', dataIndex: 'Validity', width: 180 },
				{ header: '状态', dataIndex: 'AproveStatusText', flex: 1 },
				{ header: '发行量', dataIndex: 'Stock', width: 70 },
				{
					header: '赠送', dataIndex: 'CanGiveToPeople', width: 50,
					renderer: function (value) {
						if (value) {
							return '<span><font color="green">是</font></span>';
						} else {
							return '<span color="red"><font color="red">否</font></span>';
						}
					}
				},
				{
					header: '分享', dataIndex: 'CanShareByPeople', width: 50,
					renderer: function (value) {
						if (value) {
							return '<span><font color="green">是</font></span>';
						} else {
							return '<span color="red"><font color="red">否</font></span>';
						}
					}
				},
				{ header: '库存', dataIndex: 'FreeStock', width: 70 },
				{ header: '消费返积分比例', dataIndex: 'ConsumePointRate', width: 　120 },
				{ header: '抽成比例', dataIndex: 'CommissionRate', width: 80 },
				{ header: '备注', dataIndex: 'Remark', flex: 1 },
			],
		}, {
			xtype: 'container',
			layout: 'hbox',
			width: '100%',
			height: '85%',
			items: [{
				name: 'treeMemberGroup',
				xtype: 'treepanel',
				title: '会员类别',
				height: '100%',
				width: '15%',
				useArrows: true,
				animate: true,
				displayField: 'Text',
				store: memberGroupStore,
			}, {
				xtype: 'grid',
				name: 'gridMember',
				title: '会员列表',
				width: '85%',
				store: memberStore,
				stripeRows: true,
				height: '100%',
				selType: 'checkboxmodel',
				tbar: [{
					xtype: 'form',
					layout: 'column',
					border: false,
					frame: false,
					labelAlign: 'left',
					buttonAlign: 'right',
					labelWidth: 80,
					autoWidth: true,
					autoScroll: false,
					columnWidth: 1,
					items: [{
						xtype: 'textfield',
						name: 'Keyword',
						fieldLabel: '关键字',
						labelWidth: 60,
						width: 240,
						emptyText: '电话/姓名',
						margin: "0 0 0 5"
					}, {
						action: 'search',
						xtype: 'button',
						text: '搜 索',
						iconCls: 'fa fa-search',
						cls: 'submitBtn',
						margin: '0 0 0 5',
					}, {
						action: 'grouppush',
						xtype: 'button',
						text: '按分类推送',
						cls: 'submitBtn',
						margin: '0 0 0 5',
					}, {
						action: 'batchpush',
						xtype: 'button',
						text: '批量选择推送',
						cls: 'submitBtn',
						margin: '0 0 0 5',
					}]
				}],
				columns: [
					{ header: "会员卡号", dataIndex: "CardNumber", width: 80 },
					{ header: "姓名", dataIndex: "Name", minWidth: 80, flex: 1 },
					{ header: "余额/元", dataIndex: "CardBalance", width: 70, xtype: "numbercolumn" },
					{ header: "手机号码", dataIndex: "MobilePhoneNo", width: 105 },
					{ header: "车牌号", dataIndex: "PlateList", minWidth: 80, flex: 1, permissionCode: 'Member.Member.DepartmentColumn' },
					{ header: "门店名称", dataIndex: "DepartmentName", minWidth: 100, flex: 1, permissionCode: 'Member.Member.AgentColumn' },
					{ header: "门店地址", dataIndex: "DepartmentAddress", minWidth: 100, flex: 1, permissionCode: 'Member.Member.AgentColumn' },
					{ header: "会员状态", dataIndex: "Status", width: 75, permissionCode: 'Member.Member.DepartmentColumn' },
					{ header: '保险到期', dataIndex: 'InsuranceExpirationDate', xtype: "datecolumn", format: 'Y-m-d', width: 90, permissionCode: 'Member.Member.DepartmentColumn' },
					{ header: "注册时间", dataIndex: "CreatedDate", xtype: "datecolumn", format: "Y-m-d", minWidth: 90 },
					{
						text: '操作功能',
						xtype: 'actioncolumn',
						width: 80,
						sortable: false,
						menuDisabled: true,
						height: 30,
						align: 'center',
						items: [{
							action: 'push',
							iconCls: 'x-fa fa-arrow-right',
							tooltip: '推送',
							scope: this,
							margin: '10 10 10 10',
							handler: function (grid, rowIndex, colIndex) {
								var record = grid.getStore().getAt(rowIndex);
								this.fireEvent('pushActionClick', grid, record);
							}
						}],
					}
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: me.store,
					dock: 'bottom',
					displayInfo: true,	
				}]
			}]
		}];
		this.callParent(arguments);
	}
});