Ext.define('WX.controller.Main', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.SysNavMenuStore', 'WX.store.BaseData.UserStore'],
    views: ['Main', 'WX.view.User.ChangePassword', 'WX.view.Index'],
    refs: [{
        ref: 'btnWelcome',
        selector: 'Main button[name=btnWelcome]'
    }, {
        ref: 'centerPanel',
        selector: 'Main tabpanel[name=centerPanel]'
    }, {
        ref: 'navigationPanel',
        selector: 'Main treelist[name=navigationPanel]'
    }, {
        ref: 'WinUserChangePassword',
        selector: 'UserChangePassword'
    }, {
        ref: 'FormChangePassword',
        selector: 'UserChangePassword form[name=formChangePassword]'
    }],
    init: function () {
        var me = this;
        me.control({
            'Main': {
                afterrender: me.onMainAfterRender,
            },
            'Main menuitem[action=changePassword]': {
                click: me.changePassword,
            },
            'Main button[action=exitSystem]': {
                click: me.exitSystem,
            },
            'Main treelist[name=navigationPanel]': {
                selectionchange: me.onNavigationClick
            },
            'Main button[action=refreshMenu]': {
                click: me.onRefreshMenuClick,
            },
            'Main button[action=expandAllMenu]': {
                click: me.onExpandAllMenuClick,
            },
            'Main button[action=menucollapse]': {
                click: me.menuCollapse
            },
            'UserChangePassword button[action=confirm]': {
                click: me.userChangePassword,
            },
            'Index': {
                afterrender: me.indexrender
            }
        });
    },
    menuCollapse: function (btn) {
        var me = this;
        var navigationPanel = me.getNavigationPanel().up('panel');
        navigationPanel.setCollapsed(!navigationPanel.getCollapsed());
        //var systemTitle = btn.up('toolbar').down('displayfield[name=SystemTitle]');
        //systemTitle.setHidden(!systemTitle.isHidden() && navigationPanel.getCollapsed());
    },
    onMainAfterRender: function () {
        var userName = sessionStorage.getItem('userName');
        if (userName != null && userName != '') {
            this.getBtnWelcome().setText('欢迎你，' + userName);
        }
    },
    changePassword: function () {
        var winChangePassword = Ext.widget("UserChangePassword");
        winChangePassword.show();
    },
    userChangePassword: function () {
        var me = this;
        var form = me.getFormChangePassword();
        if (form.isValid()) {
            var formValues = form.getValues();
            if (formValues.NewPassword != formValues.ConfirmPassword) {
                Ext.MessageBox.alert("提示", "密码不一致，请重新输入");
                return;
            }
            Ext.MessageBox.confirm('询问', '确认修改密码?', function (opt) {
                if (opt == 'yes') {
                    var userStore = Ext.create('WX.store.BaseData.UserStore');
                    userStore.changePassword(formValues.OldPassword, formValues.NewPassword,
                        function (response) {
                            var result = Ext.decode(response.responseText);
                            if (result.IsSuccessful) {
                                if (result.Data == true) {
                                    Ext.MessageBox.alert("提示", "修改密码成功");
                                    me.getWinUserChangePassword().close();
                                } else {
                                    Ext.MessageBox.alert("提示", "修改密码失败");
                                }
                            } else {
                                Ext.MessageBox.alert("提示", "修改密码失败, " + result.ErrorMessage);
                            }
                        });
                }
            });
        }
    },
    exitSystem: function () {
        Ext.Msg.confirm('系统提示？', '您确定要退出本系统吗？', function (optional) {
            if (optional == 'yes') {
                sessionStorage.clear();
                window.location.href = "/login/page";
            }
        });
    },
    onNavigationClick: function (v, record, item) {
        if (record.isLeaf()) { //判断是否是根节点
            if (record.raw.Type == 2) { //1:组件，2:路径
                var panelID = 'tab' + record.raw.Component;
                var center = this.getCenterPanel();
                var panels = center.getComponent(panelID);
                if (!panels) {
                    var panel = Ext.create('Ext.panel.Panel', {
                        title: record.data.text,
                        closable: true,
                        id: panelID,
                        iconCls: 'icon-activity',
                        html: '<iframe width="100%" height="100%" frameborder="0" src="' + record.raw.SysMenuUrl + '"></iframe>'
                    });
                    center.add(panel);
                    center.setActiveTab(panel);

                } else {
                    center.setActiveTab(panels);
                }
            } else if (record.raw.Type == 1) {//1:组件，2:路径
                var me = this;
                Ext.require('WX.controller.' + record.raw.Component, function () {
                    Ext.onReady(function () {
                        Ext.getApplication().loadModule(record.raw.Component);
                        var center = me.getCenterPanel();
                        var panel = center.down(record.raw.SysMenuUrl);
                        if (!panel) {
                            var panel = Ext.widget(record.raw.SysMenuUrl, { title: record.data.text, minWidth: 1100 });
                            Ext.checkActionPermission(panel);
                            center.add(panel);
                            center.setActiveTab(panel);
                        } else {
                            center.setActiveTab(panel);
                        }

                    })
                })
            }
        }
    },
    onRefreshMenuClick: function (btn) {
        var navPanel = this.getNavigationPanel();
        navPanel.store.load({
            callback: function (records, operation, success) {
                navPanel.getRootNode().expandChildren();
            }
        });
    },
    onExpandAllMenuClick: function (btn) {
        var navPanel = this.getNavigationPanel();
        navPanel.getRootNode().expandChildren(true);
    },
    indexrender: function (panel, eOpts) {
        var memberCount = panel.down("displayfield[name=MemberCount]");
        var rechargemembercount = panel.down("displayfield[name=RechargeMemberCount]");
        var discountmembercount = panel.down("displayfield[name=DiscountMemberCount]");
        var fund = panel.down("displayfield[name=Fund]");
        var rechargeandgive = panel.down("displayfield[name=RechargeAndGive]");
        var recharge = panel.down("displayfield[name=Recharge]");
        var consume = panel.down("displayfield[name=Consume]");
        var give = panel.down("displayfield[name=Give]");
        //Ext.Ajax.request({
        //    method: "Get",
        //    url: Ext.GlobalConfig.ApiDomainUrl + 'api/Member/Statistic',
        //    success: function (response) {
        //        response = JSON.parse(response.responseText);
        //        if (response.IsSuccessful) {
        //            memberCount.setValue(response.Data.MemberCount);
        //            //rechargemembercount.setValue(response.Data.RechargeMemberCount);
        //            //discountmembercount.setValue(response.Data.DiscountMemberCount);
        //            fund.setValue(Ext.util.Format.number(response.Data.Fund, '0,000.00'));
        //            rechargeandgive.setValue(Ext.util.Format.number(response.Data.RechargeAndGive, '0,000.00'));
        //            recharge.setValue(Ext.util.Format.number(response.Data.Recharge, '0,000.00'));
        //            give.setValue(Ext.util.Format.number(response.Data.Give, '0,000.00'));
        //            consume.setValue(Ext.util.Format.number(response.Data.Consume, '0,000.00'));
        //        }
        //    },
        //    failure: function (response) {
        //    }
        //});
    }
});

