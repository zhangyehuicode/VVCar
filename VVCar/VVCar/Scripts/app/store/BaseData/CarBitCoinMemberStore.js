Ext.define('WX.store.BaseData.CarBitCoinMemberStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.CarBitCoinMemberModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinMember',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinMember?All=false',
			giveAwayCarBitCoin: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinMember/GiveAwayCarBitCoin?All=false',
		},
	},
	giveAwayCarBitCoin: function (data, success, failure) {
		Ext.Ajax.request({
			url: this.proxy.api.giveAwayCarBitCoin,
			method: 'POST',
			jsonData: data,
			success: success,
			failure: failure,
		});
	}
});