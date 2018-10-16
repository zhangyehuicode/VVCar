Ext.define('WX.store.BaseData.PickUpOrderStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.PickUpOrderModel',
	autoLoad: false,
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 　'api/PickUpOrder',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrder?All=false',
		}
	}
})