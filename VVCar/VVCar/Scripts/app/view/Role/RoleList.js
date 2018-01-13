Ext.define('WX.view.Role.RoleList', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.RoleList',
    title: '角色管理',
    store: Ext.create('WX.store.BaseData.RoleStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    initComponent: function () {
        //var roleTypes = Ext.create('WX.store.DataDict.DataDictStore', { storeId: 'RoleTypeDictStore' });
        //roleTypes.load({ params: { dictType: 'RoleType' } });
        this.tbar = [
            {
                action: 'addRole',
                xtype: 'button',
                text: '添加',
                scope: this,
                iconCls: 'fa fa-plus-circle',
            },
            {
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
                items: [
                    {
                        xtype: 'textfield',
                        name: 'Code',
                        fieldLabel: '角色代码',
                        width: 170,
                        labelWidth: 60,
                        margin: '0 0 0 5',
                    },
                    {
                        xtype: 'textfield',
                        name: 'Name',
                        fieldLabel: '角色名称',
                        width: 170,
                        labelWidth: 60,
                        margin: '0 0 0 5',
                    },
                    //{
                    //    xtype: 'combobox',
                    //    fieldLabel: '所属分类',
                    //    name: 'RoleType',
                    //    width: 170,
                    //    labelWidth: 60,
                    //    margin: '0 0 0 5',
                    //    store: roleTypes,
                    //    queryMode: 'local',
                    //    displayField: 'DictName',
                    //    valueField: 'DictValue',
                    //    blankText: '请选择角色类型',
                    //    emptyText: '请选择...',
                    //},
                    {
                        action: 'search',
                        xtype: 'button',
                        text: '搜 索',
                        iconCls: 'fa fa-search',
                        cls: 'submitBtn',
                        margin: '0 0 0 5',
                    }]
            }];
        this.columns = [
            { header: '角色代码', dataIndex: 'Code', flex: 1, },
            { header: '角色名称', dataIndex: 'Name', flex: 1, },
            //{
            //    header: '角色类别', dataIndex: 'RoleType', flex: 1,
            //    renderer: function (value, cellmeta, record) {
            //        var record = roleTypes.findRecord('DictValue', value);
            //        if (record == null) return value;
            //        else return record.data.DictName;
            //    }
            //},
            { header: '创建人', dataIndex: 'CreatedUser', flex: 1 },
            { header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
            {
                text: '操作功能',
                xtype: 'actioncolumn',
                width: 80,
                sortable: false,
                menuDisabled: true,
                height: 30,
                align: 'center',
                items: [{
                    action: 'editItem',
                    iconCls: 'x-fa fa-pencil',
                    tooltip: '编辑',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('editActionClick', grid, record);
                    },
                },
                { scope: this }, {
                    action: 'deleteItem',
                    iconCls: 'x-fa fa-close',
                    tooltip: '删除',
                    scope: this,
                    margin: '10 10 10 10',
                    handler: function (grid, rowIndex, colIndex) {
                        var record = grid.getStore().getAt(rowIndex);
                        this.fireEvent('deleteActionClick', grid, record);
                    },
                }]
            }
        ];
        this.callParent();
    },

    afterRender: function () {
        this.callParent(arguments);
        this.getStore().load();
    }
});
