﻿Ext.define('WX.view.Index', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.Index',
    title: '平台首页',
    layout: 'hbox',
    initComponent: function () {
        var me = this;
        //me.items = [{
        //    xtype: 'panel',
        //    title: '会员数',
        //    titleAlign: 'center',
        //    width: 300,
        //    height: 220,
        //    padding: '50',
        //    border: true,
        //    items: [{
        //        xtype: 'displayfield',
        //        padding: '29 0 24 0',
        //        name: 'MemberCount',
        //        fieldStyle: 'text-align:center',
        //        width: 198
        //        //}, {
        //        //    xtype: 'displayfield',
        //        //    fieldLabel: '储值卡会员',
        //        //    padding: '0 10 0 0',
        //        //    name: 'RechargeMemberCount',
        //        //    width: 198
        //        //}, {
        //        //    xtype: 'displayfield',
        //        //    fieldLabel: '折扣卡会员',
        //        //    padding: '0 10 0 0',
        //        //    name: 'DiscountMemberCount',
        //        //    width: 198
        //    }]
        //}, {
        //    xtype: 'panel',
        //    title: '资金池',
        //    titleAlign: 'center',
        //    width: 300,
        //    height: 220,
        //    padding: '50',
        //    border: true,
        //    items: [{
        //        xtype: 'displayfield',
        //        padding: '29 0 24 0',
        //        name: 'Fund',
        //        fieldStyle: 'text-align:center',
        //        width: 198
        //    }]
        //}, {
        //    xtype: 'panel',
        //    title: '累计储值',
        //    titleAlign: 'center',
        //    width: 300,
        //    height: 220,
        //    padding: '50',
        //    border: true,
        //    defaults: {
        //        labelAlign: 'right',
        //        labelWidth: 70,
        //        hideEmptyLabel: false,
        //    },
        //    items: [{
        //        xtype: 'displayfield',
        //        padding: '0 0 0 0',
        //        name: 'RechargeAndGive',
        //        width: 198
        //    }, {
        //        xtype: 'displayfield',
        //        fieldLabel: '储值',
        //        padding: '0 10 0 0',
        //        name: 'Recharge',
        //        width: 198
        //    }, {
        //        xtype: 'displayfield',
        //        fieldLabel: '赠送',
        //        padding: '0 10 0 0',
        //        name: 'Give',
        //        width: 198
        //    }]
        //}, {
        //    xtype: 'panel',
        //    title: '累计消费',
        //    border: true,
        //    titleAlign: 'center',
        //    width: 300,
        //    height: 220,
        //    padding: '50',
        //    items: [{
        //        xtype: 'displayfield',
        //        padding: '29 0 24 0',
        //        name: 'Consume',
        //        fieldStyle: 'text-align:center',
        //        width: 198
        //    }]
        //}];
        me.callParent(arguments);
    }
});