Ext.define('WX.view.MemberCard.MemberCardEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.MemberCardEdit',
    title: '卡片信息编辑',
    layout: 'fit',
    width: 350,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%',
            },
            items: [{
                xtype: 'numberfield',
                name: 'CardBalance',
                fieldLabel: '初始余额',
                allowBlank: false,
            }, {
                xtype: "datefield",
                name: "ExpiredDate",
                fieldLabel: "截止日期",
                format: "Y-m-d",
            }, {
                xtype: 'textfield',
                name: 'Remark',
                fieldLabel: '备注',
                maxLength: 30,
            }]
        });

        me.items = [me.form];
        me.buttons = [{
            text: '保存',
            action: 'save',
            cls: 'submitBtn',
            scope: me,
        }, {
            text: '取消',
            scope: me,
            handler: me.close
        }];
        me.callParent(arguments);
    }
});