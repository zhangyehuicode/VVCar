Ext.define("WX.model.BaseData.CouponTemplateInfoModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "ID" },
        { name: "CouponTypeName" },
        { name: "TemplateCode" },
        { name: "CreatedDate" },
        { name: "PutInDate" },
        { name: "Title" },
        { name: "Validity" },
        { name: "FreeStock" },
        { name: "PutInStartDate", type: "date" },

    ]
});