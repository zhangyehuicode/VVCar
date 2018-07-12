Ext.define("WX.model.BaseData.SuperClassModel", {
	extend: "Ext.data.Model",
	idProperty: "ID",
	fields: [
		{ name: "Name" },
		{ name: "VideoUrl" },
		{ name: 'Description' },
		{ name: 'CreatedUser' },
		{ name: 'CreatedDate' },
		{ name: 'VideoType' },
	]
});