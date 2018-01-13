Ext.define('WX.view.DataDict.DataDictList', {
    extend: 'Ext.container.Container',
    alias: 'widget.DataDictList',
    title: '数据字典',
    layout: 'hbox',
    align: 'stretch',
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var dictTypeStore = Ext.create('WX.store.DataDict.DataDictTypeStore');
        var dictValueStore = Ext.create('WX.store.DataDict.DataDictValueStore');
        me.rowEditing = Ext.create('Ext.grid.plugin.RowEditing', {
            saveBtnText: '保存',
            cancelBtnText: "取消",
            autoCancel: false,
            listeners: {
                cancelEdit: function (rowEditing, context) {
                    //如果是新增的数据，则删除
                    if (context.record.phantom) {
                        dictValueStore.remove(context.record);
                    }
                },
                beforeedit: function (editor, context, eOpts) {
                    if (editor.editing == true)
                        return false;
                },
            }
        });
        me.items = [
            {
                xtype: 'grid',
                name: 'gridDictType',
                title: '字典类型',
                flex: 1,
                store: dictTypeStore,
                stripeRows: true,
                height: '100%',
                columns: [
                    { header: '类型代码', dataIndex: 'Code', flex: 1, },
                    { header: '类型名称', dataIndex: 'Name', flex: 1, },
                ],
            },
            {
                xtype: 'grid',
                name: 'gridDictValue',
                title: '数据字典',
                flex: 2,
                store: dictValueStore,
                stripeRows: true,
                height: '100%',
                plugins: [me.rowEditing],
                columns: [
                    { header: '序号', dataIndex: 'Index', flex: 1, editor: { xtype: 'numberfield', allowBlank: false } },
                    { header: '字典值', dataIndex: 'DictValue', flex: 1, editor: { xtype: 'textfield', allowBlank: false } },
                    { header: '字典名称', dataIndex: 'DictName', flex: 1, editor: { xtype: 'textfield', allowBlank: false } },
                    {
                        header: '是否可用', dataIndex: 'IsAvailable', width: 80,
                        renderer: function (value) { return value == false ? '否' : '是'; },
                        editor: { xtype: 'checkbox' }
                    },
                    { header: '备注', dataIndex: 'Remark', flex: 2, editor: { xtype: 'textfield' } },
                ],
                bbar: [
                    {
                        action: 'addDictValue',
                        xtype: 'button',
                        text: '添加',
                        iconCls: 'fa fa-plus-circle'
                    },
                ],
            }
        ];

        this.callParent();
    },

    //afterRender: function () {
    //    this.callParent(arguments);
    //}
});
