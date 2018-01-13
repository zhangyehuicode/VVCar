Ext.define('WX.store.BaseData.MemberCardTypeStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.MemberCardTypeModel',
    pageSize: 25,
    autoLoad: false,
    proxy: {
        type: 'rest',
        enablePaging: false,
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCardType',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCardType?All=false',
        },
    },
    getUsableTypes: function (success) {
        Ext.Ajax.request({
            url: this.proxy.url + "/UsableTypes/",
            method: 'GET',
            success: success,
        });
    },
});