Ext.define('WX.store.BaseData.MemberGradeStore', {
    extend: 'Ext.data.Store',
    model: 'WX.model.BaseData.MemberGradeModel',
    autoLoad: false,
    pageSize: 25,
    proxy: {
        type: 'rest',
        url: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberGrade',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberGrade/Search?All=false',
        },
    },
    changeStatus: function (gradeID, status, successHandler) {
        Ext.Ajax.request({
            url: this.proxy.url + "/" + gradeID + "/ChangeStatus/" + status,
            method: 'PUT',
            clientValidation: true,
            success: successHandler,
        });
    },
    changeOpen: function (gradeID, notopen, successHandler) {
        Ext.Ajax.request({
            url: this.proxy.url + "/ChangeOpen?memberGradeID=" + gradeID + "&isNotOpen=" + notopen,
            method: 'GET',
            clientValidation: true,
            success: successHandler,
        });
    },
});