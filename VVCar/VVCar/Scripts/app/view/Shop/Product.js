﻿Ext.define('WX.view.Shop.Product', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.Product',
    title: '商品',
    store: Ext.create('WX.store.BaseData.ProductStore'),
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
                action: 'add',
                xtype: 'button',
                text: '新增',
                scope: this,
                iconCls: 'fa fa-plus-circle',
            }, {
                xtype: 'textfield',
                name: 'Title',
                fieldLabel: '标题',
                width: 170,
                labelWidth: 30,
                margin: '0 0 0 15'
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
            { header: '排序', dataIndex: 'Index', width: 60 },
            { header: '标题', dataIndex: 'Title', width: 60 },
            {
                header: '商品图片', dataIndex: 'ImgUrl', width: 100,
                renderer: function (value) {
                    if (value != "" && value != null) {
                        return '<img src="' + value + '" style="width: 80px; height: 50px;" />';
                    }
                }
            },
            //{
            //    header: '类型', dataIndex: 'ProductType', width: 60,
            //    renderer: function (value) {
            //        if (value == 0)
            //            return '服务';
            //        else
            //            return '商品';
            //    }
            //},
            { header: '原单价', dataIndex: 'BasePrice', width: 80 },
            { header: '销售单价', dataIndex: 'PriceSale', width: 80 },
            { header: '兑换积分', dataIndex: 'Points', width: 80 },
            { header: '兑换上限', dataIndex: 'UpperLimit', width: 80 },
            {
                header: '是否发布', dataIndex: 'IsPublish', width: 80,
                renderer: function (value) {
                    if (value == 1)
                        return '<span style="color:green;">是</span>';
                    else
                        return '<span style="color:red;">否</span>';
                }
            },
            {
                header: '是否推荐', dataIndex: 'IsRecommend', width: 80,
                renderer: function (value) {
                    if (value == 1)
                        return '<span style="color:green;">是</span>';
                    else
                        return '<span style="color:red;">否</span>';
                }
            },
            { header: '库存', dataIndex: 'Stock', width: 60, },
            { header: '生效时间', dataIndex: 'EffectiveDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', flex: 1 },
            { header: '失效时间', dataIndex: 'ExpiredDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', flex: 1 },
            //{ header: '创建人', dataIndex: 'CreatedUser', flex: 1 },
            //{ header: '创建时间', dataIndex: 'CreatedDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', flex: 1 },
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
                }, { scope: this }, {
                    action: 'esc',
                    iconCls: 'x-fa fa-arrow-up',
                    tooltip: '升序',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('escActionClick', grid, record);
                    },
                }, { scope: this }, {
                    action: 'desc',
                    iconCls: 'x-fa fa-arrow-down',
                    tooltip: '降序',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('descActionClick', grid, record);
                    },
                }, { scope: this }, {
                    iconCls: 'x-fa fa-stack-exchange',
                    tooltip: '发布/下架',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('publishsoldoutActionClick', grid, record);
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