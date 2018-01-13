Ext.define('WX.controller.MemberPoint', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.MemberPointStore'],
    stores: [],
    views: ['MemberPoint.MemberPoint'],
    refs: [{
        ref: 'memberPoint',
        selector: 'MemberPoint'
    }],
    init: function () {
        var me = this;
        me.control({
            'MemberPoint button[action=addAppraiseAdditionalRules]': {
                click: me.addAppraiseAdditionalRules
            },
            'MemberPoint button[action=AdditionalRemove]': {
                click: me.removeAdditionalRule
            },
            'MemberPoint button[action=addShareAdditionalRules]': {
                click: me.addShareAdditionalRules
            },
            'MemberPoint button[action=addSignInAdditionalRules]': {
                click: me.addSignInAdditionalRules
            },
            'MemberPoint button[action=save]': {
                click: me.save
            },
            'MemberPoint button[action=shareiconupload]': {
                click: me.shareiconupload
            },
            'MemberPoint': {
                afterrender: me.afterrender
            },
        });
    },
    shareiconupload: function (btn) {
        var me = this;
        var form = btn.up('form').getForm();
        if (form.isValid()) {
            form.submit({
                url: Ext.GlobalConfig.ApiDomainUrl + 'api/UploadFile/MemberPointShare',
                waitMsg: '正在上传...',
                success: function (fp, o) {
                    if (o.result.success) {
                        Ext.Msg.alert('提示', '图片"' + o.result.OriginalFileName + '"上传成功！');
                        var icon = btn.up('form').down('textfield[name=Icon]');
                        icon.setValue(o.result.FileUrl);
                        me.getMemberPoint().down('box[name=IconShow]').getEl().dom.src = o.result.FileUrl;
                    } else {
                        Ext.Msg.alert('提示', o.result.errorMessage);
                    }
                },
                failure: function (fp, o) {
                    Ext.Msg.alert('提示', o.result.errorMessage);
                }
            });
        }
    },
    setMemberPoint: function (typename, point, callback) {
        if (typename == null || point == null)
            return;
        var me = this;
        var memberPoint = me.getMemberPoint();
        memberPoint.down('numberfield[name=' + typename + 'Point]').setValue(point.Point);
        memberPoint.down('checkbox[name=' + typename + 'IsAvailable]').setValue(point.IsAvailable);
        if (typename == 'Share') {
            memberPoint.down('textfield[name=Icon]').setValue(point.Icon);
            memberPoint.down('textfield[name=Title]').setValue(point.Title);
            memberPoint.down('textfield[name=SubTitle]').setValue(point.SubTitle);
            memberPoint.down('box[name=IconShow]').getEl().dom.src = point.Icon;
        }
        var limit = memberPoint.down('numberfield[name=' + typename + 'Limit]');
        if (limit != null) {
            limit.setValue(point.Limit);
        }
        if (point.AdditionalRules != null && point.AdditionalRules.length > 0) {
            for (var j = 0; j < point.AdditionalRules.length; j++) {
                if (callback != null) {
                    callback(point.AdditionalRules[j].Count, point.AdditionalRules[j].Point);
                }
            }
        }
    },
    afterrender: function (container, eOpts) {
        var me = this;
        var store = Ext.create('WX.store.BaseData.MemberPointStore');
        store.getMemberPoints(function (response, opts) {
            var data = Ext.decode(response.responseText);
            if (data.IsSuccessful && data.Data != null) {
                var Point = data.Data;
                for (var i = 0; i < Point.length; i++) {
                    switch (Point[i].Type) {
                        case 0:
                            me.setMemberPoint('Register', Point[i], null);
                            break;
                        case 1:
                            me.setMemberPoint('SignIn', Point[i], function (count, point) {
                                me.addAdditionalRules(null, "SignIn", "签到次数满", count, point);
                            });
                            break;
                        case 2:
                            me.setMemberPoint('Share', Point[i], function (count, point) {
                                me.addAdditionalRules(null, "Share", "分享次数满", count, point);
                            });
                            break;
                        case 3:
                            me.setMemberPoint('Appraise', Point[i], function (count, point) {
                                me.addAdditionalRules(null, "Appraise", "评价次数满", count, point);
                            });
                            break;
                    }
                }
            } else {
                Ext.MessageBox.alert('提示', data.ErrorMessage);
            }
        }, function (response, opts) {
            Ext.MessageBox.alert('提示', '服务器请求失败，错误码：' + response.status);
        });
    },
    addAppraiseAdditionalRules: function (btn) {
        var me = this;
        me.addAdditionalRules(btn, "Appraise", "评价次数满", 0, 0);
    },
    addShareAdditionalRules: function (btn) {
        var me = this;
        me.addAdditionalRules(btn, "Share", "分享次数满", 0, 0);
    },
    addSignInAdditionalRules: function (btn) {
        var me = this;
        me.addAdditionalRules(btn, "SignIn", "签到次数满", 0, 0);
    },
    addAdditionalRules: function (btn, typename, desc, count, point) {
        if (typename == null || count < 0 || point < 0)
            return;
        var me = this;
        var additionalrulesPanel = null;
        if (btn != null) {
            additionalrulesPanel = btn.up('panel[name=' + typename + 'Panel]').down('panel[name=additionalrules]');
        } else {
            additionalrulesPanel = me.getMemberPoint().down('panel[name=' + typename + 'Panel]').down('panel[name=additionalrules]');
        }
        if (additionalrulesPanel == null || additionalrulesPanel.items.length > 4) {
            return;
        }
        var items = [{
            xtype: 'panel',
            layout: 'hbox',
            name: typename + 'AdditionalItem',
            border: false,
            padding: '10 0',
            items: [{
                xtype: 'label',
                text: desc,
                padding: '3 10 3 10',
            }, {
                xtype: 'numberfield',
                name: typename + 'AdditionalCount',
                width: 100,
                allowDecimals: false,
                minValue: 0,
                allowBlank: false,
                value: count,
            }, {
                xtype: 'label',
                text: '次,额外送',
                padding: '3 10 3 10',
            }, {
                xtype: 'numberfield',
                name: typename + 'AdditionalPoint',
                width: 100,
                minValue: 0,
                allowBlank: false,
                value: point,
            }, {
                xtype: 'label',
                text: '积分',
                padding: '3 30 3 10',
            }, {
                xtype: 'button',
                text: '删除',
                width: 50,
                action: 'AdditionalRemove',
            }]
        }];
        additionalrulesPanel.add(items);
    },
    removeAdditionalRule: function (btn) {
        var additionalrulesPanel = btn.up('panel[name=additionalrules]');
        additionalrulesPanel.remove(btn.up('panel'));
    },
    getAdditionalRules: function (typename) {
        var me = this;
        var countarray = new Array();
        var memberpoint = me.getMemberPoint();
        var additionalRules = [];
        var AdditionalRuleItems = memberpoint.down('panel[name=' + typename + 'Panel]').down('panel[name=additionalrules]').items.items;
        for (var i = 0; i < AdditionalRuleItems.length; i++) {
            if (AdditionalRuleItems[i] != null) {
                additionalRules.push({
                    Point: AdditionalRuleItems[i].down('numberfield[name=' + typename + 'AdditionalPoint]').getValue(),
                    Count: AdditionalRuleItems[i].down('numberfield[name=' + typename + 'AdditionalCount]').getValue(),
                });
                countarray.push(AdditionalRuleItems[i].down('numberfield[name=' + typename + 'AdditionalCount]').getValue());
            }
        }
        countarray = countarray.sort();
        for (var i = 0; i < countarray.length - 1; i++) {
            if (countarray[i] == countarray[i + 1]) {
                return null;
            }
        }
        return additionalRules;
    },
    getMemberPointData: function () {
        var me = this;
        var data = [];
        var memberpoint = me.getMemberPoint();
        data.push({
            Point: memberpoint.down('numberfield[name=RegisterPoint]').getValue(),
            Type: 0,
            Limit: 0,
            IsAvailable: memberpoint.down('checkbox[name=RegisterIsAvailable]').getValue(),
        });
        data.push({
            Point: memberpoint.down('numberfield[name=SignInPoint]').getValue(),
            Type: 1,
            Limit: 0,
            IsAvailable: memberpoint.down('checkbox[name=SignInIsAvailable]').getValue(),
            AdditionalRules: me.getAdditionalRules('SignIn'),
        });
        data.push({
            Point: memberpoint.down('numberfield[name=SharePoint]').getValue(),
            Type: 2,
            Limit: memberpoint.down('numberfield[name=ShareLimit]').getValue(),
            IsAvailable: memberpoint.down('checkbox[name=ShareIsAvailable]').getValue(),
            AdditionalRules: me.getAdditionalRules('Share'),
            Icon: memberpoint.down('textfield[name=Icon]').getValue(),
            Title: memberpoint.down('textfield[name=Title]').getValue(),
            SubTitle: memberpoint.down('textfield[name=SubTitle]').getValue(),
        });
        data.push({
            Point: memberpoint.down('numberfield[name=AppraisePoint]').getValue(),
            Type: 3,
            Limit: memberpoint.down('numberfield[name=AppraiseLimit]').getValue(),
            IsAvailable: memberpoint.down('checkbox[name=AppraiseIsAvailable]').getValue(),
            AdditionalRules: me.getAdditionalRules('Appraise'),
        });
        return data;
    },
    save: function (btn) {
        var form = btn.up('form[name=MemberPointForm]');
        if (!form.isValid()) {
            Ext.MessageBox.alert('提示', '请正确填写信息');
            return;
        }
        var me = this;
        var store = Ext.create('WX.store.BaseData.MemberPointStore');
        var data = me.getMemberPointData();

        for (var i = 0; i < data.length; i++) {
            if (data[i].Type != 0 && data[i].AdditionalRules == null) {
                var message = '额外奖励规则存在重复次数的设置';
                switch (data[i].Type) {
                    case 1:
                        message = '签到送积分' + message;
                        break;
                    case 2:
                        message = '分享送积分' + message;
                        break;
                    case 3:
                        message = '评价送积分' + message;
                        break;
                }
                Ext.MessageBox.alert('提示', message);
                return;
            }
        }

        store.saveMemberPoints(data, function (response, opts) {
            var result = Ext.decode(response.responseText);
            if (result.IsSuccessful) {
                Ext.MessageBox.alert('提示', '保存成功');
            } else {
                Ext.MessageBox.alert('提示', '保存失败' + result.ErrorMessage);
            }
        }, function (response, opts) {
            Ext.MessageBox.alert('提示', '服务器请求失败，错误码：' + response.status);
        });
    },
});