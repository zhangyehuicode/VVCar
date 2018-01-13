/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.controller.MemberTrade', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.MemberCardStore', 'WX.store.BaseData.TradeHistoryStore', 'WX.store.BaseData.RechargePlanStore', "WX.store.DataDict.BusinessTypeStore"],
    stores: ['DataDict.MemberCardStatusStore', 'DataDict.TradeSourceStore', 'DataDict.ConsumeTypeStore'],
    models: ['BaseData.MemberModel', 'BaseData.CardInfoModel'],
    views: ['MemberTrade.MemberTrade', 'MemberTrade.TradeHistory', 'MemberTrade.VerifyPassword'],
    refs: [{
        ref: 'formMemberTrade',
        selector: 'MemberTrade form[name=formMemberTrade]'
    }, {
        ref: 'txtCardNumber',
        selector: 'MemberTrade textfield[name=CardNumber]'
    }, {
        ref: 'txtTradeAmount',
        selector: 'MemberTrade textfield[name=TradeAmount]'
    }, {
        ref: 'gridHistory',
        selector: 'MemberTrade grid[name=gridHistory]'
    }, {
        ref: 'tradeHistory',
        selector: 'TradeHistory'
    }, {
        ref: 'formSearch',
        selector: 'TradeHistory form[name=formSearch]'
    }, {
        ref: 'winVerifyPassword',
        selector: 'VerifyPassword'
    }, {
        ref: 'txtMemberPwd',
        selector: 'VerifyPassword textfield[name=MemberPwd]'
    }],
    init: function () {
        var me = this;
        me.control({
            'MemberTrade': {
                afterrender: me.onMemberTradeAfterRender,
            },
            'MemberTrade textfield[name=CardNumber]': {
                specialkey: me.onCardNumberSpecialkey,
                blur: me.onCardNumberBlur
            },
            'MemberTrade button[action=confirm]': {
                click: me.verifyPassword
            },
            'MemberTrade button[action=history]': {
                click: me.goTradeHistory
            },
            'TradeHistory': {
                afterrender: me.onTradeHistoryAfterRender
            },
            'TradeHistory button[action=search]': {
                click: me.searchData
            },
            'TradeHistory button[action=export]': {
                click: me.exportTradeHistory
            },
            'VerifyPassword button[action=confirm]': {
                click: me.confirmTrade
            }
        });
    },
    onMemberTradeAfterRender: function () {
        this.getTxtCardNumber().focus(true, 100);
    },
    onCardNumberSpecialkey: function (field, e, eOpts) {
        if (e.getKey() !== Ext.EventObject.ENTER) {
            return;
        }
        e.stopPropagation();
        e.stopEvent();
        this.getTxtTradeAmount().focus();
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
                    Ext.MessageBox.alert("提示", "获取卡信息失败, 找不到对应的卡。", function () {
                        me.resetForm();
                        field.setValue('');
                        field.focus(true);
                    });
                    return;
                }
                var form = me.getFormMemberTrade();
                var cardInfo = Ext.create('WX.model.BaseData.CardInfoModel', result.Data);
                form.loadRecord(cardInfo);
                me.getTxtTradeAmount().focus();
            } else {
                e.stopPropagation();
                e.stopEvent();
                Ext.MessageBox.alert("提示", "获取卡信息失败, " + result.ErrorMessage);
            }
        });
    },
    resetForm: function () {
        var me = this;
        var form = me.getFormMemberTrade().getForm();
        var cardInfo = Ext.create('WX.model.BaseData.CardInfoModel');
        form.loadRecord(cardInfo);
        form.reset(true);
        me.getGridHistory().hide();
    },
    verifyPassword: function (btn, e) {
        var me = this;
        var form = me.getFormMemberTrade().getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
            var tradeAmount = parseFloat(formValues.TradeAmount);
            if (tradeAmount <= 0) {
                Ext.MessageBox.alert("提示", "交易失败，消费金额错误", function () {
                    me.getTxtTradeAmount().focus(true);
                });
                return;
            }
            var cardBalance = parseFloat(formValues.CardBalance);
            if (cardBalance < tradeAmount) {
                Ext.MessageBox.alert("提示", "交易失败，余额不足", function () {
                    me.getTxtTradeAmount().focus(true);
                });
                return;
            }
            var win = Ext.widget("VerifyPassword");
            win.show();
            this.getTxtMemberPwd().focus(true, 100);
        }
    },
    confirmTrade: function (btn, e) {
        var me = this;
        var memberPwd = this.getTxtMemberPwd().getValue();
        if (memberPwd == null || memberPwd.length == 0) {
            Ext.MessageBox.alert("提示", "请输入密码", function () {
                me.getTxtMemberPwd().focus();
            });
            return;
        }
        me.getWinVerifyPassword().close();
        var form = me.getFormMemberTrade().getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
            var tradeAmount = parseFloat(formValues.TradeAmount);
            if (tradeAmount <= 0) {
                Ext.MessageBox.alert("提示", "交易失败，消费金额错误", function () {
                    me.getTxtTradeAmount().focus(true);
                });
                return;
            }
            var cardBalance = parseFloat(formValues.CardBalance);
            if (cardBalance < tradeAmount) {
                Ext.MessageBox.alert("提示", "交易失败，余额不足", function () {
                    me.getTxtTradeAmount().focus(true);
                });
                return;
            }
            var consumeInfo = {
                CardNumber: formValues.CardNumber,
                TradeAmount: tradeAmount,
                UseBalanceAmount: tradeAmount,
                MemberPassword: memberPwd,
            };
            var cardStore = Ext.create('WX.store.BaseData.MemberCardStore');
            cardStore.consume(consumeInfo, function (response, opts) {
                var result = Ext.decode(response.responseText);
                if (result.IsSuccessful) {
                    if (result.Data != null) {
                        Ext.MessageBox.alert("提示", "消费成功", function () {
                            form.setValues({ CardBalance: result.Data.AfterBalance });
                            me.refreshTradeHistory();
                            //me.getTxtCardNumber().focus();
                        });
                    } else {
                        Ext.MessageBox.alert("提示", "消费失败, 请重试");
                    }
                } else {
                    Ext.MessageBox.alert("提示", "消费失败, " + result.ErrorMessage);
                }
            });
        }
    },
    refreshTradeHistory: function () {
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
    goTradeHistory: function (btn, e) {
        this.getGridHistory().show();
        this.refreshTradeHistory();
    },
    searchData: function (btn) {
        var myStore = this.getTradeHistory().getStore();
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
    onTradeHistoryAfterRender: function () {
        this.searchData();
    },
    exportTradeHistory: function (btn) {
        Ext.MessageBox.show({
            msg: '正在生成数据……, 请稍侯',
            progressText: '正在生成数据……',
            width: 300,
            wait: true
        });
        var myStore = this.getTradeHistory().getStore();
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
