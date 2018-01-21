Ext.define('WX.store.BaseData.MemberCardStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.MemberCardModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard?All=false',
            saveGenerate: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard/saveGenerate',
            exportMemberCard: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard/exportMemberCard',
            verifyCode: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard/verifyCode',
            activate: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard/activate',
            getCardByNumber: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard/GetCardByNumber/',
            recharge: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard/Recharge/',
            consume: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard/Consume/',
            adjustBalance: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard/adjustBalance/',
            giftCardBatchActivate: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard/GiftCardBatchActivate/',
            batchModifyRemark: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberCard/BatchModifyRemark/',
        },
    },
    batchModifyRemark: function (data, success, failure) {
        Ext.Ajax.request({
            method: 'POST',
            url: this.proxy.api.batchModifyRemark,
            jsonData: data,
            success: success,
            failure: failure
        });
    },
    giftCardBatchActivate: function (data, success, failure) {
        Ext.Ajax.request({
            method: 'POST',
            url: this.proxy.api.giftCardBatchActivate,
            jsonData: data,
            success: success,
            failure: failure
        });
    },
    saveGenerate: function (entities, success, failure) {
        Ext.Ajax.request({
            method: "POST",
            timeout: 300000,
            url: this.proxy.api.saveGenerate,
            jsonData: entities,
            success: success,
            failure: failure
        });
    },
    exportMemberCard: function (entities, success, failure) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.exportMemberCard,
            jsonData: entities,
            success: success,
            failure: failure
        });
    },
    verifyCode: function (entity, success, failure) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.verifyCode,
            jsonData: entity,
            success: success,
            failure: failure
        });
    },
    activate: function (info, success, failure) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.activate,
            jsonData: info,
            success: success,
            failure: failure
        });
    },
    getCardByNumber: function (cardNumber, success, failure) {
        Ext.Ajax.request({
            method: "GET",
            url: this.proxy.api.getCardByNumber + cardNumber,
            success: success,
            failure: failure
        });
    },
    recharge: function (rechargeInfo, success, failure) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.recharge,
            jsonData: rechargeInfo,
            success: success,
            failure: failure
        });
    },
    consume: function (consumeInfo, success, failure) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.consume,
            jsonData: consumeInfo,
            success: success,
            failure: failure
        });
    },
    adjustBalance: function (entity, cb) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.adjustBalance,
            jsonData: entity,
            callback: cb
        });
    }
});