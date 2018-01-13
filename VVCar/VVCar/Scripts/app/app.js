Ext.Loader.setConfig({
    enabled: true
});

//创建应用程序的实例
var application;
Ext.application({
    name: 'WX',
    appFolder: '/Scripts/app',
    //requires: ['WX.view.Main'],
    //controllers: ['Main'],
    launch: function () {
        application = this;
        var me = this;
        var userToken = sessionStorage.getItem('userToken');
        if (userToken === null || userToken === '') {
            window.location.href = "/login/Page";
        } else {
            Ext.tip.QuickTipManager.init();
            //customer init.
            //Ext.GlobalConfig.ApiDomainUrl = '/';
            Ext.Ajax.setDefaultHeaders({
                'Authorization': userToken,
            });
            Ext.Ajax.timeout = 600000;//Defaults to 30000
            //Ext.override(Ext.data.Connection, {
            //    timeout: 600000
            //});
            Ext.override(Ext.data.proxy.Rest, { timeout: 600000 });
            Ext.ariaWarn = Ext.emptyFn
            Ext.Ajax.on('requestexception', me.ajaxRequestException, me);
            Ext.data.writer.Writer.override({
                constructor: function (config) {
                    config.writeAllFields = true;
                    this.initConfig(config);
                }
            });
            Ext.override(Ext.data.reader.Json, {
                type: 'json',
                rootProperty: 'Data',
                successProperty: 'IsSuccessful',
                messageProperty: 'ErrorMessage',
                totalProperty: 'TotalCount'
            });
            Ext.override(Ext.form.field.Checkbox, {
                uncheckedValue: false,
            });
            Ext.require(['WX.controller.Main', 'WX.view.Main'], function () {
                Ext.onReady(function () {
                    me.loadModule(['Main']);
                    Ext.create('WX.view.Main');
                });
            });
        }
        me.customApplyExt();
    },
    loadModule: function (controllers) {//自定义方法loadModule
        var me = this;
        controllers = Ext.Array.from(controllers);
        var controller;
        for (var i = 0; i < controllers.length; i++) {
            var name = controllers[i];

            /** 避免重复加载 */
            if (!me.controllers.containsKey(name)) {
                controller = Ext.create(
                    me.getModuleClassName(name, 'controller'),
                    {
                        application: me,
                        id: name
                    });
                controller.init(me);
                controller.onLaunch(me);
                me.controllers.add(controller);

            }
        }
    },
    alertLogin: function () {
        Ext.Msg.show({
            title: '提示',
            msg: '会话过期请重新登录!',
            icon: Ext.Msg.ERROR,
            buttons: Ext.Msg.OK,
            closable: false,
            fn: function () {
                window.location.href = "/login";
            }
        });
    },
    ajaxRequestException: function (conn, response, options) {
        if (response.status === 401) {
            this.alertLogin();
        } else {
            //Ext.Msg.alert('加载数据出错，请重试');
        }
    },
    customApplyExt: function () {
        Ext.apply(Ext.form.field.VTypes, {
            phone: function (v) {
                return /^\d{7,8}$|^0\d{2,3}-?\d{7,8}$/.test(v);
            },
            phoneText: '请输入正确的电话号码',
            phoneMask: /[\d\-]/i
        });
        Ext.apply(Ext.form.field.VTypes, {
            mobilephone: function (v) {
                return /(^0?[1][3578][0-9]{9}$)/.test(v);
            },
            mobilephoneText: '请输入正确的手机号码',
            mobilephoneMask: /[\d]/i
        });
        Ext.apply(Ext.form.field.VTypes, {
            IDNumber: function (v) {
                return /^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$/.test(v);
            },
            IDNumberText: '请输入正确的身份证号码',
            IDNumberMask: /[\d]/i
        });
        Ext.apply(Ext.form.field.VTypes, {
            Number: function (v) {
                return /^\d+(\.\d+)?$/.test(v);
            },
            NumberText: '请输入数字',
            NumberMask: /[\d\.]/i
        });
    },
    checkActionPermission: function (extContainer) {//检查功能权限
        var me = this;
        if (me.UserPermissionCache === undefined) {
            me.UserPermissionCache = [];
            Ext.Ajax.request({
                url: Ext.GlobalConfig.ApiDomainUrl + 'Api/User/PermissionList',
                method: 'GET',
                async: false,
                callback: function (options, success, response) {
                    var result = JSON.parse(response.responseText);
                    if (success) {
                        me.UserPermissionCache = result.Data;
                    }
                },
            });
        }
        var permissionControls = extContainer.query('[permissionCode]');
        if (permissionControls.length > 0) {
            permissionControls.forEach(function (ctl) {
                var allow = me.UserPermissionCache.some(function (item, index, array) {
                    return item === ctl.permissionCode;
                });
                if (allow) {
                    ctl.show();
                } else {
                    ctl.hide();
                }
            });
        }
    },
    validatePermission: function (permissionCode) {//检查是否拥有权限
        return this.UserPermissionCache.some(function (item, index, array) {
            return item === permissionCode;
        });
    },
    navigate: function (widgetName, title) {
        var mainPanel = Ext.getCmp("tabPanelMain");
        var panel = mainPanel.child(widgetName);
        if (!panel) {
            panel = Ext.widget(widgetName, { title: title, minWidth: 1100 });
            Ext.checkActionPermission(panel);
            mainPanel.add(panel);
            mainPanel.setActiveTab(panel);
        } else {
            mainPanel.setActiveTab(panel);
        }
    },
    navigateLink: function (link, title) {
        var mainPanel = Ext.getCmp("tabPanelMain");
        var panel = Ext.create('Ext.panel.Panel', {
            title: title,
            closable: true,
            iconCls: 'icon-activity',
            html: '<iframe width="100%" height="100%" frameborder="0" src="' + link + '"></iframe>'
        });
        mainPanel.add(panel);
        mainPanel.setActiveTab(panel);
    }
});

Ext.getApplication = function () {
    return application;
}

Ext.getController = function (name) {
    return Ext.getApplication().getController(name);
}

Ext.GlobalConfig = {
    ApiDomainUrl: '/',
}

Ext.checkActionPermission = function (extContainer) {
    return Ext.getApplication().checkActionPermission(extContainer);
}

Ext.validatePermission = function (permissionCode) {
    return Ext.getApplication().validatePermission(permissionCode);
}