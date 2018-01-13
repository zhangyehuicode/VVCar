Ext.define('WX.view.MemberCardActivate.MemberCardActivate', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.MemberCardActivate',
    title: '会员管理',
    stripeRows: true,
    loadMask: true,
    closable: true,
    store: Ext.create("WX.store.BaseData.MemberCardStore"),
    width: "100%",
    height: "100%",
    layout: 'vbox',
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        var memberGroupStore = Ext.create('WX.store.BaseData.MemberGroupStore');
        memberGroupStore.load();
        var cardStatus = Ext.create('WX.store.DataDict.MemberCardStatusStore');
        cardStatus.load();
        var cardStore = Ext.create('WX.store.BaseData.MemberCardStore');
        var cardStoreFilter = { CardStatus: 0, CardTypeID: '00000000-0000-0000-0000-000000000003' };
        Ext.apply(cardStore.proxy.extraParams, cardStoreFilter);
        cardStore.load();
        var memberCardTypeStore = Ext.create('WX.store.BaseData.MemberCardTypeStore');
        memberCardTypeStore.load();
        me.items = [{
            xtype: "container",
            width: '100%',
            items: [{
                xtype: 'button',
                text: '单张激活',
                action: 'signalactivate',
                cls: '',
            }, {
                xtype: 'button',
                text: '批量激活',
                action: 'batchactivate',
                permissionCode: 'Member.Member.BatchActivateCard',
            }]
        }, {
            xtype: "container",
            width: '100%',
            name: 'signalactivatecon',
            items: [{
                xtype: "container",
                margin: "15 0 10 20",
                width: "100%",
                layout: {
                    type: "vbox"
                },
                items: [{
                    id: "rmcaStepOne",
                    name: "rmcaStep",
                    xtype: "radiofield",
                    checked: true,
                    boxLabel: "步骤一：请扫描卡片上二维码或手动录入卡片编号，并填写基础信息",
                    style: {
                        color: "#FFAB42"
                    },
                    readOnly: true
                }, {
                    id: "rmcaStepTwo",
                    name: "rmcaStep",
                    xtype: "radiofield",
                    boxLabel: "步骤二：请等待客户设置密码",
                    style: {},
                    readOnly: true
                }, {
                    id: "rmcaStepThree",
                    name: "rmcaStep",
                    xtype: "radiofield",
                    boxLabel: "步骤三：恭喜您，卡片成功激活",
                    style: {},
                    readOnly: true
                }]
            }, {
                xtype: "container",
                margin: "15 0 10 20",
                width: "100%",
                defaults: {
                },
                layout: {
                    type: "vbox"
                },
                items: [{
                    xtype: "form",
                    id: "mcaFormStepOne",
                    border: false,
                    width: 550,
                    items: [{
                        xtype: "container",
                        width: "100%",
                        defaults: {
                            labelWidth: 65,
                            flex: 1
                        },
                        layout: {
                            type: "hbox"
                        },
                        items: [{
                            xtype: "textfield",
                            name: "Code",
                            fieldLabel: "会员卡号",
                            blankText: "光标定位可通过扫码读取卡号",
                            emptyText: "光标定位可通过扫码读取卡号",
                            margin: "0 20 0 0",
                            allowBlank: false
                        }, {
                            xtype: "textfield",
                            name: "VerifyCode",
                            fieldLabel: "验证码",
                            allowBlank: false
                        }]
                    }, {
                        xtype: "container",
                        width: "100%",
                        margin: "15 0 0 0",
                        defaults: {
                            labelWidth: 65,
                            flex: 1
                        },
                        layout: {
                            type: "hbox"
                        },
                        items: [{
                            xtype: "textfield",
                            name: "MemberGroupName",
                            fieldLabel: "会员分组",
                            readOnly: true,
                            margin: "0 20 0 0",
                        }, {
                            xtype: "textfield",
                            name: "MemberGroupID",
                            fieldLabel: "会员分组ID",
                            readOnly: true,
                            margin: "0 20 0 0",
                            hidden: true
                            //}, {
                            //    xtype: "datefield",
                            //    name: "ExpiredDate",
                            //    fieldLabel: "有效期至",
                            //    format: "Y-m-d",
                        }, {
                            xtype: "textfield",
                            name: "MobilePhoneNo",
                            fieldLabel: "手机号码",
                            vtype: 'mobilephone',
                        }]
                    }, {
                        xtype: "container",
                        width: "100%",
                        margin: "15 0 0 0",
                        defaults: {
                            labelWidth: 65,
                        },
                        layout: {
                            type: "hbox"
                        },
                        items: [{
                            xtype: "textfield",
                            name: "CardType",
                            fieldLabel: "卡片类型",
                            readOnly: true,
                            width: 265,
                        }]
                    }, {
                        xtype: "container",
                        width: "100%",
                        margin: "15 0 0 0",
                        defaults: {
                            labelWidth: 65,
                            flex: 1
                        },
                        layout: {
                            type: "hbox"
                        },
                        items: [{
                            xtype: "textfield",
                            name: "Remark",
                            fieldLabel: "备注",
                            maxLength: 30,
                            flex: 1
                        }]
                    }, {
                        action: "toStepTwo",
                        xtype: "button",
                        text: "下一步",
                        disabled: true,
                    }]
                }, {
                    xtype: "form",
                    id: "mcaFormStepTwo",
                    hidden: true,
                    border: false,
                    width: 300,
                    items: [{
                        xtype: "textfield",
                        fieldLabel: "设定密码",
                        name: "Password",
                        inputType: 'password',
                        minLength: 6,
                        maxLength: 6
                    }, {
                        xtype: "textfield",
                        fieldLabel: "确认密码",
                        name: "RepeatPassword",
                        inputType: 'password',
                        minLength: 6,
                        maxLength: 6
                    }, {
                        action: "toStepThree",
                        xtype: "button",
                        text: "确定"
                    }]
                }, {
                    xtype: "form",
                    id: "mcaFormStepThree",
                    hidden: true,
                    border: false,
                    width: "100%",
                    items: [{
                        xtype: "label",
                        text: "恭喜您，成功激活卡片！",
                        style: {
                            display: "block",
                            color: "#FFAB42"
                        }
                    }, {
                        action: "toStepOne",
                        xtype: "button",
                        text: "返回"
                    }]
                }]
            }]
        }, {
            xtype: "container",
            width: '100%',
            name: 'batchactivatecon',
            hidden: true,
            layout: {
                type: "vbox"
            },
            items: [{
                xtype: 'form',
                name: 'batchactivateform',
                items: [{
                    xtype: 'fieldcontainer',
                    layout: 'vbox',
                    margin: '20 0 0 20',
                    items: [{
                        html: '批量激活仅适用于礼品卡',
                        bodyPadding: '0 0 10 0',
                    }, {
                        xtype: 'fieldcontainer',
                        layout: 'hbox',
                        items: [{
                            xtype: 'radiofield',
                            boxLabel: '批次代码：',
                            name: 'batchactivateradio',
                            id: 'batchactivateradio',
                            inputValue: true,
                        }, {
                            xtype: 'textfield',
                            //fieldLabel: '批次代码',
                            name: 'BatchCode',
                            labelWidth: 60,
                            width: 140,
                            margin: '0 0 0 5',
                        }]
                    }, {
                        xtype: 'fieldcontainer',
                        layout: 'hbox',
                        items: [{
                            xtype: 'radiofield',
                            boxLabel: '起始卡号：',
                            name: 'batchactivateradio',
                            id: 'coderangeradio',
                            inputValue: true,
                        }, {
                            xtype: 'textfield',
                            //fieldLabel: '起始卡号',
                            name: 'StartCode',
                            labelWidth: 60,
                            width: 140,
                            margin: '0 0 0 5',
                        }, {
                            xtype: 'textfield',
                            fieldLabel: '终止卡号',
                            name: 'EndCode',
                            labelWidth: 60,
                            width: 200,
                            margin: '0 0 0 5',
                        }, {
                            xtype: 'button',
                            text: '搜索',
                            action: 'batchactivatesearch',
                            margin: '0 0 0 5',
                            //}, {
                            //    xtype: 'button',
                            //    text: '激活',
                            //    action: 'batchactivateaction',
                            //    margin: '0 0 0 5',
                        }]
                    }, {
                        xtype: 'form',
                        name: 'batchactivatevalueform',
                        layout: 'hbox',
                        items: [{
                            xtype: 'datefield',
                            name: 'ExpiredDate',
                            fieldLabel: '设置截止日期',
                            minValue: new Date(),
                            labelWidth: 87,
                            width: 231,
                            margin: '0 0 0 0',
                            format: 'Y-m-d',
                            editable: false,
                            allowBlank: false,
                        }, {
                            xtype: 'numberfield',
                            name: 'CardBalance',
                            fieldLabel: '卡片面值',
                            minValue: 1,
                            allowBlank: false,
                            labelWidth: 60,
                            width: 200,
                            margin: '0 0 0 5',
                        }, {
                            xtype: 'textfield',
                            name: 'BatchRemark',
                            fieldLabel: '备注',
                            allowBlank: true,
                            labelWidth: 35,
                            width: 200,
                            margin: '0 0 0 5',
                        }, {
                            xtype: 'button',
                            text: '激活',
                            action: 'batchactivateaction',
                            margin: '0 0 0 5',
                        }]
                    }, {
                        xtype: 'grid',
                        store: cardStore,
                        selType: 'checkboxmodel',
                        width: 1100,
                        height: 600,
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
                                header: '卡片状态', dataIndex: 'Status', flex: 1,
                                renderer: function (value) {
                                    var index = cardStatus.find("DictValue", value);
                                    if (index === -1)
                                        return "";
                                    return cardStatus.getAt(index).data.DictName;
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
                                        return "普通会员";
                                    }
                                    return "普通会员";
                                }
                            },
                            { header: '批次代码', dataIndex: 'BatchCode', flex: 1 },
                            { header: '生成日期', dataIndex: 'CreatedDate', flex: 1 },
                            { header: '备注', dataIndex: 'Remark', flex: 1 },
                        ],
                        dockedItems: [{
                            xtype: 'pagingtoolbar',
                            store: cardStore,
                            dock: 'bottom',
                            displayInfo: true
                        }],
                    }]
                }]
            }]
        }];
        me.callParent();
    }
});