Ext.define('WX.view.MakeCodeRule.MakeCodeRuleList', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.MakeCodeRule',
    title: '口味管理',
    store: Ext.create('WX.store.BaseData.MakeCodeRuleStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var RuleStatus = Ext.create('Ext.data.Store', {
            fields: ['Name', 'Value'],
            data: [
                { "Name": "无", "Value": 0 },
                { "Name": "固定", "Value": 1 },
                { "Name": "部门", "Value": 3 },
                { "Name": "时间", "Value": 4 }
            ]
        });
        me.tbar = [
            {
                action: 'refresh',
                xtype: 'button',
                text: '刷新',
                scope: me,
                iconCls: 'fa fa-refresh',
            }];
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
        me.plugins = [me.rowEditing];
        me.columns = [
            { header: '业务编码', dataIndex: 'Code', flex: 2 },
            { header: '业务名称', dataIndex: 'Name', flex: 2, editor: { xtype: 'textfield', allowBlank: false } },
            {
                header: '是否可用', dataIndex: 'IsAvailable', renderer: function (value, meta, record) {
                    if (value == 1) {
                        return '<span style="color:green;">可用</span>';
                    } else {
                        return '<span style="color:red;">不可用</span>';
                    }
                }, width: 80, editor: { xtype: 'checkbox' }
            },
            { header: '长度', dataIndex: 'Length', flex: 1, editor: { xtype: 'textfield', allowBlank: true } },
            { header: '前缀1', dataIndex: 'Prefix1', flex: 2, editor: { xtype: 'textfield', allowBlank: true } },
            {
                header: '前缀1规则', dataIndex: 'Prefix1Rule', flex: 2,
                renderer: function (value, meta, record) {
                    //return value;
                    switch (value) {
                        case 0:
                            return "无";
                        case 1:
                            return "固定";
                        case 3:
                            return "部门";
                        case 4:
                            return "时间";
                    }
                },
                editor: {
                    xtype: 'combo',
                    store: RuleStatus,
                    queryMode: 'local',
                    displayField: 'Name',
                    //name: 'Type',
                    valueField: 'Value',
                }
            },
            { header: '前缀1长度', dataIndex: 'Prefix1Length', flex: 2, editor: { xtype: 'textfield', allowBlank: true } },
            { header: '前缀2', dataIndex: 'Prefix2', flex: 2, editor: { xtype: 'textfield', allowBlank: true } },
            {
                header: '前缀2规则', dataIndex: 'Prefix2Rule', flex: 2,
                renderer: function (value, meta, record) {
                    switch (value) {
                        case 0:
                            return "无";
                        case 1:
                            return "固定";
                        case 3:
                            return "部门";
                        case 4:
                            return "时间";
                    }
                },
                editor: {
                    xtype: 'combo',
                    store: RuleStatus,
                    queryMode: 'local',
                    displayField: 'Name',
                    valueField: 'Value',
                }
            },
            { header: '前缀2长度', dataIndex: 'Prefix2Length', flex: 2, editor: { xtype: 'textfield', allowBlank: true } },
            { header: '前缀3', dataIndex: 'Prefix3', flex: 2, editor: { xtype: 'textfield', allowBlank: true } },
            {
                header: '前缀3规则', dataIndex: 'Prefix3Rule', flex: 2,
                renderer: function (value, meta, record) {
                    switch (value) {
                        case 0:
                            return "无";
                        case 1:
                            return "固定";
                        case 3:
                            return "部门";
                        case 4:
                            return "时间";
                    }
                },
                editor: {
                    xtype: 'combo',
                    store: RuleStatus,
                    queryMode: 'local',
                    displayField: 'Name',
                    valueField: 'Value',
                }
            },
            { header: '前缀3长度', dataIndex: 'Prefix3Length', flex: 2, editor: { xtype: 'textfield', allowBlank: true } },
        ];
        this.callParent();
    },

    afterRender: function () {
        this.callParent(arguments);
        this.getStore().load();
    }
});
