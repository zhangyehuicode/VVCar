Ext.define("WX.model.BaseData.MemberCardThemeGroupModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "Index" },
        { name: "Name" },
        { name: "CategoryName" },
        { name: "Code" },
        { name: "DepartmentCode" },
        { name: "CardThemeCategoryID" },
        { name: "TimeSlot", type: 'string' },
        { name: "GiftCardStartTime", type: "date" },
        { name: "GiftCardEndTime", type: "date" },
        { name: "week", type: 'string', defaultValue: "" },
        { name: "RecommendGroupID" },
        { name: "RuleDescription", type: 'string' },
        { name: "ImgUrlDto" },
        { name: "IsAvailableShow", type: 'string' },
        { name: "IsAvailable", type: 'boolean' },
        { name: "EffectiveDaysOfAfterBuy" },
        { name: "EffectiveDays" },
        { name: "IsNotFixationDate" },
    ]
});