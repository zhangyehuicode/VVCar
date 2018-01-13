Ext.define('WX.view.Login', {
    extend: 'Ext.window.Window',
    alias: 'widget.Login',
    layout: 'border',
    border: false,
    width: 460,
    height: 300,
    closeAction: 'hide',
    collapsible: false,
    closable: false,
    title: _systemTitle,
    items: [{
        region: 'north',
        border: false,
        height: 70,
        html: '<img width="450" height="70" border="0" src="../Content/images/loginBanner.png">'
    }, {
        xtype: 'form',
        border: false,
        region: 'center',
        fieldDefaults: {
            labelAlign: 'right',
            labelWidth: 70,
            labelSeparator: '：',
            width: 350
        },
        bodyStyle: {
            padding: '30px'
        },
        items: [{
            id: 'txtLoginUserName',
            xtype: 'textfield',
            fieldLabel: '帐&nbsp;号',
            name: 'UserName',
            blankText: '帐号不能为空,请输入!',
            maxLength: 30,
            maxLengthText: '账号的最大长度为30个字符',
            allowBlank: false,

        }, {
            xtype: 'textfield',
            fieldLabel: '密&nbsp;码',
            name: 'Password',
            inputType: 'password',
            blankText: '密码不能为空,请输入!',
            maxLength: 20,
            maxLengthText: '密码的最大长度为20个字符',
            allowBlank: false,
        }],
        buttons: [{
            id: 'btnLoginApp',
            action: 'submit',
            text: '登录系统',
            iconCls: 'login',
            cls: 'submitBtn',
            height: 35,
            width: 100,
            scale: 'medium',
        }]
    }],

    afterRender: function () {
        this.callParent(arguments);
        var lastLoginUser = Ext.util.Cookies.get('lastLoginUser');
        if (lastLoginUser != '') {
            Ext.getCmp('txtLoginUserName').setValue(lastLoginUser);
        }
        this.keyNav = Ext.create('Ext.util.KeyNav', this.el, {
            enter: {
                fn: function (e) {
                    var submitBtn = Ext.getCmp('btnLoginApp');
                    submitBtn.fireEvent('click', submitBtn);
                },
                defaultEventAction: false
            },
            scope: this
        });
    }
});