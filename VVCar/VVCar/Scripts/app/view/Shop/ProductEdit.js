Ext.define('WX.view.Shop.ProductEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.ProductEdit',
    title: '',
    layout: 'fit',
    width: 350,
    modal: true,
    bodyPadding: 5,
    initComponent: function () {
        var me = this;
        var yesNoDictStore = Ext.create('WX.store.DataDict.YesNoTypeStore');
        //var productTypeStore = Ext.create('WX.store.DataDict.ProductTypeStore');
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 70,
                anchor: '100%'
            },
            items: [{
                xtype: 'textfield',
                name: 'ProductCategoryID',
                fieldLabel: '类别ID',
                hidden: true,
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '标题',
                maxLength: 50,
                allowBlank: false,
            }, {
                xtype: 'form',
                border: false,
                layout: 'hbox',
                margin: '0 0 10 0',
                items: [{
                    xtype: 'filefield',
                    fieldLabel: '商品图片',
                    allowBlank: true,
                    buttonText: '选择图片',
                }, {
                    xtype: 'button',
                    text: '上传',
                    margin: '0 0 0 5',
                    action: 'uploadpic',
                }]
            }, {
                xtype: 'box',
                name: 'ImgShow',
                width: 83,
                height: 50,
                margin: '0 0 10 75',
                autoEl: {
                    tag: 'img',
                    src: '',
                }
            }, {
                xtype: 'textfield',
                name: 'ImgUrl',
                fieldLabel: '图片路径',
                hidden: true,
                //}, {
                //    xtype: 'combobox',
                //    name: 'ProductType',
                //    store: productTypeStore,
                //    loadMode: 'local',
                //    fieldLabel: '产品类型',
                //    displayField: 'DictName',
                //    valueField: 'DictValue',
                //    editable: false,
            }, {
                xtype: 'numberfield',
                name: 'BasePrice',
                fieldLabel: '原单价',
                minValue: 0,
                allowBlank: false,
                value: 0,
            }, {
                xtype: 'numberfield',
                name: 'PriceSale',
                fieldLabel: '销售单价',
                minValue: 0,
                allowBlank: false,
                value: 0,
            }, {
                xtype: 'numberfield',
                name: 'Points',
                fieldLabel: '兑换积分',
                minValue: 0,
                allowBlank: false,
                value: 0,
            }, {
                xtype: 'numberfield',
                name: 'UpperLimit',
                fieldLabel: '兑换上限',
                minValue: 0,
                allowBlank: false,
                value: 0,
            }, {
                xtype: 'combobox',
                name: 'IsPublish',
                store: yesNoDictStore,
                displayField: 'DictName',
                valueField: 'DictValue',
                fieldLabel: '是否发布',
                editable: false,
                value: false,
            }, {
                xtype: 'combobox',
                name: 'IsRecommend',
                store: yesNoDictStore,
                displayField: 'DictName',
                valueField: 'DictValue',
                fieldLabel: '是否推荐',
                editable: false,
                value: false,
            }, {
                xtype: 'numberfield',
                name: 'Stock',
                fieldLabel: '库存',
                minValue: 0,
                allowBlank: false,
                value: 0,
            }, {
                xtype: 'datefield',
                name: 'EffectiveDate',
                fieldLabel: '生效时间',
                allowBlank: true,
                minValue: new Date(),
                format: 'Y-m-d H:i:s',
                width: 250,
                value: new Date(),
            }, {
                xtype: 'datefield',
                name: 'ExpiredDate',
                fieldLabel: '失效时间',
                allowBlank: true,
                minValue: new Date(),
                format: 'Y-m-d H:i:s',
                width: 250,
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