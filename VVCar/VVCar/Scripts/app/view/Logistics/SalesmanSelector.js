Ext.define('WX.view.Logistics.SalesmanSelector', {
	extend: 'Ext.window.Window',
	alias: 'widget.SalesmanSelector',
	title: '选择业务员',
	layout: 'fit',
	width: 600,
	height: 500,
	bodyPadding: 5,
	autoShow: false,
	modal: true,
	buttonAlign: 'center',
	initComponent: function () {
		var me = this;
		var salesmanStore = Ext.create('WX.store.BaseData.SalesmanStore');
		me.items = [{
			xtype: 'grid',
			name: 'salesmanList',
			stripeRows: true,
			loadMask: true,
			store: salesmanStore,
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
					fieldLabel: '用户编码',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					name: 'Name',
					xtype: 'textfield',
					fieldLabel: '用户名称',
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
				{ header: '用户编号', dataIndex: 'Code', flex: 1 },
				{ header: '用户名称', dataIndex: 'Name', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		}]
		me.callParent(arguments);
	}
});