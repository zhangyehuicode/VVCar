Ext.define('WX.view.Permission.PermissionList', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.PermissionList',
    title: '权限列表',
    store: Ext.create('WX.store.BaseData.PermissionStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var permissionTypeStore = Ext.getStore('DataDict.PermissionTypeStore');
        me.tbar = [{
            action: 'addPermission',
            xtype: 'button',
            text: '添加',
            scope: this,
            iconCls: 'fa fa-plus-circle',
        }, {
            action: 'syncPermission',
            xtype: 'button',
            text: '拉取权限数据',
            scope: this,
            iconCls: 'fa fa-refresh'
        }, {
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
                xtype: 'textfield',
                name: 'Code',
                fieldLabel: '权限代码',
                width: 170,
                labelWidth: 60,
                margin: '0 0 0 5',
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '权限名称',
                width: 170,
                labelWidth: 60,
                margin: '0 0 0 5',
            }, {
                action: 'search',
                xtype: 'button',
                text: '搜 索',
                iconCls: 'fa fa-search',
                cls: 'submitBtn',
                margin: '0 0 0 5',
            }]
        }];
        me.columns = [
            { header: '权限代码', dataIndex: 'Code', width: 200, },
            { header: '名称', dataIndex: 'Name', width: 200, },
            {
                header: '权限类型', dataIndex: 'PermissionType', width: 100,
                renderer: function (value, meta, record) {
                    var record = permissionTypeStore.findRecord('DictValue', value);
                    if (record == null) return value;
                    else return record.data.DictName;
                }
            },
            {
                header: '是否开启', dataIndex: 'IsAvailable', flex: 1,
                renderer: function (value, meta, record) {
                    if (value == 1) {
                        return '<span style="color:green;">&nbsp;&nbsp;&nbsp;&nbsp;是</span>';
                    } else {
                        return '<span style="color:red;">&nbsp;&nbsp;&nbsp;&nbsp;否</span>';
                    }
                }
            }
        ];
        me.dockedItems = [{
            xtype: 'pagingtoolbar',
            store: me.store,
            dock: 'bottom',
            displayInfo: true
        }];
        this.callParent();
    },

    afterRender: function () {
        this.callParent(arguments);
        this.getStore().load();
    }
});
