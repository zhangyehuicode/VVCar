Ext.define('WX.view.ServicePeriod.ServiceSelector', {
	extend: 'Ext.window.Window',
	alias: 'widget.ServiceSelector',
	title: '选择服务',
	layout: 'fit',
	width: 600,
	height: 500,
	bodyPadding: 5,
	autoShow: false,
	modal: true,
	buttonAlign: 'center',
	initComponent: function () {
		var me = this;
		var serviceStore = Ext.create('WX.store.BaseData.ProductStore');
		me.items = [{
			xtype: 'grid',
			name: 'serviceList',
			stripeRows: true,
			loadMask: true,
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
					name: 'Name',
					xtype: 'textfield',
					fieldLabel: '服务名称',
					emptyText: '服务名称',
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
				{ header: '服务编号', dataIndex: 'Code', flex: 1 },
				{ header: '服务名称', dataIndex: 'Name', flex: 1 },
			],
			bbar: {
				xtype: 'pagingtoolbar',
				displayInfo: true
			}
		}]
		me.callParent(arguments);
	}
});