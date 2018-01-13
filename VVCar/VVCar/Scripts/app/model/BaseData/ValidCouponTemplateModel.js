Ext.define("WX.model.BaseData.ValidCouponTemplateModel", {
    extend: "Ext.data.Model",
    fields: [
        { name: "ID" },
        { name: "CouponTypeName" },
        { name: "Title" },
        { name: "Validity" },
        { name: "AproveStatusText" },
        { name: "FreeStock" },
    ]
});