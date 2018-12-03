Ext.define('WX.store.BaseData.DailyExpenseStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.DailyExpenseModel',
	pageSize: 20,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/DailyExpense',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/DailyExpense?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/DailyExpense/BatchDelete',
		}
	},
	batchDelete: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchDelete,
			method: 'DELETE',
			jsonData: { IdList: ids },
			callback: cb
		});
	},
});