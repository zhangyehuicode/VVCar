Ext.define("WX.view.CouponPush.CouponPushList", {
    extend: 'Ext.container.Container',
    alias: 'widget.CouponPushList',
    title: '卡券推送',
    layout: 'hbox',
    align: 'stretch',
    loadMask: true,
    closable: true,
    initComponent: function() {
        var me = this;
        var couponPushStore = Ext.create('WX.store.BaseData.CouponPushStore');
        couponPushStore.load();
        var couponPushItemStore = Ext.create('WX.store.BaseData.CouponPushItemStore');
        var couponPushMemberStore = Ext.create('WX.store.BaseData.CouponPushMemberStore');
        me.items = [{
            xtype: 'grid',
            name: 'gridCouponPush',
            title: '卡券推送任务',
            flex: 6,
            height: "100%",
            store: couponPushStore,
            stripeRow: true,
            selType: 'checkboxmodel',
            selModel: {
                selection: 'rowmodel',
                mode: 'single'
            },
            tbar: [
                {
                    action: 'addCouponPush',
                    xtype: 'button',
                    text: '添加任务',
                    iconCls: 'x-fa fa-plus-circle'
                }, {
                    action: 'deleteCouponPush',
                    xtype: 'button',
                    text: '删除任务',
                    iconCls: 'x-fa fa-close'
                }, {
                    action: 'batchHandCouponPush',
                    xtype: 'button',
                    text: '手动推送',
                    scope: this,
                    iconCls: 'fa fa-arrow-up',
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
                        name: 'Title',
                        fieldLabel: '标题',
                        width: 170,
                        labelWidth: 30,
                        margin: '0 0 0 5'
                    }, {
                        xtype: 'combobox',
                        fieldLabel: '推送状态',
                        name: 'Status',
                        width: 175,
                        labelWidth: 60,
                        margin: '0 0 0 5',
                        store: [
                            [0, '未推送'],
                            [1, '已推送'],
                            [-1, '终止推送'],
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
                { header: '标题', dataIndex: 'Title', flex: 1 },
                {
                    header: '推送时间', dataIndex: 'PushDate', flex: 1,
                    renderer: Ext.util.Format.dateRenderer('Y-m-d')
                },
                {
                    header: '推送状态', dataIndex: 'Status', flex: 1,
                    renderer: function(value) {
                        if (value == 0)
                            return '<span><font>未推送</font></span>';
                        if (value == 1)
                            return '<span><font color="green">已推送</font></span>';
                        if (value == -1)
                            return '<span><font color="red">终止推送</font></span>';
                    }
                },
                {
                    header: '是否推送所有会员', dataIndex: 'PushAllMembers', flex: 1,
                    renderer: function(value) {
                        if (value == 0)
                            return '<span><font>否</font></span>';
                        if (value == 1)
                            return '<span><font>是</font></span>';
                    }
                },
                { header: '创建日期', dataIndex: 'CreatedDate', flex: 1 },
                {
                    text: '操作功能',
                    xtype: 'actioncolumn',
                    width: 80,
                    sortable: false,
                    menuDisabled: true,
                    height: 30,
                    align: 'center',
                    items: [{
                        action: 'updateItem',
                        iconCls: 'x-fa fa-pencil',
                        tooltip: '编辑',
                        scope: this,
                        margin: '10 10 10 10',
                        handler: function(grid, rowIndex, colIndex) {
                            var record = grid.getStore().getAt(rowIndex);
                            this.fireEvent('updateActionClick', grid, record);
                        }
                    }]
                }
            ],
            bbar: {
                xtype: "pagingtoolbar",
                store: couponPushStore,
                dock: "bottom",
                displayInfo: true
            }
        }, {
            xtype: 'splitter',
        }, {
            xtype: 'container',
            flex: 4,
            layout: { type: 'vbox', align: 'stretch' },
            height: '100%',
            autoScroll: true,
            items: [{
                xtype: 'grid',
                name: 'gridCouponPushItem',
                title: '卡券',
                flex: 1,
                stripeRows: true,
                store: couponPushItemStore,
                selType: 'checkboxmodel',
                tbar: [
                    {
                        action: 'addCouponPushItem',
                        xtype: 'button',
                        text: '添加优惠券',
                        iconCls: 'x-fa fa-plus-circle',
                        margin: '5 5 5 5',
                    }, {
                        action: 'addCouponPushCardItem',
                        xtype: 'button',
                        text: '添加会员卡',
                        iconCls: 'x-fa fa-plus-circle',
                    }, {
                        action: 'deleteCouponPushItem',
                        xtype: 'button',
                        text: '删除卡券',
                        iconCls: 'x-fa fa-close',
                        margin: '5 5 5 5',
                    }
                ],
                columns: [
                    { header: '卡券编号', dataIndex: 'TemplateCode', flex: 1 },
                    { header: '优惠券模板标题', dataIndex: 'CouponTemplateTitle', flex: 1 },
                ],
                bbar: {
                    xtype: "pagingtoolbar",
                    store: couponPushItemStore,
                    dock: "bottom",
                    displayInfo: true
                }
            }, {
                xtype: 'splitter',
            }, {
                xtype: 'grid',
                name: 'gridCouponPushMember',
                title: '会员',
                flex: 1,
                stripeRows: true,
                store: couponPushMemberStore,
                selType: 'checkboxmodel',
                tbar: [
                    {
                        action: 'addCouponPushMember',
                        xtype: 'button',
                        text: '添加推送会员',
                        iconCls: 'x-fa fa-plus-circle',
                        margin: '5 5 5 5',
                    }, {
                        action: 'deleteCouponPushMember',
                        xtype: 'button',
                        text: '删除推送会员',
                        iconCls: 'x-fa fa-close',
                        margin: '5 5 5 5',
                    }
                ],
                columns: [
                    { header: '会员名称', dataIndex: 'Name', flex: 1 },
                    { header: '手机号', dataIndex: 'MobilePhoneNo', flex: 1 },
                    { header: '车牌号', dataIndex: 'PlateList', flex: 1 },
                ],
                dockedItems: {
                    xtype: "pagingtoolbar",
                    store: couponPushMemberStore,
                    dock: "bottom",
                    displayInfo: true
                }
            }]
        }]
        this.callParent();
    }
});