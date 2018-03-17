Ext.define('WX.view.Shop.Order', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.Order',
    title: '订单',
    store: Ext.create('WX.store.BaseData.OrderStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        me.tbar = [{
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
                xtype: 'textfield',
                name: 'TNoLMPAddEN',
                width: 275,
                margin: '0 0 0 10',
                emptyText: '订单号/联系人/联系电话/收货地址/快递单号',
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
            { header: '序号', dataIndex: 'Index', width: 60 },
            { header: '订单号', dataIndex: 'TradeNo', flex: 1 },
            { header: '订单日期', dataIndex: 'CreatedDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', flex: 1 },
            { header: '数量', dataIndex: 'Quantity', width: 80 },
            { header: '备注', dataIndex: 'Remark', flex: 1 },
            { header: '联系人', dataIndex: 'LinkMan', flex: 1 },
            { header: '联系电话', dataIndex: 'Phone', flex: 1 },
            { header: '详细地址', dataIndex: 'Address', width: 200, },
            {
                header: '发货状态', dataIndex: 'Status', width: 80,
                renderer: function (value) {
                    if (value == 1)
                        return "已发货";
                    else
                        return "未发货";
                }
            },
            { header: '快递单号', dataIndex: 'ExpressNumber', flex: 1 },
            {
                text: '操作',
                xtype: 'actioncolumn',
                width: 180,
                sortable: false,
                menuDisabled: true,
                height: 30,
                align: 'center',
                items: [{
                    action: 'editItem',
                    iconCls: 'x-fa fa-pencil',
                    tooltip: '编辑',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('editActionClick', grid, record);
                    },
                }, { scope: this }, {
                    action: 'deleteItem',
                    iconCls: 'x-fa fa-close',
                    tooltip: '删除',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('deleteActionClick', grid, record);
                    },
                }]
            },
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