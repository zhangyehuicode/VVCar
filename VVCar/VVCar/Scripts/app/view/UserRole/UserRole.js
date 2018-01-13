Ext.define('WX.view.UserRole.UserRole', {
    extend: 'Ext.container.Container',
    alias: 'widget.UserRole',
    title: '用户角色关联',
    layout: 'hbox',
    align: 'stretch',
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var roleStore = Ext.create('WX.store.BaseData.RoleStore');
        roleStore.load();
        var userRoleStore = Ext.create('WX.store.BaseData.UserRoleStore');
        me.items = [
            {
                xtype: 'grid',
                name: 'gridRole',
                title: '角色列表',
                flex: 1,
                height: '100%',
                store: roleStore,
                stripeRows: true,
                tbar: [
                    {
                        action: 'addUser',
                        xtype: 'button',
                        text: '分配用户',
                        iconCls: 'x-fa fa-plus-circle'
                    },
                ],
                columns: [
                    { header: '角色代码', dataIndex: 'Code', flex: 1, },
                    { header: '角色名称', dataIndex: 'Name', flex: 1, },
                ],
            },
            {
                xtype: 'grid',
                name: 'gridUserRole',
                title: '关联用户列表',
                flex: 2,
                height: '100%',
                store: userRoleStore,
                stripeRows: true,
                selType: 'checkboxmodel',
                tbar: [
                    {
                        action: 'deleteUser',
                        xtype: 'button',
                        text: '删除关联',
                        iconCls: 'x-fa fa-close'
                    },
                ],
                columns: [
                    { header: '人员编号', dataIndex: 'UserCode', flex: 1 },
                    { header: '人员名称', dataIndex: 'UserName', flex: 1 },
                    { header: '分配人', dataIndex: 'CreatedUser', flex: 1 },
                    { header: '分配时间', dataIndex: 'CreatedDate', flex: 1 },
                ],
                bbar: {
                    xtype: 'pagingtoolbar',
                    store: userRoleStore,
                    displayInfo: true
                }
            }
        ];
        this.callParent();
    },
});
