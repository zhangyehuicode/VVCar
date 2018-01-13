/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.DataDictValueStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.DataDict.DataDictModel',
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePaging: false,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/DataDictValue',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/DataDictValue/AllData',
        },
    },
});