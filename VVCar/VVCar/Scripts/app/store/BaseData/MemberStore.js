/// <reference path="../../ext/ext-all-dev.js" />

Ext.define("WX.store.BaseData.MemberStore", {
    extend: "Ext.data.Store",
    model: "WX.model.BaseData.MemberModel",
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: "rest",
        url: Ext.GlobalConfig.ApiDomainUrl + "api/Member",
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + "api/Member?All=false",
            add: Ext.GlobalConfig.ApiDomainUrl + "api/Member",
            update: Ext.GlobalConfig.ApiDomainUrl + "api/Member",
            getBaseInfo: Ext.GlobalConfig.ApiDomainUrl + "api/Member/getBaseInfo",
            reportLoss: Ext.GlobalConfig.ApiDomainUrl + "api/Member/reportLoss",
            cancelLoss: Ext.GlobalConfig.ApiDomainUrl + "api/Member/cancelLoss",
            resetPassword: Ext.GlobalConfig.ApiDomainUrl + "api/Member/ResetPassword/",
            changeCard: Ext.GlobalConfig.ApiDomainUrl + "api/Member/ChangeCard/",
            exportMember: Ext.GlobalConfig.ApiDomainUrl + "api/Member/exportMember?All=false",
            getPhoneLocation: Ext.GlobalConfig.ApiDomainUrl + "api/Member/GetPhoneLoaction/",
            changeMemberGroup: Ext.GlobalConfig.ApiDomainUrl + "api/Member/ChangeMemberGroup",
            adjustMemberPoint: Ext.GlobalConfig.ApiDomainUrl + "api/Member/AdjustMemberPoint",
        },
    },
    failure: function (response) {
        Ext.Msg.alert("提示", response.responseText);
    },
    addMember: function (entity, success) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.add,
            jsonData: entity,
            success: success,
            failure: this.failure
        });
    },
    updateMember: function (entity, success) {
        Ext.Ajax.request({
            method: "PUT",
            url: this.proxy.api.update,
            jsonData: entity,
            success: success,
            failure: this.failure
        });
    },
    getBaseInfo: function (memberId, success) {
        Ext.Ajax.request({
            method: "GET",
            url: this.proxy.api.getBaseInfo + "?memberID=" + memberId,
            success: success,
            failure: this.failure
        });
    },
    reportLoss: function (cardNumber, success) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.reportLoss,
            params: "=" + cardNumber,
            success: success,
            failure: this.failure
        });
    },
    cancelLoss: function (cardNumber, success) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.cancelLoss,
            params: "=" + cardNumber,
            success: success,
            failure: this.failure
        });
    },
    resetPassword: function (memberID, success) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.resetPassword + memberID,
            success: success,
            failure: this.failure
        });
    },
    changeCard: function (changeCardInfo, success) {
        Ext.Ajax.request({
            method: "POST",
            url: this.proxy.api.changeCard,
            jsonData: changeCardInfo,
            success: success,
            failure: this.failure
        });
    },
    exportMember: function (p, cb) {
        Ext.Ajax.request({
            method: "GET",
            url: this.proxy.api.exportMember,
            params: p,
            callback: cb
        });
    },
    getPhoneLocation: function (phoneNumber, success) {
        Ext.Ajax.request({
            method: "GET",
            url: this.proxy.api.getPhoneLocation + phoneNumber,
            success: success
        });
    },
    changeMemberGroup: function (data, successHanlder) {
        Ext.Ajax.request({
            method: "PUT",
            url: this.proxy.api.changeMemberGroup,
            jsonData: data,
            success: successHanlder
        });
    },
    adjustMemberPoint: function (params, cb) {
        Ext.Ajax.request({
            method: "GET",
            url: this.proxy.api.adjustMemberPoint,
            params: params,
            callback: cb
        });
    },
});