Ext.define("WX.view.Member.AdjustMemberPoint", {
    extend: "Ext.window.Window",
    alias: "widget.AdjustMemberPoint",
    layout: "fit",
    title: "会员积分调整",
    width: 400,
    closeable: true,
    modal: true,
    bodyPadding: 5,
    buttonAlign: 'right',
    initComponent: function () {
        var me = this;
        var adjustTypeStore = Ext.create("WX.store.DataDict.AdjustMemberPointTypeDicStore");
        me.items = [
            {
                xtype: "form",
                name: "MemberInfo",
                layout: "vbox",
                border: false,
                frame: false,
                labelAlign: "left",
                labelWidth: 60,
                padding: 5,
                autoWidth: true,
                autoScroll: true,
                columnWidth: 1,
                items: [
                    {
                        xtype: "textfield",
                        name: "MemberID",
                        fieldLabel: "会员ID",
                        margin: "10 20 0 5",
                        allowBlank: false,
                        readOnly: true,
                        hidden: true,
                    },
                    //{
                    //    xtype: "textfield",
                    //    name: "CardNumber",
                    //    fieldLabel: "会员卡号",
                    //    margin: "10 20 0 5",
                    //    allowBlank: false,
                    //    readOnly: true
                    //},
                    {
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
                        name: "Point",
                        fieldLabel: "会员积分",
                        margin: "10 20 0 5",
                        minValue: 0,
                        readOnly: true,
                        hidden: true,
                    }, {
                        xtype: "numberfield",
                        name: "AdjustMemberPoint",
                        fieldLabel: "调整积分",
                        margin: "10 20 0 5",
                        minValue: 1,
                        allowBlank: false,
                    }
                ]

            }
        ];
        me.dockedItems = [{
            xtype: 'toolbar',
            dock: 'bottom',
            ui: 'footer',
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
        }];

        me.callParent(arguments);
    }
});