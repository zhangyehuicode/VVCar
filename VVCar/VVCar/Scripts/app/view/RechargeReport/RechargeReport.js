Ext.define('WX.view.RechargeReport.RechargeReport', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.RechargeReport',
    requires: ['WX.view.RechargeReport.RHDepartmentReport', 'WX.view.RechargeReport.RHMemberReport', 'WX.view.RechargeReport.RHPayTypeReport'],
    title: '会员储值统计',
    layout: {
        type: 'vbox',
        align: 'stretch',
        pack: 'start',
    },
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        me.items = [{
            xtype: "container",
            padding: '12 13 0 13',
            items: [{
                xtype: 'combobox',
                name: 'ReportType',
                fieldLabel: '报表类型',
                labelWidth: 60,
                width: 315,
                store: Ext.getStore('DataDict.RechargeReportTypeStore'),
                displayField: 'DictName',
                valueField: 'DictValue',
                mode: 'local',
                editable: false,
                allowBlank: false,
                forceSelection: true,
                value: 0
            }]
        }, {
            xtype: 'RHDepartmentReport',//门店储值业绩报表
            flex: 1,
        }, {
            xtype: 'RHMemberReport',//业务员储值业绩报表
            flex: 1,
        }, {
            xtype: 'RHPayTypeReport',//支付方式报表
            flex: 1,
        }];
        me.callParent(arguments);
    },
});