Ext.define('WX.view.MemberTrade.TradeHistory', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.TradeHistory',
    title: '会员消费记录',
    store: Ext.create('WX.store.BaseData.TradeHistoryStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    viewConfig: {
        forceFit: true,
        getRowClass: function (record, rowIndex, rowParams, store) {
            if (record.data.BusinessType === 1) {
                return 'x-grid-record-red';
            } else {
                return '';
            }
        }
    },
    initComponent: function () {
        var memberCardStatus = Ext.getStore('DataDict.MemberCardStatusStore');
        var tradeSources = Ext.getStore('DataDict.TradeSourceStore');
        var businessTypeStore = Ext.create('WX.store.DataDict.BusinessTypeStore');
        var departmentStore = Ext.create("WX.store.BaseData.DepartmentStore");
        var consumeTypes = Ext.getStore('DataDict.ConsumeTypeStore');
        var memberCardTypeStore = Ext.create('WX.store.BaseData.MemberCardTypeStore');
        Ext.apply(departmentStore.proxy.extraParams, { All: true });
        this.tbar = [{
            xtype: 'form',
            name: 'formSearch',
            layout: 'column',
            border: false,
            frame: false,
            labelAlign: 'left',
            buttonAlign: 'right',
            labelWidth: 100,
            padding: 5,
            autoWidth: true,
            autoScroll: true,
            columnWidth: 1,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 60,
                width: 190,
                margin: '0 0 0 5',
            },
            items: [
                {
                    xtype: "container",
                    layout: "vbox",
                    items: [
                        {
                            xtype: "container",
                            margin: "0 0 10 0",
                            layout: "hbox",
                            items: [
                                {
                                    xtype: 'textfield',
                                    name: 'CardNumber',
                                    fieldLabel: '会员卡号',
                                }, {
                                    xtype: 'textfield',
                                    name: 'MobilePhoneNo',
                                    fieldLabel: '手机号码',
                                }, {
                                    xtype: 'combobox',
                                    name: 'Status',
                                    fieldLabel: '会员状态',
                                    store: memberCardStatus,
                                    displayField: 'DictName',
                                    valueField: 'DictValue',
                                }, {
                                    xtype: 'combobox',
                                    name: 'BusinessType',
                                    fieldLabel: '业务类型',
                                    store: businessTypeStore,
                                    displayField: 'DictName',
                                    valueField: 'DictValue'
                                },
                                //{
                                //    xtype: 'combobox',
                                //    name: 'CardTypeID',
                                //    fieldLabel: '卡片类型',
                                //    store: memberCardTypeStore,
                                //    displayField: 'Name',
                                //    valueField: 'ID',
                                //},
                                {
                                    xtype: "datefield",
                                    name: "StartDate",
                                    fieldLabel: "开始时间",
                                    format: "Y-m-d",
                                    value: new Date()
                                }, {
                                    xtype: "datefield",
                                    name: "FinishDate",
                                    fieldLabel: "结束时间",
                                    format: "Y-m-d",
                                    value: new Date()
                                },
                                {
                                    action: 'search',
                                    xtype: 'button',
                                    text: '搜 索',
                                    iconCls: 'fa fa-search',
                                    cls: 'submitBtn',
                                    margin: '0 0 0 5'
                                }, {
                                    action: 'export',
                                    xtype: 'button',
                                    text: '导 出',
                                    iconCls: 'fa fa-download',
                                    margin: '0 0 0 10'
                                },
                            ]
                        },
                        //{
                        //    xtype: "container",
                        //    layout: "hbox",
                        //    items: [
                        //        {
                        //            xtype: "datefield",
                        //            name: "StartDate",
                        //            fieldLabel: "开始时间",
                        //            format: "Y-m-d",
                        //            value: new Date()
                        //        }, {
                        //            xtype: "datefield",
                        //            name: "FinishDate",
                        //            fieldLabel: "结束时间",
                        //            format: "Y-m-d",
                        //            value: new Date()
                        //        }, {
                        //            //    xtype: 'combobox',
                        //            //    fieldLabel: '消费门店',
                        //            //    name: 'TradeDepartmentID',
                        //            //    store: departmentStore,
                        //            //    displayField: 'Name',
                        //            //    valueField: 'ID',
                        //            //    //}, {
                        //            //    //    xtype: 'combobox',
                        //            //    //    name: 'ConsumeType',
                        //            //    //    fieldLabel: '消费类型',
                        //            //    //    store: consumeTypes,
                        //            //    //    displayField: 'DictName',
                        //            //    //    valueField: 'DictValue'
                        //            //}, {
                        //            //    xtype: 'textfield',
                        //            //    name: 'BatchCode',
                        //            //    fieldLabel: '批次代码',
                        //            //    width: 190,
                        //            //    labelWidth: 60,
                        //            //    margin: '0 0 0 5'
                        //        }, {
                        //            action: 'search',
                        //            xtype: 'button',
                        //            text: '搜 索',
                        //            iconCls: 'fa fa-search',
                        //            cls: 'submitBtn',
                        //            margin: '0 0 0 5'
                        //        }, {
                        //            action: 'export',
                        //            xtype: 'button',
                        //            text: '导 出',
                        //            iconCls: 'fa fa-download',
                        //            margin: '0 0 0 10'
                        //        }
                        //    ]
                        //}
                    ]
                }]
        }];
        this.columns = [
            { header: '交易流水号', dataIndex: 'TradeNo', flex: 1, },
            { header: '会员卡号', dataIndex: 'CardNumber', flex: 1, },
            //{ header: '卡片类型', dataIndex: 'CardTypeDesc', flex: 1 },
            { header: '会员姓名', dataIndex: 'MemberName', flex: 1, },
            { header: '消费金额', dataIndex: 'TradeAmount', flex: 1, },
            { header: '外部订单号', dataIndex: 'OutTradeNo', flex: 1, },
            {
                header: '交易来源', dataIndex: 'TradeSource', flex: 1,
                renderer: function (value, cellmeta, record) {
                    var record = tradeSources.findRecord('DictValue', value);
                    if (record == null) return value;
                    else return record.data.DictName;
                }
            },
            //{ header: '消费类型', dataIndex: 'ConsumeTypeDesc', flex: 1, },
            { header: '会员余额支付金额', dataIndex: 'UseBalanceAmount', flex: 1 },
            { header: '消费时间', dataIndex: 'CreatedDate', flex: 1, },
            { header: '消费门店', dataIndex: 'TradeDepartment', flex: 1, },
            { header: '业务员', dataIndex: 'CreatedUser', flex: 1, },
            { header: "业务类型", dataIndex: 'BusinessTypeDesc', flex: 1 },
            //{ header: '卡片备注', dataIndex: 'CardRemark', flex: 1 }
        ];
        this.dockedItems = [{
            xtype: 'pagingtoolbar',
            store: this.store,
            dock: 'bottom',
            displayInfo: true
        }];
        this.callParent();
    },

    afterRender: function () {
        this.callParent(arguments);
    }
});
