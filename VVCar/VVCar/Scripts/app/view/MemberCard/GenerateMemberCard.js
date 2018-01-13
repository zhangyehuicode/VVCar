Ext.define('WX.view.MemberCard.GenerateMemberCard', {
    extend: 'Ext.window.Window',
    alias: 'widget.GenerateMemberCard',
    title: '卡片制作',
    stripeRows: true,
    loadMask: true,
    closable: true,
    width: 1350,
    height: 605,
    layout: 'fit',
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        var memberCardStore = Ext.create('WX.store.BaseData.MemberCardStore');
        var memberGroupStore = Ext.create('WX.store.BaseData.MemberGroupStore');
        var memberCardTypeStore = Ext.create('WX.store.BaseData.MemberCardTypeStore');
        var memberGradeStore = Ext.create('WX.store.BaseData.MemberGradeStore');
        Ext.apply(memberGradeStore.proxy.extraParams, { Start: null, Limit: null, Status: 1 });
        memberGradeStore.load();

        memberGroupStore.load();

        me.items = [
            {
                xtype: 'form',
                layout: 'column',
                border: false,
                frame: false,
                labelAlign: 'left',
                buttonAlign: 'right',
                labelWidth: 100,
                padding: 5,
                width: 1170,
                height: 600,
                //autoWidth: true,
                //autoScroll: true,
                columnWidth: 1,
                items: [
                    {
                        xtype: 'textfield',
                        name: 'BatchCode',
                        fieldLabel: '批次代码',
                        width: 170,
                        labelWidth: 60,
                        margin: '0 0 0 5',
                        allowBlank: false,
                        readOnly: true
                    },
                    {
                        xtype: 'textfield',
                        name: 'GenerateRule',
                        fieldLabel: '生成规则',
                        width: 180,
                        labelWidth: 60,
                        margin: '0 0 0 5',
                        allowBlank: false,
                        minLength: 4,
                        maxLength: 4,
                        allowNegative: false,
                        emptyText: "输入卡片前四位",
                        blankText: "输入卡片前四位",
                        regex: /^\d*$/,
                        regexText: "只能使用数字"
                    },
                    {
                        xtype: 'combobox',
                        store: memberCardTypeStore,
                        displayField: 'Name',
                        valueField: 'ID',
                        name: 'CardTypeID',
                        fieldLabel: '卡片类型',
                        width: 170,
                        labelWidth: 60,
                        margin: '0 0 0 5',
                        editable: true,
                        allowBlank: false,
                    },
                    {
                        xtype: 'combobox',
                        store: memberGroupStore,
                        displayField: 'Name',
                        valueField: 'ID',
                        name: 'MemberGroupID',
                        fieldLabel: '会员分组',
                        width: 170,
                        labelWidth: 60,
                        margin: '0 0 0 5',
                        editable: true
                    },
                    {
                        xtype: 'combobox',
                        store: memberGradeStore,
                        displayField: 'Name',
                        valueField: 'ID',
                        name: 'MemberGradeID',
                        fieldLabel: '会员等级',
                        width: 170,
                        labelWidth: 60,
                        margin: '0 0 0 5',
                        editable: true
                    },
                    {
                        xtype: 'numberfield',
                        name: 'Count',
                        fieldLabel: '张数',
                        width: 140,
                        labelWidth: 30,
                        margin: '0 0 0 5',
                        allowBlank: false,
                        minValue: 1,
                        maxValue: 9999
                    },
                    {
                        xtype: 'numberfield',
                        name: 'CardBalance',
                        fieldLabel: '初始余额',
                        width: 195,
                        labelWidth: 60,
                        margin: '0 20 0 5',
                        allowBlank: false,
                        emptyText: "请输入初始余额",
                        blankText: "请输入初始余额",
                        minValue: 0,
                        value: 0
                    },
                    {
                        xtype: "button",
                        action: 'preGenerate',
                        text: '预生成'
                    },
                    {
                        xtype: "grid",
                        width: 1320,
                        height: 500,
                        margin: "10 0 0 0",
                        store: memberCardStore,
                        stripeRows: true,
                        loadMask: true,
                        // selModel: Ext.create('Ext.selection.CheckboxModel', { mode: "SIMPLE" }),
                        columns: [
                            { header: '卡片编号', dataIndex: 'Code', flex: 1 },
                            {
                                header: '卡片类型', dataIndex: 'CardTypeID', flex: 1,
                                renderer: function (value) {
                                    var record = memberCardTypeStore.findRecord("ID", value);
                                    if (record != null)
                                        return record.data.Name;
                                }
                            },
                            {
                                header: '会员分组', dataIndex: 'MemberGroupID', flex: 1,
                                renderer: function (value) {
                                    if (value != null && value != '') {
                                        var record = memberGroupStore.findRecord("ID", value);
                                        if (record != null) {
                                            return record.data.Name;
                                        }
                                    }
                                    return "普通会员";
                                }
                            },
                            {
                                header: '会员等级', dataIndex: 'MemberGradeID', flex: 1,
                                renderer: function (value) {
                                    if (value != null && value != '') {
                                        var record = memberGradeStore.findRecord("ID", value);
                                        if (record != null) {
                                            return record.data.Name;
                                        }
                                    }
                                }
                            },
                            { header: '验证码', dataIndex: 'VerifyCode', flex: 1 },
                            { header: '初始余额', dataIndex: 'CardBalance', flex: 1 },
                            { header: '批次代码', dataIndex: 'BatchCode', flex: 1 },
                            { text: '生成日期', dataIndex: 'CreatedDate', flex: 1, columntype: "date", format: 'Y-m-d' },
                            {
                                text: '操作功能',
                                xtype: 'actioncolumn',
                                width: 80,
                                sortable: false,
                                menuDisabled: true,
                                items: [
                                    {
                                        iconCls: 'x-fa fa-close',
                                        tooltip: '删除',
                                        scope: this,
                                        margin: '10 10 10 10',
                                        handler: function (grid, rowIndex, colIndex) {
                                            var record = grid.getStore().getAt(rowIndex);
                                            me.fireEvent('deleteMemberCardClick', grid, record);
                                        }
                                    }
                                ]
                            }
                        ],
                        dockedItems: [
                            {
                                xtype: "container",
                                layout: { type: "hbox", align: "right" },
                                items: [
                                    {
                                        align: "right",
                                        xtype: 'button',
                                        action: 'saveGenerate',
                                        text: '确定生成并导出execel'
                                    },
                                    {
                                        xtype: 'button',
                                        action: 'cancel',
                                        text: '取消'
                                    }
                                ],
                                dock: 'bottom'
                            }
                        ]
                    }
                ]
            }

        ];
        this.callParent();
    }
});
