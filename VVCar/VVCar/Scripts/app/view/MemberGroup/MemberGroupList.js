Ext.define('WX.view.MemberGroup.MemberGroupList', {
    extend: 'Ext.window.Window',
    alias: 'widget.MemberGroupList',
    title: '编辑分组',
    layout: 'fit',
    width: 500,
    modal: true,
    initComponent: function () {
        var me = this;
        var groupstore = Ext.create('WX.store.BaseData.MemberGroupStore');

        me.grid = Ext.create('Ext.grid.Panel', {
            store: groupstore,
            width: '100%',
            height: 300,
            border: false,
            tbar: [{
                xtype: 'button',
                action: 'addgroup',
                text: '新增',
                iconCls: 'fa fa-plus-circle'
            }, {
                xtype: 'button',
                action: 'editgroup',
                text: '修改',
                iconCls: 'x-fa fa-pencil'
            }, {
                xtype: 'button',
                action: 'deletegroup',
                text: '删除',
                iconCls: 'x-fa fa-close'
            }],
            columns: [
                { header: '名称', dataIndex: 'Name', flex: 1, },
                { header: '编号', dataIndex: 'Code', flex: 1, },
                { header: '排序', dataIndex: 'Index', flex: 1 },
                {
                    header: '批发价', dataIndex: 'IsWholesalePrice', flex: 1,
                    renderer: function (value) {
                        if (value)
                            return "<span style='color:green;'>是</span>";
                        else
                            return "<span style='color:red;'>否</span>";
                    }
                },
            ],
            dockedItems: [{
                xtype: 'pagingtoolbar',
                dock: 'bottom',
                store: groupstore,
                displayInfo: true
            }]
        });

        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            items: [me.grid]
        });

        me.items = [me.form];
        me.callParent(arguments);
    }
});