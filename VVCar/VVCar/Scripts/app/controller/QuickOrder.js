Ext.define('WX.controller.QuickOrder', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.QuickOrderStore'],
	models: ['BaseData.QuickOrderModel'],
	views: ['QuickOrder.QuickOrderList'],
	refs: [{
		ref: 'quickOrderList',
		selector: 'QuickOrderList'
	}],
	init: function () {
		var me = this;
		me.control({

		})
	}
})