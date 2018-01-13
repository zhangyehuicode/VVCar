Ext.define('WX.view.MemberCardTheme.MemberCardThemeEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.MemberCardThemeEdit',
    title: '',
    layout: 'fit',
    width: 350,
    modal: true,
    bodyPadding: 5,
    initComponent: function () {
        var me = this;
        var memberCardThemeGroupStore = Ext.create('WX.store.BaseData.MemberCardThemeGroupStore');
        memberCardThemeGroupStore.proxy.extraParams = { IsFromPortal: true };
        memberCardThemeGroupStore.load();
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 70,
                anchor: '100%',
            },
            items: [{
                xtype: 'textfield',
                name: 'CardTypeID',
                fieldLabel: '卡片类型ID',
                hidden: true,
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '主题名称',
                maxLength: 20,
                maxLengthText: '主题名称的最大长度为20个字符',
                blankText: '主题名称不能为空',
                allowBlank: false,
            }, {
                xtype: 'combobox',
                name: 'CardThemeGroupID',
                store: memberCardThemeGroupStore,
                displayField: 'Name',
                valueField: 'ID',
                fieldLabel: '主题分组',
                editable: true,
            }, {
                xtype: 'form',
                border: false,
                layout: 'hbox',
                margin: '0 0 10 0',
                items: [{
                    xtype: 'filefield',
                    fieldLabel: '主题图片',
                    allowBlank: true,
                    buttonText: '选择图片',
                }, {
                    xtype: 'button',
                    text: '上传',
                    margin: '0 0 0 5',
                    action: 'uploadthemepic',
                }]
            }, {
                html: '<div style="font-size:6px;padding-bottom:5px;padding-left:75px;">建议上传分辨率为556x334的图片，双击删除图片</div>',
            }, {
                xtype: 'box',
                name: 'ThemeShow',
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
            }, {
                xtype: 'combobox',
                name: 'IsDefault',
                store: Ext.create('WX.store.DataDict.YesNoTypeStore'),
                valueField: 'DictValue',
                displayField: 'DictName',
                fieldLabel: '是否默认',
                editable: false,
                allowBlank: false,
                value: false,
            }]
        });

        me.items = [me.form];
        me.buttons = [
            {
                text: '保存',
                cls: 'submitBtn',
                action: 'save',
            },
            {
                text: '取消',
                scope: me,
                handler: me.close,
            }
        ];

        me.callParent(arguments);
    }
});