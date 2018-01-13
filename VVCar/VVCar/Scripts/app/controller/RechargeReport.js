Ext.define('WX.controller.RechargeReport', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.Reporting.RechargeReportStore', 'WX.store.BaseData.DepartmentStore', 'WX.store.DataDict.UserStore'],
    stores: ['DataDict.RechargeReportTypeStore', 'DataDict.PaymentTypeStore'],
    views: ['RechargeReport.RechargeReport'],
    refs: [{
        ref: 'RHDepartmentReport',
        selector: 'RHDepartmentReport'
    }, {
        ref: 'deptFormSearch',
        selector: 'RHDepartmentReport form[name=formSearch]'
    }, {
        ref: 'RHMemberReport',
        selector: 'RHMemberReport'
    }, {
        ref: 'memberFormSearch',
        selector: 'RHMemberReport form[name=formSearch]'
    }, {
        ref: 'cmbTradeUser',
        selector: 'RHMemberReport combobox[name=TradeUser]'
    }, {
        ref: 'RHPayTypeReport',
        selector: 'RHPayTypeReport'
    }, {
        ref: 'payTypeFormSearch',
        selector: 'RHPayTypeReport form[name=formSearch]'
    }, {
        ref: 'rechargeReportCmb',
        selector: 'RechargeReport combobox[name=ReportType]'
    }],
    init: function () {
        var me = this;
        me.control({
            'RechargeReport': {
                afterrender: me.onRechargeReportAfterRender
            },
            'RechargeReport combobox[name=ReportType]': {
                select: me.selectReportType
            },
            'RHDepartmentReport button[action=dp_search]': {
                click: me.dpSearch
            },
            'RHDepartmentReport button[action=export]': {
                click: me.exportRechargeReport
            },
            'RHMemberReport button[action=me_search]': {
                click: me.meSearch
            },
            'RHMemberReport combobox[name=TradeDepartment]': {
                select: me.selectTradeDepartment
            },
            'RHMemberReport button[action=export]': {
                click: me.exportRechargeReport
            },
            'RHPayTypeReport button[action=pt_search]': {
                click: me.ptSearch
            },
            'RHPayTypeReport button[action=export]': {
                click: me.exportRechargeReport
            },
        });
    },
    onRechargeReportAfterRender: function () {
        this.dpSearch();
    },
    selectReportType: function (combo, record, eOpts) {
        var me = this;
        if (record.data.DictName == "门店储值业绩报表") {
            me.getRHDepartmentReport().show();
            me.getRHMemberReport().hide();
            me.getRHPayTypeReport().hide();
        }
        else if (record.data.DictName == "业务员储值业绩报表") {
            me.getRHDepartmentReport().hide();
            me.getRHMemberReport().show();
            me.getRHPayTypeReport().hide();
            if (me.memberReportLoaded == undefined) {//仅首次自动加载数据
                me.meSearch();
                me.memberReportLoaded = true;
            }
        }
        else if (record.data.DictName == "支付方式报表") {
            me.getRHDepartmentReport().hide();
            me.getRHMemberReport().hide();
            me.getRHPayTypeReport().show();
            if (me.payTypeReportLoaded == undefined) {//仅首次自动加载数据
                me.ptSearch();
                me.payTypeReportLoaded = true;
            }
        }
    },
    dpSearch: function (btn) {
        var dpStore = this.getRHDepartmentReport().getStore();
        var queryValues = this.getDeptFormSearch().getValues();
        if (queryValues != null) {
            queryValues.ReportType = '0';
            dpStore.proxy.extraParams = queryValues;
            dpStore.load();
        } else {
            Ext.MessageBox.alert('系统提示', '请输入过滤条件！');
        }
    },
    meSearch: function (btn) {
        var dpStore = this.getRHMemberReport().getStore();
        var queryValues = this.getMemberFormSearch().getValues();
        if (queryValues != null) {
            queryValues.ReportType = '1';
            dpStore.proxy.extraParams = queryValues;
            dpStore.load();
        } else {
            Ext.MessageBox.alert('系统提示', '请输入过滤条件！');
        }
    },
    selectTradeDepartment: function (combo, records, eOpts) {
        var me = this;
        var params = { departmentID: '' };
        if (records != null || records.length > 0)
            params.departmentID = records[0].data.ID;
        var userStore = this.getCmbTradeUser().getStore();
        userStore.proxy.extraParams = params;
        userStore.reload();
    },
    ptSearch: function (btn) {
        var dpStore = this.getRHPayTypeReport().getStore();
        var queryValues = this.getPayTypeFormSearch().getValues();
        if (queryValues != null) {
            queryValues.ReportType = '2';
            dpStore.proxy.extraParams = queryValues;
            dpStore.load();
        } else {
            Ext.MessageBox.alert('系统提示', '请输入过滤条件！');
        }
    },
    exportRechargeReport: function (btn) {
        var grid = btn.up("grid");
        var store = grid.getStore();
        var queryValues = btn.up("form").getValues();
        queryValues.ReportType = this.getRechargeReportCmb().getValue();
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
        store.exportRechargeReport(queryValues, success, failure);
    },
});