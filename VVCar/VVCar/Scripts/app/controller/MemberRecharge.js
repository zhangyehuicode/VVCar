/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.MemberRecharge', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.MemberCardStore', 'WX.store.BaseData.RechargeHistoryStore', 'WX.store.BaseData.RechargePlanStore', 'WX.store.BaseData.DepartmentStore', 'WX.store.DataDict.BusinessTypeStore'],
    stores: ['DataDict.MemberCardStatusStore', 'DataDict.PaymentTypeStore', 'DataDict.TradeSourceStore'],
    models: ['BaseData.MemberModel'],
    views: ['MemberRecharge.MemberRecharge', 'MemberRecharge.RechargeHistory'],
    refs: [{
        ref: 'formMemberRecharge',
        selector: 'MemberRecharge form[name=formMemberRecharge]'
    }, {
        ref: 'txtCardNumber',
        selector: 'MemberRecharge textfield[name=CardNumber]'
    }, {
        ref: 'cmbRechargePlan',
        selector: 'MemberRecharge combobox[name=RechargePlanID]'
    }, {
        ref: 'txtRechargeAmount',
        selector: 'MemberRecharge textfield[name=RechargeAmount]'
    }, {
        ref: 'txtGiveAmount',
        selector: 'MemberRecharge textfield[name=GiveAmount]'
    }, {
        ref: 'gridHistory',
        selector: 'MemberRecharge grid[name=gridHistory]'
    }, {
        ref: 'rechargeHistory',
        selector: 'RechargeHistory'
    }, {
        ref: 'formSearch',
        selector: 'RechargeHistory form[name=formSearch]'
    }],
    init: function () {
        var me = this;
        me.control({
            'MemberRecharge': {
                afterrender: me.onMemberRechargeAfterRender,
            },
            'MemberRecharge textfield[name=CardNumber]': {
                specialkey: me.onCardNumberSpecialkey,
                blur: me.onCardNumberBlur
            },
            'MemberRecharge combobox[name=RechargePlanID]': {
                select: me.onRechargePlanSelect
            },
            'MemberRecharge button[action=confirm]': {
                click: me.confirmRecharge
            },
            'MemberRecharge button[action=history]': {
                click: me.goRechargeHistory
            },
            "RechargeHistory": {
                beforeedit: me.onRechargeHistoryBeforeEdit,
                edit: me.onRechargeHistoryEdit,
                afterrender: me.onRechargeHistoryAfterRender
            },
            'RechargeHistory button[action=search]': {
                click: me.searchData
            },
            'RechargeHistory button[action=export]': {
                click: me.exportRechargeHistory
            }
        });
    },
    onMemberRechargeAfterRender: function () {
        var me = this;
        this.getTxtCardNumber().focus(true, 100);
        //var rechargePlanStore = me.getCmbRechargePlan().store;
        //rechargePlanStore.getUsablePlans(function (response, opts) {
        //    var result = Ext.decode(response.responseText);
        //    if (result.IsSuccessful) {
        //        if (result.Data == null) {
        //            Ext.MessageBox.alert("提示", "没有可用的储值方案");
        //            return;
        //        }
        //        rechargePlanStore.add(result.Data);
        //        rechargePlanStore.commitChanges();
        //    } else {
        //        Ext.MessageBox.alert("提示", "获取储值方案失败, " + result.ErrorMessage);
        //    }
        //});
    },
    onCardNumberSpecialkey: function (field, e, eOpts) {
        if (e.getKey() != Ext.EventObject.ENTER) {
            return;
        }
        e.stopPropagation();
        e.stopEvent();
        this.getCmbRechargePlan().focus();
    },
    onCardNumberBlur: function (field, e, eOpts) {
        var me = this;
        var cardNumber = field.getValue();
        if (cardNumber.length == 0) {
            return;
        }
        var cardStore = Ext.create('WX.store.BaseData.MemberCardStore');
        cardStore.getCardByNumber(cardNumber, function (response, opts) {
            var result = Ext.decode(response.responseText);
            if (result.IsSuccessful) {
                if (result.Data == null) {
                    Ext.MessageBox.alert("提示", "获取卡信息失败, 找不到对应的卡", function () {
                        me.resetForm();
                        field.setValue('');
                        field.focus(true);
                    });
                    return;
                }
                var form = me.getFormMemberRecharge();
                var cardInfo = Ext.create('WX.model.BaseData.CardInfoModel', result.Data);
                form.loadRecord(cardInfo);
                me.getCmbRechargePlan().focus();

                if (cardInfo.data.CardTypeID == "00000000-0000-0000-0000-000000000003") {
                    if (!form.down('combobox[name=RechargePlanID]').disabled && !form.down('button[action=confirm]').disabled) {
                        form.down('combobox[name=RechargePlanID]').setDisabled(true);
                        form.down('button[action=confirm]').setDisabled(true);
                        //Ext.Msg.alert('提示', '礼品卡不允许储值');
                    }
                    return;
                }
                form.down('combobox[name=RechargePlanID]').setDisabled(false);
                form.down('button[action=confirm]').setDisabled(false);

                var rechargePlanStore = me.getCmbRechargePlan().store;
                rechargePlanStore.getUsablePlans(function (response, opts) {
                    var result = Ext.decode(response.responseText);
                    if (result.IsSuccessful) {
                        if (result.Data == null) {
                            Ext.MessageBox.alert("提示", "没有可用的储值方案");
                            return;
                        }
                        //rechargePlanStore.clearData();
                        //rechargePlanStore.add(result.Data);
                        rechargePlanStore.setData(result.Data);
                        rechargePlanStore.commitChanges();
                    } else {
                        Ext.MessageBox.alert("提示", "获取储值方案失败, " + result.ErrorMessage);
                    }
                }, cardInfo.data.CardTypeID);

            } else {
                e.stopPropagation();
                e.stopEvent();
                Ext.MessageBox.alert("提示", "获取卡信息失败, " + result.ErrorMessage);
            }
        });
    },
    onRechargePlanSelect: function (combo, record, eOpts) {
        this.getTxtRechargeAmount().setValue(record.data.RechargeAmount);
        this.getTxtGiveAmount().setValue(record.data.GiveAmount);
    },
    resetForm: function () {
        var me = this;
        var form = me.getFormMemberRecharge().getForm();
        var cardInfo = Ext.create('WX.model.BaseData.CardInfoModel');
        form.loadRecord(cardInfo);
        form.reset(true);
        me.getGridHistory().hide();
    },
    confirmRecharge: function (btn, e) {
        var me = this;

        var cardTypeID = me.getFormMemberRecharge().down('textfield[name=CardTypeID]').getValue();
        if (cardTypeID == "00000000-0000-0000-0000-000000000003") {
            Ext.Msg.alert('提示', '礼品卡不允许储值');
            return;
        }
        var form = me.getFormMemberRecharge().getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
            var rechargeAmount = parseFloat(formValues.RechargeAmount);
            if (rechargeAmount <= 0) {
                Ext.MessageBox.alert("提示", "储值失败，储值金额错误", function () {
                    me.getTxtRechargeAmount().focus(true);
                });
                return;
            }
            var giveAmount = parseFloat(formValues.GiveAmount);
            if (giveAmount < 0) {
                Ext.MessageBox.alert("提示", "储值失败，赠送金额错误", function () {
                    me.getTxtGiveAmount().focus(true);
                });
                return;
            }
            Ext.MessageBox.confirm('询问', '是否确认储值?', function (opt) {
                if (opt == 'yes') {
                    var rechargeInfo = {
                        CardID: formValues.CardID,
                        RechargePlanID: formValues.RechargePlanID,
                        RechargeAmount: rechargeAmount,
                        GiveAmount: giveAmount,
                        PaymentType: formValues.PaymentType,
                    };
                    var cardStore = Ext.create('WX.store.BaseData.MemberCardStore');
                    cardStore.recharge(rechargeInfo, function (response, opts) {
                        var result = Ext.decode(response.responseText);
                        if (result.IsSuccessful) {
                            if (result.Data != null) {
                                Ext.MessageBox.alert("提示", "储值成功", function () {
                                    form.setValues({ CardBalance: result.Data.AfterBalance });
                                    me.refreshRechargeHistory();
                                    //me.getTxtCardNumber().focus();
                                });
                            } else {
                                Ext.MessageBox.alert("提示", "储值失败, 请重试");
                            }
                        } else {
                            Ext.MessageBox.alert("提示", "储值失败, " + result.ErrorMessage);
                        }
                    });
                }
            });
        }
    },
    refreshRechargeHistory: function () {
        var me = this;
        var gridHistory = me.getGridHistory();
        if (gridHistory.isVisible() === false)
            return;
        var cardNumber = me.getTxtCardNumber().getValue();
        if (cardNumber == null || cardNumber.length < 1) {
            Ext.MessageBox.alert("提示", "请输入卡号", function () {
                me.getTxtCardNumber().focus();
            });
            return;
        }
        gridHistory.store.load({ params: { CardNumber: cardNumber } });
    },
    goRechargeHistory: function (btn, e) {
        this.getGridHistory().show();
        this.refreshRechargeHistory();
    },
    searchData: function (btn) {
        var myStore = this.getRechargeHistory().getStore();
        var queryValues = this.getFormSearch().getValues();
        if (queryValues != null) {
            queryValues.All = true;
            Ext.apply(myStore.proxy.extraParams, queryValues);
            myStore.currentPage = 1;
            myStore.load();
        } else {
            Ext.MessageBox.alert("系统提示", "请输入过滤条件！");
        }
    },
    onRechargeHistoryAfterRender: function () {
        this.searchData();
    },
    onRechargeHistoryBeforeEdit: function (editor, context, eOpts) {
        if (editor.editing == true)
            return false;
        if (!Ext.validatePermission('Portal.TradeHistory.InvoiceEdit')) {
            Ext.Msg.alert('拒绝', '没有权限');
            return false;
        }
    },
    onRechargeHistoryEdit: function (editPlugin, view) {
        var rowData = view.record.data;
        var store = view.grid.store;
        Ext.MessageBox.show({
            msg: '正在请求数据, 请稍侯',
            progressText: '正在请求数据',
            width: 300,
            wait: true,
            waitConfig: { interval: 200 }
        });
        store.drawReceipt(rowData, function (request, success, response) {
            Ext.MessageBox.hide();
            if (response.timedout) {
                Ext.Msg.alert('提示', '操作超时');
                store.load();
                return;
            }
            var result = JSON.parse(response.responseText);
            if (success) {
                if (result.IsSuccessful) {
                    Ext.Msg.alert('提示', '操作成功');
                    store.load();
                } else {
                    Ext.Msg.alert('提示', result.ErrorMessage);
                }
            } else {
                Ext.Msg.alert('提示', result.Message);
            }
        });
    },
    exportRechargeHistory: function (btn) {
        Ext.MessageBox.show({
            msg: '正在生成数据……, 请稍侯',
            progressText: '正在生成数据……',
            width: 300,
            wait: true
        });
        var myStore = this.getRechargeHistory().getStore();
        var queryValues = btn.up('form').getValues();
        myStore.exportTradeHistory(queryValues, function (req, success, res) {
            Ext.MessageBox.hide();
            if (res.status === 200) {
                var response = JSON.parse(res.responseText);
                if (response.IsSuccessful) {
                    window.location.href = response.Data;
                } else {
                    Ext.Msg.alert("提示", response.ErrorMessage);
                }
            } else {
                Ext.Msg.alert("提示", "网络请求异常：" + res.status);
            }
        });
    }
});
