Ext.define("WX.view.Combo.ComboList", {
	extend: 'Ext.container.Container',
	alias: 'widget.ComboList',
	title: '套餐设置',
	layout: 'hbox',
	align: 'stretch',
	loadMask: true,
	closable: true,
	initComponent: function () {
		var me = this;
		var comboStore = Ext.create('WX.store.BaseData.ProductStore');
		var comboItemStore = Ext.create('WX.store.BaseData.ComboItemStore');
		me.items = [{
			xtype: 'grid',
			name: 'gridCombo',
			title: '套餐',
			flex: 6,
			height: '100%',
			store: comboStore,
			stripeRow: true,
			//selType: 'checkboxmodel',
			//selModel: {
			//	selection: 'rowmodel',
			//	mode: 'single'
			//},
			tbar: [
				{
					xtype: 'form',
					layout: 'column',
					border: false,
					frame: false,
					labelAlign: 'left',
					buttonAlign: 'right',
					labelWidth: 100,
					padding: 5,
					autoWidth: true,
					autoScroll: true,
					columnWidth: 1,
					items: [{
						xtype: 'textfield',
						name: 'Code',
						fieldLabel: '编码',
						width: 170,
						labelWidth: 30,
						margin: '0 0 0 5'
					}, {
						xtype: 'textfield',
						name: 'Name',
						fieldLabel: '名称',
						width: 170,
						labelWidth: 30,
						margin: '0 0 0 5'
					}, {
						action: 'search',
						xtype: 'button',
						text: '搜索',
						iconCls: 'fa fa-search',
						cls: 'submitBtn',
						margin: '0 0 0 5'
					}]
				}
			],
			columns: [
				//{ header: '排序', dataIndex: 'Index', width: 60 },
				{ header: '编码', dataIndex: 'Code', width: 80 },
				{ header: '名称', dataIndex: 'Name', flex: 1 },
				//{
				//	header: '商品图片', dataIndex: 'ImgUrl', width: 100,
				//	renderer: function (value) {
				//		if (value != "" && value != null) {
				//			return '<img src="' + value + '" style="width: 80px; height: 50px;" />';
				//		}
				//	}
				//},
				{
					header: '类型', dataIndex: 'ProductType', width: 60,
					renderer: function (value) {
						if (value == 0)
							return '服务';
						else
							return '商品';
					}
				},
				{ header: '原单价', dataIndex: 'BasePrice', width: 100 },
				{ header: '销售单价', dataIndex: 'PriceSale', width: 100 },
				{
					header: '库存', dataIndex: 'Stock', width: 100,
					renderer: function (value) {
						return '<span style="color:green;">' + value + '</span>';
					}
				},
				{ header: '单位', dataIndex: 'Unit', width: 60 },
				{
					header: '积分兑换', dataIndex: 'IsCanPointExchange', width: 80,
					renderer: function (value) {
						if (value)
							return '<span style="color:green;">允许</span>';
						else
							return '<span style="color:red;">关闭</span>';
					}
				},
				{ header: '兑换积分', dataIndex: 'Points', width: 80 },
				{ header: '兑换上限', dataIndex: 'UpperLimit', width: 80 },
				//{
				//	header: '是否套餐', dataIndex: 'IsCombo', width: 80,
				//	renderer: function (value) {
				//		if (value == true)
				//			return '<span style="color:green;">是</span>';
				//		else
				//			return '<span style="color:red;">否</span>';
				//	}
				//},
				//{
				//	header: '是否上架', dataIndex: 'IsPublish', width: 80,
				//	renderer: function (value) {
				//		if (value == 1)
				//			return '<span style="color:green;">是</span>';
				//		else
				//			return '<span style="color:red;">否</span>';
				//	}
				//},
				//{
				//	header: '是否推荐', dataIndex: 'IsRecommend', width: 80,
				//	renderer: function (value) {
				//		if (value == 1)
				//			return '<span style="color:green;">是</span>';
				//		else
				//			return '<span style="color:red;">否</span>';
				//	}
				//},
				//{
				//	header: '抽成比例', dataIndex: 'CommissionRate', width: 80,
				//	renderer: function (value) {
				//		return value + '%';
				//	}
				//},
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		}, {
			xtype: 'splitter',
		}, {
			xtype: 'grid',
			name: 'gridComboItem',
			title: '套餐子项',
			flex: 4,
			height: '100%',
			stripeRows: true,
			store: comboItemStore,
			selType: 'checkboxmodel',
			tbar: [
				{
					action: 'addComboItem',
					xtype: 'button',
					text: '添加套餐子项',
					iconCls: 'x-fa fa-plus-circle',
					margin: '5 5 5 5',
				}, {
					action: 'deleteComboItem',
					xtype: 'button',
					text: '删除套餐子项',
					iconCls: 'x-fa fa-close',
					margin: '5 5 5 5',
				}
			],
			plugins: [
				Ext.create('Ext.grid.plugin.RowEditing', {
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
				})
			],
			columns: [
				//{ header: 'ID', dataIndex: 'ID', flex: 1 },
				//{ header: '产品ID', dataIndex: 'ProductID', flex: 1 },
				{ header: '产品代码', dataIndex: 'ProductCode', flex: 1 },
				{ header: '产品名称', dataIndex: 'ProductName', flex: 1 },
				{ header: '原单价', dataIndex: 'BasePrice', flex: 1 },
				{ header: '销售单价', dataIndex: 'PriceSale', flex: 1 },
				{ header: '数量', dataIndex: 'Quantity', flex: 1, editor: { xtype: 'textfield', allowBlank: false } },
				{ header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		}]
		this.callParent();
	}
});