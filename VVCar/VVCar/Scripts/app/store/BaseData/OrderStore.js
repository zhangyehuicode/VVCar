Ext.define('WX.store.BaseData.OrderStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.OrderModel',
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Order',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Order?All=false',
			delivery: Ext.GlobalConfig.ApiDomainUrl + 'api/Order/Delivery',
			antiDelivery: Ext.GlobalConfig.ApiDomainUrl + 'api/Order/AntiDelivery',
			revisitTips: Ext.GlobalConfig.ApiDomainUrl + 'api/Order/RevisitTips',
		},
	},
	adjustIndex: function (params, success, failure) {
		Ext.Ajax.request({
			method: 'GET',
			url: '',
			params: params,
			success: success,
			failure: failure,
		});
	},
	delivery: function (order, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'POST',
			url: this.proxy.api.delivery,
			jsonData: order,
			callback: cb,
		});
	},
	antiDelivery: function (id, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'GET',
			url: this.proxy.api.antiDelivery + '?id=' + id,
			callback: cb,
		});
	},
	revisitTips: function (id, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'GET',
			url: this.proxy.api.revisitTips + '?id=' + id,
			callback: cb,
		})
	}
});