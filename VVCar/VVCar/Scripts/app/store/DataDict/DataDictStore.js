/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.DataDictStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.DataDict.DataDictModel',
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePaging: false,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/DataDictValue',
    },
});