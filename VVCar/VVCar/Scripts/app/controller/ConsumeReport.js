Ext.define('WX.controller.ConsumeReport', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.DepartmentStore', 'WX.store.Reporting.ConsumeReportStore'],
    stores: ['DataDict.ConsumeReportTypeStore'],
    views: ['ConsumeReport.ConsumeReport'],
    refs: [{
        ref: 'gridConsumeReport',
        selector: 'ConsumeReport grid[name=gridConsumeReport]'
    }, {
        ref: 'formSearch',
        selector: 'ConsumeReport form[name=formSearch]'
    }],
    init: function () {
        var me = this;
        me.control({
            'ConsumeReport button[action=searchConsumeReport]': {
                click: me.searchConsumeReport
            },
            'ConsumeReport': {
                afterrender: me.onConsumeReportAfterRender
            },
            'ConsumeReport button[action=export]': {
                click: me.exportConsumeReport
            },
        });
    },
    searchConsumeReport: function (btn) {
        var myStore = this.getGridConsumeReport().getStore();
        var queryValues = this.getFormSearch().getValues();
        if (queryValues != null) {
            myStore.load({ params: queryValues });
        } else {
            Ext.MessageBox.alert("系统提示", "请输入过滤条件！");
        }
    },
    onConsumeReportAfterRender: function () {
        this.searchConsumeReport();
    },
    exportConsumeReport: function (btn) {
        var grid = btn.up("grid");
        var store = grid.getStore();
        var queryValues = btn.up("form").getValues();
        function success(response) {
            Ext.MessageBox.close();
            response = JSON.parse(response.responseText);
            if (response.IsSuccessful) {
                window.location.href = response.Data;
            }
            else {
                Ext.Msg.alert("提示", response.ErrorMessage);
            }
        }
        function failure(response) {
            Ext.Msg.alert("提示", response.responseText);
        }
        Ext.MessageBox.show({
            msg: '正在生成数据...,请稍后',
            progressText: '正在生成数据...',
            width: 300,
            wait: true
        });
        store.exportConsumeReport(queryValues, success, failure);
    }
});
