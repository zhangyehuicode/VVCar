Ext.define('WX.controller.Login', {
    extend: 'Ext.app.Controller',
    views: ['Login'],
    refs: [{
        ref: 'loginWindow',
        selector: 'Login'
    }, {
        ref: 'loginForm',
        selector: 'Login form'
    }],
    init: function () {
        this.control({
            'Login button[action=submit]': {
                click: this.onSubmit
            }
        })
    },
    onSubmit: function (button) {
        var me = this;
        form = this.getLoginForm(),
        values = form.getValues();
        if (form.isValid()) {
            Ext.MessageBox.show({
                msg: '正在登录中……, 请稍侯',
                progressText: '正在登录中……',
                width: 300,
                wait: true,
                waitConfig: { interval: 200 }
            });
            Ext.Ajax.request({
                url: '/Api/Login',
                method: 'POST',
                clientValidation: true,
                params: values,
                callback: function (options, success, response) {
                    Ext.MessageBox.hide();
                    var result = JSON.parse(response.responseText);
                    if (success && result.IsSuccess == true) {
                        Ext.util.Cookies.set('lastLoginUser', values.UserName);
                        sessionStorage.setItem('LoginUser', values.UserName);
                        window.location.href = "/Main/Index";
                    } else {
                        Ext.MessageBox.alert("登录失败", result.ErrorMsg);
                    }
                },
            });
        } else {
            Ext.Msg.alert({
                title: '系统提示',
                msg: '请输入正确用户名与密码!',
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    }
});