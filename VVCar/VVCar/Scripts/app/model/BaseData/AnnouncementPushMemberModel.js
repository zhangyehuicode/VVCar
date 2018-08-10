Ext.define("WX.model.BaseData.AnnouncementPushMemberModel", {
	extend: "Ext.data.Model",
	idProperty: "ID",
	fields: [
		{ name: "ID" },
		{ name: "Name" },
		{ name: "MobilePhoneNo" },
		{ name: "PlateList" },
	]
});