Ext.define('WX.controller.StockRecord', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.StockRecordStore'],
	models: ['BaseData.StockRecordModel'],
	views: ['Shop.StockRecord'],
	refs: [{
		ref: 'StockRecord',
		selector: 'StockRecord'
	}],
	init: function () {
		var me = this;
		me.control({
			'StockRecord button[action=search]': {
				click: me.search
			},
			'StockRecord combobox[name=StockRecordType]': {
				change: me.stockRecordTypeChange
			},
		});
	},
	stockRecordTypeChange: function (com, newValue, oldValue, eOpts) {
		this.search(com);
	},
	search: function (btn) {
		var store = this.getStockRecord().getStore();
		var queryValues = btn.up('form').getValues();
		store.load({ params: queryValues });
	}
});
