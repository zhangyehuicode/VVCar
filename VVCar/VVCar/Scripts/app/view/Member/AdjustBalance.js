Ext.define("WX.view.Member.AdjustBalance", {
    extend: "Ext.window.Window",
    alias: "widget.AdjustBalance",
    layout: "fit",
    title: "会员余额调整",
    width: 300,
    closeable: true,
    modal: true,
    bodyPadding: 0,
    buttonAlign: 'center',
    initComponent: function () {
        var me = this;
        var adjustTypeStore = Ext.create("WX.store.DataDict.AdjustTypeDicStore");
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
                padding: 0,
                autoWidth: true,
                autoScroll: true,
                columnWidth: 1,
                trackResetOnLoad: true,
                items: [
                    {
                        xtype: "textfield",
                        name: "CardNumber",
                        fieldLabel: "会员卡号",
                        margin: "10 5 0 5",
                        allowBlank: false,
                        readOnly: true,
                        hidden: true,
                    }, {
                        xtype: "combobox",
                        store: adjustTypeStore,
                        displayField: "DictName",
                        valueField: "DictValue",
                        name: "AdjustType",
                        fieldLabel: "调整类别",
                        margin: "10 5 0 5",
                        allowBlank: false
                    }, {
                        xtype: "numberfield",
                        name: "AdjustBalance",
                        fieldLabel: "调整金额",
                        margin: "10 5 0 5",
                        minValue: 1,
                        allowDecimals: true,
                        decimalPrecision: 3,
                        allowBlank: false
                    }, {
                        xtype: "displayfield",
                        value: "调整说明：（微信通知）",
                        margin: "5 0 0 5",
                    },
                    {
                        xtype: "textareafield",
                        name: "AdjustMark",
                        margin: "0 5 5 5",
                        width: '100%',
                        allowBlank: false
                    }, {
                        //xtype: "container",
                        //layout: {
                        //    type: "hbox",
                        //    align: 'right'
                        //},
                        //items: [
                        //    {
                        //        align: "right",
                        //        action: "save",
                        //        margin: "5 5 5 0",
                        //        xtype: "button",
                        //        text: "保 存",
                        //        dock: "bottom"
                        //    }, {
                        //        margin: "5 0 5 5",
                        //        xtype: "button",
                        //        text: "取 消",
                        //        scope: me,
                        //        handler: me.close
                        //    }
                        //]
                    }
                ]

            }
        ];
        me.buttons = [
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
        ];
        me.callParent(arguments);
    }
});