Ext.define("WX.view.SysMenu.AddSysMenu", {
    extend: 'Ext.window.Window',
    alias: 'widget.AddSysMenu',
    title: '添加菜单',
    layout: 'fit',
    autoShow: true,
    modal: true,
    initComponent: function () {
        var isLeafStore = Ext.create("Ext.data.Store", {
            fields: ["Name", "Value"],
            data: [
                { "Name": "是", "Value": true },
                { "Name": "否", "Value": false }
            ]
        });

        var isAvailable = Ext.create("Ext.data.Store", {
            fields: ["Name", "Value"],
            data: [
                { "Name": "是", "Value": true },
                { "Name": "否", "Value": false }
            ]
        });

        var typeStore = Ext.create("Ext.data.Store", {
            fields: ["Name", "Value"],
            data: [
                { "Name": "组件", "Value": 1 },
                { "Name": "路径", "Value": 2 }
            ]
        });

        Ext.apply(this, {
            items: [
                {
                    xtype: 'form',
                    bodyStyle: 'padding:5px 5px 0',
                    width: 600,
                    items: [
                        {
                            xtype: 'SysMenuTreeComboBox',
                            name: 'parentId',
                            width: 400,
                            emptyText: '请选择...',
                            fieldLabel: '父级菜单',
                            allowBlank: false,
                            blankText: '请选择父级菜单!'
                        },
                        {
                            xtype: 'combo',
                            editable: false,
                            store: isLeafStore,
                            displayField: 'Name',
                            valueField: 'Value',
                            name: 'IsLeaf',
                            fieldLabel: '是否为终极菜单',
                            allowBlank: false,
                            blankText: '请选择菜单类型！'
                        },
                        {
                            xtype: 'combo',
                            editable: false,
                            store: typeStore,
                            displayField: 'Name',
                            valueField: 'Value',
                            name: 'Type',
                            fieldLabel: '类型',
                            allowBlank: false,
                            blankText: '请选择调用类型！'
                        },
                        {
                            xtype: 'textfield',
                            name: 'Name',
                            fieldLabel: '菜单标题',
                            allowBlank: false,
                            blankText: '请输入菜单标题'
                        },

                        {
                            xtype: 'textfield',
                            name: 'Component',
                            fieldLabel: '组件名称',
                            allowBlank: false,
                            blankText: '请输入组件名称'
                        },
                        {
                            xtype: 'textfield',
                            name: 'SysMenuUrl',
                            fieldLabel: '菜单路径',
                            allowBlank: false,
                            blankText: '请输入菜单URL'
                        },
                        {
                            xtype: 'numberfield',
                            name: 'Index',
                            fieldLabel: '排序',
                            allowBlank: false,
                            blankText: '请输入...'
                        },
                        {
                            xtype: 'combo',
                            editable: false,
                            store: isAvailable,
                            displayField: 'Name',
                            valueField: 'Value',
                            name: 'IsAvailable',
                            fieldLabel: '是否可用',
                            allowBlank: false,
                            blankText: ''
                        },

                    ]
                }
            ],
            buttons: [
                {
                    text: '确定保存',
                    cls: 'submitBtn',
                    action: 'save'
                },
                {
                    text: '取消',
                    scope: this,
                    handler: this.close
                }
            ]
        });
        this.callParent(arguments);
    },
    render: function () {
        //this.down('SysMenuTreeComboBox').tree.store.load();
        this.callParent(arguments);
    }
});