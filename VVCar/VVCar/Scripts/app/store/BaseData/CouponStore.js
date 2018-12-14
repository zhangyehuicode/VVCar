Ext.define('WX.store.BaseData.CouponStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.CouponModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate/GetCouponTemplateInfo?All=false',
			qrcode: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate/GenerateQRCode',
			push: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPush/ImmediatePush',
			configinfo: Ext.GlobalConfig.ApiDomainUrl + 'api/Common/ConfigInfo',
			updatestatus: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate/UpdateStatus',
			receivecouponswidthcode: Ext.GlobalConfig.ApiDomainUrl + 'api/Coupon/ReceiveCouponsWidthCode',
		}
	},
	qrcode: function (url, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'GET',
			url: this.proxy.api.qrcode + "?url=" + url,
			callback: cb,
		})
	},
	push: function (entity, cb) {
		Ext.Ajax.request({
			method: 'POST',
			url: this.proxy.api.push,
			jsonData: entity,
			callback: cb
		});
	},
	configinfo: function (cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'GET',
			url: this.proxy.api.configinfo,
			callback: cb,
		})
	},
	updatestatus: function (entity, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'PUT',
			url: this.proxy.api.updatestatus,
			jsonData: entity,
			callback: cb,
		})
	},
	receivecouponswidthcode: function (entity, cb) {
		Ext.Ajax.request({
			method: 'POST',
			url: this.proxy.api.receivecouponswidthcode,
			jsonData: entity,
			callback: cb
		});
	},
});