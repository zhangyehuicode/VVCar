Ext.define('WX.store.BaseData.PickUpOrderTaskDistributionStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.PickUpOrderTaskDistributionModel',
	autoLoad: false,
	pageSize: 5,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderTaskDistribution',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderTaskDistribution?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderTaskDistribution/BatchAdd',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderTaskDistribution/BatchDelete',
		},
	},
	batchAdd: function (data, success, failure) {
		Ext.Ajax.request({
			url: this.proxy.api.batchAdd,
			method: 'POST',
			jsonData: data,
			success: success,
			failure: failure,
		});
	},
	batchDelete: function (ids, cb) {
		Ext.Ajax.request({
			url: this.proxy.api.batchDelete,
			method: 'DELETE',
			jsonData: { IdList: ids },
			callback: cb,
		})
	}
});