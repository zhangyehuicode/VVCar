Ext.define('WX.view.selector.MemberSelector', {
    extend: 'Ext.window.Window',
    alias: 'widget.MemberSelector',
    title: '选择会员',
    layout: 'fit',
	width: 600,
	height: 530,
    bodyPadding: 5,
    modal: true,
    initComponent: function() {
        var me = this;
        var store = Ext.create('WX.store.BaseData.MemberStore');
		store.limit = 10;
		store.pageSize = 10;
        store.load();
		me.grid = Ext.create('Ext.grid.Panel', {
			name: "gridMember",
			flex: 1,
			store: store,
			stripeRows: true,
			selModel: Ext.create('Ext.selection.CheckboxModel', { model: 'SIMPLE' }),
			tbar: {
				xtype: 'form',
				layout: 'column',
				border: false,
				frame: false,
				labelAlign: 'left',
				buttonAlign: 'right',
				labelWidth: 100,
				padding: 5,
				autoWidth: true,
				autoScroll: true,
				columnWidth: 1,
				items: [{
					name: 'Keyword',
					xtype: 'textfield',
					fieldLabel: '关键字',
					width: 170,
					labelWidth: 60,
					margin: '0 0 0 5',
				}, {
					action: 'search',
					xtype: 'button',
					text: '搜索',
					iconCls: 'submitBtn',
					margin: '0 0 0 5',
				}]
			},
			columns: [
                { header: '会员名称', dataIndex: 'Name', flex: 1 },
                { header: '手机号码', dataIndex: 'MobilePhoneNo', flex: 1 },
                { header: '车牌号', dataIndex: 'PlateList', flex: 1 },
            ],
            bbar: {
                xtype: 'pagingtoolbar',
                displayInfo: true
            }
        });
        me.items = [me.grid];
        me.buttons = [
            {
                text: '保存',
                action: 'save',
                cls: 'submitBtn',
                scope: me
            },
            {
                text: '取消',
                scope: me,
                handler: me.close
            }
        ];
        me.callParent(arguments);
    }
})