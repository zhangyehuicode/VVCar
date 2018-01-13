
Ext.define('WX.store.BaseData.SysMenuStore', {
    extend: 'Ext.data.TreeStore',
    model: 'WX.model.BaseData.SysMenuModel',
    autoLoad: true,
    root: {
        Name: "菜单目录",
        ID: "00000000-0000-0000-0000-000000000000"
    },
    nodeParam: 'ParentID',
    defaultRootId: '00000000-0000-0000-0000-000000000000',
    defaultRootText: '菜单目录',
    defaultRootProperty: 'Children',
    rootProperty: 'Children',
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/SysMenu',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/SysMenu/GetByParentID',
            create: Ext.GlobalConfig.ApiDomainUrl + 'api/SysMenu',
            update: Ext.GlobalConfig.ApiDomainUrl + 'api/SysMenu',
            destroy: Ext.GlobalConfig.ApiDomainUrl + 'api/SysMenu',
        },
    },
    addSysMenu: function (entity, cb) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.create,
            jsonData: entity,
            callback: cb
        });
    },
    deleteSysMenu: function (id, cb) {
        Ext.Ajax.request({
            method: "DELETE",
            url: this.proxy.api.destroy + "/" + id,
            callback: cb
        });
    },
    updateSysMenu: function (entity, cb) {
        Ext.Ajax.request({
            method: "PUT",
            url: this.proxy.api.update,
            jsonData: entity,
            callback: cb
        });
    }
});
