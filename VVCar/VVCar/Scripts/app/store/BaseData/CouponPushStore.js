Ext.define('WX.store.BaseData.CouponPushStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.CouponPushModel',
	pageSize: 20,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPush',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPush?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPush/deleteCouponPushs',
			batchHandCouponPush: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPush/batchHandCouponPush',
		}
	},
	batchDelete: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchDelete,
			method: 'DELETE',
			jsonData: { IdList: ids },
			callback: cb
		});
	},
	batchHandCouponPush: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchHandCouponPush,
			method: 'POST',
			jsonData: { IdList: ids },
			callback: cb
		});
	}
});