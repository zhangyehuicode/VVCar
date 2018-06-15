
(function () {
    var rowEditing = Ext.create('Ext.grid.plugin.RowEditing', {
        clicksToMoveEditor: 1,
        autoCancel: false,
        saveBtnText: '保存',
        cancelBtnText: "取消",
        clicksToEdit: 2
    });

    var isLeafStore = Ext.create("Ext.data.Store", {
        fields: ["Name", "Value"],
        data: [
            { "Name": "是", "Value": true },
            { "Name": "否", "Value": false }
        ]
    });

    var isAvailableStore = Ext.create("Ext.data.Store", {
        fields: ["Name", "Value"],
        data: [
            { "Name": "是", "Value": true },
            { "Name": "否", "Value": false }
        ]
    });

    var typeStore = Ext.create("Ext.data.Store", {
        fields: ["Name", "Value"],
        data: [
            { "Name": "组件", "Value": 1 },
            { "Name": "路径", "Value": 2 }
        ]
    });

    var sysMenuStore = Ext.create('WX.store.BaseData.SysMenuStore');

    Ext.define('WX.view.SysMenu.SysMenu', {
        extend: "Ext.tree.Panel",
        alias: "widget.SysMenu",
        title: '菜单管理',
        width: 200,
        height: 150,
        store: sysMenuStore,
        useArrows: true,
        rootVisible: false,
        multiSelect: true,
        //singleExpand: true,
        stripeRows: true,
        loadMask: true,
        closable: true,
        collapsible: true,
        plugins: [rowEditing],
        initComponent: function () {
            Ext.apply(this, {
                store: sysMenuStore,
                tbar: [
                    {
                        action: "addSysMenu",
                        xtype: "button",
                        iconCls: 'x-fa fa-plus-circle',
                        text: "添加"
                    },
                    {
                        action: "refreshSysMenu",
                        xtype: "button",
                        iconCls: 'fa fa-refresh',
                        text: "刷新"
                    }
                ],
                columns: [
                    {
                        xtype: 'treecolumn', //this is so we know which column will show the tree
                        text: '导航标题',
                        width: 350,
                        sortable: true,
                        dataIndex: 'Name',
                        editor: {
                            xtype: "textfield",
                            allowBlank: false
                        }
                    }, {
                        text: "导航图标",
                        flex: 1,
                        dataIndex: "SysMenuIcon",
                        editor: {
                            xtype: "textfield",
                            allowBlank: true
                        }
                    },
                    {
                        text: "模块名称",
                        flex: 1,
                        dataIndex: "Component",
                        editor: {
                            xtype: "textfield",
                            allowBlank: false
                        }
                    },
                    {
                        header: "模块类型",
                        flex: 1,
                        dataIndex: "Type",
                        editor: {
                            xtype: "combobox",
                            displayField: "Name",
                            valueField: "Value",
                            store: typeStore,
                            allowBlank: false,
                        },
                        renderer: function (value) { return value == 1 ? "组件" : (value == 2 ? "路径" : "") }
                    },
                    {
                        text: "模块地址",
                        flex: 1,
                        dataIndex: "SysMenuUrl",
                        editor: {
                            xtype: "textfield",
                            allowBlank: false
                        }
                    },
                    {
                        text: "排序",
                        flex: 1,
                        dataIndex: "Index",
                        editor: {
                            xtype: "numberfield",
                            allowBlank: false
                        }
                    },
                    {
                        header: "是否可用",
                        xtype: "booleancolumn",
                        trueText: "是",
                        falseText: "否",
                        flex: 1,
                        dataIndex: "IsAvailable",
                        editor: {
                            xtype: "combobox",
                            displayField: "Name",
                            valueField: "Value",
                            store: isAvailableStore
                        },
                        allowBlank: false
                    },
                    {
                        header: "是否终极",
                        xtype: "booleancolumn",
                        trueText: "是",
                        falseText: "否",
                        flex: 1,
                        dataIndex: "IsLeaf",
                        editor: {
                            xtype: "combobox",
                            displayField: "Name",
                            valueField: "Value",
                            store: isLeafStore
                        },
                        allowBlank: false
                    },
                    {
                        text: '操作功能',
                        xtype: 'actioncolumn',
                        width: 80,
                        sortable: false,
                        menuDisabled: true,
                        height: 30,
                        align: 'center',
                        items: [{
                            action: 'editItem',
                            iconCls: 'x-fa fa-close',
                            tooltip: '删除',
                            scope: this,
                            margin: '10 10 10 10',
                            handler: function (grid, rowIndex, colIndex) {
                                var record = grid.getStore().getAt(rowIndex);
                                this.fireEvent('deleteActionClick', grid, record);
                            },
                        }
                        ]
                    }
                ]
            });
            this.callParent(arguments);
        },
        render: function () {
            //this.store.load();
            this.callParent(arguments);

        }
    });

}());