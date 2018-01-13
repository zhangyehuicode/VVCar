Ext.define('WX.view.MemberTrade.VerifyPassword', {
    extend: 'Ext.window.Window',
    alias: 'widget.VerifyPassword',
    title: '请输入密码',
    layout: 'fit',
    width: 350,
    modal: true,
    bodyPadding: 5,
    initComponent: function () {
        var me = this;
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            items: [{
                xtype: 'textfield',
                name: 'MemberPwd',
                inputType: 'password',
                anchor: '100%',
                fieldStyle: 'font-size: 22px;line-height: normal',
                height: 30,
            }]
        });

        me.items = [me.form];
        me.buttons = [
            {
                text: '确认',
                cls: 'submitBtn',
                action: 'confirm'
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