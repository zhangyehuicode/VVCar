/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.DataDictTypeStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.DataDict.DataDictTypeModel',
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePaging: false,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/DataDictType',
    }
});