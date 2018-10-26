Ext.define('WX.view.PickUpOrder.ProductSelector', {
	extend: 'Ext.window.Window',
	alias: 'widget.ProductSelector',
	title: '选择产品',
	layout: 'fit',
	width: 600,
	height: 500,
	bodyPadding: 5,
	autoShow: false,
	modal: true,
	buttonAlign: 'right',
	initComponent: function () {
		var me = this;
		var productStore = Ext.create('WX.store.BaseData.ProductStore');
		productStore.limit = 10;
		productStore.pageSize = 10;
		productStore.load();
		me.items = [{
			xtype: 'grid',
			name: 'gridProduct',
			stripeRows: true,
			loadMask: true,
			store: productStore,
			selType: 'checkboxmodel',
			tbar: {
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
					name: 'PickUpOrderID',
					xtype: 'textfield',
					fieldLabel: '接车单ID',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
					hidden: true,
				},{
					name: 'Code',
					xtype: 'textfield',
					fieldLabel: '产品编码',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					name: 'Name',
					xtype: 'textfield',
					fieldLabel: '产品名称',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					margin: '0 0 0 5',
				}]
			},
			columns: [
				{ header: '产品编号', dataIndex: 'Code', flex: 1 },
				{ header: '产品名称', dataIndex: 'Name', flex: 1 },
				//{ header: '原单价', dataIndex: 'BasePrice', flex: 1 },
				{ header: '销售单价', dataIndex: 'PriceSale', flex: 1 },
				//{ header: '库存', dataIndex: 'Stock', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true,
			},		
		}];
		me.buttons = [{
			text: '保存',
			action: 'save',
			cls: 'submitBtn',
			scope: me
		}, {
			text: '取消',
			scope: me,
			handler: me.close
		}]
		me.callParent(arguments);
	}
})