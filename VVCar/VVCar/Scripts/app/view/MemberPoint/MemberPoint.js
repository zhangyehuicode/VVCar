Ext.define('WX.view.MemberPoint.MemberPoint', {
    extend: 'Ext.container.Container',
    alias: 'widget.MemberPoint',
    title: '积分生成',
    stripeRows: true,
    loadMask: true,
    closable: true,
    width: "100%",
    height: '100%',
    overflowY: 'auto',
    modal: true,
    layout: {
        type: 'vbox',
        align: 'stretch',
        padding: 5,
    },
    initComponent: function () {
        var me = this;
        var items = [{
            xtype: 'panel',
            title: '注册送积分',
            name: 'RegisterPanel',
            layout: 'vbox',
            padding: '20 50',
            border: true,
            items: [{
                xtype: 'checkbox',
                boxLabel: '开启注册送积分规则',
                padding: '10 0 0 28',
                name: 'RegisterIsAvailable',
            }, {
                xtype: 'panel',
                layout: 'hbox',
                border: false,
                padding: '20 20 20 20',
                items: [{
                    xtype: 'label',
                    text: '基本规则',
                    padding: '3 50 3 10',
                }, {
                    xtype: 'numberfield',
                    name: 'RegisterPoint',
                    fieldLabel: '新用户注册即送',
                    labelWidth: 100,
                    width: 200,
                    minValue: 0,
                    allowBlank: false,
                }, {
                    xtype: 'label',
                    text: '积分',
                    padding: '3 10',
                }]
            }]
        }, {
            xtype: 'panel',
            title: '签到送积分',
            name: 'SignInPanel',
            layout: 'vbox',
            padding: '20 50',
            border: true,
            items: [{
                xtype: 'checkbox',
                boxLabel: '开启签到送积分规则',
                padding: '10 0 0 28',
                name: 'SignInIsAvailable',
            }, {
                xtype: 'panel',
                layout: 'hbox',
                border: false,
                padding: '20 20 20 20',
                items: [{
                    xtype: 'label',
                    text: '基本规则',
                    padding: '3 50 3 10',
                }, {
                    xtype: 'numberfield',
                    name: 'SignInPoint',
                    fieldLabel: '每日签到送',
                    labelWidth: 100,
                    width: 200,
                    minValue: 0,
                    allowBlank: false,
                }, {
                    xtype: 'label',
                    text: '积分',
                    padding: '3 10',
                }]
            }, {
                xtype: 'panel',
                layout: 'hbox',
                border: false,
                padding: '20',
                items: [{
                    xtype: 'label',
                    text: '额外奖励规则',
                    padding: '3 50 3 10',
                }, {
                    xtype: 'button',
                    text: '添加奖励规则',
                    action: 'addSignInAdditionalRules',
                    width: 90,
                }, {
                    xtype: 'label',
                    text: '注：最多设置5个额外奖励规则，多个奖励不叠加',
                    padding: '3 10',
                }]
            }, {
                xtype: 'panel',
                layout: 'vbox',
                name: 'additionalrules',
                border: false,
                padding: '0 0 20 150',
                items: []
            }]
        }, {
            xtype: 'panel',
            title: '分享送积分',
            name: 'SharePanel',
            layout: 'vbox',
            padding: '20 50',
            border: true,
            items: [{
                xtype: 'checkbox',
                boxLabel: '开启分享送积分规则',
                padding: '10 0 0 28',
                name: 'ShareIsAvailable',
            }, {
                xtype: 'form',
                name: 'iconupload',
                layout: 'hbox',
                border: false,
                padding: '20 20 20 30',
                items: [{
                    xtype: 'filefield',
                    fieldLabel: '分享图标',
                    labelWidth: 100,
                    width: 300,
                    msgTarget: 'side',
                    allowBlank: true,
                    anchor: '100%',
                    buttonText: '选择图片'
                }, {
                    xtype: 'button',
                    text: '上传图片',
                    action: 'shareiconupload',
                    margin: '0 0 0 20',
                }, {
                    xtype: 'textfield',
                    name: 'Icon',
                    fieldLabel: '图片保存路径',
                    padding: '0 0 0 20',
                    hidden: true,
                }]
            }, {
                xtype: 'box',
                name: 'IconShow',
                width: 100,
                height: 100,
                margin: '0 0 0 140',
                autoEl: {
                    tag: 'img',
                    src: ''
                }
            }, {
                xtype: 'panel',
                layout: 'hbox',
                border: false,
                padding: '20 20 20 30',
                items: [{
                    xtype: 'textfield',
                    name: 'Title',
                    fieldLabel: '分享标题',
                    labelWidth: 100,
                    width: 300,
                }]
            }, {
                xtype: 'panel',
                layout: 'hbox',
                border: false,
                padding: '20 20 20 30',
                items: [{
                    xtype: 'textfield',
                    name: 'SubTitle',
                    fieldLabel: '分享副标题',
                    labelWidth: 100,
                    width: 300,
                }]
            }, {
                xtype: 'panel',
                layout: 'hbox',
                border: false,
                padding: '20 20 20 20',
                items: [{
                    xtype: 'label',
                    text: '基本规则',
                    padding: '3 50 3 10',
                }, {
                    xtype: 'numberfield',
                    name: 'SharePoint',
                    fieldLabel: '每次分享送',
                    labelWidth: 100,
                    width: 200,
                    minValue: 0,
                    allowBlank: false,
                }, {
                    xtype: 'label',
                    text: '积分',
                    padding: '3 10',
                }]
            }, {
                xtype: 'panel',
                layout: 'hbox',
                border: false,
                padding: '20',
                items: [{
                    xtype: 'label',
                    text: '规则限制',
                    padding: '3 50 3 10',
                }, {
                    xtype: 'numberfield',
                    name: 'ShareLimit',
                    fieldLabel: '每人每天最多可获得',
                    labelWidth: 130,
                    width: 230,
                    allowDecimals: false,
                    minValue: 0,
                    allowBlank: false,
                }, {
                    xtype: 'label',
                    text: '次分享积分',
                    padding: '3 10',
                }]
            }, {
                xtype: 'panel',
                layout: 'hbox',
                border: false,
                padding: '20',
                items: [{
                    xtype: 'label',
                    text: '额外奖励规则',
                    padding: '3 50 3 10',
                }, {
                    xtype: 'button',
                    text: '添加奖励规则',
                    action: 'addShareAdditionalRules',
                    width: 90,
                }, {
                    xtype: 'label',
                    text: '注：最多设置5个额外奖励规则，多个奖励不叠加',
                    padding: '3 10',
                }]
            }, {
                xtype: 'panel',
                layout: 'vbox',
                name: 'additionalrules',
                border: false,
                padding: '0 0 20 150',
                items: []
            }]
        }, {
            xtype: 'panel',
            title: '评价送积分',
            name: 'AppraisePanel',
            layout: 'vbox',
            padding: '20 50',
            border: true,
            items: [{
                xtype: 'checkbox',
                boxLabel: '开启评价送积分规则',
                padding: '10 0 0 28',
                name: 'AppraiseIsAvailable',
            }, {
                xtype: 'panel',
                layout: 'hbox',
                border: false,
                padding: '20 20 20 20',
                items: [{
                    xtype: 'label',
                    text: '基本规则',
                    padding: '3 50 3 10',
                }, {
                    xtype: 'numberfield',
                    name: 'AppraisePoint',
                    fieldLabel: '每次评价送',
                    labelWidth: 100,
                    width: 200,
                    minValue: 0,
                    allowBlank: false,
                }, {
                    xtype: 'label',
                    text: '积分',
                    padding: '3 10',
                }]
            }, {
                xtype: 'panel',
                layout: 'hbox',
                border: false,
                padding: '20',
                items: [{
                    xtype: 'label',
                    text: '规则限制',
                    padding: '3 50 3 10',
                }, {
                    xtype: 'numberfield',
                    name: 'AppraiseLimit',
                    fieldLabel: '每人每天最多可获得',
                    labelWidth: 130,
                    width: 230,
                    allowDecimals: false,
                    minValue: 0,
                    allowBlank: false,
                }, {
                    xtype: 'label',
                    text: '次评价积分',
                    padding: '3 10',
                }]
            }, {
                xtype: 'panel',
                layout: 'hbox',
                border: false,
                padding: '20',
                items: [{
                    xtype: 'label',
                    text: '额外奖励规则',
                    padding: '3 50 3 10',
                }, {
                    xtype: 'button',
                    text: '添加奖励规则',
                    action: 'addAppraiseAdditionalRules',
                    width: 90,
                }, {
                    xtype: 'label',
                    text: '注：最多设置5个额外奖励规则，多个奖励不叠加',
                    padding: '3 10',
                }]
            }, {
                xtype: 'panel',
                layout: 'vbox',
                name: 'additionalrules',
                border: false,
                padding: '0 0 20 150',
                items: []
            }]
        }, {
            xtype: 'panel',
            layout: {
                type: 'hbox',
                align: 'middle',
                pack: 'center',
            },
            border: false,
            items: [{
                xtype: 'button',
                text: '保存',
                action: 'save',
                margin: '10',
                //}, {
                //xtype: 'button',
                //text: '取消',
                //action: 'cancel',
                //margin: '10',
            }]
        }];
        me.items = [{
            xtype: 'form',
            name: 'MemberPointForm',
            border: false,
            items: items,
        }];

        me.callParent(arguments);
    }
});