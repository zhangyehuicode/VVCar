Ext.define('WX.controller.OperationStatement', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.OperationStatementStore', 'WX.store.BaseData.OperationStatementDetailStore'],
	models: ['BaseData.OperationStatementModel', 'BaseData.OperationStatementDetailModel'],
	views: ['Report.OperationStatement', 'Report.OperationStatementDetail'],
	refs: [{
		ref: 'operationStatement',
		selector: 'OperationStatement',
	}, {
		ref: 'operationStatementDetail',
		selector: 'OperationStatementDetail grid[name=gridOperationStatement]',
	}],
	init: function () {
		var me = this;
		me.control({
			'OperationStatement button[action=search]': {
				click: me.search
			},
			'OperationStatement': {
				detailClick: me.detailClick,
				itemdblclick: me.detailClick,
			},
			'OperationStatementDetail button[action=search]': {
				click: me.searchDetail
			}
		});
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getOperationStatement().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	searchDetail: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getOperationStatementDetail().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	detailClick: function (grid, record) {
		var me = this;
		var win = Ext.widget('OperationStatementDetail');
		win.down('textfield[name=StartDate]').setValue(record.data.Code);
		var store = me.getOperationStatementDetail().getStore();
		var params = {
			StartDate: record.data.Code
		}
		store.limit = 10;
		store.pageSize = 10;
		Ext.apply(store.proxy.extraParams, params);
		store.load();
		win.show();
	},
});