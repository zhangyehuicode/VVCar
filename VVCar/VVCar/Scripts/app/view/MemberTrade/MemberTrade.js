Ext.define('WX.view.MemberTrade.MemberTrade', {
    extend: 'Ext.container.Container',
    alias: 'widget.MemberTrade',
    title: '会员消费',
    layout: 'anchor',
    loadMask: true,
    closable: true,
    padding: 15,
    initComponent: function () {
        var me = this;
        me.form = Ext.create('Ext.form.Panel', {
            name: 'formMemberTrade',
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
                value: '新增会员消费',
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
                    fieldLabel: '卡片状态',
                    name: "CardStatus",
                    flex: 1,
                }, {
                    xtype: 'textfield',
                    fieldLabel: '卡片类型',
                    name: "CardType",
                    margin: '0 0 0 10',
                    flex: 1,
                }]
            }, {
                xtype: 'fieldcontainer',
                layout: 'hbox',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '出生年月',
                    name: "Birthday",
                    flex: 1,
                }, {
                    xtype: 'textfield',
                    fieldLabel: '注册门店',
                    name: "CreatedDepartment",
                    margin: '0 0 0 10',
                    flex: 1,
                }]
            }, {
                xtype: 'fieldcontainer',
                layout: 'hbox',
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
                    fieldLabel: '卡片余额',
                    name: "CardBalance",
                    flex: 1,
                    labelStyle: 'font-weight: bold',
                    fieldStyle: 'font-weight: bold',
                }, {
                    xtype: 'numberfield',
                    fieldLabel: '本次消费总金额',
                    name: "TradeAmount",
                    margin: '0 0 0 10',
                    flex: 1,
                    labelWidth: 110,
                    allowBlank: false,
                    readOnly: false,
                    tabIndex: 2,
                    labelStyle: 'font-weight: bold',
                    fieldStyle: 'font-weight: bold; font-size: 30px; height: 37px',
                }]
            }],
            buttons: [{
                text: '确认消费',
                cls: 'submitBtn',
                action: 'confirm',
                scale: 'medium',
                tabIndex: 3,
            }, {
                text: '历史数据',
                action: 'history',
                scale: 'medium',
                tabIndex: 4,
            }]
        });

        me.gridHistory = Ext.create('Ext.grid.Panel', {
            name: 'gridHistory',
            title: '消费历史纪录',
            store: Ext.create('WX.store.BaseData.TradeHistoryStore'),
            margin: '20 0 0 0',
            width: 550,
            hidden: true,
            columns: [
                { header: '消费时间', dataIndex: 'CreatedDate', flex: 1, },
                { header: '消费门店', dataIndex: 'TradeDepartment', flex: 1, },
                { header: '业务员', dataIndex: 'CreatedUser', width: 80, },
                { header: '消费金额', dataIndex: 'TradeAmount', width: 80, },
            ]
        });

        me.items = [me.form, me.gridHistory];
        me.callParent(arguments);
    }
});