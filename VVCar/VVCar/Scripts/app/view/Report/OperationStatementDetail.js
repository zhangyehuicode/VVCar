Ext.define('WX.view.Report.OperationStatementDetail', {
    extend: 'Ext.window.Window',
    alias: 'widget.OperationStatementDetail',
    title: '收支报表详情',
    layout: 'fit',
    width: 700,
    height: 500,
    bodyPadding: 5,
    autoShow: false,
    modal: true,
    buttonAlign: 'right',
    initComponent: function () {
        var me = this;
        var store = Ext.create('WX.store.BaseData.OperationStatementDetailStore');
        store.limit = 10;
        store.pageSize = 10;
        me.items = [{
            xtype: 'grid',
            name: 'gridOperationStatement',
            stripeRows: true,
            loadMask: true,
            store: store,
            selType: 'checkboxmodel',
            tbar: {
                xtype: 'form',
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
                items: [{
                    name: 'StartDate',
                    xtype: 'textfield',
                    fieldLabel: '时间',
                    width: 200,
                    labelWidth: 60,
                    margin: '0 0 0 5',
                    hidden: true,
                }, {
                    name: 'TradeNo',
                    xtype: 'textfield',
                    fieldLabel: '单号',
                    width: 200,
                    labelWidth: 30,
                    margin: '0 0 0 5',
                }, {
                    xtype: 'combobox',
                    name: 'BudgetType',
                    store: [
                        [0, '收入'],
                        [1, '支出'],
                    ],
                    fieldLabel: '收支类型',
                    margin: '0 0 0 15',
                    width: 170,
                    labelWidth: 60,
                }, {
                    xtype: 'combobox',
                    name: 'ResourceType',
                    store: [
                        [0, '报销单'],
                        [1, '商城订单'],
                        [2, '接车单'],
                    ],
                    fieldLabel: '数据来源',
                    margin: '0 0 0 15',
                    width: 170,
                    labelWidth: 60,
                }, {
                    action: 'search',
                    xtype: 'button',
                    text: '搜索',
                    margin: '0 0 0 5',
                }]
            },
            columns: [
                { header: '单号', dataIndex: 'TradeNo', flex: 2 },
                {
                    header: '收支类型', dataIndex: 'BudgetType', flex: 1,
                    renderer: function (value) {
                        if (value == 0) {
                            return '<span style="color:green">收入</span>';
                        }
                        if (value == 1) {
                            return '<span style="color:red">支出</span>';
                        }
                    }
                },
                { header: '金额', dataIndex: 'Money', flex: 1 },
                {
                    header: '数据来源', dataIndex: 'ResourceType', flex: 1,
                    renderer: function (value) {
                        if (value == 0) {
                            return '报销单';
                        }
                        if (value == 1) {
                            return '商城订单';
                        }
                        if (value == 2) {
                            return '接车单';
                        }
                    }
                },
            ],
            bbar: {
                xtype: 'pagingtoolbar',
                displayInfo: true,
            },
        }];
        me.callParent(arguments);
    }
})