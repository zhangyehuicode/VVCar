Ext.define('WX.store.BaseData.MerchantCrowdOrderStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.MerchantCrowdOrderModel',
	pageSize: 20,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/MerchantCrowdOrder',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/MerchantCrowdOrder?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/MerchantCrowdOrder/BatchDelete',
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
	}
})