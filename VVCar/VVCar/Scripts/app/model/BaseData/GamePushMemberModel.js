Ext.define("WX.model.BaseData.GamePushMemberModel", {
	extend: "Ext.data.Model",
	idProperty: "ID",
	fields: [
		{ name: "ID" },
		{ name: "Name" },
		{ name: "MobilePhoneNo" },
		{ name: "PlateList" },
	]
});