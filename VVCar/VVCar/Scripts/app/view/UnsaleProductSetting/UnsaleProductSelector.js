Ext.define('WX.view.UnsaleProductSetting.UnsaleProductSelector', {
	extend: 'Ext.window.Window',
	alias: 'widget.UnsaleProductSelector',
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
		var serviceStore = Ext.create('WX.store.BaseData.ProductStore');
		me.grid = Ext.create('Ext.grid.Panel', {
			name: 'productList',
			stripeRows: true,
			loadMask: true,
			selModel: Ext.create('Ext.selection.CheckboxModel', { model: 'SIMPLE' }),
			store: serviceStore,
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
					iconCls: 'submitBtn',
					margin: '0 0 0 5',
				}]
			},
			columns: [
				{ header: '产品编号', dataIndex: 'Code', flex: 1 },
				{ header: '产品名称', dataIndex: 'Name', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		});
		me.items = [me.grid];
		me.buttons = [{
			text: '保存',
			action: 'save',
			cls: 'submitBtn',
			scope: me
		}, {
			text: '取消',
			scope: me,
			handler: me.close
		}];
		me.callParent(arguments);
	}
});