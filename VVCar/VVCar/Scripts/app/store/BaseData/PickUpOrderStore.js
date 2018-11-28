Ext.define('WX.store.BaseData.PickUpOrderStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.PickUpOrderModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrder',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrder?All=false',
            payorder: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrderPaymentDetails/?All=false',
            verificationByMemberCard: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrder/VerificationByMemberCard/?All=false',
            wechatmicropay: Ext.GlobalConfig.ApiDomainUrl + 'api/PickUpOrder/WeChatMicroPay'
        }
    },
    payorder: function (data, success, failure) {
        Ext.Ajax.request({
            url: this.proxy.api.payorder,
            method: 'POST',
            jsonData: data,
            success: success,
            failure: failure,
        });
    },
    verificationByMemberCard: function (data, success, failure) {
        Ext.Ajax.request({
            url: this.proxy.api.verificationByMemberCard,
            method: 'POST',
            jsonData: data,
            success: success,
            failure: failure,
        });
    },
    wechatmicropay: function (auth_code, total_fee, out_trade_no, success, failure) {
        Ext.Ajax.request({
            url: this.proxy.api.wechatmicropay + '?auth_code=' + auth_code + '&total_fee=' + total_fee + '&out_trade_no=' + out_trade_no,
            method: 'GET',
            success: success,
            failure: failure
        });
    }
});