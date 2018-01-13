Ext.define("WX.view.Member.ChangeCard", {
    extend: "Ext.window.Window",
    alias: "widget.ChangeCard",
    title: "换卡",
    layout: "fit",
    width: 350,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        me.form = Ext.create('Ext.form.Panel', {
            name: 'formChangeCard',
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%',
            },
            items: [{
                xtype: 'textfield',
                name: 'MemberID',
                fieldLabel: '会员ID',
                hidden: true,
            }, {
                xtype: 'textfield',
                name: 'OldPassword',
                fieldLabel: '原卡密码',
                inputType: 'password',
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'CardNumber',
                fieldLabel: '新卡卡号',
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'VerifyCode',
                fieldLabel: '新卡校验码',
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'NewPassword',
                fieldLabel: '新卡密码',
                inputType: 'password',
                allowBlank: false,
                regex: /\d{6}/,
                regexText :'只能输入6位数字',
            }, {
                xtype: 'textfield',
                name: 'ConfirmPassword',
                fieldLabel: '确认密码',
                inputType: 'password',
                allowBlank: false,
                regex: /\d{6}/,
                regexText: '只能输入6位数字',
            }]
        });

        me.items = [me.form];
        me.buttons = [
            {
                text: '确定',
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
        me.callParent();
    }

});