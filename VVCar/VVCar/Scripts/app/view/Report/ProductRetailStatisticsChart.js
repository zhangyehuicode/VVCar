Ext.define('WX.view.Report.ProductRetailStatisticsChart', {
	extend: 'Ext.window.Window',
	alias: 'widget.ProductRetailStatisticsChart',
	title: '产品销售汇总统计图',
	width: '95%',
	height: 600,
	modal: true,
	initComponent: function () {
		var me = this;
		var url = window.location.origin;
		var productid = sessionStorage.getItem("ProductID");
		me.html = '<iframe width="100%" height=580 frameborder=0 src="' + url + '/Reporting/ProductRetailStatisticsChart"></iframe>';
		this.callParent();
	}
});