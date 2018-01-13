Ext.define("WX.model.BaseData.IDCodeNameModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "ID" },
        { name: "Code" },
        { name: "Name" },
        { name: "Discount" },
    ]
});