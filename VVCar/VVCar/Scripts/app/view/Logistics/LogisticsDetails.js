Ext.define('WX.view.Logistics.LogisticsDetails', {
	extend: 'Ext.window.Window',
	alias: 'widget.LogisticsDetails',
	title: '物流详情',
	layout: 'vbox',
	width: 550,
	modal: true,
	bodyPadding: 5,
	initComponent: function () {
		var me = this;

		var logisticsItemStore = Ext.create('WX.store.BaseData.OrderItemStore');

		me.logisticsItemsGrid = {
			title: "订单子项",
			name: "logisticsitemgrid",
			xtype: "grid",
			height: 300,
			width: '100%',
			flex: 1,
			store: logisticsItemStore,
			columns: [
				{ header: '商品名称', dataIndex: 'ProductName', flex: 1 },
				{
					header: '商品图片', dataIndex: 'ImgUrl', width: 100,
					renderer: function (value) {
						if (value != "" && value != null) {
							return '<img src="' + value + '" style="width: 80px; height: 50px;" />';
						}
					}
				},
				{
					header: '类型', dataIndex: 'ProductType', width: 80,
					renderer: function (value) {
						if (value == 1)
							return "商品";
						else if (value == 2)
							return "会员卡";
					}
				},
				{ header: '销售单价', dataIndex: 'PriceSale', width: 80 },
				{ header: '数量', dataIndex: 'Quantity', width: 60 },
				{ header: '销售总价', dataIndex: 'Money', width: 80 },
				//{ header: '兑换积分', dataIndex: 'Points', flex: 1 },
			],
			dockedItems: [{
				xtype: 'pagingtoolbar',
				store: logisticsItemStore,
				dock: 'bottom',
				displayInfo: true
			}]
		};

		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			width: '100%',
			height: 210,
			fieldDefaults: {
				labelAlign: 'left',
				width: 200,
				labelWidth: 70,
				anchor: '100%',
				flex: 1,
				margin: '5',
			},
			items: [{
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					name: 'ID',
					fieldLabel: '订单ID',
					disabled: true,
					hidden: true,
				}, {
					xtype: 'textfield',
					name: 'Code',
					fieldLabel: '订单号',
					disabled: true,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					name: 'Money',
					fieldLabel: '订单金额',
					disabled: true,
				}, {
					xtype: 'textfield',
					name: 'CreatedDate',
					fieldLabel: '订单日期',
					disabled: true,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				tiems: [{
					xtype: 'textfield',
					name: 'LinkMan',
					fieldLabel: '联系人',
					disabled: true,
				}, {
					xtype: 'textfield',
					name: 'Phone',
					fieldLabel: '联系电话',
					disabled: true,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					name: 'Status',
					fieldLabel: '发货状态',
					disabled: true,
				}, {
					xtype: 'textfield',
					name: 'ExpressNumber',
					fieldLabel: '快递单号',
					disabled: true,
				}]
			}, {
				xtype: 'textfield',
				name: 'Address',
				fieldLabel: '收货地址',
				disabled: true,
			}, {
				xtype: 'textfield',
				name: 'Remark',
				fieldLabel: '备注',
				disabled: true,
			}]
		});

		me.items = [me.form, me.logisticsItemsGrid];
		me.callParent(arguments);
	},
});