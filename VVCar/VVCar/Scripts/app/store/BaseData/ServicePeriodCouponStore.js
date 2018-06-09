Ext.define('WX.store.BaseData.ServicePeriodCouponStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.ServicePeriodCouponModel',
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/ServicePeriodCoupon',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/ServicePeriodCoupon?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/ServicePeriodCoupon/BatchAdd',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/ServicePeriodCoupon/BatchDelete',
		}
	},
	batchAdd: function (data, success, failure) {
		Ext.Ajax.request({
			url: this.proxy.api.batchAdd,
			method: 'POST',
			jsonData: data,
			success: success,
			failure: failure
		});
	},
	batchDelete: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'DELETE',
			url: this.proxy.api.batchDelete,
			jsonData: { IdList: ids },
			callback: cb
		});
	}
});