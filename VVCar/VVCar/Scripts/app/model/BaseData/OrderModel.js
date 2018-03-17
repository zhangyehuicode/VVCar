Ext.define("WX.model.BaseData.OrderModel", {
    extend: "Ext.data.Model",
    idProperty: "ID",
    fields: [
        { name: "Index" },
        { name: "TradeNo" },
        { name: "LinkMan" },
        { name: "Phone" },
        { name: "Address" },
        { name: "ExpressNumber" },
        { name: "Status" },
        { name: "Points" },
        { name: "CreatedDate" },
        { name: "Remark" },
    ]
});