Ext.define('WX.store.DataDict.CarBitCoinRecordTypeStore', {
	extend: 'Ext.data.Store',
	fields: ['DictValue', 'DictName'],
	data: [
		{ "DictValue": -4, "DictName": "赠送币" },
		{ "DictValue": -3, "DictName": "系统分配币" },
		{ "DictValue": -2, "DictName": "购买比特币" },
		{ "DictValue": -1, "DictName": "出售比特币" },
		{ "DictValue": 0, "DictName": "未知" },
		{ "DictValue": 1, "DictName": "购买引擎增加马力" },
		{ "DictValue": 2, "DictName": "商城下单增加马力" },
		{ "DictValue": 3, "DictName": "接车单消费增加马力" },
		{ "DictValue": 4, "DictName": "员工业绩增加马力" },
	]
});
