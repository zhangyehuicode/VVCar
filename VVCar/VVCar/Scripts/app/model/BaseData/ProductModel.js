Ext.define("WX.model.BaseData.ProductModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "Index" },
        { name: "Title" },
        { name: "ImgUrl" },
        { name: "Points" },
        { name: "UpperLimit" },
        { name: "IsPublish" },
        { name: "IsRecommend" },
        { name: "Stock" },
        { name: "CreatedUser" },
        { name: "EffectiveDate" },
        { name: "ExpiredDate" },
        { name: "CreatedDate" },
        { name: "BasePrice" },
        { name: "PriceSale" },
        //{ name: "ProductType" },
    ]
});