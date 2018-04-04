Ext.define('WX.view.Shop.ProductCategoryEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.ProductCategoryEdit',
    title: '编辑类别',
    layout: 'fit',
    width: 370,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        var parentProductCategorys = Ext.create('WX.store.BaseData.ProductCategoryLiteStore');
        parentProductCategorys.load({ params: { All: true } });
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 100,
                anchor: '100%'
            },
            items: [{
                xtype: 'combo',
                name: 'ParentId',
                fieldLabel: '上级分类',
                store: parentProductCategorys,
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
                fieldLabel: 'Pos排序',
                minValue: 0,
                allowBlank: false,
                allowDecimals: false
            }, {
                xtype: 'checkbox',
                fieldLabel: '零售显示',
                name: 'IsPosUsed',
                blankText: '是否在零售端显示!',
                inputValue: true,
            }, {
                xtype: 'checkbox',
                fieldLabel: '会员产品',
                name: 'IsForMember',
                inputValue: true,
            }, {
                xtype: 'checkbox',
                fieldLabel: '强制选择',
                name: 'IsRequiredOrderAlert',
                inputValue: true,
                value: false,
            }, {
                xtype: 'textareafield',
                name: 'RequiredOrderAlertText',
                fieldLabel: '强制选择提醒语',
                maxLength: 64,
                hidden: true,
            }, {
                xtype: 'checkbox',
                fieldLabel: '重复下单提醒',
                name: 'IsOverOrderAlert',
                inputValue: true,
                value: false,
            }, {
                xtype: 'radiogroup',
                name: 'radioOverOrderAlertType',
                width: 160,
                labelWidth: 60,
                padding: '0 0 0 100',
                hidden: true,
                items: [
                    { boxLabel: '普通提醒', name: 'OverOrderAlertType', inputValue: 0 },
                    { boxLabel: '强制唯一', name: 'OverOrderAlertType', inputValue: 1, },
                ]
            }, {
                xtype: 'textareafield',
                name: 'OverOrderAlertText',
                fieldLabel: '重复下单提醒语',
                maxLength: 64,
                hidden: true,
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