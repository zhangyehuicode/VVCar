Ext.define('WX.view.MemberRecharge.MemberRecharge', {
    extend: 'Ext.container.Container',
    alias: 'widget.MemberRecharge',
    title: '会员储值',
    layout: 'anchor',
    loadMask: true,
    closable: true,
    padding: 15,
    initComponent: function () {
        var me = this;
        me.form = Ext.create('Ext.form.Panel', {
            name: 'formMemberRecharge',
            border: false,
            trackResetOnLoad: true,
            width: 630,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 80,
                anchor: '100%',
                readOnly: true,
                margin: '0 0 8 0',
                tabIndex: 0,
                fieldStyle: 'font-size: 16px; line-height: normal',
            },
            items: [{
                xtype: 'displayfield',
                value: '会员储值',
                fieldStyle: 'font-weight: bold; font-size: 22px',
                margin: '0 0 20 0',
            }, {
                xtype: 'textfield',
                name: 'CardNumber',
                fieldLabel: '会员卡号',
                emptyText: '输入会员卡号或手机号码',
                allowBlank: false,
                readOnly: false,
                tabIndex: 1,
                labelStyle: 'font-weight: bold font-size: 22px;',
                fieldStyle: 'font-weight: bold; font-size: 22px;line-height: normal',
                height: 30,
                margin: '0 0 16 0',
            }, {
                xtype: 'textfield',
                name: 'CardID',
                fieldLabel: '会员卡ID',
                hidden: true,
            }, {
                xtype: 'textfield',
                name: 'CardTypeID',
                fieldLabel: '卡片类型ID',
                hidden: true,
            }, {
                xtype: 'textfield',
                name: 'MemberID',
                fieldLabel: '会员ID',
                hidden: true,
            }, {
                xtype: 'fieldcontainer',
                layout: 'hbox',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '姓名',
                    name: "MemberName",
                    flex: 1,
                }, {
                    xtype: 'textfield',
                    fieldLabel: '手机号码',
                    name: "MobilePhoneNo",
                    margin: '0 0 0 10',
                    flex: 1,
                }]
            }, {
                xtype: 'fieldcontainer',
                layout: 'hbox',
                anchor: '100%',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '出生年月',
                    name: "Birthday",
                    flex: 1,
                }, {
                    xtype: 'textfield',
                    fieldLabel: '状态',
                    name: "CardStatus",
                    margin: '0 0 0 10',
                    flex: 1,
                }, {
                    xtype: 'textfield',
                    fieldLabel: '卡片类型',
                    name: "CardType",
                    margin: '0 0 0 10',
                    flex: 1,
                    hidden: true,
                }]
            }, {
                xtype: 'fieldcontainer',
                layout: 'hbox',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '出生年月',
                    name: "Birthday",
                    flex: 1,
                    hidden: true,
                }, {
                    xtype: 'textfield',
                    fieldLabel: '注册门店',
                    name: "CreatedDepartment",
                    margin: '0 0 0 10',
                    flex: 1,
                    hidden: true,
                }]
            }, {
                xtype: 'fieldcontainer',
                layout: 'hbox',
                hidden: true,
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '生效日期',
                    name: "EffectiveDate",
                    flex: 1,
                }, {
                    xtype: 'datefield',
                    fieldLabel: '截止日期',
                    name: "ExpiredDate",
                    format: "Y-m-d",
                    margin: '0 0 0 10',
                    flex: 1,
                }]
            }, {
                xtype: 'fieldcontainer',
                layout: 'hbox',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '余额',
                    name: "CardBalance",
                    flex: 1,
                    labelStyle: 'font-weight: bold',
                    fieldStyle: 'font-weight: bold',
                }, {
                    xtype: 'combobox',
                    fieldLabel: '支付方式',
                    name: 'PaymentType',
                    flex: 1,
                    queryMode: 'local',
                    store: Ext.getStore('DataDict.PaymentTypeStore'),
                    displayField: 'DictName',
                    valueField: 'DictValue',
                    editable: false,
                    forceSelection: true,
                    allowBlank: false,
                    readOnly: false,
                    tabIndex: 3,
                    labelStyle: 'font-weight: bold',
                    fieldStyle: 'font-weight: bold',
                    //margin: '0 5 8 0',
                    margin: '0 0 0 10',
                }, {
                    xtype: 'combobox',
                    fieldLabel: '储值方案',
                    name: 'RechargePlanID',
                    flex: 1,
                    margin: '0 0 0 10',
                    queryMode: 'local',
                    //store: Ext.create('WX.store.BaseData.RechargePlanStore'),
                    displayField: 'Name',
                    valueField: 'ID',
                    editable: false,
                    forceSelection: true,
                    //allowBlank: false,
                    readOnly: false,
                    tabIndex: 2,
                    labelStyle: 'font-weight: bold',
                    fieldStyle: 'font-weight: bold',
                    hidden: true,
                }]
            }, {
                xtype: 'fieldcontainer',
                layout: 'hbox',
                anchor: '50%',
                items: [
                    //{
                    //    xtype: 'combobox',
                    //    fieldLabel: '支付方式',
                    //    name: 'PaymentType',
                    //    flex: 1,
                    //    queryMode: 'local',
                    //    store: Ext.getStore('DataDict.PaymentTypeStore'),
                    //    displayField: 'DictName',
                    //    valueField: 'DictValue',
                    //    editable: false,
                    //    forceSelection: true,
                    //    allowBlank: false,
                    //    readOnly: false,
                    //    tabIndex: 3,
                    //    labelStyle: 'font-weight: bold',
                    //    fieldStyle: 'font-weight: bold',
                    //    margin: '0 5 8 0',
                    //}
                ]
            }, {
                xtype: 'fieldcontainer',
                layout: 'hbox',
                items: [{
                    xtype: 'numberfield',
                    fieldLabel: '本次储值金额',
                    name: "RechargeAmount",
                    flex: 1,
                    labelWidth: 85,
                    allowBlank: false,
                    readOnly: false,
                    //tabIndex: 3,
                    labelStyle: 'font-weight: bold',
                    fieldStyle: 'font-weight: bold; font-size: 30px; height: 37px',
                    minValue: 0,
                }, {
                    xtype: 'numberfield',
                    fieldLabel: '赠送金额',
                    name: "GiveAmount",
                    margin: '0 0 0 10',
                    flex: 1,
                    labelWidth: 80,
                    allowBlank: false,
                    readOnly: false,
                    //tabIndex: 4,
                    labelStyle: 'font-weight: bold',
                    fieldStyle: 'font-weight: bold; font-size: 30px; height: 37px',
                    minValue: 0,
                    value: 0,
                }]
            }],
            buttons: [{
                text: '确认储值',
                cls: 'submitBtn',
                action: 'confirm',
                scale: 'medium',
                tabIndex: 5,
            }, {
                text: '历史数据',
                action: 'history',
                scale: 'medium',
                tabIndex: 6,
            }]
        });

        me.gridHistory = Ext.create('Ext.grid.Panel', {
            name: 'gridHistory',
            title: '储值历史纪录',
            store: Ext.create('WX.store.BaseData.RechargeHistoryStore'),
            margin: '20 0 0 0',
            width: 630,
            hidden: true,
            columns: [
                { header: '储值时间', dataIndex: 'CreatedDate', width: 150, },
                { header: '储值门店', dataIndex: 'TradeDepartment', flex: 1, },
                { header: '业务员', dataIndex: 'CreatedUser', width: 100, },
                { header: '储值金额', dataIndex: 'TradeAmount', width: 80, },
                { header: '赠送金额', dataIndex: 'GiveAmount', width: 80, },
            ]
        });

        me.items = [me.form, me.gridHistory];
        me.callParent(arguments);
    }
});