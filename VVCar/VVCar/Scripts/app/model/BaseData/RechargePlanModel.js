Ext.define('WX.model.BaseData.RechargePlanModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: ['ID', 'Code', 'Name', 'PlanType', 'EffectiveDate', 'ExpiredDate', 'RechargeAmount', 'GiveAmount',
        'CreatedUser', 'CreatedDate', 'LastUpdateUser', 'LastUpdateDate', 'MatchCardType', 'MatchRechargeCard', 'MatchDiscountCard', 'MatchGiftCard',
        { name: 'IsAvailable', type: 'boolean', defaultValue: true },
        { name: "MaxRechargeCount", type: "number", defaultValue: 0 },
        { name: "VisibleAtPortal", type: "boolean" },
        { name: "VisibleAtWeChat", type: "boolean" },
        { name: "RechargePlanCouponTemplates" },
    ],
});