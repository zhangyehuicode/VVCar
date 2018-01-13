Ext.define("WX.model.BaseData.MemberCardThemeModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "Index" },
        { name: "Name" },
        { name: "ImgUrl" },
        { name: "IsDefault" },
        { name: "IsAvailable" },
        { name: "CardThemeGroupID" },
        { name: "GroupName", mapping: "CardThemeGroup.Name" },
    ]
});