Ext.define('WX.view.selector.PosRetailPlanSelector', {
    extend: 'Ext.window.Window',
    requires: ['WX.store.BaseData.PosRetailPlanStore'],
    alias: 'widget.PosRetailPlanSelector',
    title: '选择折扣方案',
    layout: 'fit',
    autoShow: true,
    modal: true,
    height: 450,
    width: 700,
    multiSelect: true,//是否多选
    storeParam: {},//store 额外查询参数
    initComponent: function () {
        var me = this;
        me.gridStore = Ext.create('WX.store.BaseData.PosRetailPlanStore');
        Ext.apply(me.gridStore.proxy.extraParams, me.storeParam);
        me.items = [{
            xtype: 'grid',
            border: null,
            //selModel: Ext.create('Ext.selection.CheckboxModel', { mode: "SIMPLE" }),
            selType: me.multiSelect == true ? 'checkboxmodel' : 'rowmodel',
            store: me.gridStore,
            columns: [
                { header: '代码', dataIndex: 'Code', flex: 1 },
                { header: '名称', dataIndex: 'Name', flex: 1 }
            ],
            listeners: {
                scope: me,
                itemdblclick: me.confirmSelect
            },
            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: me.gridStore,
                displayInfo: true,
                dock: "bottom"
            }]
        }];
        me.buttons = [{
            type: 'button',
            text: '确定',
            scope: me,
            handler: me.confirmSelect
        }, {
            xtype: 'button',
            text: '取消',
            scope: me,
            handler: me.close
        }];
        this.callParent(arguments);
    },
    afterRender: function () {
        this.callParent(arguments);
        this.gridStore.load();
    },
    confirmSelect: function () {
        var me = this;
        var selection = me.down('grid').getSelectionModel().getSelection();
        if (selection.length === 0) return;
        var selectedData;
        if (me.multiSelect == true) {
            selectedData = [];
            for (var i = 0; i < selection.length; i++) {
                selectedData.push(selection[i].data);
            }
        } else {
            selectedData = selection[0].data;
        }
        me.onConfirm(selectedData);
        me.close();
    },
    onConfirm: function (selectedData) { },
});