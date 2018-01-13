/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.model.BaseData.TradeHistoryModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'TradeNo', 'OutTradeNo', 'CardID', 'CardNumber', 'MemberName', 'CardBalance', 'TradeAmount', 'TradeSource',
        'TradeDepartment', 'CreatedUser', 'CreatedDate', "BusinessType", "ConsumeTypeDesc", 'CardRemark', 'UseBalanceAmount', 'CardTypeDesc'
    ],
});