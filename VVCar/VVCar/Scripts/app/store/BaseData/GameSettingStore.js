Ext.define('WX.store.BaseData.GameSettingStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.GameSettingModel',
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/GameSetting',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/GameSetting?All=false',
		}
	},
});