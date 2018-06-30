Ext.define('WX.view.CarBitCoin.CarBitCoinProductCategoryEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.CarBitCoinProductCategoryEdit',
    title: '编辑类别',
    layout: 'fit',
    width: 250,
    bodyPadding: 5,
    modal: true,
    initComponent: function() {
        var me = this;
        var parentCarBitCoinProductCategorys = Ext.create('WX.store.BaseData.CarBitCoinProductCategoryLiteStore');
        parentCarBitCoinProductCategorys.load({ params: { All: true } });
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 60,
                anchor: '100%'
            },
            items: [{
                xtype: 'combo',
                name: 'ParentId',
                fieldLabel: '上级分类',
                store: parentCarBitCoinProductCategorys,
                queryMode: 'local',
                displayField: 'Name',
                valueField: 'ID',
                emptyText: '请选择...',
                blankText: '请选择所属类型',
                tpl: Ext.create('Ext.XTemplate', '<tpl for=".">', '<div class="x-boundlist-item">{Code} - {Name}</div>', '</tpl>'),
                displayTpl: Ext.create('Ext.XTemplate', '<tpl for=".">', '{Code} - {Name}', '</tpl>'),
            }, {
                xtype: 'textfield',
                name: 'ID',
                hidden: true,
                hideLabel: true
            }, {
                xtype: 'textfield',
                name: 'CreatedDate',
                hidden: true,
                hideLabel: true
            }, {
                xtype: 'textfield',
                name: 'Code',
                fieldLabel: '类别代码',
                maxLength: 20,
                maxLengthText: '分区代码的最大长度为20个字符',
                blankText: '分区代码不能为空,请输入!',
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '类别名称',
                maxLength: 20,
                maxLengthText: '分区名称的最大长度为20个字符',
                blankText: '分区名称不能为空,请输入!',
                allowBlank: false,
            }, {
                xtype: 'numberfield',
                name: 'Index',
                fieldLabel: '排序',
                minValue: 0,
                allowBlank: false,
                allowDecimals: false,
                value: 1,
                //}, {
                //    xtype: 'checkbox',
                //    fieldLabel: '会员产品',
                //    name: 'IsForMember',
                //    inputValue: true,
            }]
        });
        me.items = [me.form];
        me.buttons = [{
            text: '保存',
            cls: 'submitBtn',
            action: 'save'
        }, {
            text: '取消',
            scope: me,
            handler: me.close
        }];
        me.callParent(arguments);
    }
});