Ext.define('WX.controller.OrderDividend', {
	extend: 'Ext.app.Controller',
	requires: ['WX.store.BaseData.OrderDividendStore'],
	models: ['BaseData.OrderDividendModel'],
	views: ['OrderDividend.OrderDividend'],
	refs: [{
		ref: 'orderDividend',
		selector: 'OrderDividend'
	}],
	init: function () {
		var me = this;
		me.control({
			'OrderDividend button[action=search]': {
				click: me.search
			},
			'OrderDividend button[action=balance]': {
				click: me.balance
			},
		});
	},
	balance: function () {
		var me = this;
		var store = me.getOrderDividend().getStore();
		var selectedItems = me.getOrderDividend().getSelectionModel().getSelection();
		if (selectedItems.length < 1) {
			Ext.Msg.alert('提示', '未选择数据');
			return;
		} 
		var ids = [];
		var balanceData = false;
		var totalMoney = 0;
		selectedItems.forEach(function (item) {
			if (item.data.IsBalance == true) {
				balanceData = true;
				return;
			} else {
				ids.push(item.data.ID);
				totalMoney += item.data.Commission;
			}
		});
		if (balanceData) {
			Ext.Msg.alert('提示', '请选择未结算的数据!');
			return;
		}
		Ext.Msg.confirm('询问', '结算金额: <font color="red">' + totalMoney+ '</font>元<br/>'+'确定要结算已选中的数据吗?', function (operational) {
			if (operational == 'yes') {
				store.balance(ids,
					function success(response, request, c) {
						var result = Ext.decode(c.responseText);
						if (result.IsSuccessful) {
							store.reload();
							Ext.Msg.alert('提示', '结算成功!');
						} else {
							Ext.Msg.alert('提示', result.ErrorMessage);
						}
					},
					function failure(a, b, c) {
						Ext.Msg.alert('提示', '结算失败!');
					}
				);
			}
		});
	},
	search: function (btn) {
		var store = this.getOrderDividend().getStore();
		var queryValues = btn.up('form').getValues();
		if (queryValues != null) {
			queryValues.All = true;
			store.load({ params: queryValues });
		}
	},
});
