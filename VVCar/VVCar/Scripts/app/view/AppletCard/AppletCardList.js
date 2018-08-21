﻿Ext.define('WX.view.AppletCard.AppletCardList', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.AppletCardList',
	title: '股东卡',
	store: Ext.create('WX.store.BaseData.CouponTemplateInfoStore'),
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
		this.tbar = [
			{
				action: 'addAppletCard',
				xtype: 'button',
				text: '新增小程序卡券',
				scope: this,
				iconCls: 'fa fa-plus-circle',
			},
		];
		me.columnLines = true;
		this.columns = [
			{ header: '优惠类型', dataIndex: 'CouponTypeName', width: 80, },
			{ header: '编号', dataIndex: 'TemplateCode', width: 110 },
			{ header: '投放时间', dataIndex: 'PutInDate', width: 160 },
			{ header: '标题', dataIndex: 'Title', flex: 1 },
			{ header: '有效时间', dataIndex: 'Validity', flex: 1 },
			{ header: '消费返佣比例', dataIndex: 'ConsumePointRate', width: 100 },
			{ header: '折扣系数', dataIndex: 'DiscountRate', width: 100 },
			{ header: '状态', dataIndex: 'AproveStatusText', flex: 1 },
			{ header: '发行量', dataIndex: 'Stock', flex: 1 },
			{
				header: '分享与赠送', dataIndex: 'CanGiveToPeople', flex: 1,
				renderer: function (value) {
					return value ? '分享' : '无';
				}
			},
			{ header: '备注', dataIndex: 'Remark', flex: 1 },
			{
				header: '创建时间', dataIndex: 'CreatedDate', width: 100,
				renderer: Ext.util.Format.dateRenderer('Y-m-d'),
			},
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
			IsPutApplet: true,
			CouponType: -1,
			AproveStatus: -2,
		}
		Ext.apply(store.proxy.extraParams, params);
		store.load();
	}
});