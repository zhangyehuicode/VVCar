Ext.define('WX.view.PickUpOrder.PickUpOrderDetails', {
	extend: 'Ext.window.Window',
	alias: 'widget.PickUpOrderDetails',
	title: '接车单详情',
	layout: 'vbox',
	width: 550,
	modal: true,
	bodyPadding: 5,
	initComponent: function () {
		var me = this;
		var orderItemStore = Ext.create('WX.store.BaseData.PickUpOrderItemStore');
		me.orderItemsGrid = {
			title: "接车单子项",
			name: "pickuporderitemgrid",
			xtype: "grid",
			height: 300,
			width: '100%',
			flex: 1,
			store: orderItemStore,
			plugins: [
				Ext.create('Ext.grid.plugin.RowEditing', {
					saveBtnText: '保存',
					cancelBtnText: '取消',
					antoCancel: false,
					listeners: {
						cancelEdit: function (rowEditing, context) {
							if (context.record.phantom) {
								me.store.remove(context.record);
							}
						},
						beforeedit: function (editor, context, eOpts) {
							if (editor.editing == true)
								return false;
						}
					}
				})
			],
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
				//{
				//	header: '类型', dataIndex: 'ProductType', width: 80,
				//	renderer: function (value) {
				//		if (value == 1)
				//			return "商品";
				//		else if (value == 2)
				//			return "会员卡";
				//	}
				//},
				{ header: '销售单价', dataIndex: 'PriceSale', width: 80 },
				{ header: '数量', dataIndex: 'Quantity', width: 60, editor: { xtype: 'numberfield', allowBlank: false, minValue:1 }},
				{ header: '销售总价', dataIndex: 'Money', width: 80 },
			],
			dockedItems: [{
				xtype: 'pagingtoolbar',
				store: orderItemStore,
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
					readOnly: true,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					name: 'PlateNumber',
					fieldLabel: '车牌号',
					readOnly: true,
				}, {
					xtype: 'textfield',
					name: 'StaffName',
					fieldLabel: '开单店员',
					readOnly: true,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					name: 'Money',
					fieldLabel: '订单总额',
					
				}, {
					xtype: 'textfield',
					name: 'ReceivedMoney',
					fieldLabel: '已收金额',
					readOnly: true,
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					name: 'StillOwedMoney',
					fieldLabel: '尚欠金额',
					readOnly: true,
				}, {
					xtype: 'textfield',
					name: 'Status',
					fieldLabel: '定单状态',
					readOnly: true,
				}]
			},{
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					name: 'MemberName',
					fieldLabel: '会员名称',
					readOnly: true,
				}, {
					xtype: 'textfield',
					name: 'MemberMobilePhoneNo',
					fieldLabel: '会员手机号',
					readOnly: true,
				}]
			}]
		});

		me.items = [me.form, me.orderItemsGrid];
		me.callParent(arguments);
	},
});