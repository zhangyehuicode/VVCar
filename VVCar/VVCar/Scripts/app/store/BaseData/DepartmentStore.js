/// <reference path="../../ext/ext-all-dev.js" />

Ext.define('WX.store.BaseData.DepartmentStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.DepartmentModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/Department',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/Department?All=false',
        },
    }
});