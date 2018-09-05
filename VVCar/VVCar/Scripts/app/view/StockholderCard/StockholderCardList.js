Ext.define('WX.view.StockholderCard.StockholderCardList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.StockholderCardList',
	title: '股东卡',
	store: Ext.create('WX.store.BaseData.MemberStore'),
	stripeRows: true,
	loadMask: true,
	closable: true,
	selType: 'checkboxmodel',
	selModel: {
		selection: 'rowmodel',
		mode: 'single'
	},
	initComponent: function () {
		var me = this;
		me.rowEditing = Ext.create('Ext.grid.plugin.RowEditing', {
			saveBtnText: '保存',
			cancelBtnText: "取消",
			autoCancel: false,
			listeners: {
				cancelEdit: function (rowEditing, context) {
					//如果是新增的数据，则删除
					if (context.record.phantom) {
						me.store.remove(context.record);
					}
				},
				beforeedit: function (editor, context, eOpts) {
					if (editor.editing == true)
						return false;
				},
			}
		});
		this.tbar = [
			{
				action: 'addStockholderCard',
				xtype: 'button',
				text: '新增股东',
				scope: this,
				iconCls: 'fa fa-plus-circle',
			},
		];
		me.columnLines = true;
		me.plugins = [me.rowEditing];
		this.columns = [
			//{ header: 'ID', dataIndex: 'ID', flex: 1 },
			//{
			//	header: '卡券类型', dataIndex: 'Nature', flex: 1,
			//	renderer: function (value) {
			//		if (value == 0) return '优惠券';
			//		if (value == 1) return '会员卡';
			//	}
			//},
			//{ header: '优惠类型', dataIndex: 'CouponTypeName', width: 80, },
			//{ header: '编号', dataIndex: 'TemplateCode', width: 110 },
			//{ header: '投放时间', dataIndex: 'PutInDate', width: 160 },
			//{ header: '标题', dataIndex: 'Title', flex: 1 },
			//{ header: '有效时间', dataIndex: 'Validity', flex: 1 },
			//{ header: '消费返佣比例', dataIndex: 'ConsumePointRate', width: 100, editor: { xtype: 'textfield', allowBlank: true } },
			//{ header: '折扣系数', dataIndex: 'DiscountRate', width: 100, editor: { xtype: 'textfield', allowBlank: true } },
			//{ header: '状态', dataIndex: 'AproveStatusText', flex: 1 },
			//{ header: '发行量', dataIndex: 'Stock', flex: 1 },
			//{
			//	header: '分享与赠送', dataIndex: 'CanGiveToPeople', flex: 1,
			//	renderer: function (value) {
			//		return value ? '分享' : '无';
			//	}
			//},
			////{ header: '库存', dataIndex: 'FreeStock', flex: 1 },
			//{ header: '备注', dataIndex: 'Remark', flex: 1 },
			//{
			//	header: '创建时间', dataIndex: 'CreatedDate', width: 100,
			//	renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			//},
			{ header: '会员卡号', dataIndex: 'CardNumber', flex: 1, },
			{ header: '会员姓名', dataIndex: 'Name', flex: 1 },
			{ header: "余额/元", dataIndex: "CardBalance", width: 70, xtype: "numbercolumn" },
			{ header: '手机号码', dataIndex: 'MobilePhoneNo', flex: 1 },
			{ header: '车牌号', dataIndex: 'PlateList', flex: 1 },
			//{ header: '是否是股东', dataIndex: 'IsStockholder', width: 100 },
			{ header: '返佣比例(%)', dataIndex: 'ConsumePointRate', width: 100, editor: { xtype: 'numberfield', allowBlank: true } },
			{ header: '折扣系数(%)', dataIndex: 'DiscountRate', width: 100, editor: { xtype: 'numberfield', allowBlank: true }},
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
			displayInfo: true
		}];
		this.callParent();
	},
	afterRender: function () {
		this.callParent(arguments);
		var store = this.getStore();
		var params = {
			Nature: 1,
			CouponType: -1,
			AproveStatus: -2,
			IsStockholderCard: true,
		}
		Ext.apply(store.proxy.extraParams, params);
		store.load();
	}
});
