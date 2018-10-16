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
		},
	},
});