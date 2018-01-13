Ext.define('WX.store.BaseData.PermissionStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.PermissionFuncModel',
    pageSize: 25,
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePaging: true,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/Permission',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/Permission?All=false',
        },
    },
    syncPermission: function (success) {
        Ext.Ajax.request({
            url: this.proxy.url + "/SyncPermission/",
            method: 'POST',
            success: success,
        });
    },
    getRolePermissionList: function (roleCode, success) {
        Ext.Ajax.request({
            url: this.proxy.url + "/GetRolePermissionList/" + roleCode,
            method: 'GET',
            success: success,
        });
    },
    assignPermission: function (assignModel, success) {
        Ext.Ajax.request({
            url: this.proxy.url + "/AssignPermission/",
            method: 'POST',
            jsonData: assignModel,
            success: success,
        });
    },
});