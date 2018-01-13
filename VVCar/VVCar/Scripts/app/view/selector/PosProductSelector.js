Ext.define('WX.view.selector.PosProductSelector', {
    extend: 'Ext.window.Window',
    requires: ['WX.store.BaseData.PosProductStore'],
    alias: 'widget.PosProductSelector',
    title: '选择产品',
    layout: 'fit',
    autoShow: true,
    modal: true,
    height: 450,
    width: 700,
    multiSelect: true,//是否多选
    storeParam: {},//store 额外查询参数
    initComponent: function () {
        var me = this;
        me.gridStore = Ext.create('WX.store.BaseData.PosProductStore');
        Ext.apply(me.gridStore.proxy.extraParams, { All: false, IncludeCategory: true, IsSale: true });
        Ext.apply(me.gridStore.proxy.extraParams, me.storeParam);
        me.items = [{
            xtype: 'grid',
            selType: me.multiSelect == true ? 'checkboxmodel' : 'rowmodel',
            store: me.gridStore,
            tbar: [{
                xtype: 'form',
                name: 'formSearch',
                layout: 'column',
                border: false,
                frame: false,
                labelAlign: 'left',
                buttonAlign: 'right',
                labelWidth: 80,
                autoWidth: true,
                autoScroll: true,
                columnWidth: 1,
                scope: this,
                items: [{
                    margin: '0 0 0 5',
                    xtype: 'textfield',
                    name: 'Code',
                    width: 200,
                    displayField: 'Code',
                    valueField: 'Code',
                    fieldLabel: '代码',
                    labelWidth: 50
                }, {
                    xtype: 'textfield',
                    name: 'Name',
                    fieldLabel: '名称',
                    width: 220,
                    margin: '0 0 0 5',
                    labelWidth: 60
                }, {
                    xtype: 'button',
                    text: '搜 索',
                    iconCls: 'fa fa-search',
                    cls: 'submitBtn',
                    margin: '0 0 0 5',
                    listeners: {
                        click: function () {
                            me.gridStore.currentPage = 1;
                            Ext.apply(me.gridStore.proxy.extraParams, me.down('form[name=formSearch]').getValues());
                            me.gridStore.load();
                        }
                    }
                }]
            }],
            columns: [
                { header: '产品代码', dataIndex: 'Code', flex: 1, },
                { header: '产品名称', dataIndex: 'Name', flex: 1, },
                { header: 'ProductCategoryID', dataIndex: 'ProductCategoryID', flex: 1, hidden: true },
                { header: '规格', dataIndex: 'Unit', flex: 1, editor: { xtype: 'textfield' } },
                { header: '零售价', dataIndex: 'PriceSale', flex: 1, editor: { xtype: 'textfield' } }
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