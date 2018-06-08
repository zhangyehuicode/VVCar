Ext.define('WX.store.BaseData.CouponPushItemStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.CouponPushItemModel',
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPushItem',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPushItem?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPushItem/BatchAdd',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPushItem/deleteCouponPushItems'
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