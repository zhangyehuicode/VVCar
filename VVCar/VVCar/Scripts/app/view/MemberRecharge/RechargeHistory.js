(function () {
    var rowEditing = Ext.create('Ext.grid.plugin.RowEditing', {
        clicksToMoveEditor: 1,
        autoCancel: false,
        saveBtnText: '保存',
        cancelBtnText: "取消",
        autoCancel: false,
        clicksToEdit: 2
    });
    function getColumnLeft() {
        var hcs = document.getElementsByClassName('x-column-header-inner');
        for (var i = 0; i < hcs.length; i++) {
            var hc = hcs[i];
            var ht = hc.getElementsByTagName('span')[0].innerHTML;
            if (ht.indexOf('是否开票') >= 0) {
                return hc.parentElement.style.left;
            }
        }
    }

    Ext.define('WX.view.MemberRecharge.RechargeHistory', {
        extend: 'Ext.grid.Panel',
        alias: 'widget.RechargeHistory',
        title: '会员储值记录',
        store: Ext.create('WX.store.BaseData.RechargeHistoryStore'),
        stripeRows: true,
        loadMask: true,
        closable: true,
        plugins: [rowEditing],
        viewConfig: {
            forceFit: true,
            getRowClass: function (record, rowIndex, rowParams, store) {
                if (record.data.PaymentType === 7) {
                    return 'x-grid-record-red';
                } else {
                    return '';
                }
            }
        },
        initComponent: function () {
            var memberCardStatus = Ext.getStore('DataDict.MemberCardStatusStore');
            var paymentTypes = Ext.getStore('DataDict.PaymentTypeStore');
            var tradeSources = Ext.getStore('DataDict.TradeSourceStore');
            var businessTypeStore = Ext.create('WX.store.DataDict.BusinessTypeStore');
            var departmentStore = Ext.create("WX.store.BaseData.DepartmentStore");
            var memberCardTypeStore = Ext.create('WX.store.BaseData.MemberCardTypeStore');
            Ext.apply(departmentStore.proxy.extraParams, { All: true });
            this.tbar = [
                {
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
                                        }, {
                                            //xtype: 'combobox',
                                            //name: 'CardTypeID',
                                            //fieldLabel: '卡片类型',
                                            //store: memberCardTypeStore,
                                            //displayField: 'Name',
                                            //valueField: 'ID',
                                        },
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
                                        }, {
                                            //    xtype: 'combobox',
                                            //    fieldLabel: '储值门店',
                                            //    name: 'TradeDepartmentID',
                                            //    store: departmentStore,
                                            //    displayField: 'Name',
                                            //    valueField: 'ID',
                                            //}, {
                                            //    xtype: 'textfield',
                                            //    name: 'BatchCode',
                                            //    fieldLabel: '批次代码',
                                            //    width: 190,
                                            //    labelWidth: 60,
                                            //    margin: '0 0 0 5'
                                        }, {
                                            action: 'search',
                                            xtype: 'button',
                                            text: '搜 索',
                                            iconCls: 'fa fa-search',
                                            cls: 'submitBtn',
                                            margin: '0 0 0 5',
                                        }, {
                                            action: 'export',
                                            xtype: 'button',
                                            text: '导 出',
                                            iconCls: 'fa fa-download',
                                            margin: '0 0 0 10'
                                        }
                                    ]
                                }, {
                                    //xtype: "container",
                                    //layout: "hbox",
                                    //items: [
                                    //    //{
                                    //    //    xtype: "datefield",
                                    //    //    name: "StartDate",
                                    //    //    fieldLabel: "开始时间",
                                    //    //    format: "Y-m-d",
                                    //    //    value: new Date()
                                    //    //}, {
                                    //    //    xtype: "datefield",
                                    //    //    name: "FinishDate",
                                    //    //    fieldLabel: "结束时间",
                                    //    //    format: "Y-m-d",
                                    //    //    value: new Date()
                                    //    //}, {
                                    //    //    //    xtype: 'combobox',
                                    //    //    //    fieldLabel: '储值门店',
                                    //    //    //    name: 'TradeDepartmentID',
                                    //    //    //    store: departmentStore,
                                    //    //    //    displayField: 'Name',
                                    //    //    //    valueField: 'ID',
                                    //    //    //}, {
                                    //    //    //    xtype: 'textfield',
                                    //    //    //    name: 'BatchCode',
                                    //    //    //    fieldLabel: '批次代码',
                                    //    //    //    width: 190,
                                    //    //    //    labelWidth: 60,
                                    //    //    //    margin: '0 0 0 5'
                                    //    //}, {
                                    //    //    action: 'search',
                                    //    //    xtype: 'button',
                                    //    //    text: '搜 索',
                                    //    //    iconCls: 'fa fa-search',
                                    //    //    cls: 'submitBtn',
                                    //    //    margin: '0 0 0 5',
                                    //    //}, {
                                    //    //    action: 'export',
                                    //    //    xtype: 'button',
                                    //    //    text: '导 出',
                                    //    //    iconCls: 'fa fa-download',
                                    //    //    margin: '0 0 0 10'
                                    //    //}
                                    //]
                                }
                            ]
                        }
                    ]
                }];
            this.columns = [
                { header: '交易流水号', dataIndex: 'TradeNo', flex: 1, },
                { header: '会员卡号', dataIndex: 'CardNumber', flex: 1, },
                //{ header: '卡片类型', dataIndex: 'CardTypeDesc', flex: 1 },
                { header: '会员姓名', dataIndex: 'MemberName', flex: 1, },
                { header: '储值金额', dataIndex: 'TradeAmount', flex: 1, },
                { header: '赠送金额', dataIndex: 'GiveAmount', flex: 1, },
                {
                    header: '支付方式', dataIndex: 'PaymentType', flex: 1,
                    renderer: function (value, cellmeta) {

                        //设置编辑控件样式
                        if (document.head.getElementsByTagName("style").length === 0) {
                            var st = document.createElement("style");

                            //st.innerText = "#roweditorbuttons-1076{left:" + (parseInt(document.getElementById('booleancolumn-1037').style.left)+25) + "px !important;}";
                            st.innerText = ".x-grid-row-editor-buttons{left:" + (parseInt(getColumnLeft()) + 25) + "px !important;}";
                            document.head.appendChild(st);
                        }
                        if (value === 7)
                            return "";
                        var record = paymentTypes.findRecord('DictValue', value);
                        if (record == null) return value;
                        else return record.data.DictName;
                    }
                },
                {
                    header: '交易来源', dataIndex: 'TradeSource', flex: 1,
                    renderer: function (value, cellmeta, record) {
                        var record = tradeSources.findRecord('DictValue', value);
                        if (record == null) return value;
                        else return record.data.DictName;
                    }
                },
                { header: '储值时间', dataIndex: 'CreatedDate', flex: 1 },
                { header: '储值门店', dataIndex: 'TradeDepartment', flex: 1 },
                { header: '业务员', dataIndex: 'CreatedUser', flex: 1 },
                //{
                //    text: '是否开票', dataIndex: 'HasDrawReceipt', xtype: "booleancolumn", trueText: "是", falseText: "否", flex: 1, align: "center",
                //    editor: {
                //        xtype: "checkbox"
                //    }
                //},
                //{
                //    header: '开票金额', dataIndex: 'DrawReceiptMoney', flex: 1,
                //    editor: {
                //        xtype: "textfield",
                //        vtype: "Number",
                //        //regex: /^\d+(\.\d+)?$/,
                //        allowBlank: false
                //    }
                //},
                //{ header: '开票人', dataIndex: 'DrawReceiptUser', flex: 1 },
                //{ header: '开票门店', dataIndex: 'DrawReceiptDepartment', flex: 1 },
                {
                    header: "业务类型", dataIndex: 'BusinessType', flex: 1, renderer: function (value) {
                        var record = businessTypeStore.findRecord('DictValue', value);
                        if (record == null) return value;
                        else return record.data.DictName;
                    }
                },
                //{ header: '卡片备注', dataIndex: 'CardRemark', flex: 1 },
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
            window.onresize = function () {
                var to = setTimeout(function () {
                    var styles = document.head.getElementsByTagName("style");
                    if (styles.length === 0)
                        return;
                    styles[0].innerText = ".x-grid-row-editor-buttons{left:" + (parseInt(getColumnLeft()) + 25) + "px !important;}";
                    clearTimeout(to);
                }, 100);
            }
        }
    });

}());