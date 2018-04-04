﻿Ext.define('WX.view.Shop.ProductCategoryList', {
    extend: 'Ext.window.Window',
    alias: 'widget.ProductCategoryList',
    title: '分类管理',
    layout: 'fit',
    width: 700,
    height: 600,
    modal: true,
    initComponent: function () {
        var me = this;
        //var treegridProductCategoryStore = Ext.create('WX.store.BaseData.ProductCategoryTreeStore');
        me.tbar = [{
            action: 'addProductCategory',
            xtype: 'button',
            text: '新增',
            scope: this,
            iconCls: 'add'
        }, {
            action: 'editProductCategory',
            xtype: 'button',
            text: '修改',
            scope: this,
            iconCls: 'edit'
        }, {
            action: 'delProductCategory',
            xtype: 'button',
            text: '删除',
            scope: this,
            iconCls: 'delete'
        }];
        me.items = [{
            xtype: 'Ext.tree.Panel',
            name: 'treegridProductCategory',
            store: '',//treegridProductCategoryStore,
            useArrows: true,
            rootVisible: false,
            stripeRows: true,
            columns: [
                { xtype: 'treecolumn', header: '类别代码', dataIndex: 'Code', flex: 1, },
                { header: '类别名称', dataIndex: 'Name', flex: 1, },
                { header: 'Pos排序', dataIndex: 'Index', flex: 1, },
                {
                    header: '零售显示', dataIndex: 'IsPosUsed', flex: 1,
                    renderer: function (value) {
                        if (value == 1) {
                            return '<span style="color:green;">是</span>';
                        } else {
                            return '<span style="color:red;">否</span>';
                        }
                    }
                },
                {
                    header: '重复下单提醒', dataIndex: 'OverOrderAlertTypeListShow', flex: 1,
                    renderer: function (value) {
                        if (value == "否") {
                            return '<span style="color:red;">否</span>';
                        } else {
                            return '<span style="color:green;">' + value + '</span>';
                        }
                    }
                },
                {
                    header: '强制选择', dataIndex: 'IsRequiredOrderAlert', flex: 1,
                    renderer: function (value) {
                        if (value == 1) {
                            return '<span style="color:green;">是</span>';
                        } else {
                            return '<span style="color:red;">否</span>';
                        }
                    }
                }, {
                    header: '会员产品', dataIndex: 'IsForMember', flex: 1,
                    renderer: function (value) {
                        if (value == 1) {
                            return '<span style="color:green;">是</span>';
                        } else {
                            return '<span style="color:red;">否</span>';
                        }
                    }
                }
            ],
        }];
        me.buttons = [{
            text: '关闭',
            action: 'close',
            scope: me,
            handler: me.close
        }];
        this.callParent();
    },

    afterRender: function () {
        this.callParent(arguments);
    }
});