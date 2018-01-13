/// <reference path="../../../_references.js" />
Ext.define('WX.view.Permission.RolePermission', {
    extend: 'Ext.container.Container',
    alias: 'widget.RolePermission',
    title: '角色关联权限',
    layout: 'hbox',
    align: 'stretch',
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var roleStore = Ext.create('WX.store.BaseData.RoleStore');
        roleStore.load();
        me.items = [{
            xtype: 'grid',
            name: 'gridRole',
            title: '角色列表',
            width: 300,
            height: '100%',
            store: roleStore,
            stripeRows: true,
            columns: [
                { header: '角色代码', dataIndex: 'Code', flex: 1, },
                { header: '角色名称', dataIndex: 'Name', flex: 1, }
            ],
        }, {
            xtype: 'form',
            name: 'permissionForm',
            title: '权限列表',
            flex: 1,
            height: '100%',
            bodyPadding: 5,
            items: []
        }];
        this.callParent();
    },
});
