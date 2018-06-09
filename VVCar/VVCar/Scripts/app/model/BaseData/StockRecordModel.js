Ext.define("WX.model.BaseData.StockRecordModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "ProductCode" },
        { name: "ProductName" },
        { name: "ProductCategoryName" },
        { name: "Quantity" },
        { name: "Reason" },
        { name: "StaffName" },
        { name: "CreatedDate" },
        { name: "StockRecordType" },
        { name: "Source" },
    ]
});