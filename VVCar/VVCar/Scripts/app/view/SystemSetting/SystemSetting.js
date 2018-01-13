Ext.define('WX.view.SystemSetting.SystemSetting', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.SystemSetting',
    title: '系统参数设置',
    store: Ext.create('WX.store.BaseData.SystemSettingStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        me.rowEditing = Ext.create('Ext.grid.plugin.RowEditing', {
            saveBtnText: '保存',
            cancelBtnText: "取消",
            autoCancel: false,
            listeners: {
                cancelEdit: function (rowEditing, context) {
                    //如果是新增的数据，则删除
                    if (context.record.phantom) {
                        me.store.remove(context.record);
                    }
                },
                beforeedit: function (editor, context, eOpts) {
                    if (editor.editing == true)
                        return false;
                },
            }
        });
        me.columnLines = true;
        me.plugins = [me.rowEditing];
        me.columns = [
            { header: '名称', dataIndex: 'Caption', width: 200, },
            { header: '值', dataIndex: 'SettingValue', flex: 1, editor: { xtype: 'textfield', allowBlank: true } }
        ];
        this.callParent();
    },
    afterRender: function () {
        this.callParent(arguments);
        this.getStore().load();
    }
});

