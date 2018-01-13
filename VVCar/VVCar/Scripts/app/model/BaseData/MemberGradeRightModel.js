Ext.define("WX.model.BaseData.MemberGradeRightModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "ID" },
        { name: "MemberGradeID" },
        { name: "RightType" },
        { name: "PosRightID" },
        { name: "PosRightCode" },
        { name: "PosRightName" },
    ]
});