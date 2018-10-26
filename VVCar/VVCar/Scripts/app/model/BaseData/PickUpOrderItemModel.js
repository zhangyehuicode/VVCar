Ext.define("WX.model.BaseData.PickUpOrderItemModel", {
	extend: "Ext.data.Model",
	idProperty: "ID",
	fields: [
		{ name: "ID" },
		{ name: "ProductName" },
		{ name: "ImgUrl" },
		{ name: "ProductType" },
		{ name: "PriceSale" },
		{ name: "Quantity" },
		{ name: "ReducedPrice" },
		{ name: "IsReduce" },
		{ name: "Money" },
		{ name: "Remark" },
	]
});