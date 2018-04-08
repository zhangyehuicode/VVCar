Ext.define('WX.view.Shop.ChangeCategory', {
    extend: 'Ext.window.Window',
    alias: 'widget.ChangeCategory',
    title: '移动到其他分类',
    layout: 'fit',
    width: 350,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        var categoryStore = Ext.create('WX.store.BaseData.ProductCategoryLiteStore');
        categoryStore.load({ params: { All: true } });
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%'
            },
            items: [
                {
                    xtype: 'textfield',
                    name: 'ID',
                    fieldLabel: 'ID',
                    readOnly: true,
                    hidden: true,
                    hideLabel: true
                },
                //{
                //    xtype: 'textfield',
                //    name: 'Code',
                //    fieldLabel: '产品代码',
                //    readOnly: true,
                //},
                {
                    xtype: 'textfield',
                    name: 'Name',
                    fieldLabel: '产品名称',
                    readOnly: true,
                },
                {
                    xtype: 'combo',
                    name: 'CurrCategory',
                    fieldLabel: '当前分类',
                    store: categoryStore,
                    queryMode: 'local',
                    displayField: 'Name',
                    valueField: 'ID',
                    readOnly: true,
                },
                {
                    xtype: 'combo',
                    name: 'DestCategory',
                    fieldLabel: '移动到分类',
                    store: categoryStore,
                    queryMode: 'local',
                    displayField: 'Name',
                    valueField: 'ID',
                    emptyText: '请选择...',
                    blankText: '请选择移动目标分区',
                    allowBlank: false,
                    tpl: Ext.create('Ext.XTemplate',
                        '<tpl for=".">',
                        '<div class="x-boundlist-item">{Code} - {Name}</div>',
                        '</tpl>'),
                    displayTpl: Ext.create('Ext.XTemplate',
                        '<tpl for=".">',
                        '{Code} - {Name}',
                        '</tpl>')
                },
            ]
        });
        me.items = [me.form];
        me.buttons = [
            {
                text: '保存',
                action: 'saveProductCategoryBtn'
            },
            {
                text: '取消',
                scope: me,
                handler: me.close
            }
        ];
        me.callParent(arguments);
    }
});