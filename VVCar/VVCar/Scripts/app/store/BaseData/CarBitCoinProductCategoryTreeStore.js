Ext.define('WX.store.BaseData.CarBitCoinProductCategoryTreeStore', {
    extend: 'Ext.data.TreeStore',
    model: 'WX.model.BaseData.CarBitCoinProductCategoryTreeModel',
    nodeParam: 'ParentId',
    defaultRootId: '00000000-0000-0000-0000-000000000001',
    defaultRootProperty: 'Children',
    rootProperty: 'Children',
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProductCategory',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProductCategory/GetTree?parentID=',
            create: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProductCategory',
            update: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProductCategory',
            destroy: Ext.GlobalConfig.ApiDomainUrl + 'api/CarBitCoinProductCategory',
        },
        reader: {
            type: 'json',
            root: 'Children',
            successProperty: 'IsSuccessful',
            messageProperty: 'ErrorMessage',
        }
    },
    root: {
        Text: '全部分类',
        id: '00000000-0000-0000-0000-000000000001',
        expanded: true,
    },
    addCarBitCoinProductCategory: function(entity, cb) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.create,
            jsonData: entity,
            callback: cb
        });
    },
    updateCarBitCoinProductCategory: function(entity, cb) {
        Ext.Ajax.request({
            method: "PUT",
            url: this.proxy.api.update,
            jsonData: entity,
            callback: cb
        });
    },
    deleteCarBitCoinProductCategory: function(id, cb) {
        Ext.Ajax.request({
            method: "DELETE",
            url: this.proxy.api.destroy + "/" + id,
            callback: cb
        });
    },
});