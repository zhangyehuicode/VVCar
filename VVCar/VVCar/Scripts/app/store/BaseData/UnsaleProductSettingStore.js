Ext.define('WX.store.BaseData.UnsaleProductSettingStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.UnsaleProductSettingModel',
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/UnsaleProductSetting',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/UnsaleProductSetting?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + "api/UnsaleProductSetting/BatchDelete",
			enableUnsaleProductSetting: Ext.GlobalConfig.ApiDomainUrl + "api/UnsaleProductSetting/EnableUnsaleProductSetting",
			disableUnsaleProductSetting: Ext.GlobalConfig.ApiDomainUrl + "api/UnsaleProductSetting/DisableUnsaleProductSetting",
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
	enableUnsaleProductSetting: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.enableUnsaleProductSetting,
			method: 'POST',
			jsonData: { IdList: ids },
			callback: cb
		});
	},
	disableUnsaleProductSetting: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.disableUnsaleProductSetting,
			method: 'POST',
			jsonData: { IdList: ids },
			callback: cb
		});
	},
})