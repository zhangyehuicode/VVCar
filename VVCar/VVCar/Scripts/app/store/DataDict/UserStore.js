Ext.define('WX.store.DataDict.UserStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.IDCodeNameModel',
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePagging: false,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/User/getUserNameCollect',
    }
});