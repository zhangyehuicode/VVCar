Ext.define('WX.store.BaseData.CrowdOrderStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.CrowdOrderModel',
	pageSize: 20,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/CrowdOrder',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/CrowdOrder?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/CrowdOrder/BatchDelete',
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