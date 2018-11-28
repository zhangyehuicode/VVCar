Ext.define('WX.view.Report.OperationStatementChart', {
	extend: 'Ext.window.Window',
	alias: 'widget.OperationStatementChart',
	title: '收支报表折线图',
	width: '95%',
	height: 600,
	initComponent: function () {
		var me = this;
		var url = window.location.origin;
		me.html = '<iframe width="100%" height=580 frameborder=0 src="' + url + '/Reporting/AnalyseLineChart?AnalyseType=1"></iframe>';
		this.callParent();
	}
});