Ext.define("WX.model.BaseData.OrderItemModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "ID" },
        { name: "ProductName" },
        { name: "ImgUrl" },
        { name: "ProductType" },
        { name: "PriceSale" },
        { name: "Quantity" },
        { name: "Money" },
    ]
});