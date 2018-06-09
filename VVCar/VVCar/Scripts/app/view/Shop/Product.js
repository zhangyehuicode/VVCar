Ext.define('WX.view.Shop.Product', {
    extend: 'Ext.container.Container',
    alias: 'widget.Product',
    title: '产品管理',
    layout: 'hbox',
    align: 'stretch',
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var productStore = Ext.create('WX.store.BaseData.ProductStore');
        productStore.load();
        var treeProductCategoryStore = Ext.create('WX.store.BaseData.ProductCategoryTreeStore');
        var productTypeStore = Ext.create('WX.store.DataDict.ProductTypeStore');
        me.items = [{
            name: 'treeProductCategory',
            xtype: 'treepanel',
            title: '产品类别',
            height: '100%',
            width: 200,
            useArrows: true,
            animate: true,
            //rootVisible: false,
            displayField: 'Text',
            store: treeProductCategoryStore,
            bbar: ['->', {
                action: 'manageProductCategory',
                xtype: 'button',
                text: '分类管理',
                tooltip: '分类管理',
                width: 100,
                margin: '0 0 2 0',
                permissionCode: 'Portal.BaseDataEdit',
            }, '->'],
        }, {
            xtype: 'grid',
            name: 'grdProduct',
            title: '产品列表',
            flex: 1,
            store: productStore,
            stripeRows: true,
            height: '100%',
            tbar: [{
                xtype: 'form',
                layout: 'column',
                border: false,
                frame: false,
                labelAlign: 'left',
                buttonAlign: 'right',
                labelWidth: 100,
                //padding: 5,
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
                    name: 'Name',
                    fieldLabel: '名称',
                    width: 170,
                    labelWidth: 30,
                    margin: '0 0 0 15'
                }, {
                    xtype: 'combobox',
                    name: 'ProductType',
                    store: productTypeStore,
                    loadMode: 'local',
                    fieldLabel: '产品类型',
                    displayField: 'DictName',
                    valueField: 'DictValue',
                    editable: true,
                    margin: '0 0 0 15',
                    width: 170,
                    labelWidth: 60,
                }, {
                    action: 'search',
                    xtype: 'button',
                    text: '搜索',
                    iconCls: 'fa fa-search',
                    cls: 'submitBtn',
                    margin: '0 0 0 10'
                }, {
                    action: 'changeCategory',
                    xtype: 'button',
                    text: '移动至其他分类',
                    scope: this,
                    permissionCode: 'Portal.BaseDataEdit',
                    margin: '0 0 0 5'
                }, {
                    action: 'stockOutIn',
                    xtype: 'button',
                    text: '出/入库',
                    scope: this,
                    permissionCode: 'Portal.BaseDataEdit',
                    margin: '0 0 0 5'
                }]
            }],
            columns: [
                { header: '排序', dataIndex: 'Index', width: 60 },
                { header: '标题', dataIndex: 'Name', flex: 1 },
                { header: '编码', dataIndex: 'Code', width: 80 },
                {
                    header: '商品图片', dataIndex: 'ImgUrl', width: 100,
                    renderer: function (value) {
                        if (value != "" && value != null) {
                            return '<img src="' + value + '" style="width: 80px; height: 50px;" />';
                        }
                    }
                },
                {
                    header: '类型', dataIndex: 'ProductType', width: 60,
                    renderer: function (value) {
                        if (value == 0)
                            return '服务';
                        else
                            return '商品';
                    }
                },
                { header: '原单价', dataIndex: 'BasePrice', width: 100 },
                { header: '销售单价', dataIndex: 'PriceSale', width: 100 },
                {
                    header: '库存', dataIndex: 'Stock', width: 100,
                    renderer: function (value) {
                        return '<span style="color:green;">' + value + '</span>';
                    }
                },
                { header: '单位', dataIndex: 'Unit', width: 60 },
                {
                    header: '积分兑换', dataIndex: 'IsCanPointExchange', width: 80,
                    renderer: function (value) {
                        if (value)
                            return '<span style="color:green;">允许</span>';
                        else
                            return '<span style="color:red;">关闭</span>';
                    }
                },
                { header: '兑换积分', dataIndex: 'Points', width: 80 },
                { header: '兑换上限', dataIndex: 'UpperLimit', width: 80 },
                {
                    header: '是否上架', dataIndex: 'IsPublish', width: 80,
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
                {
                    header: '抽成比例', dataIndex: 'CommissionRate', width: 80,
                    renderer: function (value) {
                        return value + '%';
                    }
                },
                //{ header: '生效时间', dataIndex: 'EffectiveDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', flex: 1 },
                //{ header: '失效时间', dataIndex: 'ExpiredDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', flex: 1 },
                //{ header: '创建人', dataIndex: 'CreatedUser', flex: 1 },
                //{ header: '创建时间', dataIndex: 'CreatedDate', xtype: 'datecolumn', format: 'Y-m-d H:i:s', flex: 1 },
                {
                    text: '操作',
                    xtype: 'actioncolumn',
                    width: 200,
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
                        tooltip: '上架/下架',
                        scope: this,
                        margin: '10 10 10 10',
                        handler: function (grid, rowIndex, colIndex) {
                            var record = grid.getStore().getAt(rowIndex);
                            this.fireEvent('publishsoldoutActionClick', grid, record);
                        },
                    }]
                },
            ],
            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: productStore,
                dock: 'bottom',
                displayInfo: true
            }]
        }];
        this.callParent(arguments);
    },
    //afterRender: function () {
    //    this.callParent(arguments);
    //},
});