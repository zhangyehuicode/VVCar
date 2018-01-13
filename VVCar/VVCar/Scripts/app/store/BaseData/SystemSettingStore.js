Ext.define('WX.store.BaseData.SystemSettingStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.SystemSettingModel',
    pageSize: 25,
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePaging: false,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/SystemSetting',
    },
});