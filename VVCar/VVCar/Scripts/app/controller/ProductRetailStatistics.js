Ext.define('WX.controller.ProductRetailStatistics', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.ProductRetailStatisticsStore'],
	models: ['BaseData.ProductRetailStatisticsModel'],
	views: ['Report.ProductRetailStatisticsList'],
	refs: [{
		ref: 'productRetailStatisticsList',
		selector: 'ProductRetailStatisticsList',
	}],
	init: function () {
		var me = this;
		me.control({
			'ProductRetailStatisticsList button[action=search]': {
				click: me.search
			},
			'ProductRetailStatisticsList button[action=searchSellWell]': {
				click: me.searchSellWell
			},
			'ProductRetailStatisticsList button[action=searchUnsalable]': {
				click: me.searchUnsalable
			},
			'ProductRetailStatisticsList button[action=unsaleNotify]': {
				click: me.unsaleNotify
			},
			'ProductRetailStatisticsList button[action=reset]': {
				click: me.reset
			},
			'ProductRetailStatisticsList button[action=export]': {
				click: me.export
			},
			'ProductRetailStatisticsList combobox[name=OrderType]': {
				change: me.ordertype
			}
		});
	},
	searchSellWell: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.IsSaleWell = true;
			var store = me.getProductRetailStatisticsList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	searchUnsalable: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.IsSaleWell = false;
			if (queryValues.StartDate == '' && queryValues.EndDate == '') {
				Ext.Msg.alert('提示', '请选择开始时间和结束时间!');
				return;
			}
			var store = me.getProductRetailStatisticsList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	unsaleNotify: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		queryValues.IsSaleWell = false;
		var store = me.getProductRetailStatisticsList().getStore();
		Ext.Msg.confirm('询问', '确定要发送滞销通知吗?', function (operational) {
			if (operational === 'yes') {
				function success(response) {
					Ext.Msg.hide();
					response = JSON.parse(response.responseText);
					if (!response.IsSuccessful) {
						Ext.Msg.alert('提示', response.ErrorMessage);
						return;
					}
					store.proxy.extraParams = queryValues;
					store.load();
				};
				function failure(response) {
					Ext.Msg.hide();
					Ext.Msg.alert('提示', response.responseText);
				};
				store.unsaleNotify(queryValues, success, failure);
			}
		});
	},
	ordertype: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getProductRetailStatisticsList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	search: function (btn) {
		var me = this;
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			var store = me.getProductRetailStatisticsList().getStore();
			store.proxy.extraParams = queryValues;
			store.load();
		} else {
			Ext.Msg.alert('提示', '请输入过滤条件');
		}
	},
	reset: function (btn) {
		btn.up('form').form.reset();
	},
	export: function (btn) {
		var me = this;
		Ext.Msg.show({
			msg: '正在生成数据...,请稍候',
			progressText: '正在生成数据...',
			width: 300,
			wait: true,
		});
		var queryValues = btn.up('form').getValues();
		var store = me.getProductRetailStatisticsList().getStore();
		store.export(queryValues, function (req, success, res) {
			Ext.Msg.hide();
			if (res.status === 200) {
				var response = JSON.parse(res.responseText);
				if (response.IsSuccessful) {
					window.location.href = response.Data;
				} else {
					Ext.Msg.alert('提示', response.ErrorMessage);
				}
			} else {
				Ext.Msg.alert('提示', '网络请求异常:' + res.status);
			}
		});
	},
});