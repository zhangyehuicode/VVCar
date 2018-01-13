
var adjustTypeStore = Ext.create("WX.store.DataDict.AdjustTypeDicStore");
Ext.define("WX.view.Member.AdjustBalance", {
    extend: "Ext.window.Window",
    alias: "widget.AdjustBalance",
    layout: "fit",
    title: "会员卡余额调整",
    width: 400,
    closeable: true,
    modal: true,
    bodyPadding: 5,
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: "form",
                name: "MemberInfo",
                layout: "vbox",
                border: false,
                frame: false,
                labelAlign: "left",
                buttonAlign: "right",
                labelWidth: 60,
                padding: 5,
                autoWidth: true,
                autoScroll: true,
                columnWidth: 1,
                items: [
                    {
                        xtype: "textfield",
                        name: "CardNumber",
                        fieldLabel: "会员卡号",
                        margin: "10 20 0 5",
                        allowBlank: false,
                        readOnly: true
                    }, {
                        xtype: "combobox",
                        store: adjustTypeStore,
                        displayField: "DictName",
                        valueField: "DictValue",
                        name: "AdjustType",
                        fieldLabel: "调整类别",
                        margin: "10 20 0 5",
                        allowBlank: false
                    }, {
                        xtype: "numberfield",
                        name: "AdjustBalance",
                        fieldLabel: "调整金额",
                        margin: "10 20 0 5",
                        minValue: 1,
                        allowDecimals: true,
                        decimalPrecision: 3,
                        allowBlank: false
                    }, {
                        xtype: "displayfield",
                        value: "调整说明：（微信通知）",
                        margin: "10 20 0 0"
                    },
                    {
                        xtype: "textareafield",
                        name: "AdjustMark",
                        margin: "0 0 0 0",
                        width: 300,
                        allowBlank: false
                    }, {
                        xtype: "container",
                        layout: {
                            type: "hbox",
                            align: 'right'
                        },
                        items: [
                            {
                                align: "right",
                                action: "save",
                                margin: "5 5 5 0",
                                xtype: "button",
                                text: "保 存",
                                dock: "bottom"
                            }, {
                                margin: "5 0 5 5",
                                xtype: "button",
                                text: "取 消",
                                scope: me,
                                handler: me.close
                            }
                        ]
                    }
                ]

            }
        ];
        me.callParent(arguments);
    }
});