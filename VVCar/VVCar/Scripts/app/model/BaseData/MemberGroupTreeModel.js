Ext.define('WX.model.BaseData.MemberGroupTreeModel', {
	extend: 'WX.model.BaseData.MemberGroupModel',
	idProperty: 'ID',
	fields: ['Text', 'leaf', 'expanded', 'Children']
});