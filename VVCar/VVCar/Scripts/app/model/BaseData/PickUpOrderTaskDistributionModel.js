Ext.define("WX.model.BaseData.PickUpOrderTaskDistributionModel", {
	extend: "Ext.data.Model",
	idProperty: "ID",
	fields: [
		{ name: "ID" },
		{ name: "UserCode" },
		{ name: "UserName" },
		{ name: "PeopleType" },
		{ name: "CommissionRate" },
		{ name: "Commission" },
	]
});