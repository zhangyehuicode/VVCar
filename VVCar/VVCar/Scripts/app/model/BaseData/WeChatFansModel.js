Ext.define("WX.model.BaseData.WeChatFansModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "ID" },
        { name: "OpenID" },
        { name: "NickName" },
        { name: "Sex" },
        { name: "HeadImgUrl" },
        { name: "SubscribeTime" },
        { name: "Country" },
        { name: "Province" },
        { name: "City" },
        { name: "Remark" },
        { name: "Tags" },
        { name: "Region" },
    ]
});