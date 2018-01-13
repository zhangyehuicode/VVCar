Ext.define('WX.view.MemberGrade.MemberGradeEdit', {
    extend: 'Ext.container.Container',
    alias: 'widget.MemberGradeEdit',
    title: '编辑会员等级',
    layout: 'anchor',
    anchor: '100%',
    loadMask: true,
    closable: true,
    padding: '20 30 20 30',
    initComponent: function () {
        var me = this;
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 120,
            },
            items: [{
                xtype: 'displayfield',
                value: '基本信息',
                border: '0 0 1 0',
                padding: '0 0 5 0',
                height: 30,
                anchor: '100%',
                style: {
                    borderColor: 'gray',
                    borderStyle: 'solid',
                },
                renderer: function (rawValue) {
                    return '<div style="font-size: 26px;">' + rawValue + '</div>';
                }
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '等级名称',
                layout: {
                    type: 'hbox',
                    pack: 'start',
                    align: 'bottom'
                },
                items: [{
                    xtype: 'textfield',
                    name: 'Name',
                    width: 150,
                    maxLength: 10,
                    allowBlank: false,
                }, {
                    xtype: 'checkboxfield',
                    name: 'IsDefault',
                    fieldLabel: '是否设置默认等级',
                    labelWidth: 120,
                    margin: '0 0 0 10',
                    inputValue: true,
                    checked: false,
                    handler: function (checkbox, checked) {
                        if (checked) {
                            me.form.down('numberfield[name=Level]').setValue(1).setReadOnly(true);
                            me.form.down('radiofield[action=radioNeverExpires]').setValue('true');
                            me.form.down('radiofield[action=radioHasExpiresDay]').setReadOnly(true);
                        } else {
                            me.form.down('numberfield[name=Level]').setReadOnly(false);
                            me.form.down('radiofield[action=radioHasExpiresDay]').setReadOnly(false);
                        }
                    }
                }, {
                    xtype: 'checkboxfield',
                    name: 'IsNotOpen',
                    fieldLabel: '不对外开放',
                    labelWidth: 80,
                    margin: '0 0 0 20',
                    inputValue: true,
                    checked: false,
                    handler: function (checkbox, checked) {
                    }
                }]
            }, {
                xtype: 'numberfield',
                name: 'Level',
                fieldLabel: '等级排序',
                minValue: 1,
                allowDecimals: false,
                allowBlank: false,
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '有效期',
                layout: {
                    type: 'hbox',
                    pack: 'start',
                    align: 'bottom'
                },
                items: [{
                    xtype: 'fieldcontainer',
                    defaultType: 'radiofield',
                    layout: 'vbox',
                    width: 100,
                    items: [{
                        xtype: 'hiddenfield',
                        name: 'IsNeverExpires',
                        value: true,
                    }, {
                        boxLabel: '永久有效',
                        name: 'IsNeverExpiresRadioValue',
                        action: 'radioNeverExpires',
                        inputValue: 'true',
                    }, {
                        boxLabel: '发卡/升级后',
                        name: 'IsNeverExpiresRadioValue',
                        action: 'radioHasExpiresDay',
                        inputValue: 'false',
                        handler: function (checkbox, checked) {
                            me.form.down('hiddenfield[name=IsNeverExpires]').setValue(!checked);
                            me.setFieldReadOnly(me.form.down('numberfield[name=ExpireAfterJoinDays]'), !checked);
                        }
                    }]
                }, {
                    xtype: 'numberfield',
                    name: 'ExpireAfterJoinDays',
                    minValue: 1,
                    allowDecimals: false,
                    width: 80,
                    hideLabel: true,
                    readOnly: true,
                    margin: '5 0 0 0',
                }, {
                    xtype: 'displayfield',
                    value: '天',
                    margin: '5 0 0 5',
                }]
            }, {
                xtype: 'displayfield',
                value: '获取资格',
                border: '0 0 1 0',
                padding: '0 0 5 0',
                height: 30,
                anchor: '100%',
                style: {
                    borderColor: 'gray',
                    borderStyle: 'solid',
                },
                renderer: function (rawValue) {
                    return '<div style="font-size: 26px;">' + rawValue + '</div>';
                }
            }, {
                xtype: 'panel',
                title: '消费',
                layout: 'anchor',
                bodyPadding: 10,
                margin: '0 0 10 0',
                items: [{
                    xtype: 'checkboxfield',
                    name: 'IsQualifyByConsume',
                    boxLabel: '通过消费获得资格',
                    inputValue: true,
                    handler: function (checkbox, checked) {
                        if (checked) {
                            me.form.down('checkboxfield[name=IsQualifyByConsumeTotalAmount]').setReadOnly(false);
                            me.form.down('checkboxfield[name=IsQualifyByConsumeOneOffAmount]').setReadOnly(false);
                            me.form.down('checkboxfield[name=IsQualifyByConsumeLimitedMonths]').setReadOnly(false);
                        } else {
                            me.form.down('checkboxfield[name=IsQualifyByConsumeTotalAmount]').setValue(false).setReadOnly(true);
                            me.form.down('checkboxfield[name=IsQualifyByConsumeOneOffAmount]').setValue(false).setReadOnly(true);
                            me.form.down('checkboxfield[name=IsQualifyByConsumeLimitedMonths]').setValue(false).setReadOnly(true);
                        }
                    }
                }, {
                    xtype: 'fieldcontainer',
                    fieldLabel: '按消费金额',
                    layout: 'vbox',
                    items: [{
                        xtype: 'fieldcontainer',
                        layout: {
                            type: 'hbox',
                            pack: 'start',
                            align: 'bottom'
                        },
                        items: [{
                            xtype: 'checkboxfield',
                            name: 'IsQualifyByConsumeTotalAmount',
                            width: 90,
                            boxLabel: '累计消费达',
                            inputValue: true,
                            readOnly: true,
                            handler: function (checkbox, checked) {
                                me.setFieldReadOnly(me.form.down('numberfield[name=QualifyByConsumeTotalAmount]'), !checked);
                            }
                        }, {
                            xtype: 'numberfield',
                            name: 'QualifyByConsumeTotalAmount',
                            minValue: 1,
                            width: 80,
                            hideLabel: true,
                            readOnly: true,
                        }, {
                            xtype: 'displayfield',
                            value: '元，成为该等级会员',
                            margin: '0 0 0 5',
                        }]
                    }, {
                        xtype: 'fieldcontainer',
                        layout: {
                            type: 'hbox',
                            pack: 'start',
                            align: 'bottom'
                        },
                        items: [{
                            xtype: 'checkboxfield',
                            name: 'IsQualifyByConsumeOneOffAmount',
                            width: 100,
                            boxLabel: '一次性消费达',
                            inputValue: true,
                            readOnly: true,
                            handler: function (checkbox, checked) {
                                me.setFieldReadOnly(me.form.down('numberfield[name=QualifyByConsumeOneOffAmount]'), !checked);
                            }
                        }, {
                            xtype: 'numberfield',
                            name: 'QualifyByConsumeOneOffAmount',
                            minValue: 1,
                            width: 80,
                            hideLabel: true,
                            readOnly: true,
                        }, {
                            xtype: 'displayfield',
                            value: '元，成为该等级会员',
                            margin: '0 0 0 5',
                        }]
                    }]
                }, {
                    xtype: 'fieldcontainer',
                    fieldLabel: '按消费次数',
                    layout: {
                        type: 'hbox',
                        pack: 'start',
                        align: 'bottom'
                    },
                    items: [{
                        xtype: 'checkboxfield',
                        name: 'IsQualifyByConsumeLimitedMonths',
                        width: 20,
                        inputValue: true,
                        readOnly: true,
                        handler: function (checkbox, checked) {
                            me.setFieldReadOnly(me.form.down('numberfield[name=QualifyByConsumeLimitedMonths]'), !checked);
                            me.setFieldReadOnly(me.form.down('numberfield[name=QualifyByConsumeTotalCount]'), !checked);
                        }
                    }, {
                        xtype: 'numberfield',
                        name: 'QualifyByConsumeLimitedMonths',
                        minValue: 1,
                        allowDecimals: false,
                        width: 80,
                        hideLabel: true,
                        readOnly: true,
                    }, {
                        xtype: 'displayfield',
                        value: '个月内，累计消费达',
                        width: 120,
                        margin: '0 0 0 5',
                    }, {
                        xtype: 'numberfield',
                        name: 'QualifyByConsumeTotalCount',
                        minValue: 1,
                        allowDecimals: false,
                        width: 80,
                        hideLabel: true,
                        readOnly: true,
                    }, {
                        xtype: 'displayfield',
                        value: '次，成为该等级会员',
                        margin: '0 0 0 5',
                    }]
                }]
            }, {
                xtype: 'panel',
                title: '储值',
                layout: 'anchor',
                bodyPadding: 10,
                margin: '0 0 10 0',
                items: [{
                    xtype: 'checkboxfield',
                    name: 'IsQualifyByRecharge',
                    boxLabel: '通过储值获得资格',
                    inputValue: true,
                    handler: function (checkbox, checked) {
                        if (checked) {
                            me.form.down('checkboxfield[name=IsQualifyByRechargeTotalAmount]').setReadOnly(false);
                            me.form.down('checkboxfield[name=IsQualifyByRechargeOneOffAmount]').setReadOnly(false);
                        } else {
                            me.form.down('checkboxfield[name=IsQualifyByRechargeTotalAmount]').setValue(false).setReadOnly(true);
                            me.form.down('checkboxfield[name=IsQualifyByRechargeOneOffAmount]').setValue(false).setReadOnly(true);
                        }
                    }
                }, {
                    xtype: 'fieldcontainer',
                    fieldLabel: '按储值金额',
                    layout: 'vbox',
                    items: [{
                        xtype: 'fieldcontainer',
                        layout: {
                            type: 'hbox',
                            pack: 'start',
                            align: 'bottom'
                        },
                        items: [{
                            xtype: 'checkboxfield',
                            name: 'IsQualifyByRechargeTotalAmount',
                            width: 90,
                            boxLabel: '累计储值达',
                            inputValue: true,
                            readOnly: true,
                            handler: function (checkbox, checked) {
                                me.setFieldReadOnly(me.form.down('numberfield[name=QualifyByRechargeTotalAmount]'), !checked);
                            }
                        }, {
                            xtype: 'numberfield',
                            name: 'QualifyByRechargeTotalAmount',
                            minValue: 1,
                            width: 80,
                            hideLabel: true,
                            readOnly: true,
                        }, {
                            xtype: 'displayfield',
                            value: '元，成为该等级会员',
                            margin: '0 0 0 5',
                        }]
                    }, {
                        xtype: 'fieldcontainer',
                        layout: {
                            type: 'hbox',
                            pack: 'start',
                            align: 'bottom'
                        },
                        items: [{
                            xtype: 'checkboxfield',
                            name: 'IsQualifyByRechargeOneOffAmount',
                            width: 100,
                            boxLabel: '一次性储值达',
                            inputValue: true,
                            readOnly: true,
                            handler: function (checkbox, checked) {
                                me.setFieldReadOnly(me.form.down('numberfield[name=QualifyByRechargeOneOffAmount]'), !checked);
                            }
                        }, {
                            xtype: 'numberfield',
                            name: 'QualifyByRechargeOneOffAmount',
                            minValue: 1,
                            width: 80,
                            hideLabel: true,
                            readOnly: true,
                        }, {
                            xtype: 'displayfield',
                            value: '元，成为该等级会员',
                            margin: '0 0 0 5',
                        }]
                    }]
                }]
            }, {
                xtype: 'panel',
                title: '购买',
                layout: 'anchor',
                bodyPadding: 10,
                margin: '0 0 10 0',
                items: [{
                    xtype: 'checkboxfield',
                    name: 'IsQualifyByPurchase',
                    boxLabel: '通过购买获得资格',
                    inputValue: true,
                    handler: function (checkbox, checked) {
                        me.setFieldReadOnly(me.form.down('numberfield[name=QualifyByPurchaseAmount]'), !checked);
                        me.setFieldReadOnly(me.form.down('checkboxfield[name=IsAllowDiffPurchaseAmount]'), !checked);
                    }
                }, {
                    xtype: 'fieldcontainer',
                    fieldLabel: '基础方案',
                    layout: {
                        type: 'hbox',
                        pack: 'start',
                        align: 'bottom'
                    },
                    items: [{
                        xtype: 'displayfield',
                        value: '直接花费',
                        width: 60,
                    }, {
                        xtype: 'numberfield',
                        name: 'QualifyByPurchaseAmount',
                        minValue: 0,
                        width: 80,
                        hideLabel: true,
                        readOnly: true,
                    }, {
                        xtype: 'displayfield',
                        value: '元，成为该等级会员',
                        width: 120,
                        margin: '0 50 0 5',
                    }, {
                        xtype: 'checkboxfield',
                        name: 'IsAllowDiffPurchaseAmount',
                        boxLabel: '是否支持差价购买',
                        inputValue: true,
                        readOnly: true,
                    },]
                }]
            }, {
                xtype: 'displayfield',
                value: '会员特权',
                border: '0 0 1 0',
                padding: '0 0 5 0',
                height: 30,
                anchor: '100%',
                style: {
                    borderColor: 'gray',
                    borderStyle: 'solid',
                },
                renderer: function (rawValue) {
                    return '<div style="font-size: 26px;">' + rawValue + '</div>';
                }
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '初始积分',
                combineErrors: false,
                layout: {
                    type: 'hbox',
                    pack: 'start',
                    align: 'bottom'
                },
                items: [{
                    xtype: 'numberfield',
                    name: 'GradePoint',
                    minValue: 0,
                    allowDecimals: false,
                    width: 100,
                    allowBlank: false,
                }, {
                    xtype: 'displayfield',
                    value: '成为该等级会员后获得的积分',
                    margin: '0 0 0 5',
                }]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '消费折扣系数',
                layout: 'vbox',
                items: [{
                    xtype: 'hiddenfield',
                    name: 'IsYunPosIntegration',
                    value: false,
                }, {
                    xtype: 'fieldcontainer',
                    layout: {
                        type: 'hbox',
                        pack: 'start',
                        align: 'bottom'
                    },
                    items: [{
                        xtype: 'radiofield',
                        name: 'IsYunPosIntegrationRadioValue',
                        boxLabel: '不关联POS',
                        width: 90,
                        inputValue: 'false',
                        handler: function (checkbox, checked) {
                            me.setFieldReadOnly(me.form.down('numberfield[name=DiscountRate]'), !checked);
                        }
                    }, {
                        xtype: 'numberfield',
                        name: 'DiscountRate',
                        minValue: 0,
                        maxValue: 1,
                        width: 80,
                        hideLabel: true,
                        readOnly: true,
                        margin: '0 0 0 10',
                    }, {
                        xtype: 'displayfield',
                        value: '（范围0~1，保留两位小数）',
                        margin: '0 0 0 5',
                    }]
                }, {
                    xtype: 'fieldcontainer',
                    layout: {
                        type: 'hbox',
                        pack: 'start',
                        align: 'bottom'
                    },
                    items: [{
                        xtype: 'radiofield',
                        name: 'IsYunPosIntegrationRadioValue',
                        boxLabel: '关联POS',
                        width: 90,
                        inputValue: 'true',
                        handler: function (checkbox, checked) {
                            me.form.down('hiddenfield[name=IsYunPosIntegration]').setValue(checked);
                            me.form.down('button[name=btnSelectPosRightDiscount]').setVisible(checked);
                        }
                    }, {
                        xtype: 'button',
                        name: 'btnSelectPosRightDiscount',
                        text: '添加折扣方案',
                        margin: '0 0 0 10',
                        hidden: true,
                    }]
                }, {
                    xtype: 'container',
                    name: 'panelPosRightDiscount',
                    layout: 'vbox',
                }]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '积分赠送',
                layout: 'vbox',
                items: [{
                    xtype: 'fieldcontainer',
                    layout: {
                        type: 'hbox',
                        pack: 'start',
                        align: 'bottom'
                    },
                    items: [{
                        xtype: 'checkboxfield',
                        name: 'IsGiftPointByConsumeAmount',
                        boxLabel: '每消费',
                        width: 70,
                        inputValue: true,
                        handler: function (checkbox, checked) {
                            me.setFieldReadOnly(me.form.down('numberfield[name=GiftPointByConsumeAmount]'), !checked);
                            me.setFieldReadOnly(me.form.down('numberfield[name=ConsumeGiftPoint]'), !checked);
                        }
                    }, {
                        xtype: 'numberfield',
                        name: 'GiftPointByConsumeAmount',
                        minValue: 1,
                        width: 80,
                        hideLabel: true,
                        readOnly: true,
                    }, {
                        xtype: 'displayfield',
                        value: '元，送',
                        margin: '0 5 0 5',
                    }, {
                        xtype: 'numberfield',
                        name: 'ConsumeGiftPoint',
                        minValue: 0,
                        allowDecimals: false,
                        width: 80,
                        hideLabel: true,
                        readOnly: true,
                    }, {
                        xtype: 'displayfield',
                        value: '积分',
                        margin: '0 0 0 5',
                    }]
                }, {
                    xtype: 'fieldcontainer',
                    layout: {
                        type: 'hbox',
                        pack: 'start',
                        align: 'bottom'
                    },
                    items: [{
                        xtype: 'checkboxfield',
                        name: 'IsGiftPointByRechargeAmount',
                        boxLabel: '每储值',
                        width: 70,
                        inputValue: true,
                        handler: function (checkbox, checked) {
                            me.setFieldReadOnly(me.form.down('numberfield[name=GiftPointByRechargeAmount]'), !checked);
                            me.setFieldReadOnly(me.form.down('numberfield[name=RechargeGiftPoint]'), !checked);
                        }
                    }, {
                        xtype: 'numberfield',
                        name: 'GiftPointByRechargeAmount',
                        minValue: 1,
                        width: 80,
                        hideLabel: true,
                        readOnly: true,
                    }, {
                        xtype: 'displayfield',
                        value: '元，送',
                        margin: '0 5 0 5',
                    }, {
                        xtype: 'numberfield',
                        name: 'RechargeGiftPoint',
                        minValue: 1,
                        allowDecimals: false,
                        width: 80,
                        hideLabel: true,
                        readOnly: true,
                    }, {
                        xtype: 'displayfield',
                        value: '积分',
                        margin: '0 0 0 5',
                    }]
                }]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '会员专享',
                combineErrors: false,
                layout: 'vbox',
                items: [{
                    xtype: 'button',
                    name: 'btnSelectPosRightProduct',
                    text: '选择商品',
                }, {
                    xtype: 'container',
                    name: 'panelPosRightProduct',
                    layout: 'vbox',
                }]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '是否支持积分支付',
                layout: 'vbox',
                items: [{
                    xtype: 'hiddenfield',
                    name: 'IsAllowPointPayment',
                    value: false,
                }, {
                    xtype: 'fieldcontainer',
                    layout: {
                        type: 'hbox',
                        pack: 'start',
                        align: 'bottom'
                    },
                    items: [{
                        xtype: 'radiofield',
                        name: 'IsAllowPointPaymentRadioValue',
                        boxLabel: '是，1积分抵扣',
                        width: 110,
                        inputValue: 'true',
                        handler: function (checkbox, checked) {
                            me.form.down('hiddenfield[name=IsAllowPointPayment]').setValue(checked);
                            me.setFieldReadOnly(me.form.down('numberfield[name=PonitExchangeValue]'), !checked);
                        }
                    }, {
                        xtype: 'numberfield',
                        name: 'PonitExchangeValue',
                        minValue: 0.01,
                        width: 80,
                        hideLabel: true,
                        readOnly: true,
                    }, {
                        xtype: 'displayfield',
                        value: '元',
                        margin: '0 0 0 5',
                    }]
                }, {
                    xtype: 'radiofield',
                    name: 'IsAllowPointPaymentRadioValue',
                    boxLabel: '否',
                    width: 90,
                    inputValue: 'false',
                }]
            }, {
                xtype: 'displayfield',
                value: '备注（选填）',
                border: '0 0 1 0',
                padding: '0 0 5 0',
                height: 30,
                anchor: '100%',
                style: {
                    borderColor: 'gray',
                    borderStyle: 'solid',
                },
                renderer: function (rawValue) {
                    return '<div style="font-size: 26px;">' + rawValue + '</div>';
                }
            }, {
                xtype: 'textareafield',
                name: 'Remark',
                hideLabel: true,
                grow: true,
                anchor: '100%',
            }]
        });

        me.items = [{
            xtype: 'button',
            action: 'cancel',
            text: '返回',
            scale: 'medium',
            scope: me,
            margin: '0 0 10',
        },
        me.form,
        {
            xtype: 'container',
            items: [{
                xtype: 'button',
                text: '保存',
                action: 'save',
                scale: 'medium',
                cls: 'submitBtn',
            }, {
                xtype: 'button',
                action: 'cancel',
                text: '取消',
                scale: 'medium',
                scope: me,
                margin: '0 0 0 30',
            }]
        }];
        me.callParent(arguments);
    },
    setFieldReadOnly: function (field, readOnly) {
        if (readOnly) {
            field.allowBlank = true;
            field.setValue('').setReadOnly(true);
        } else {
            field.allowBlank = false;
            field.setReadOnly(false);
        }
    }
});