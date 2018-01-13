Ext.define('WX.view.MemberCardTheme.MemberCardThemeGroup', {
    extend: 'Ext.window.Window',
    alias: 'widget.MemberCardThemeGroup',
    title: '推荐分组管理',
    layout: 'vbox',
    height: 500,
    width: 900,
    modal: true,
    bodyPadding: 5,
    initComponent: function () {
        var me = this;
        var memberCardThemeGroupStore = Ext.create('WX.store.BaseData.MemberCardThemeGroupStore');

        memberCardThemeGroupStore.proxy.extraParams = { IsFromPortal: true };
        memberCardThemeGroupStore.load();

        me.grid = Ext.create('Ext.grid.Panel', {
            width: '100%',
            height: '100%',
            store: memberCardThemeGroupStore,
            tbar: [
                {
                    xtype: 'button',
                    text: '添加',
                    action: 'add',
                    iconCls: 'fa fa-plus-circle',
                }, {
                    xtype: 'button',
                    text: '编辑',
                    action: 'edit',
                    iconCls: 'x-fa fa-pencil',
                }, {
                    xtype: 'button',
                    text: '删除',
                    action: 'delete',
                    iconCls: 'x-fa fa-close'
                }
            ],
            columns: [
                { text: '编号', dataIndex: 'Code', flex: 1 },
                { text: '所属推荐', dataIndex: 'CategoryName', flex: 1 },
                { text: '分组名称', dataIndex: 'Name', flex: 1 },
                { text: '排序', dataIndex: 'Index', flex: 1 },
            ],
            bbar: {
                xtype: 'pagingtoolbar',
                store: memberCardThemeGroupStore,
                displayInfo: true
            }
        });
        me.items = [me.grid];

        me.callParent(arguments);
    }
});