Ext.define('WX.store.BaseData.ProductCategoryTreeStore', {
    extend: 'Ext.data.TreeStore',
    model: 'WX.model.BaseData.ProductCategoryTreeModel',
    nodeParam: 'ParentId',
    defaultRootId: '00000000-0000-0000-0000-000000000001',
    defaultRootProperty: 'Children',
    rootProperty: 'Children',
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/ProductCategory',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/ProductCategory/GetTree?parentID=',
            create: Ext.GlobalConfig.ApiDomainUrl + 'api/ProductCategory',
            update: Ext.GlobalConfig.ApiDomainUrl + 'api/ProductCategory',
            destroy: Ext.GlobalConfig.ApiDomainUrl + 'api/ProductCategory',
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
    addProductCategory: function (entity, cb) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.create,
            jsonData: entity,
            callback: cb
        });
    },
    updateProductCategory: function (entity, cb) {
        Ext.Ajax.request({
            method: "PUT",
            url: this.proxy.api.update,
            jsonData: entity,
            callback: cb
        });
    },
    deleteProductCategory: function (id, cb) {
        Ext.Ajax.request({
            method: "DELETE",
            url: this.proxy.api.destroy + "/" + id,
            callback: cb
        });
    },
});