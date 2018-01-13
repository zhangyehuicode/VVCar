/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.MemberCardActivate', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.MemberCardStore', 'WX.store.DataDict.MemberCardStatusStore', 'WX.store.BaseData.MemberCardTypeStore'],
    models: ['BaseData.MemberCardModel'],
    views: ["MemberCardActivate.MemberCardActivate"],
    refs: [{
        ref: 'memberCardActivate',
        selector: 'MemberCardActivate'
    }, {
        ref: 'btnToStepTwo',
        selector: 'MemberCardActivate button[action=toStepTwo]'
    }],
    init: function () {
        var me = this;
        me.control({
            "MemberCardActivate button[action=toStepOne]": {
                click: me.toStepOne
            },
            "MemberCardActivate button[action=toStepTwo]": {
                click: me.toStepTwo
            },
            "MemberCardActivate button[action=toStepThree]": {
                click: me.toStepThree
            },
            "MemberCardActivate textfield[name=Code]": {
                change: me.codeInputChanged
            },
            "MemberCardActivate button[action=signalactivate]": {
                click: me.signalActivateTab
            },
            "MemberCardActivate button[action=batchactivate]": {
                click: me.batchActivateTab
            },
            "MemberCardActivate button[action=batchactivatesearch]": {
                click: me.batchActivateSearch
            },
            "MemberCardActivate button[action=batchactivateaction]": {
                click: me.batchActivateAction
            }
        });
    },
    batchActivateAction: function (btn) {
        var form = btn.up('form[name=batchactivatevalueform]').getForm();
        if (!form.isValid())
            return;
        var me = this;
        var grid = btn.up('form[name=batchactivateform]').down('grid');
        var selectedModels = grid.getSelection();
        if (selectedModels.length < 1) {
            Ext.Msg.alert("提示", "请选择需要激活的卡片");
            return;
        }
        var cardIds = [];
        var expiredDate = null;
        var cardBalance = 0;
        var remark = "";
        for (var i = 0; i < selectedModels.length; i++) {
            cardIds.push(selectedModels[i].get('ID'));
        }
        expiredDate = me.getMemberCardActivate().down('datefield[name=ExpiredDate]').getValue();
        cardBalance = me.getMemberCardActivate().down('numberfield[name=CardBalance]').getValue();
        remark = me.getMemberCardActivate().down('textfield[name=BatchRemark]').getValue();
        var store = grid.getStore();
        function success(response) {
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert('提示', response.ErrorMessage);
                return;
            }
            Ext.Msg.alert('提示', '批量激活成功！');
            store.load();
            var expiredDate = btn.up('form[name=batchactivatevalueform]').down('datefield[name=ExpiredDate]').getValue();
            var batchRemark = btn.up('form[name=batchactivatevalueform]').down('textfield[name=BatchRemark]').getValue();
            btn.up('form[name=batchactivateform]').reset();
            btn.up('form[name=batchactivatevalueform]').down('datefield[name=ExpiredDate]').setValue(expiredDate);
            btn.up('form[name=batchactivatevalueform]').down('textfield[name=BatchRemark]').setValue(batchRemark);
        };
        function failure(response) {
            Ext.Msg.alert('提示', response.responseText);
        };
        store.giftCardBatchActivate({ GiftCardIds: cardIds, ExpiredDate: expiredDate, CardBalance: cardBalance, Remark: remark }, success, failure);
    },
    batchActivateSearch: function (btn) {
        var form = btn.up('form').getForm();
        var values = form.getValues();
        var grid = btn.up('form').down('grid');
        var store = grid.getStore();
        var batchcoderadio = btn.up('form').down('radio[id=batchactivateradio]');
        var coderangeradio = btn.up('form').down('radio[id=coderangeradio]');
        if (!batchcoderadio.getValue()) {
            values.BatchCode = null;
        }
        if (!coderangeradio.getValue()) {
            values.StartCode = null;
            values.EndCode = null;
        }
        Ext.apply(store.proxy.extraParams, values);
        store.load();
    },
    signalActivateTab: function (btn) {
        var memberCardActivate = this.getMemberCardActivate();
        memberCardActivate.down('container[name=signalactivatecon]').setVisible(true);
        memberCardActivate.down('container[name=batchactivatecon]').setVisible(false);
    },
    batchActivateTab: function (btn) {
        if (!Ext.validatePermission('Member.Member.BatchActivateCard')) {
            Ext.Msg.alert('拒绝', '没有权限');
            return;
        }
        var memberCardActivate = this.getMemberCardActivate();
        memberCardActivate.down('container[name=batchactivatecon]').setVisible(true);
        memberCardActivate.down('container[name=signalactivatecon]').setVisible(false);
    },
    toStepOne: function (btn) {
        Ext.getCmp("rmcaStepOne").setValue(true);
        Ext.getCmp("rmcaStepOne").getEl().el.dom.style.color = "#FFAB42";
        Ext.getCmp("rmcaStepThree").getEl().el.dom.style.color = "";

        Ext.getCmp("mcaFormStepThree").hide();
        Ext.getCmp("mcaFormStepOne").show();
        Ext.getCmp("mcaFormStepOne").form.reset();
    },
    toStepTwo: function (btn) {
        function verifySuccess() {
            Ext.getCmp("rmcaStepOne").getEl().el.dom.style.color = "";
            Ext.getCmp("rmcaStepTwo").setValue(true);
            Ext.getCmp("rmcaStepTwo").getEl().el.dom.style.color = "#FFAB42";

            Ext.getCmp("mcaFormStepOne").hide();
            Ext.getCmp("mcaFormStepTwo").form.reset();
            Ext.getCmp("mcaFormStepTwo").show();
        }
        this.verifyCode(btn, verifySuccess);
    },
    toStepThree: function (btn) {
        if (!btn.up("form").isValid())
            return;
        var formStepOne = Ext.getCmp("mcaFormStepOne");
        var info = formStepOne.getValues();
        //info.ExpiredDate = formStepOne.down('datefield[name=ExpiredDate]').getValue();
        var passwordInfo = btn.up("form").getValues();
        if (passwordInfo.Password !== passwordInfo.RepeatPassword) {
            Ext.Msg.alert("提示", "两次密码不一致，请重新输入。");
            return;
        }
        info.Password = passwordInfo.Password;
        var store = this.getMemberCardActivate().store;

        function success(response) {
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert("提示", response.ErrorMessage);
                return;
            }

            Ext.getCmp("rmcaStepTwo").getEl().el.dom.style.color = "";
            Ext.getCmp("rmcaStepThree").setValue(true);
            Ext.getCmp("rmcaStepThree").getEl().el.dom.style.color = "#FFAB42";

            Ext.getCmp("mcaFormStepTwo").hide();
            Ext.getCmp("mcaFormStepThree").show();
        }
        store.activate(info, success, this.failure);
    },
    verifyCode: function (btn, cb) {
        var store = this.getMemberCardActivate().store;
        if (!btn.up("form").isValid())
            return;
        var entity = btn.up("form").getValues();

        function success(response) {
            response = JSON.parse(response.responseText);
            if (response.IsSuccessful) {
                if (response.Data)
                    cb();
                else {
                    Ext.Msg.alert("提示", "卡号校验失败");
                }
            } else {
                Ext.Msg.alert("提示", response.ErrorMessage);
            }
        }
        store.verifyCode(entity, success, this.failure);
    },
    failure: function (response) {
        Ext.Msg.alert("提示", response.responseText);
    },
    codeInputChanged: function (txtField, newValue, oldValue, eOpts) {
        var me = this;
        if (newValue.length === 8) {
            var store = this.getMemberCardActivate().store;
            me.getBtnToStepTwo().enable();
            store.proxy.extraParams = { Code: newValue };
            store.load(function (records, operation, success) {
                if (success) {

                    if (records[0].data != null) {
                        var memberCardTypeStore = Ext.create('WX.store.BaseData.MemberCardTypeStore');
                        memberCardTypeStore.load(function (res, operation, success) {
                            if (success) {
                                var res = memberCardTypeStore.findRecord("ID", records[0].data.CardTypeID);
                                if (res != null)
                                    txtField.up("form").down("textfield[name=CardType]").setValue(res.data.Name);
                            }
                        });
                    }

                    var memberGroupStore = Ext.create('WX.store.BaseData.MemberGroupStore');
                    memberGroupStore.load(function (grouprecords, groupoperation, groupsuccess) {
                        if (groupsuccess) {
                            if (records != null && records.length > 0 && grouprecords != null && grouprecords.length > 0) {
                                var membergroup = memberGroupStore.findRecord('ID', records[0].data.MemberGroupID);
                                if (membergroup != null) {
                                    txtField.up("form").down("textfield[name=MemberGroupName]").setValue(membergroup.data.Name);
                                } else {
                                    txtField.up("form").down("textfield[name=MemberGroupName]").setValue("普通会员");
                                }
                            }
                        }
                    });
                    txtField.up("form").down("textfield[name=MemberGroupID]").setValue(records[0].data.MemberGroupID);
                }
            });

        }
    }
});
