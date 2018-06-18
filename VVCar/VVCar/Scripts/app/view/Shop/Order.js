﻿Ext.define('WX.view.Shop.Order', {
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
                fieldLabel: '关键字',
                width: 275,
                labelWidth: 60,
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
            { header: '订单号', dataIndex: 'Code', width: 150 },
            { header: '订单金额', dataIndex: 'Money', width: 80 },
            { header: '订单日期', dataIndex: 'CreatedDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', width: 150 },
            { header: '联系人', dataIndex: 'LinkMan', width: 100 },
            { header: '联系电话', dataIndex: 'Phone', width: 120 },
            { header: '详细地址', dataIndex: 'Address', flex: 1 },
            { header: '备注', dataIndex: 'Remark', flex: 1 },
            {
                header: '发货状态', dataIndex: 'Status', width: 100,
                renderer: function (value) {
                    if (value == 2)
                        return "已发货";
                    else if (value == 1)
                        return "已付款未发货";
                    else if (value == 0)
                        return "未付款";
                    else if (value == 3)
                        return "已完成";
                    else if (value == -1)
                        return "付款不足";
                }
            },
            { header: '快递单号', dataIndex: 'ExpressNumber', width: 180 },
            {
                text: '操作',
                xtype: 'actioncolumn',
                width: 150,
                sortable: false,
                menuDisabled: true,
                height: 30,
                align: 'center',
                items: [{
                    action: 'orderdetails',
                    iconCls: 'x-fa fa-reorder',
                    tooltip: '详情',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('orderdetailsClick', grid, record);
                    },
                }, { scope: this }, {
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