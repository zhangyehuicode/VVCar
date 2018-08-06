Ext.define("WX.view.Member.EditMember", {
    extend: "Ext.window.Window",
    alias: "widget.EditMember",
    title: "新增会员",
    layout: {
        type: 'vbox',
        align: 'stretch',
        pack: 'start',
    },
    width: 600,
    bodyPadding: 0,
    modal: true,
    buttonAlign: 'center',
    initComponent: function () {
        var me = this;
        var statusStore = Ext.create("WX.store.DataDict.MemberCardStatusStore");
        var membergroupStore = Ext.create('WX.store.BaseData.MemberGroupStore');
        membergroupStore.proxy.extraParams = { All: true };
        membergroupStore.load();
        me.items = [{
            xtype: "form",
            name: "MemberInfo",
            trackResetOnLoad: true,
            //layout: {
            //    type: 'fit',
            //    //align: 'stretch',
            //    //pack: 'start',
            //},
            border: false,
            frame: false,
            labelAlign: "left",
            buttonAlign: "right",
            //labelWidth: 60,
            padding: 0,
            //autoWidth: true,
            autoScroll: true,
            //columnWidth: 1,
            defaults: {
                bodyBorder: false
            },
            items: [{
                xtype: "panel",
                name: 'membercardinfo',
                margin: "0 0 10 0",
                header: {
                    cls: 'panel-header-customize',
                    html: "<span class:'panel-title-customize'>基础信息（非编辑）</span>",
                },
                border: false,
                hidden: true,
                defaults: {
                    labelWidth: 70,
                    margin: "5 20 0 15",
                    fieldStyle: "color:gray;font-size:14px;",
                    labelStyle: "width:60px;font-size:14px;",
                },
                items: [
                    {
                        xtype: "displayfield",
                        name: "CardNumber",
                        fieldLabel: "会员卡号",
                        allowBlank: false
                    },
                    {
                        xtype: "displayfield",
                        name: "CardTypeDesc",
                        fieldlabel: "卡片类型",
                    },
                    {
                        xtype: "displayfield",
                        name: "Status",
                        store: statusStore,
                        fieldLabel: "卡片状态",
                        displayField: "DictName",
                        valueField: "DictValue",
                        readOnly: true
                    },
                    {
                        xtype: "container",
                        layout: "hbox",
                        defaults: {
                            labelWidth: 70,
                            fieldStyle: "color:gray;font-size:14px;",
                            labelStyle: "width:60px;font-size:14px;",
                            width: 170
                        },
                        items: [{
                            xtype: "displayfield",
                            name: "EffectiveDate",
                            format: "Y-m-d",
                            fieldLabel: "有效期",
                            readOnly: true
                        }, {
                            xtype: "displayfield",
                            format: "Y-m-d",
                            name: "ExpiredDate",
                            fieldLabel: "至",
                            readOnly: true
                        }]
                    }]
            }, {
                xtype: "panel",
                margin: "0 0 15 0",
                header: {
                    cls: 'panel-header-customize',
                    html: "<span class:'panel-title-customize'>会员信息</span>"
                },
                border: false,
                defaults: {
                    margin: "5 0 0 0",
                    //labelWidth: 90,
                    layout: "hbox",
                },
                items: [{
                    xtype: "container",
                    defaults: {
                        margin: "5 20 0 15",
                        labelWidth: 90,
                        fieldStyle: "font-size:14px;",
                        labelStyle: "width:70px;font-size:14px;",
                        //width: 200
                    },
                    //layout: "vbox",
                    items: [{
                        xtype: "textfield",
                        name: "Name",
                        fieldLabel: "姓名",
                        allowBlank: false
                    }, {
                        xtype: "combobox",
                        name: "Sex",
                        displayField: "DictName",
                        valueField: "DictValue",
                        store: Ext.create("WX.store.DataDict.SexStore"),
                        fieldLabel: "性别",
                        allowBlank: false,
                        editable: false,
                    },]
                }, {
                    xtype: "container",
                    //layout: "vbox",
                    defaults: {
                        margin: "5 20 0 15",
                        labelWidth: 90,
                        fieldStyle: "font-size:14px;",
                        labelStyle: "width:70px;font-size:14px;",
                        //width: 200
                    },
                    items: [{
                        xtype: "datefield",
                        name: "Birthday",
                        fieldLabel: "出生年月",
                        format: "Y-m-d",
                        allowBlank: true,
                        maxValue: new Date(),
                    }, {
                        xtype: 'combobox',
                        store: membergroupStore,
                        fieldLabel: '会员分组',
                        displayField: 'Name',
                        valueField: 'ID',
                        editable: true,
                        name: 'MemberGroupID'
                    }]
                }, {
                    xtype: "container",
                    //layout: "vbox",
                    defaults: {
                        margin: "5 20 0 15",
                        labelWidth: 90,
                        fieldStyle: "font-size:14px;",
                        labelStyle: "width:70px;font-size:14px;",
                        //width: 200
                    },
                    items: [
                        //        {
                        //    xtype: "textfield",
                        //    name: "OwnerDepartment",
                        //    fieldLabel: "所属门店",
                        //    readOnly: true,
                        //},
                        {
                            xtype: "textfield",
                            name: "MobilePhoneNo",
                            fieldLabel: "手机号码",
                            allowBlank: false,
                            vtype: 'mobilephone'
                        }, {
                            xtype: "textfield",
                            name: "PhoneLocation",
                            fieldLabel: "归属地",
                            readOnly: true,
                        }]
                }, {
                    xtype: "container",
                    defaults: {
                        margin: "5 20 0 15",
                        labelWidth: 90,
                        fieldStyle: "font-size:14px;",
                        labelStyle: "width:70px;font-size:14px;",
                        //width: 200
                    },
                    //layout: "vbox",
                    items: [{
                        xtype: 'textfield',
                        name: 'PlateNumber',
                        fieldLabel: '车牌号',
                        allowBlank: true,
                        minLength: 7,
                        maxLength: 8,
                    }]
                }, {
                    xtype: "container",
                    defaults: {
                        margin: "5 20 0 15",
                        labelWidth: 90,
                        fieldStyle: "font-size:14px;",
                        labelStyle: "width:70px;font-size:14px;",
                        //width: 200
                    },
                    //layout: "vbox",
                    items: [{
                        xtype: 'datefield',
                        name: 'InsuranceExpirationDate',
                        fieldLabel: '保险到期',
                        format: "Y-m-d 00:00:00",
                        allowBlank: true,
                    }, {
                        xtype: 'textfield',
                        name: 'Password',
                        fieldLabel: '核销密码',
                        inputType: 'password',
                        //width: 200,
                        allowBlank: true,
                    }]
                }, {
                    //xtype: "container",
                    ////layout: "vbox",
                    //defaults: {
                    //    margin: "5 20 0 15",
                    //    //labelWidth: 110,
                    //    fieldStyle: "font-size:14px;",
                    //    labelStyle: "width:70px;font-size:14px;",
                    //    //width: 200
                    //},
                    //items: [{
                    //    xtype: 'textfield',
                    //    name: 'Password',
                    //    fieldLabel: '核销密码',
                    //    inputType: 'password',
                    //    //width: 200,
                    //    allowBlank: true,
                    //}]
                    //}, {
                    //    xtype: "container",
                    //    layout: "hbox",
                    //    defaults: {
                    //        margin: "5 20 0 15",
                    //        labelWidth: 70,
                    //        fieldStyle: "font-size:14px;",
                    //        labelStyle: "width:60px;font-size:14px;",
                    //        width: 190
                    //    },
                    //    items: [{
                    //        xtype: 'combobox',
                    //        store: membergroupStore,
                    //        fieldLabel: '会员分组',
                    //        displayField: 'Name',
                    //        valueField: 'ID',
                    //        editable: false,
                    //        name: 'MemberGroupID'
                    //    }, {
                    //        xtype: "textfield",
                    //        name: "MemberGradeName",
                    //        fieldLabel: "会员等级",
                    //        readOnly: true,
                    //    }]
                    //}, {
                    //    xtype: "container",
                    //    layout: "hbox",
                    //    defaults: {
                    //        margin: "5 20 0 15",
                    //        labelWidth: 70,
                    //        fieldStyle: "font-size:14px;",
                    //        labelStyle: "width:60px;font-size:14px;",
                    //        width: 170
                    //    },
                    //    items: [{
                    //        xtype: 'textfield',
                    //        name: 'WeChatOpenID',
                    //        fieldLabel: 'OpenId',
                    //        width: 315,
                    //        readOnly: true,
                    //    }]
                }]
            }]
        }, {
            xtype: "form",
            name: "formExtraInfo",
            layout: {
                type: 'vbox',
                align: 'stretch',
                pack: 'start',
            },
            border: false,
            frame: false,
            labelAlign: "left",
            buttonAlign: "right",
            labelWidth: 60,
            padding: 0,
            autoWidth: true,
            autoScroll: true,
            columnWidth: 1,
            hidden: true,
            items: [{
                xtype: "panel",
                margin: "0 0 0 0",
                bodyBorder: false,
                header: {
                    cls: 'panel-header-customize',
                    html: "<span class:'panel-title-customize'>账目信息（非编辑）</span>"
                },
                border: false,
                style: {
                    borderStyle: 'none'
                }, items: [{
                    xtype: "container",
                    margin: "10 0 0 0",
                    layout: "hbox",
                    defaults: {
                        margin: "0 20 0 15",
                        fieldStyle: "color:gray;font-size:14px;",
                        labelStyle: "width:100px;font-size:14px;",
                        width: 170
                    },
                    items: [{
                        xtype: "displayfield",
                        name: "TotalRecharge",
                        fieldLabel: "累计储值金额",
                        readOnly: true
                    }, {
                        xtype: "displayfield",
                        name: "LastRechargeMoney",
                        fieldLabel: "末次储值金额",
                        readOnly: true
                    }]
                }, {
                    xtype: "container",
                    margin: "0 0 0 0",
                    layout: "hbox",
                    defaults: {
                        margin: "5 20 0 15",
                        fieldStyle: "color:gray;font-size:14px;",
                        labelStyle: "100px;font-size:14px;",
                        width: 170
                    },
                    items: [{
                        xtype: "displayfield",
                        name: "TotalConsume",
                        fieldLabel: "累计消费金额",
                        readOnly: true
                    }, {
                        xtype: "displayfield",
                        name: "CardBalance",
                        fieldStyle: "color:red;",
                        fieldLabel: "会员卡余额",
                        readOnly: true
                    }]
                }, {
                    xtype: "container",
                    margin: "0 0 10 0",
                    layout: "hbox",
                    defaults: {
                        margin: "5 20 0 15",
                        fieldStyle: "color:gray;font-size:14px;",
                        labelStyle: "100px;font-size:14px;",
                        width: 170
                    },
                    items: [{
                        xtype: "displayfield",
                        name: "Point",
                        fieldLabel: "剩余积分",
                        readOnly: true
                    }]
                }]
            }]
        }, {
            xtype: "tabpanel",
            bodyBorder: false,
            border: false,
            name: "tabpanelExtraInfo",
            activeTab: 0,
            height: 200,
            hidden: true,
            items: [{
                title: "会员储值记录",
                name: "gridRecharge",
                store: Ext.create("WX.store.BaseData.RechargeHistoryStore"),
                xtype: "grid",
                columns: [
                    { header: "储值时间", dataIndex: "CreatedDate", flex: 1 },
                    { header: "储值门店", dataIndex: "TradeDepartment", flex: 1 },
                    { header: "业务员", dataIndex: "CreatedUser", flex: 1 },
                    { header: "储值金额", dataIndex: "TradeAmount", flex: 1 },
                    { header: "赠送金额", dataIndex: "GiveAmount", flex: 1 }
                ]
            }, {
                title: "会员（储值余额）消费记录",
                name: "gridConsume",
                xtype: "grid",
                store: Ext.create("WX.store.BaseData.TradeHistoryStore"),
                columns: [
                    { header: "消费时间", dataIndex: "CreatedDate", flex: 1 },
                    { header: "消费门店", dataIndex: "TradeDepartment", flex: 1 },
                    { header: "业务员", dataIndex: "CreatedUser", flex: 1 },
                    { header: "消费金额", dataIndex: "TradeAmount", flex: 1 }
                ]
            }]
        }, {
            //xtype: "panel",
            //border: false,
            //margin: '0 0 0 90',
            //items: [{
            //    action: "save",
            //    margin: "5 5 5 10",
            //    xtype: "button",
            //    text: "保 存"
            //}, {
            //    margin: "5 0 5 5",
            //    xtype: "button",
            //    text: "取 消",
            //    scope: me,
            //    handler: me.close
            //}]
        }];

        me.buttons = [{
            action: "save",
            margin: "5 5 5 10",
            xtype: "button",
            text: "保 存"
        }, {
            margin: "5 0 5 5",
            xtype: "button",
            text: "取 消",
            scope: me,
            handler: me.close
        }];

        me.callParent();
    }
});