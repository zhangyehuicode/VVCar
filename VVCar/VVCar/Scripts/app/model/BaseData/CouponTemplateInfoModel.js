Ext.define("WX.model.BaseData.CouponTemplateInfoModel", {
	extend: "Ext.data.Model",
	idProperty: "ID",
	fields: [
		{ name: "ID" },
		{ name: "CouponTypeName" },
		{ name: "TemplateCode" },
		{ name: "CreatedDate" },
		{ name: "PutInDate" },
		{ name: "Title" },
		{ name: "Nature" },
		{ name: "Validity" },
		{ name: "AproveStatusText" },
		{ name: "Stock" },
		{ name: "FreeStock" },
		{ name: "CanGiveToPeople" },
		{ name: "ConsumePointRate" },
		{ name: "PutInStartDate", type: "date" },
	]
});