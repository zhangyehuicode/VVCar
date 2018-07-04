Ext.define('WX.store.DataDict.RecruitmentSexStore', {
	extend: 'Ext.data.Store',
	fields: ['DictValue', 'DictName'],
	data: [
		{ 'DictValue': 0, 'DictName': '不限' },
		{ 'DictValue': 1, 'DictName': '男' },
		{ 'DictValue': 2, 'DictName': '女' },
	]
});
