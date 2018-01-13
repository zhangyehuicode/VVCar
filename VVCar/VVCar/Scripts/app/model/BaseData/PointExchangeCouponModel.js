Ext.define("WX.model.BaseData.PointExchangeCouponModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "ID" },
        { name: "Point", type: "number" },
        { name: "BeginDate", type: "date" },
        { name: "FinishDate", type: "date" },
        { name: "DateStr", },
        { name: "ExchangeCount", type: "number" },
        { name: "CreateDate", type: "date" },
        { name: "CouponTemplateId" },
        { name: "CouponTitle" },
        { name: "ExchangeType" },
        { name: "ExchangePremise" },
        { name: "PutInDate" },
        { name: "TemplateCode" },
    ]
});