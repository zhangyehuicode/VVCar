Ext.define('WX.view.Coupon.QRCode', {
	extend: 'Ext.window.Window',
	alias: 'widget.QRCode',
	title: '查看二维码',
	width: 680,
	height: 350,
	modal: true,
	bodyPadding: 5,
	resizable: false,
	initComponent: function () {
		var me = this;
		me.form = Ext.create('Ext.form.Panel', {
			border: false,
			width: 640,
			trackResetOnLoad: true,
			fieldDefaults: {
				labelAlign: 'left',
				labelWidth: 80,
				anchor: '100%',
				flex: 1,
				margin: '5',
			},
			items: [{
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'box',
					margin: '5 10 0 5',
					name: 'ImgShow',
					width: 200,
					height: 200,
					margin: '0 0 5 72',
					autoEl: {
						tag: 'img',
						src: '',
					},				
				}, {
					action: 'download',
					xtype: 'button',
					text: '下载',
					iconCls: 'fa fa-arrow-down',
					margin: '100 0 0 100',
				}]
			}, {
				xtype: 'form',
				layout: 'hbox',
				items: [{
					xtype: 'textfield',
					id: 'linktext',
					name: 'Url',
					fieldLabel: '二维码链接',
				}, {
					action: 'copy',
					xtype: 'button',
					text: '复制链接',
					iconCls: 'fa fa-copy',
					margin: '5 0 0 0',
					hidden: true,
				}],
			}]
		});
		me.items = [me.form];
		me.callParent(arguments);
	},
});