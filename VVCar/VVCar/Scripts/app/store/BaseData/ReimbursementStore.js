Ext.define('WX.store.BaseData.ReimbursementStore', {
	extend: 'Ext.data.Store',
	model: 'WX.model.BaseData.ReimbursementModel',
	pageSize: 25,
	proxy: {
		type: 'rest',
		url: Ext.GlobalConfig.ApiDomainUrl + 'api/Reimbursement',
		api: {
			read: Ext.GlobalConfig.ApiDomainUrl + 'api/Reimbursement?All=false',
			batchDelete: Ext.GlobalConfig.ApiDomainUrl + 'api/Reimbursement/BatchDelete',
			approveReimbursement: Ext.GlobalConfig.ApiDomainUrl + 'api/Reimbursement/ApproveReimbursement',
			antiApproveReimbursement: Ext.GlobalConfig.ApiDomainUrl + 'api/Reimbursement/AntiApproveReimbursement',
		}
	},
	batchDelete: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			url: this.proxy.api.batchDelete,
			method: 'POST',
			jsonData: { IdList: ids },
			callback: cb
		});
	},
	approveReimbursement: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'POST',
			url: this.proxy.api.approveReimbursement,
			jsonData: { IdList: ids },
			callback: cb,
		})
	},
	antiApproveReimbursement: function (ids, cb) {
		Ext.Ajax.request({
			ContentType: 'application/json',
			method: 'POST',
			url: this.proxy.api.antiApproveReimbursement,
			jsonData: { IdList: ids },
			callback: cb,
		})
	},
});