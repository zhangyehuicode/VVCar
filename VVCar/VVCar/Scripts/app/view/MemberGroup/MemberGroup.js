Ext.define('WX.view.MemberGroup.MemberGroup', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.MemberGroup',
    //title: '会员分组',
    closable: false,
    initComponent: function () {
        var me = this;
        var groupTreeStore = Ext.create('WX.store.BaseData.MemberGroupTreeStore');
        Ext.apply(this, {
            name: 'gridMemberGroup',
            //title: '全部用户',
            width: 250,
            height: '100%',
            store: groupTreeStore,
            stripeRows: true,
            hideHeaders: true,
            columns: [
                {
                    header: '标签', dataIndex: 'Name', flex: 1, align: 'center',
                }
            ],
            bbar: ['->', {
                action: 'editmembergroup',
                xtype: 'button',
                text: '编辑分组',
                margin: '1 0 1 0',
            }, '->']
        });
        me.callParent(arguments);
    }
});
