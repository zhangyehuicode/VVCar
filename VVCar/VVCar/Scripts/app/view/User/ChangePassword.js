Ext.define('WX.view.User.ChangePassword', {
    extend: 'Ext.window.Window',
    alias: 'widget.UserChangePassword',
    title: '修改密码',
    layout: 'fit',
    width: 300,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        me.form = Ext.create('Ext.form.Panel', {
            name: 'formChangePassword',
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%',
            },
            items: [{
                xtype: 'textfield',
                name: 'OldPassword',
                fieldLabel: '原密码',
                inputType: 'password',
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'NewPassword',
                fieldLabel: '新密码',
                inputType: 'password',
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'ConfirmPassword',
                fieldLabel: '确认密码',
                inputType: 'password',
                allowBlank: false,
            }]
        });

        me.items = [me.form];
        me.buttons = [
            {
                text: '确认',
                action: 'confirm',
                cls: 'submitBtn',
                scope: me,
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