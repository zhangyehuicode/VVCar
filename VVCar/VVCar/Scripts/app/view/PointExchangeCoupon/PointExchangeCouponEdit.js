Ext.define('WX.view.PointExchangeCoupon.PointExchangeCouponEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.PointExchangeCouponEdit',
    resizable: false,
    title: '添加积分兑换',
    layout: 'fit',
    width: 850,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        me.addCouponPnl = Ext.create("Ext.panel.Panel", {
            bodyPadding: 5,
            border: 0,
            height: 40,
            items: [{
                xtype: 'label',
                text: '添加优惠券',
                width: 80,
                margin: '20 20 0 0',
            }, {
                xtype: 'button',
                action: 'AddCoupontemplate',
                text: '添加',
            }],
        })
        me.couponGrid = Ext.create("Ext.grid.Panel", {
            height: 70,
            name: 'grid',
            store: Ext.create('WX.store.BaseData.CouponTemplateInfoStore'),
            flex: 1,
            scroll: false,
            columns: [
                { header: '类型', dataIndex: 'CouponTypeName', width: 80 },
                { header: '编号', dataIndex: 'TemplateCode', flex: 1 },
                { header: '标题', dataIndex: 'Title', flex: 1 },
                { header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
                { header: '投放时间', dataIndex: 'PutInDate', flex: 1 },
                { header: '有效期', dataIndex: 'Validity', flex: 1 },
                { header: '库存', dataIndex: 'FreeStock', width: 50 },
                //{ header: '投放时间', dataIndex: 'PutInStartDate', flex: 1, hidden: true },
                {
                    xtype: 'actioncolumn',
                    width: 50,
                    sortable: false,
                    menuDisabled: true,
                    height: 30,
                    align: 'center',
                    text: '操作',
                    items: [
                        { scope: this }, {
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
                }
            ]
        })
        me.form = Ext.create('Ext.form.Panel', {
            height: 280,
            name: 'form',
            border: false,
            margin: '20 0 0 0',
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%',
            },
            items: [
                {
                    xtype: 'textfield',
                    name: 'ID',
                    hidden: true
                },
                {
                    xtype: 'fieldcontainer',
                    fieldLabel: '兑换条件',
                    labelWidth: 80,
                    items: [
                        {
                            xtype: 'radio',
                            flex: 1,
                            //checked: true,
                            boxLabel: '免费兑换',
                            name: 'ExchangeType',
                            inputValue: 0,
                            labelWidth: 80,
                            margin: '0 0 10 0',
                        },
                        {
                            xtype: 'fieldcontainer',
                            layout: {
                                type: 'table',
                                columns: 5
                            },
                            flex: 1,
                            items: [
                                {
                                    xtype: 'radio',
                                    name: 'ExchangeType',
                                    boxLabel: '积分兑换',
                                    inputValue: 1,
                                    labelWidth: 80,
                                    colspan: 2,
                                    width: 200,
                                    margin: '0 0 10 0',
                                }, {
                                    xtype: 'numberfield',
                                    name: 'Point',
                                    allowBlank: false,
                                    fieldLabel: '所需积分',
                                    labelWidth: 80,
                                    colspan: 3,
                                    margin: '0 0 10 0',
                                },
                            ]
                        }
                    ]
                },
                {
                    xtype: 'fieldcontainer',
                    fieldLabel: '兑换有效期',
                    labelWidth: 80,
                    layout: 'hbox',
                    allowBlank: false,
                    items: [{
                        xtype: "datefield",
                        name: "BeginDate",
                        width: 150,
                        format: "Y-m-d",
                    }, {
                        xtype: 'label',
                        text: '~',
                    },
                    {
                        xtype: "datefield",
                        name: "FinishDate",
                        width: 150,
                        labelWidth: 30,
                        allowBlank: false,
                        format: "Y-m-d",
                    }]
                },
            ]
        });

        me.items = [me.addCouponPnl, me.couponGrid, me.form];
        me.buttons = [{
            text: '保存',
            action: 'save',
            cls: 'submitBtn',
            scope: me,
        }, {
            text: '取消',
            scope: me,
            handler: me.close
        }];
        me.callParent(arguments);
    }
});