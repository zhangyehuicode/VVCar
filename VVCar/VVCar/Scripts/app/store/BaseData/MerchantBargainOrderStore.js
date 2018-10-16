Ext.define('WX.store.BaseData.MerchantBargainOrderStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.MerchantBargainOrderModel',
	pageSize: 20,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/MerchantBargainOrder',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/MerchantBargainOrder?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/MerchantBargainOrder/BatchDelete',
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