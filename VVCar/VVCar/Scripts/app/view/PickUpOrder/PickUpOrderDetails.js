Ext.define('WX.view.PickUpOrder.PickUpOrderDetails', {
	extend: 'Ext.window.Window',
	alias: 'widget.PickUpOrderDetails',
	title: '接车单详情',
	layout: 'vbox',
	width: 1150,
	modal: true,
	bodyPadding: 5,
	initComponent: function () {
		var me = this;
		me.container = {
			xtype: "container",
			name: 'details',
			flex: 1,
			layout: {
				type: 'hbox', align: 'stretch'
			},
			width: '100%',
			height: 300,
			autoScroll: true,
			items: [{
				title: '项目',
				name: 'pickuporderitemgrid',
				xtype: 'grid',
				flex: 7,
				store: Ext.create('WX.store.BaseData.PickUpOrderItemStore'),
				tbar: [{
					action: 'addPickUpOrderItem',
					xtype: 'button',
					text: '新增项目',
					iconCls: 'fa fa-plus-circle',
					margin: '5 5 5 5',
				}, {
					action: 'delPickUpOrderItem',
					xtype: 'button',
					text: '删除项目',
					iconCls: 'x-fa fa-close',
					margin: '5 5 5 5',
				}],
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
					{ header: '项目名称', dataIndex: 'ProductName', flex: 1 },
					{
						header: '项目图片', dataIndex: 'ImgUrl', width: 100,
						renderer: function (value) {
							if (value != "" && value != null) {
								return '<img src="' + value + '" style="width: 50px; height: 20px;" />';
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
					{
						header: '是否优惠', dataIndex: 'IsReduce', width: 80,
						renderer: function (value) {
							return '<input type="checkbox" disabled="disabled"' + (value == "1" ? " checked" : "") + '/>';
						},
						editor: { xtype: 'checkboxfield' },
					},
					{ header: '优惠价', dataIndex: 'ReducedPrice', width: 80, editor: { xtype: 'numberfield', allowBlank: false } },
					{ header: '数量', dataIndex: 'Quantity', width: 70, editor: { xtype: 'numberfield', allowBlank: false, minValue: 1 } },
					{ header: '备注', dataIndex: 'Remark', flex: 1, editor: { xtype: 'textfield', allowBlank: true }},
					{ header: '销售总价', dataIndex: 'Money', width: 80 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}, {
				xtype: 'splitter'
			}, {
				flex: 3,
				title: '人员',
				name: 'pickuporderitemusergrid',
				xtype: 'grid',
				store: Ext.create('WX.store.BaseData.PickUpOrderTaskDistributionStore'),
				tbar: [{
					action: 'addPickUpOrderItemCrew',
					xtype: 'button',
					text: '新增施工员',
					margin: '5 5 5 5',
				}, {
					action: 'addPickUpOrderItemSalesman',
					xtype: 'button',
					text: '新增业务员',
					margin: '5 5 5 5',
				}, {
					action: 'delPickUpOrderItemUser',
					xtype: 'button',
					text: '删除',
					margin: '5 5 5 5',
				}],
				columns: [
					{
						header: '用户类型', dataIndex: 'PeopleType', flex: 1,
						renderer: function (value) {
							if (value == 0) {
								return '施工员';
							}
							if (value == 1) {
								return '业务员';
							}
						}
					},
					{ header: '用户名称', dataIndex: 'UserName', flex: 1 },
					{ header: '抽成', dataIndex: 'Commission', flex: 1 },
				],
				dockedItems: [{
					xtype: 'pagingtoolbar',
					store: this.store,
					dock: 'bottom',
					displayInfo: true
				}]
			}]
		};
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			name: 'pickuporderformpanel',
			width: '100%',
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
				name: 'form1',
				layout: 'hbox',
				items: [{
					name: 'ID',
					xtype: 'textfield',
					fieldLabel: '接车单ID',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
					hidden: true,
				}, {
					xtype: 'textfield',
					name: 'Code',
					fieldLabel: '订单号',
					readOnly: true,
				}, {
					xtype: 'textfield',
					name: 'PlateNumber',
					fieldLabel: '车牌号',
					readOnly: true,
				}, {
					xtype: 'textfield',
					name: 'MemberID',
					fieldLabel: '会员ID',
					readOnly: true,
					hidden: true,
				}, {
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
			}, {
				xtype: 'form',
				name: 'form2',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					name: 'CardNumber',
					fieldLabel: '储值卡号',
					readOnly: true,
				}, {
					xtype: 'textfield',
					name: 'CardStatus',
					fieldLabel: '是否激活',
					readOnly: true,
				}, {
					xtype: 'textfield',
					name: 'EffectiveDate',
					fieldLabel: '生效日期',
					readOnly: true,
				}, {
					xtype: 'numberfield',
					fieldLabel: '储值余额',
					name: 'CardBalance',
					readOnly: true,
				}]
			}, {
				xtype: 'form',
				name: 'form3',
				layout: 'hbox',
				items: [{
					action: 'checkOpenOrder',
					xtype: 'button',
					text: '开单',
					margin: '5 5 5 1050',
					hidden: true,
				}]
			}, {
				xtype: 'form',
				name: 'form4',
				layout: 'hbox',
					items: [{
					xtype: 'textfield',
					name: 'StaffID',
					fieldLabel: '开单店员ID',
					hidden: true,
				},{
					xtype: 'textfield',
					name: 'StaffName',
					fieldLabel: '开单店员',
					readOnly: true,
				}, {
					xtype: 'numberfield',
					name: 'Money',
					fieldLabel: '订单总额',
					readOnly: true,
				}, {
					xtype: 'numberfield',
					name: 'ReceivedMoney',
					fieldLabel: '已收金额',
					readOnly: true,

				}, {
					xtype: 'numberfield',
					name: 'StillOwedMoney',
					fieldLabel: '尚欠金额',
					readOnly: true,
				}]
			}, {
				xtype: 'form',
				name: 'form5',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					name: 'Status',
					fieldLabel: '接车单状态',
					readOnly: true,
				}, {
					xtype: 'textfield',
					name: 'CreatedDate',
					fieldLabel: '订单日期',
					readOnly: true,
				}, {
					xtype: 'numberfield',
					name: 'PayMoney',
					fieldLabel: '结算金额',
					minValue: 0,
					value: 0,
					allowBlank: false,
				}, {
					xtype: "radiogroup",
					name: 'PayType',
					columns: 3,
					items: [
						{ boxLabel: '现金', name: 'type', inputValue: 2, width: 50, checked: true },
						{ boxLabel: '微信', name: 'type', inputValue: 1, width: 50, },
						{ boxLabel: '储值卡', name: 'type', inputValue: 6, width: 70 },
					]
				}]
			}, {
				xtype: 'form',
				name: 'form6',
				layout: 'hbox',
				items: [{
					action: 'payorder',
					xtype: 'button',
					text: '结算',
					margin: '5 5 5 1000',
				}, {
					action: 'paydetails',
					xtype: 'button',
					text: '结算详情',
					margin: '5 5 5 5',
				}]
			}]
		});

		me.items = [me.form, me.container];//, me.orderItemsGrid
		me.callParent(arguments);
	},
});