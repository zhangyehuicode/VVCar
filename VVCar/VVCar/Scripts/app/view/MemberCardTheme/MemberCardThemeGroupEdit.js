Ext.define('WX.view.MemberCardTheme.MemberCardThemeGroupEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.MemberCardThemeGroupEdit',
    //layout: 'fit',
    width: 750,
    scrollDelay: true,
    autoScroll: true,
    height: 750,
    bodyPadding: 10,
    modal: true,
    initComponent: function () {
        var me = this;
        var cardThemeCategoryStore = Ext.create('WX.store.BaseData.CardThemeCategoryStore');
        var memberCardThemeGroupStore = Ext.create('WX.store.BaseData.MemberCardThemeGroupStore');
        memberCardThemeGroupStore.proxy.extraParams = { IsFromPortal: true, IsNotRecommended: true };
        memberCardThemeGroupStore.load();
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 80,
                //anchor: '100%',
            },
            items: [{
                hidden: true,
                xtype: 'textfield',
                fieldLabel: "ID",
                name: 'ID',
            }, {
                xtype: 'numberfield',
                name: 'Index',
                fieldLabel: '排序',
                width: 300,
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '主题名称',
                allowBlank: false,
                width: 300,
            }, {
                xtype: "container",
                width: "100%",
                margin: "0 0 10 0",
                layout: {
                    type: "hbox"
                },
                items: [{
                    xtype: 'combobox',
                    name: 'CardThemeCategoryID',
                    fieldLabel: '主题分组',
                    store: cardThemeCategoryStore,
                    valueField: 'ID',
                    displayField: 'Name',
                    editable: false,
                    allowBlank: false,
                    width: 300,
                }, {
                    xtype: 'combobox',
                    name: 'RecommendGroupID',
                    fieldLabel: '推荐主题',
                    store: memberCardThemeGroupStore,
                    valueField: 'ID',
                    displayField: 'Name',
                    editable: false,
                    allowBlank: true,
                    width: 300,
                    margin: "0 0 0 20",
                }]
            }, {
                xtype: 'form',
                name: 'ChooseImgform',
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
                    name: 'uploadthemepic',
                    action: 'uploadthemepic',
                }]
            }, {
                html: '<div style="font-size:6px;padding-bottom:5px;padding-left:85px;">建议上传分辨率为640x540的图片，最多支持12张卡面，双击删除图片</div>',
            }, {
                xtype: 'container',
                name: 'imgcontainer',  //图片父类
                width: "100%",
                columns: 6,
                margin: '0 0 10 75',
                layout: {
                    //type: "hbox"
                },
                scrollable: 'x',
                items: [{

                }]
            }, {
                xtype: 'textfield',
                name: 'ImgUrl',
                fieldLabel: '图片路径',
                hidden: true,
            }, {
                hidden: true,
                xtype: 'textfield',
                fieldLabel: "存放门店Code",
                name: 'DepartmentCode',
            }, {
                xtype: "container",
                width: "100%",
                margin: "0 0 10 0",
                layout: {
                    type: "hbox"
                },
                items: [{
                    xtype: "label",
                    forId: "department",
                    text: "适用门店:"
                }, {
                    xtype: 'button',
                    name: 'chosedepartment',
                    action: "chosedepartment",
                    text: "列表选择",
                    margin: "0 0 0 25",
                }]
            }, {
                xtype: 'container',
                width: "100%",
                margin: "0 0 10 0",
                layout: {
                    type: "hbox"
                },
                items: [{
                    xtype: 'radiogroup',
                    fieldLabel: '有效期限',
                    columns: 1,
                    vertical: true,
                    name: 'EffectiveDate',
                    simpleValue: true,
                    padding: '0 10 0 0',
                    items: [{
                        boxLabel: '固定日期',
                        inputValue: 0,
                        name: 'DateType',
                    }, {
                        boxLabel: '自定义',
                        name: 'DateType',
                        inputValue: 1,
                        padding: '10 0 0 0',
                    }]
                }, {
                    xtype: 'container',
                    width: "100%",
                    margin: "0 0 0 10",
                    layout: {
                        type: "vbox"
                    },
                    items: [{
                        xtype: 'container',
                        width: "100%",
                        name: 'FixationDateTypeCon',
                        margin: "0 0 10 0",
                        layout: {
                            type: "hbox"
                        },
                        items: [{
                            xtype: "datefield",
                            name: 'GiftCardStartTime',
                            fieldLabel: "生效日期",
                            allowBlank: true,
                            minValue: new Date(),
                            format: "Y-m-d",
                            width: 200,
                            labelWidth: 60,
                        }, {
                            xtype: "datefield",
                            name: 'GiftCardEndTime',
                            fieldLabel: "截止日期",
                            allowBlank: true,
                            minValue: new Date(),
                            format: "Y-m-d",
                            width: 200,
                            labelWidth: 60,
                            margin: "0 0 0 10",
                        }]
                    }, {
                        xtype: 'container',
                        width: "100%",
                        name: 'CustomDateTypeCon',
                        margin: "42 0 0 0",
                        hidden: true,
                        layout: {
                            type: "hbox"
                        },
                        items: [{
                            xtype: 'label',
                            text: '购买后',
                            padding: '8 0 0 0',
                        }, {
                            xtype: 'numberfield',
                            width: 80,
                            name: 'EffectiveDaysOfAfterBuy',
                            padding: '0 0 0 10',
                            minValue: 0,
                        }, {
                            xtype: 'label',
                            text: '天生效，有效天数',
                            padding: '8 0 0 10',
                        }, {
                            xtype: 'numberfield',
                            width: 90,
                            name: 'EffectiveDays',
                            padding: '0 0 0 10',
                            minValue: 0,
                        }, {
                            xtype: 'label',
                            text: '天',
                            padding: '8 0 0 10',
                        }]
                    }]
                }]
            }, {
                //    xtype: 'container',
                //    width: "100%",
                //    margin: "0 0 10 0",
                //    layout: {
                //        type: "hbox"
                //    },
                //    items: [{
                //        xtype: "datefield",
                //        name: 'GiftCardStartTime',
                //        fieldLabel: "开始日期",
                //        allowBlank: false,
                //        minValue: new Date(),
                //        format: "Y-m-d",//日期的格式
                //        width: 250
                //    }, {
                //        xtype: "datefield",
                //        name: 'GiftCardEndTime',
                //        fieldLabel: "截止日期",
                //        allowBlank: false,
                //        minValue: new Date(),
                //        format: "Y-m-d",//日期的格
                //        width: 250,
                //        margin: "0 0 0 10",
                //    }]
                //}, {
                //    items: [{
                //        xtype: 'radiogroup',
                //        fieldLabel: '可用时间段',
                //        columns: 1,
                //        vertical: true,
                //        name: 'TimeSlot',
                //        simpleValue: true,
                //        items: [{
                //            boxLabel: '全部时段',
                //            id: 'TimeSlotsALL',
                //            inputValue: true,
                //            name: 'TimeSlots',
                //        }, {
                //            boxLabel: '部分时段',
                //            id: 'TimeSlotsSome',
                //            name: 'TimeSlots',
                //            inputValue: false,
                //        }]
                //    }]
                //}, {
                //    xtype: "container",
                //    width: "100%",
                //    id: 'weekcontainer',
                //    hidden: true,
                //    margin: "0 0 0 80",
                //    layout: {
                //        type: "hbox"
                //    },
                //    items: [{
                //        xtype: "label",
                //        forId: "department",
                //        text: "星期:",
                //    }, {
                //        items: [{
                //            xtype: 'checkboxgroup',
                //            name: 'weeks',
                //            id: 'weeks',
                //            // defaultType: 'checkbox',
                //            layout: 'hbox',
                //            margin: "-8 0 0 0",
                //            items: [
                //                { boxLabel: '周一', name: 'week1', inputValue: '1', margin: "0 0 0 10" },
                //                { boxLabel: '周二', name: 'week2', inputValue: '2', margin: "0 0 0 10" },
                //                { boxLabel: '周三', name: 'week3', inputValue: '3', margin: "0 0 0 10" },
                //                { boxLabel: '周四', name: 'week4', inputValue: '4', margin: "0 0 0 10" },
                //                { boxLabel: '周五', name: 'week5', inputValue: '5', margin: "0 0 0 10" },
                //                { boxLabel: '周六', name: 'week6', inputValue: '6', margin: "0 0 0 10" },
                //                { boxLabel: '周日', name: 'week7', inputValue: '7', margin: "0 0 0 10" },
                //            ]
                //        }]
                //    }]
                //}, {
                //    id: 'addTimeCon',
                //    xtype: "container",
                //    hidden: true,
                //    width: "100%",
                //    margin: "0 0 10 80",
                //    layout: {
                //        type: "hbox"
                //    },
                //    items: [{
                //        xtype: "label",
                //        name: 'timelabel',
                //        text: "时间:",
                //        hidden: true,
                //        margin: "5 0 0 0",
                //    }, {
                //        xtype: "container",
                //        name: 'AllShowTimecontainer', //添加元素父类container
                //        width: "100%",
                //        items: [

                //        ]
                //    }]
                //}, {
                //    xtype: "container",
                //    name: 'AddNewTimeCon',
                //    margin: "0 0 0 280",
                //    layout: {
                //        type: "hbox"
                //    },
                //    items: [{
                //        id: 'AddtimeLab',
                //        name: 'AddNewTime',
                //        hidden: true,
                //        xtype: 'button',
                //        text: '添加时间段',
                //        action: 'AddNewTime',
                //    }]
                //}, {
                //    id: 'shoumessage',
                //    hidden: true,
                //    html: '<div style="font-size:6px;padding-bottom:5px;padding-left:200px;padding-top:5px;">请使用24小时制输入时间，格式如：11:00至14:30</div>',
                //}, {
                xtype: "label",
                forId: "Addtime",
                text: "规则说明:",
                padding: "0 0 20 0 ",
            }, {
                xtype: 'htmleditor',
                name: 'RuleDescription',
                anchor: '100%',
                width: "100%",
                padding: "10 0 0 0 ",
                height: 200,
                enableFont: false
            },]
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
            handler: me.close
        }];
        me.callParent(arguments);
    }
});