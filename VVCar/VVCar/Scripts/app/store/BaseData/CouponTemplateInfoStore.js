/// <reference path="../../ext/ext-all-dev.js" />

Ext.define('WX.store.BaseData.CouponTemplateInfoStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.CouponTemplateInfoModel',
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate/GetCouponTemplateInfo',
			getCouponTemplateDto: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate/GetCouponTemplateDto',
			setConsumePointRate: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate/SetConsumePointRate',
			setConsumePointRateAndDiscountRate: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate/SetConsumePointRateAndDiscountRate',
			putInApplet: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponTemplate/PutInApplet',
		},
	},
	getCouponTemplateDto: function (id, cb) {
		Ext.Ajax.request({
			method: "GET",
			timeout: 300000,
			url: this.proxy.api.getCouponTemplateDto + '/' + id,
			// jsonData: entities,
			callback: cb
		});
	},
	setConsumePointRate: function (id, rate, cb) {
		Ext.Ajax.request({
			method: "GET",
			timeout: 300000,
			url: this.proxy.api.setConsumePointRate + '?id=' + id + '&rate=' + rate,
			// jsonData: entities,
			callback: cb
		});
	},
	setConsumePointRateAndDiscountRate(id, consumePointRate, discountRate, cb) {
		Ext.Ajax.request({
			method: "GET",
			timeout: 300000,
			url: this.proxy.api.setConsumePointRateAndDiscountRate + '?id=' + id + '&consumePointRate=' + consumePointRate + '&discountRate=' + discountRate,
			// jsonData: entities,
			callback: cb
		});
	},
	putInApplet(id, cb) {
		Ext.Ajax.request({
			method: "GET",
			timeout: 300000,
			url: this.proxy.api.putInApplet + '?id=' + id,
			// jsonData: entities,
			callback: cb
		});
	}
});