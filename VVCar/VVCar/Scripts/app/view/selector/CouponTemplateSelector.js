Ext.define('WX.view.selector.CouponTemplateSelector', {
    extend: 'Ext.window.Window',
    alias: 'widget.CouponTemplateSelector',
    title: '选择优惠券',
    layout: 'fit',
    autoShow: true,
    modal: true,
    height: 450,
    width: 700,
    onOk: function (data) { },
    initComponent: function () {
        var me = this;
        me.store = Ext.create('WX.store.BaseData.CouponTemplateInfoStore');
        me.couponTemplateTypeStore = Ext.create('WX.store.DataDict.CouponTemplateTypeStore');
        me.dbar = [
            {
                xtype: 'form',
                name: 'formSearch',
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
                        name: 'AproveStatus',
                        value: -2,
                        hidden: true
                    },
                    {
                        xtype: 'combobox',
                        fieldLabel: '卡片类型',
                        name: 'CouponType',
                        queryMode: 'local',
                        store: me.couponTemplateTypeStore,
                        displayField: "DictName",
                        valueField: "DictValue",
                        editable: false,
                        forceSelection: true,
                        width: 170,
                        labelWidth: 60,
                        margin: '0 0 0 5',
                        allowBlank: false
                    }, {
                        xtype: 'textfield',
                        name: 'Title',
                        fieldLabel: '',
                        width: 170,
                        labelWidth: 60,
                        margin: '0 0 0 5'
                    }, {
                        action: 'search',
                        xtype: 'button',
                        text: '搜索',
                        iconCls: 'fa fa-search',
                        cls: 'submitBtn',
                        margin: '0 0 0 10'
                    }]
            }];
        me.mcolumns = [
            { header: '类型', dataIndex: 'CouponTypeName', flex: 1 },
            { header: '编号', dataIndex: 'TemplateCode', flex: 1 },
            { header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
            { header: '投放时间', dataIndex: 'PutInDate', flex: 1 },
            { header: '标题', dataIndex: 'Title', flex: 1 },
            { header: '有效期', dataIndex: 'Validity', flex: 1 },
            { header: '库存', dataIndex: 'FreeStock', flex: 1 },
            //{ header: '投放时间', dataIndex: 'PutInStartDate', flex: 1, hidden: true },

        ];
        me.mdockedItems = [{
            xtype: 'pagingtoolbar',
            store: me.store,
            dock: 'bottom',
            displayInfo: true
        }];
        me.grid = Ext.create('Ext.grid.Panel', {
            name: "grid",
            store: me.store,
            tbar: me.dbar,
            columns: me.mcolumns,
            dockedItems: me.mdockedItems,
            height: '100%',
            flex: 1,
            //selModel: Ext.create('Ext.selection.CheckboxModel', {
            //    singleSelect: true
            //}),
            //selType: 'checkboxmodel'
        })
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
        this.callParent();
    },
    afterRender: function () {
        this.callParent(arguments);
        this.couponTemplateTypeStore.load();
        this.down("combobox[name=CouponType]").select(-1);
        let formValue = this.down("form[name=formSearch]").getValues();
        formValue.CouponType = -1;
        formValue.HiddenExpirePutInDate = true;
        formValue.IsNotSpecialCoupon = true;
        Ext.apply(this.store.proxy.extraParams, formValue);
        this.store.load();
    }
});