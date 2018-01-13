Ext.define('WX.view.WeChatFans.WeChatFansTagEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.WeChatFansTagEdit',
    layout: 'fit',
    width: 300,
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
                xtype: 'textfield',
                name: 'Code',
                fieldLabel: '编号',
                maxLength: 20,
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '名称',
                maxLength: 16,
                allowBlank: false,
            }, {
                xtype: 'numberfield',
                name: 'Index',
                fieldLabel: '排序',
                minValue: 1,
                value: 1,
                allowBlank: false,
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