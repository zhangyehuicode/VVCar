Ext.define('WX.view.WeChatFans.WeChatFansTagList', {
    extend: 'Ext.window.Window',
    alias: 'widget.WeChatFansTagList',
    title: '管理粉丝标签',
    layout: 'fit',
    width: 500,
    modal: true,
    initComponent: function () {
        var me = this;
        var fansTagStore = Ext.create('WX.store.BaseData.WeChatFansTagStore');
        fansTagStore.load();
        me.grid = Ext.create('Ext.grid.Panel', {
            store: fansTagStore,
            width: '100%',
            height: 300,
            border: false,
            tbar: [{
                xtype: 'button',
                action: 'addFansTag',
                text: '新增',
                iconCls: 'x-fa fa-plus-circle'
            }, {
                xtype: 'button',
                action: 'editFansTag',
                text: '修改',
                iconCls: 'x-fa fa-pencil'
            }, {
                xtype: 'button',
                action: 'deleteFansTag',
                text: '删除',
                iconCls: 'x-fa fa-close'
            }],
            columns: [
                { header: '编号', dataIndex: 'Code', flex: 1, },
                { header: '名称', dataIndex: 'Name', flex: 1, },
                { header: '排序', dataIndex: 'Index', flex: 1 }
            ],
            dockedItems: [{
                xtype: 'pagingtoolbar',
                dock: 'bottom',
                store: fansTagStore,
                displayInfo: true
            }]
        });
        me.items = [me.grid];
        me.callParent(arguments);
    }
});