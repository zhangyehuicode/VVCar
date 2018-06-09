Ext.define('WX.view.Shop.StockRecord', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.StockRecord',
    title: '库存记录',
    store: Ext.create('WX.store.BaseData.StockRecordStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var stocktype = Ext.create('Ext.data.Store', {
            fields: ['Name', 'Value'],
            data: [
                { 'Name': '出库', 'Value': 0 },
                { 'Name': '入库', 'Value': 1 },
            ]
        });
        me.tbar = [{
            xtype: 'form',
            layout: 'column',
            border: false,
            frame: false,
            labelAlign: 'left',
            buttonAlign: 'right',
            padding: 5,
            width: '100%',
            autoWidth: true,
            autoScroll: false,
            columnWidth: 1,
            items: [{
                xtype: 'textfield',
                name: 'NameCodeStaff',
                fieldLabel: '关键字',
                labelWidth: 50,
                width: 245,
                emptyText: '产品名称/产品编码/操作员工',
            }, {
                xtype: 'datefield',
                name: 'CreatedDate',
                width: 180,
                labelWidth: 30,
                format: 'Y-m-d',
                fieldLabel: '日期',
                margin: '0 0 0 10',
            }, {
                xtype: 'combobox',
                name: 'StockRecordType',
                fieldLabel: '类型',
                queryMode: 'local',
                store: stocktype,
                displayField: 'Name',
                valueField: 'Value',
                width: 150,
                labelWidth: 30,
                margin: '0 0 0 10',
            }, {
                action: 'search',
                xtype: 'button',
                text: '搜索',
                iconCls: 'fa fa-search',
                cls: 'submitBtn',
                margin: '0 0 0 10'
            }]
        }];
        me.columns = [
            { header: '产品类别', dataIndex: 'ProductCategoryName', flex: 1 },
            { header: '产品名称', dataIndex: 'ProductName', flex: 1 },
            { header: '产品编码', dataIndex: 'ProductCode', flex: 1 },
            {
                header: '类型', dataIndex: 'StockRecordType', width: 80,
                renderer: function (value) {
                    if (value == 0)
                        return "<span style='color:red;''>出库</span>";
                    else if (value == 1)
                        return "<span style='color:green;''>入库</span>";
                }
            },
            {
                header: '数量', dataIndex: 'Quantity', width: 100,
                renderer: function (value) {
                    if (value < 0)
                        return "<span style='color:red;''>" + value + "</span>";
                    else if (value > 0)
                        return "<span style='color:green;''>" + value + "</span>";
                }
            },
            { header: '备注', dataIndex: 'Reason', flex: 1 },
            {
                header: '来源', dataIndex: 'Source', width: 60,
                renderer: function (value) {
                    if (value == 0)
                        return "<span style='color:green;''>微信</span>";
                    else if (value == 1)
                        return "<span style='color:green;''>后台</span>";
                }
            },
            { header: '操作员工', dataIndex: 'StaffName', flex: 1 },
            { header: '操作时间', dataIndex: 'CreatedDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', flex: 1 },
        ];
        me.dockedItems = [{
            xtype: 'pagingtoolbar',
            store: me.store,
            dock: 'bottom',
            displayInfo: true
        }];
        this.callParent();
    },
    afterRender: function () {
        this.callParent();
        this.store.load();
    },
});