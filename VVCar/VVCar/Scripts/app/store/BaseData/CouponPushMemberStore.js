Ext.define('WX.store.BaseData.CouponPushMemberStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.CouponPushMemberModel',
    pageSize: 10,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPushMember',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPushMember?All=false',
            batchAdd: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPushMember/BatchAdd',
            batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/CouponPushMember/BatchDelete'
        }
    },
    batchAdd: function(data, success, failure) {
        Ext.Ajax.request({
            url: this.proxy.api.batchAdd,
            method: 'POST',
            jsonData: data,
            success: success,
            failure: failure
        });
    },
    batchDelete: function(ids, cb) {
        Ext.Ajax.request({
            ContentType: 'application/json',
            method: 'DELETE',
            url: this.proxy.api.batchDelete,
            jsonData: { IdList: ids },
            callback: cb
        });
    }
});