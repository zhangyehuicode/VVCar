Ext.define('WX.view.RechargePlan.RechargePlanEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.RechargePlanEdit',
    title: '编辑储值方案',
    width: 475,
    height: 680,
    scrollable: 'y',
    modal: true,
    bodyPadding: 5,
    initComponent: function () {
        var me = this;
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            width: 445,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 70,
                anchor: '100%'
            },
            items: [{
                xtype: 'textfield',
                name: 'ID',
                fieldLabel: 'ID',
                hidden: true,
            }, {
                xtype: 'textfield',
                name: 'Code',
                fieldLabel: '方案编号',
                maxLength: 20,
                maxLengthText: '方案编号的最大长度为20个字符',
                blankText: '方案编号不能为空,请输入!',
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '方案名称',
                maxLength: 20,
                maxLengthText: '方案名称的最大长度为20个字符',
                blankText: '方案名称不能为空,请输入!',
                allowBlank: false,
            }, {
                xtype: 'combobox',
                name: 'PlanType',
                fieldLabel: '优惠类型',
                store: Ext.getStore('DataDict.RechargePlanTypeStore'),
                displayField: 'DictName',
                valueField: 'DictValue',
                emptyText: '请选择...',
                blankText: '请选择优惠类型',
                allowBlank: false,
            }, {
                xtype: 'combobox',
                name: 'IsAvailable',
                fieldLabel: '方案状态',
                store: Ext.getStore('DataDict.EnableDisableTypeStore'),
                displayField: 'DictName',
                valueField: 'DictValue',
                emptyText: '请选择...',
                blankText: '请选择方案状态',
                allowBlank: false,
                value: true
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '有效期',
                combineErrors: true,
                layout: 'hbox',
                items: [{
                    xtype: 'datefield',
                    name: "EffectiveDate",
                    flex: 1,
                    format: 'Y-m-d H:i:s',
                    fieldLabel: '生效日期',
                    hideLabel: true,
                    allowBlank: false,
                }, {
                    xtype: 'displayfield',
                    value: '至'
                }, {
                    xtype: 'datefield',
                    name: "ExpiredDate",
                    margin: '0 0 0 10',
                    flex: 1,
                    format: 'Y-m-d H:i:s',
                    fieldLabel: '截止日期',
                    hideLabel: true,
                    allowBlank: false,
                }]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '储值金额',
                combineErrors: false,
                layout: 'hbox',
                items: [{
                    name: 'RechargeAmount',
                    xtype: 'numberfield',
                    flex: 1,
                    minValue: 0,
                    hideLabel: true,
                    allowBlank: false
                }, {
                    xtype: 'displayfield',
                    value: '元'
                }, {
                    xtype: 'displayfield',
                    margin: '0 0 0 10',
                    flex: 1,
                    fieldStyle: 'color: gray;',
                    value: '*储值金额为0时，不允许勾选微信在线储值。'
                }]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: '赠送金额',
                combineErrors: false,
                layout: 'hbox',
                items: [{
                    name: 'GiveAmount',
                    xtype: 'numberfield',
                    width: 165,
                    minValue: 0,
                    hideLabel: true,
                    allowBlank: false
                }, {
                    xtype: 'displayfield',
                    value: '元'
                }]
            },
            {
                xtype: 'fieldcontainer',
                fieldLabel: '储值次数',
                combineErrors: false,
                layout: 'hbox',
                items: [{
                    xtype: 'numberfield',
                    name: 'MaxRechargeCount',
                    flex: 1,
                    blankText: '储值次数不能为空,请输入!',
                    value: 0,
                    allowDecimals: false,
                    allowBlank: false
                }, {
                    xtype: 'displayfield',
                    value: '&nbsp;&nbsp;&nbsp;'
                }, {
                    xtype: 'displayfield',
                    margin: '0 0 0 10',
                    flex: 1,
                    fieldStyle: 'color: gray;',
                    value: '*每张卡片最多使用本储值方案的次数。不限制请输入0。'
                }]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: "允许储值端",
                layout: "hbox",
                items: [
                    {
                        xtype: 'fieldcontainer',
                        //fieldLabel: "允许储值端",
                        defaultType: 'checkboxfield',
                        flex: 1,
                        layout: {
                            type: "vbox"
                        },
                        defaults: {
                            margin: "0 20 0 0"
                        },
                        items: [
                            {
                                boxLabel: "微信在线储值端",
                                name: "VisibleAtWeChat",
                                inputValue: true,
                                checked: true
                            },
                            {
                                boxLabel: "会员管理系统",
                                name: "VisibleAtPortal",
                                inputValue: true,
                                checked: true
                            }
                        ]
                    }, {
                        xtype: 'displayfield',
                        value: '&nbsp;&nbsp;&nbsp;'
                    }, {
                        xtype: 'displayfield',
                        margin: '0 0 0 10',
                        flex: 1,
                        fieldStyle: 'color: gray;',
                        value: '*勾选后在对应平台可使用本储值方案。'
                    }
                ]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: "适用卡片类型",
                labelWidth: 90,
                defaultType: 'checkboxfield',
                layout: "hbox",
                items: [{
                    boxLabel: "储值卡",
                    name: "MatchRechargeCard",
                    inputValue: true,
                    checked: true,
                }, {
                    boxLabel: "折扣卡",
                    name: "MatchDiscountCard",
                    inputValue: true,
                    margin: '0 0 0 10',
                    checked: true,
                }, {
                    boxLabel: "礼品卡",
                    name: "MatchGiftCard",
                    inputValue: true,
                    margin: '0 0 0 10',
                    checked: true,
                }]
            }, {
                xtype: 'container',
                layout: 'vbox',
                items: [{
                    xtype: 'label',
                    html: '<div style="font-size:20px;">储值激励（赠券）</div>',
                }, {
                    xtype: 'container',
                    name: 'CouponContainer',
                    layout: 'vbox',
                    padding: '20 0 0 0',
                    items: []
                }, {
                    xtype: 'button',
                    text: '新增电子券',
                    action: 'SelectCoupon',
                    margin: '10 0 0 20',
                }]
            }]
        });

        me.items = [me.form];
        me.buttons = [
            {
                text: '保存',
                cls: 'submitBtn',
                action: 'save'
            },
            {
                text: '取消',
                scope: me,
                handler: me.close
            }
        ];
        me.callParent(arguments);
    }
});