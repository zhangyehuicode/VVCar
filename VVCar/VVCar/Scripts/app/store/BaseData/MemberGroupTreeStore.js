Ext.define("WX.store.BaseData.MemberGroupTreeStore", {
	extend: "Ext.data.TreeStore",
	model: "WX.model.BaseData.MemberGroupTreeModel",
	nodeParam: 'ParentId',
	defaultRootId: '00000000-0000-0000-0000-000000000000',
	defaultRootProperty: 'Children',
	rootProperty: 'Children',
	proxy: {
		type: "rest",
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberGroup',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberGroup/GetTree?parentID=',
			create: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberGroup',
			update: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberGroup',
			destroy: Ext.GlobalConfig.ApiDomainUrl + 'api/MemberGroup',
		},
		reader: {
			type: 'json',
			root: 'Children',
			successProperty: 'IsSuccessful',
			messageProperty: 'ErrorMessage',
		}
	},
	root: {
		Text: '全部分类',
		id: '0000000-0000-0000-0000-000000000000',
		expanded: true,
	},
	addMemberGroup: function (entity, cb) {
		Ext.Ajax.request({
			method: 'POST',
			url: this.proxy.api.create,
			jsonData: entity,
			callback: cb
		});
	},
	updateMemberGroup: function (entity, cb) {
		Ext.Ajax.request({
			method: 'PUT',
			url: this.proxy.api.update,
			jsonData: entity,
			callback: cb
		});
	},
	deleteMemberGroup: function (id, cb) {
		Ext.Ajax.request({
			method: 'DELETE',
			url: this.proxy.api.destroy + '/' + id,
			callback: cb
		});
	},
});