Ext.define('WX.view.RechargeReport.RHPayTypeReport', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.RHPayTypeReport',
    store: Ext.create('WX.store.Reporting.RechargeReportStore'),
    border: false,
    hidden: true,
    initComponent: function () {
        var me = this;
        me.tbar = [{
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
                width: 170,
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
                width: 140,
                value: new Date(),
            }, {
                xtype: "combobox",
                name: "PaymentType",
                fieldLabel: "支付方式",
                store: Ext.getStore('DataDict.PaymentTypeStore'),
                displayField: "DictName",
                valueField: "DictValue",
                mode: 'local',
                editable: false,
            }, {
                xtype: 'button',
                action: 'pt_search',
                text: '搜 索',
                iconCls: 'fa fa-search',
                cls: 'submitBtn',
                margin: '0 0 0 5'
            }, {
                action: 'export',
                xtype: 'button',
                text: '导出',
                iconCls: '',
                cls: '',
                margin: '0 0 0 5',
            }]
        }];
        me.columns = [
            { header: '支付方式', dataIndex: 'PaymentType', flex: 1, },
            { header: '交易笔数', dataIndex: 'TradeBillCount', flex: 1 },
            { header: '储值总额', dataIndex: 'TotalAmount', flex: 1 },
            { header: '实收总额', dataIndex: 'TotalRechargeAmount', flex: 1 },
            { header: '赠送金额', dataIndex: 'TotalGiveAmount', flex: 1 },
            { header: '发票总额', dataIndex: 'TotalInvoiceAmount', flex: 1 }
        ];
        me.dockedItems = [{
            xtype: "pagingtoolbar",
            store: me.store,
            dock: "bottom",
            displayInfo: true
        }];
        this.callParent(arguments);
    }
});
