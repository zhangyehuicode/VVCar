Ext.define('WX.store.BaseData.PickUpOrderItemStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.PickUpOrderItemModel',
	autoLoad: false,
	pageSize: 5,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderItem',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderItem?All=false',
			batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderItem/BatchAdd',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderItem/BatchDelete',
			updatepickuporder: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderItem/UpdatePickUpOrder',
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
	},
	updatepickuporder: function (data, success, failure) {
		Ext.Ajax.request({
			url: this.proxy.api.updatepickuporder,
			method: 'PUT',
			jsonData: data,
			success: success,
			failure: failure,
		});
	}
});