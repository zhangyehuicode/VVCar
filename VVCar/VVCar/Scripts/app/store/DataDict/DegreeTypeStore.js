Ext.define('WX.store.DataDict.DegreeTypeStore', {
	extend: 'Ext.data.Store',
	fields: ['DictValue', 'DictName'],
	data: [
		{ 'DictValue': -1, 'DictName': '不限' },
		{ 'DictValue': 0, 'DictName': '大专' },
		{ 'DictValue': 1, 'DictName': '本科' },
		{ 'DictValue': 2, 'DictName': '硕士' },
		{ 'DictValue': 3, 'DictName': '博士' },
	]
});
