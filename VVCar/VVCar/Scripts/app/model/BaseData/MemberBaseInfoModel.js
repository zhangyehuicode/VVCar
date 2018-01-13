Ext.define("WX.model.BaseData.MemberBaseInfoModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "Status" },
        { name: "EffectiveDate" },
        { name: "ExpiredDate" },
        { name: "TotalRecharge" },
        { name: "TotalConsume" },
        { name: "CardBalance" },
        { name: "LastRechargeMoney" }
    ]
});