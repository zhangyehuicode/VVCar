Ext.define('WX.store.BaseData.ComboItemStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.ComboItemModel',
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/ComboItem',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/ComboItem/?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/ComboItem/BatchAdd',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/ComboItem/BatchDelete',
		}
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