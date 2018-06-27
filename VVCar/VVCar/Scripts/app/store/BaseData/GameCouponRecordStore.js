Ext.define('WX.store.BaseData.GameCouponRecordStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.GameCouponRecordModel',
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/GameCouponRecord',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/GameCouponRecord?All=false',
		}
	},
});