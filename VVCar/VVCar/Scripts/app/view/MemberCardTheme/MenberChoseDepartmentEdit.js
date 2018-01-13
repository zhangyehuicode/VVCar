Ext.define('WX.view.MemberCardTheme.MenberChoseDepartmentEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.MenberChoseDepartmentEdit',
    title: '卡片主题管理-门店选择',
    //layout: 'fit',
    width: 500,
    scrollDelay: true,
    autoScroll: true, 
    height: 500,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        var DepartmentStore = Ext.create('WX.store.BaseData.DepartmentStore');
        DepartmentStore.proxy.extraParams = { IsFromPortal: true };
        DepartmentStore.load();

        //DepartmentStore.on('load', function (store, records) {
        //    var index = store.find('Code', 021);
        //    grid.getSelectionModel().selectRow(index);
        //    //Ext.Msg.alert('提示', '数据加载');
        //});

        me.grid = Ext.create('Ext.grid.Panel', {
            width: '100%',
            height: '100%',
            store: DepartmentStore,
            columns: [{
                text: '门店名称',
                dataIndex: 'Name',
                flex: 1
            }],
            selModel: Ext.create('Ext.selection.CheckboxModel', {
                mode: "SIMPLE",
            }),
            bbar: {
                xtype: 'pagingtoolbar',
                store: DepartmentStore,
                displayInfo: true
            }


        });


        me.items = [me.grid];

        me.buttons = [{
            text: '保存',
            action: 'save',
            cls: 'submitBtn',
            scope: me,
        }, {
            text: '取消',
            scope: me,
            handler: me.close
        }];
        me.callParent(arguments);
    }
});