Ext.define("WX.model.BaseData.MemberGradeModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "ID" },
        { name: "Name" },
        { name: "IsDefault" },
        { name: "Level" },
        {
            name: "IsNeverExpiresRadioValue", convert: function (v, record) {
                return record.data.IsNeverExpires === true ? 'true' : 'false';
            }
        },
        { name: "IsNeverExpires" },
        { name: "ExpireAfterJoinDays" },
        { name: "IsQualifyByConsume" },
        {
            name: "IsQualifyByConsumeTotalAmount", defaultValue: false, convert: function (v, record) {
                return record.data.QualifyByConsumeTotalAmount ? true : false;
            }
        },
        { name: "QualifyByConsumeTotalAmount" },
        {
            name: "IsQualifyByConsumeOneOffAmount", defaultValue: false, convert: function (v, record) {
                return record.data.QualifyByConsumeOneOffAmount ? true : false;
            }
        },
        { name: "QualifyByConsumeOneOffAmount" },
        {
            name: "IsQualifyByConsumeLimitedMonths", defaultValue: false, convert: function (v, record) {
                return record.data.QualifyByConsumeLimitedMonths ? true : false;
            }
        },
        { name: "QualifyByConsumeLimitedMonths" },
        { name: "QualifyByConsumeTotalCount" },
        { name: "IsQualifyByRecharge" },
        {
            name: "IsQualifyByRechargeTotalAmount", defaultValue: false, convert: function (v, record) {
                return record.data.QualifyByRechargeTotalAmount ? true : false;
            }
        },
        { name: "QualifyByRechargeTotalAmount" },
        {
            name: "IsQualifyByRechargeOneOffAmount", defaultValue: false, convert: function (v, record) {
                return record.data.QualifyByRechargeOneOffAmount ? true : false;
            }
        },
        { name: "QualifyByRechargeOneOffAmount" },
        { name: "IsQualifyByPurchase" },
        { name: "QualifyByPurchaseAmount" },
        { name: "IsAllowDiffPurchaseAmount" },
        { name: "GradePoint" },
        {
            name: "IsGiftPointByConsumeAmount", defaultValue: false, convert: function (v, record) {
                return record.data.GiftPointByConsumeAmount ? true : false;
            }
        },
        { name: "GiftPointByConsumeAmount" },
        { name: "ConsumeGiftPoint" },
        {
            name: "IsGiftPointByRechargeAmount", defaultValue: false, convert: function (v, record) {
                return record.data.GiftPointByRechargeAmount ? true : false;
            }
        },
        { name: "GiftPointByRechargeAmount" },
        { name: "RechargeGiftPoint" },
        {
            name: "IsYunPosIntegrationRadioValue", convert: function (v, record) {
                return record.data.IsYunPosIntegration === true ? 'true' : 'false';
            }
        },
        { name: "IsYunPosIntegration" },
        { name: "DiscountRate" },
        {
            name: "IsAllowPointPaymentRadioValue", convert: function (v, record) {
                return record.data.IsAllowPointPayment === true ? 'true' : 'false';
            }
        },
        { name: "IsAllowPointPayment" },
        { name: "PonitExchangeValue" },
        { name: "Status" },
        { name: "Remark" },
        { name: "CreatedDate" },
        { name: "GradeRights" },
        { name: "IsNotOpen" },
    ]
});