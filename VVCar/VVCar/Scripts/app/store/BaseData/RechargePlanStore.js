Ext.define('WX.store.BaseData.RechargePlanStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.RechargePlanModel',
    pageSize: 25,
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePaging: false,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/RechargePlan',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/RechargePlan?All=false',
        },
    },
    changeStatus: function (planID, isAvailable, success) {
        Ext.Ajax.request({
            url: this.proxy.url + "/" + planID + "/ChangeStatus/" + isAvailable,
            method: 'PUT',
            clientValidation: true,
            success: success,
        });
    },
    getUsablePlans: function (success, params) {
        Ext.Ajax.request({
            url: this.proxy.url + "/UsablePlans?cardTypeId=" + params,
            method: 'GET',
            success: success,
        });
    },
    newRechargePlan: function (params, success) {
        Ext.Ajax.request({
            url: this.proxy.url + "/NewRechargePlan",
            method: 'POST',
            success: success,
            jsonData: params,
        });
    },
    updateRechargePlan: function (params, success) {
        Ext.Ajax.request({
            url: this.proxy.url + "/UpdateRechargePlan",
            method: 'POST',
            success: success,
            jsonData: params,
        });
    },
});