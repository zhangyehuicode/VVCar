Ext.define('WX.view.QuickOrder.QuickOrderList', {
	extend: 'Ext.container.Container',
	alias: 'widget.QuickOrderList',
	title: '开单',
	layout: 'anchor',
	loadMask: true,
	closable: true,
	padding: 15,
	initComponent: function () {
		var me = this;
		me.items = [{
			xtype:'panel'
		}]
		this.callParent();
	}
})