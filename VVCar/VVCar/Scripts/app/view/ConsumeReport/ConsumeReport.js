Ext.define('WX.view.ConsumeReport.ConsumeReport', {
    extend: 'Ext.container.Container',
    alias: 'widget.ConsumeReport',
    title: '会员消费统计报表',
    layout: {
        type: 'vbox',
        align: 'stretch',
        pack: 'start',
    },
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var departmentStore = Ext.create("WX.store.BaseData.DepartmentStore");
        Ext.apply(departmentStore.proxy.extraParams, { All: true });

        var consumeReportStore = Ext.create("WX.store.Reporting.ConsumeReportStore");
        me.gridConsumeReport = Ext.create('Ext.grid.Panel', {
            name: 'gridConsumeReport',
            //title: '门店储值消费报表',
            store: consumeReportStore,
            flex: 1,
            border: false,
            tbar: [{
                xtype: 'form',
                name: 'formSearch',
                layout: 'column',
                border: false,
                frame: false,
                labelAlign: 'left',
                buttonAlign: 'right',
                labelWidth: 100,
                padding: '0 5 0 5',
                autoWidth: true,
                autoScroll: true,
                columnWidth: 1,
                fieldDefaults: {
                    labelAlign: 'left',
                    labelWidth: 60,
                    width: 190,
                    margin: '0 5 0 0',
                },
                items: [{
                    xtype: "datefield",
                    name: "StartDate",
                    fieldLabel: "统计时间",
                    format: "Y-m-d",
                    value: new Date(),
                }, {
                    xtype: "datefield",
                    name: "FinishDate",
                    fieldLabel: "至",
                    labelWidth: 20,
                    format: "Y-m-d",
                    width: 155,
                    value: new Date(),
                }, {
                    xtype: 'combobox',
                    fieldLabel: '统计门店',
                    name: 'TradeDepartment',
                    store: departmentStore,
                    displayField: 'Name',
                    valueField: 'ID',
                    width: 210,
                }, {
                    action: 'searchConsumeReport',
                    xtype: 'button',
                    text: '搜 索',
                    iconCls: 'fa fa-search',
                    cls: 'submitBtn',
                    margin: '0 0 0 5',
                }, {
                    action: 'export',
                    xtype: 'button',
                    text: '导出',
                    iconCls: 'fa fa-download',
                    cls: '',
                    margin: '0 0 0 5',
                }]
            }],
            columns: [
                { header: '门店', dataIndex: 'TradeDepartmentName', width: 200, },
                { header: '交易笔数', dataIndex: 'TradeBillCount', width: 200, },
                { header: '消费总额', dataIndex: 'TradeAmount', flex: 1, },
            ],
            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: consumeReportStore,
                dock: 'bottom',
                displayInfo: true
            }]
        });
        this.items = [{
            xtype: "container",
            padding: '12 13 0 13',
            items: [{
                xtype: 'combobox',
                name: 'ReportType',
                fieldLabel: '报表类型',
                labelWidth: 60,
                width: 315,
                store: Ext.getStore('DataDict.ConsumeReportTypeStore'),
                displayField: 'DictName',
                valueField: 'DictValue',
                mode: 'local',
                editable: false,
                allowBlank: false,
                forceSelection: true,
                value: 0
            }]
        }, me.gridConsumeReport];
        this.callParent();
    },

    afterRender: function () {
        this.callParent(arguments);
    }
});
