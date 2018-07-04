﻿Ext.define('WX.store.BaseData.CarBitCoinProductCategoryLiteStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.IDCodeNameModel',
    pageSize: 25,
    autoLoad: false,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProductCategory',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProductCategory/LiteData?All=false',
        },
        reader: {
            type: 'json',
            root: 'Data',
            successProperty: 'IsSuccessful',
            messageProperty: 'ErrorMessage',
        }
    }
});