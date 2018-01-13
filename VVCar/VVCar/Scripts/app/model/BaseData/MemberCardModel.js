Ext.define("WX.model.BaseData.MemberCardModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "Code" },
        { name: "VerifyCode" },
        { name: "BatchCode" },
        { name: "Status" },
        { name: "EffectiveDate" },//, type: "date" 
        { name: "ExpiredDate" },
        { name: "CreatedUserID" },
        { name: "CreatedUser" },
        { name: "CreatedDate", },
        { name: "LastUpdateUserID" },
        { name: "LastUpdateUser" },
        { name: "LastUpdateDate" },
        { name: "CardBalance", type: "number" },
        { name: "TotalRecharge", type: "number" },
        { name: "TotalGive", type: "number" },
        { name: "TotalConsume", type: "number" },
        { name: "IsVirtual" },
        { name: "Remark" },
        { name: "MemberGroupID" },
        { name: "CardTypeID" },
        { name: "MemberGradeID" },
    ]
});