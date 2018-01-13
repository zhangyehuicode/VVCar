Ext.define("WX.model.BaseData.CardInfoModel", {
    extend: "Ext.data.Model",
    idProperty: "CardID",
    fields: [
        { name: "CardID" },
        { name: "CardTypeID" },
        { name: "CardNumber" },
        { name: "CardStatus" },
        { name: "EffectiveDate" },
        { name: "ExpiredDate", type: 'date' },
        { name: "CardBalance" },
        { name: "MemberID" },
        { name: "MemberName" },
        { name: "MobilePhoneNo" },
        { name: "Birthday" }
    ]
});