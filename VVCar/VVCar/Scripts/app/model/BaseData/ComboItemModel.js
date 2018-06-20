Ext.define("WX.model.BaseData.ComboItemModel", {
	extend: "Ext.data.Model",
	idProperty: "ID",
	fields: [
		{ name: "ID" },
		{ name: "ProductID" },
		{ name: "ProductName" },
		{ name: "ProductCode" },
		{ name: "BasePrice" },
		{ name: "PriceSale" },
		{ name: "Quantity" },
		{ name: "CreatedDate" },
	]
});