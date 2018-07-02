Ext.define('WX.store.BaseData.CarBitCoinRecordStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.CarBitCoinRecordModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinMember/SearchCarBitCoinRecord',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinMember/SearchCarBitCoinRecord?All=false',
		},
	}
});