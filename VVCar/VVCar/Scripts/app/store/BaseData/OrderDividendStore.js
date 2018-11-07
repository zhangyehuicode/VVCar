Ext.define('WX.store.BaseData.OrderDividendStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.OrderDividendModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/OrderDividend',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/OrderDividend?All=false',
			balance: Ext.GlobalConfig.ApiDomainUrl + 'api/OrderDividend/Balance',
		}
	},
	balance: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'POST',
			url: this.proxy.api.balance,
			jsonData: { IdList: ids },
			callback: cb,
		})
	},
});