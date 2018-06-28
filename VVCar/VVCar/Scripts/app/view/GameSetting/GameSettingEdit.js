Ext.define('WX.view.GameSetting.GameSettingEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.GameSettingEdit',
    title: '游戏设置编辑',
    layout: 'fit',
    width: 360,
    bodyPadding: 5,
    modal: true,
    initComponent: function() {
        var me = this;
        var yesNoDictStore = Ext.create('WX.store.DataDict.YesNoTypeStore');
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 60,
                anchor: '100%'
            },
            items: [
                {
                    xtype: 'combobox',
                    name: 'GameType',
                    store: [
                        [0, '拓客转盘'],
                        [1, '活动转盘'],
                    ],
                    fieldLabel: '游戏类型',
                    labelWidth: 90,
                    displayField: 'DictName',
                    valueField: 'DictValue',
                    margin: '5 0 0 5',
                    editable: false,
                    readOnly: true,
                }, {
                    xtype: 'datefield',
                    name: 'StartTime',
                    fieldLabel: '游戏开始',
                    labelWidth: 90,
                    margin: '5 0 0 5',
                    allowBlank: true,
                    format: 'Y-m-d H:i:s',
                    editable: false,
                    allowBlank: false,
                    value: new Date()
                }, {

                    xtype: 'datefield',
                    name: 'EndTime',
                    fieldLabel: '游戏结束',
                    labelWidth: 90,
                    margin: '5 0 0 5',
                    allowBlank: true,
                    format: 'Y-m-d H:i:s',
                    editable: false,
                    allowBlank: false,
                    value: new Date()
                }, {
                    xtype: 'form',
                    layout: 'hbox',
                    items: [{
                        xtype: 'label',
                        margin: '13 30 0 5',
                        text: '游戏周期:',
                    }, {
                        xtype: 'numberfield',
                        margin: '5 0 0 10',
                        name: 'PeriodDays',
                        width: 80,
                        minValue: 0,
                        maxValue: 366,
                        value: 0,
                        allowBlank: false,
                    }, {
                        xtype: 'label',
                        margin: '13 0 0 5',
                        text: '天',
                    }, {
                        xtype: 'numberfield',
                        margin: '5 0 0 5',
                        name: 'PeriodCounts',
                        width: 80,
                        minValue: 0,
                        maxValue: 400,
                        value: 0,
                        allowBlank: false,
                    }, {
                        xtype: 'label',
                        margin: '13 0 0 5',
                        text: '次',
                    },]
                }, {
                    xtype: 'numberfield',
                    margin: '5 0 0 5',
                    name: 'Limit',
                    fieldLabel: '上限',
                    labelWidth: 90,
                    minValue: 0,
                    value: 0,
                    allowBlank: false,
                }, {
                    xtype: 'textfield',
                    margin: '5 0 0 5',
                    fieldLabel: '分享标题',
                    labelWidth: 90,
                    name: 'ShareTitle',
                    minValue: 0,
                    value: 0,
                    allowBlank: false,
                }, {
                    xtype: 'combobox',
                    name: 'IsAvailable',
                    store: yesNoDictStore,
                    displayField: 'DictName',
                    valueField: 'DictValue',
                    fieldLabel: '是否启用',
                    margin: '5 0 0 5',
                    labelWidth: 90,
                    editable: false,
                    allowBlank: false,
                }, {
                    xtype: 'combobox',
                    name: 'IsShare',
                    store: yesNoDictStore,
                    displayField: 'DictName',
                    valueField: 'DictValue',
                    fieldLabel: '是否可分享',
                    margin: '5 0 0 5',
                    labelWidth: 90,
                    editable: false,
                    allowBlank: false,
                }, {
                    xtype: 'form',
                    layout: 'hbox',
                    items: [{
                        xtype: 'combobox',
                        name: 'IsOrderShow',
                        store: yesNoDictStore,
                        displayField: 'DictName',
                        valueField: 'DictValue',
                        fieldLabel: '显示游戏链接',
                        margin: '5 0 0 5',
                        labelWidth: 90,
                        width: 180,
                        editable: false,
                        allowBlank: false,
                    }, {
                        xtype: 'label',
                        name: 'tip',
                        margin: '15 0 0 5',
                        html: '<span><font style="color:gray;font-size:6px">(客户消费后显示游戏链接)</font></span>',
                    }],
                },
            ]
        });
        me.items = [me.form];
        me.buttons = [{
            text: '保存',
            action: 'save',
            cls: 'submitBtn',
            scope: me,
        }, {
            text: '取消',
            scope: me,
            handler: me.close,
        }];
        me.callParent(arguments);
    }
});