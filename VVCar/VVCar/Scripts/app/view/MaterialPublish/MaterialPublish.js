Ext.define("WX.view.MaterialPublish.MaterialPublish", {
    extend: 'Ext.container.Container',
    alias: 'widget.MaterialPublish',
    title: '广告发布',
    layout: 'hbox',
    align: 'stretch',
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var materialPublishStore = Ext.create('WX.store.BaseData.MaterialPublishStore');
        materialPublishStore.load();
        var materialPublishItemStore = Ext.create('WX.store.BaseData.MaterialPublishItemStore');
        me.items = [{
            xtype: 'grid',
            name: 'gridMaterialPublish',
            title: '广告发布',
            flex: 6,
            height: "100%",
            store: materialPublishStore,
            stripeRow: true,
            selType: 'checkboxmodel',
            selModel: {
                selection: 'rowmodel',
                mode: 'single'
            },
            tbar: [
                {
                    action: 'addMaterialPublish',
                    xtype: 'button',
                    text: '添加广告',
                    iconCls: 'x-fa fa-plus-circle'
                }, {
                    action: 'deleteMaterialPublish',
                    xtype: 'button',
                    text: '删除广告',
                    iconCls: 'x-fa fa-close'
                }, {
                    action: 'batchHandMaterialPublish',
                    xtype: 'button',
                    text: '发布',
                    scope: this,
                    iconCls: 'fa fa-hand-paper-o',
                }, {
                    action: 'batchHandCancelMaterialPublish',
                    xtype: 'button',
                    text: '取消发布',
                    scope: this,
                    iconCls: 'fa fa-close',
                }, {
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
                        name: 'Name',
                        fieldLabel: '广告名称',
                        width: 170,
                        labelWidth: 60,
                        margin: '0 0 0 5'
                    }, {
                        xtype: 'combobox',
                        fieldLabel: '发布状态',
                        name: 'Status',
                        width: 175,
                        labelWidth: 60,
                        margin: '0 0 0 5',
                        store: [
                            [0, '未发布'],
                            [1, '已发布'],
                            [2, '当前发布'],
                            [-1, '取消发布'],
                        ],
                        queryMode: 'local',
                        displayField: 'DictName',
                        valueField: 'DictValue',
                    }, {
                        action: 'search',
                        xtype: 'button',
                        text: '搜索',
                        iconCls: 'fa fa-search',
                        cls: 'submitBtn',
                        margin: '0 0 0 5'
                    }]
                }
            ],
            columns: [
                //{ header: '编号', dataIndex: 'Code', flex: 1 },
                { header: '名称', dataIndex: 'Name', flex: 1 },
                {
                    header: '发布时间', dataIndex: 'PublishDate', flex: 1,
                    //renderer: Ext.util.Format.dateRenderer('Y-m-d'),
                },
                {
                    header: '发布状态', dataIndex: 'Status', flex: 1,
                    renderer: function (value) {
                        if (value == 0) {
                            return '<span><font>未发布</font></span>';
                        }
                        if (value == 1) {
                            return '<span><font color="blue">已发布</font></span>';
                        }
                        if (value == 2) {
                            return '<span><font color="green">当前发布</font></span>';
                        }
                        if (value == -1) {
                            return '<span><font color="red">取消发布</font></span>';
                        }
                    }
                },
                { header: '创建日期', dataIndex: 'CreatedDate', flex: 1 },
            ],
            bbar: {
                xtype: "pagingtoolbar",
                store: materialPublishStore,
                dock: "bottom",
                displayInfo: true
            }
        }, {
            xtype: 'splitter',
        }, {
            xtype: 'grid',
            name: 'gridMaterialPublishItem',
            title: '素材',
            flex: 4,
            stripeRows: true,
            store: materialPublishItemStore,
            selType: 'checkboxmodel',
            height: '100%',
            tbar: [
                {
                    action: 'addMaterialPublishItem',
                    xtype: 'button',
                    text: '添加素材',
                    iconCls: 'x-fa fa-plus-circle',
                    margin: '5 5 5 5',
                }, {
                    action: 'deleteMaterialPublishItem',
                    xtype: 'button',
                    text: '删除素材',
                    iconCls: 'x-fa fa-close',
                    margin: '5 5 5 5',
                }, {
                    action: 'fleshMaterialPublishItem',
                    xtype: 'button',
                    text: '刷新',
                    iconCls: 'x-fa fa-refresh',
                    margin: '5 5 5 5',
                }
            ],
            columns: [
                { header: '序号', dataIndex: 'Index', flex: 1 },
                { header: '名称', dataIndex: 'Name', flex: 1 },
                {
                    header: '素材', dataIndex: 'Url', width: 300,
                    renderer: function (value) {
                        if (value != "" && value != null) {
                            var extension = value.substring(value.lastIndexOf("."), value.length);
                            var reg = new RegExp('.jpg|.gif|.png|.jpeg');
                            if (reg.test(extension)) {
                                return '<img src="' + value + '" style="width:200px; height: 100px;" />';
                            } else {
                                return '<video src="' + value + '" width=200 height=100 controls></video>';
                            }
                        }
                    },
                },
                {
                    text: '操作功能',
                    xtype: 'actioncolumn',
                    width: 100,
                    sortable: false,
                    menuDisabled: true,
                    height: 30,
                    align: 'center',
                    items: [{
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
                    }]
                }
            ],
            bbar: {
                xtype: "pagingtoolbar",
                store: materialPublishItemStore,
                dock: "bottom",
                displayInfo: true
            }
        }]
        this.callParent();
    }
});