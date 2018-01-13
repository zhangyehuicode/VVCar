Ext.define("WX.model.BaseData.MemberGroupModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "ID" },
        { name: "Name" },
        { name: "Code" },
        { name: "Index" }
    ]
});