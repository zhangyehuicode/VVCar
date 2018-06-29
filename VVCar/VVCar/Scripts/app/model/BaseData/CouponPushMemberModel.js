Ext.define("WX.model.BaseData.CouponPushMemberModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "ID" },
        { name: "Name" },
        { name: "MobilePhoneNo" },
        { name: "PlateList" },
    ]
});