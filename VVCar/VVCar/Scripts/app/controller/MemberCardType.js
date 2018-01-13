Ext.define('WX.controller.MemberCardType', {
    extend: 'Ext.app.Controller',
    requires: ['WX.store.BaseData.MemberCardTypeStore'],
    stores: ['DataDict.YesNoTypeStore', 'BaseData.MemberCardThemeStore', 'BaseData.MemberCardThemeGroupStore', 'BaseData.CardThemeCategoryStore',
        'BaseData.DepartmentStore'],
    models: ['BaseData.MemberCardTypeModel', 'BaseData.MemberCardThemeModel', 'BaseData.MemberCardThemeGroupModel', 'BaseData.CardThemeCategoryModel',
        'BaseData.DepartmentModel'],
    views: ['MemberCardType.MemberCardTypeList', 'MemberCardType.MemberCardTypeEdit', 'MemberCardTheme.MemberCardTheme', 'MemberCardTheme.MemberCardThemeEdit', 'MemberCardTheme.MemberCardThemeGroup', 'MemberCardTheme.MemberCardThemeGroupEdit',
        'MemberCardTheme.MenberChoseDepartmentEdit'],
    refs: [{
        ref: 'MenberChoseDepartmentEdit',
        selector: 'MenberChoseDepartmentEdit'
    }, {
        ref: 'MemberCardTypeList',
        selector: 'MemberCardTypeList'
    }, {
        ref: 'MemberCardTypeEdit',
        selector: 'MemberCardTypeEdit'
    }, {
        ref: 'txtMaxRecharge',
        selector: 'MemberCardTypeEdit numberfield[name=MaxRecharge]'
    }, {
        ref: 'MemberCardThemeGrid',
        selector: 'MemberCardTheme grid'
    }, {
        ref: 'MemberCardThemeEdit',
        selector: 'MemberCardThemeEdit'
    }, {
        ref: 'MemberCardThemeCategoryTree',
        selector: 'MemberCardTheme treelist[name=cardcategorytree]'
    }, {
        ref: 'MemberCardThemeGroupEdit',
        selector: 'MemberCardThemeGroupEdit',
    }, {
        ref: 'MemberCardThemeGroup',
        selector: 'MemberCardThemeGroup',
    }, {
        ref: 'MemberCardThemeGroupGrid',
        selector: 'MemberCardThemeGroup grid',
    }, {
        ref: 'MemberCardTheme',
        selector: 'MemberCardTheme',
    }],
    init: function () {
        var me = this;
        me.control({
            'MemberCardThemeGroupEdit radiogroup[name=TimeSlot]': {
                change: me.TimeSlot
            },
            'MemberCardThemeGroupEdit button[action=AddNewTime]': {
                click: me.AddNewTime
            },
            'MemberCardThemeGroupEdit button[action=Deletetimes]': {
                click: me.Deletetimes
            },
            'MemberCardThemeGroupEdit button[action=chosedepartment]': {
                click: me.chosedepartment
            },
            'MemberCardThemeGroupEdit combobox[name=CardThemeCategoryID]': {
                change: me.JudgeGrade
            },
            'MemberCardThemeGroupEdit ': {
                onclose: me.MemberCardThemeGroupEditonclose
            },
            'MemberCardTypeList button[action=addMemberCardType]': {
                click: me.addMemberCardType
            },
            'MemberCardTypeList button[action=editMemberCardType]': {
                click: me.editMemberCardType
            },
            'MemberCardTypeList button[action=search]': {
                click: me.searchData
            },
            'MemberCardTypeList button[action=theme]': {
                click: me.theme
            },
            'MemberCardTypeList': {
                itemdblclick: me.showEdit
            },
            'MemberCardTypeEdit button[action=save]': {
                click: me.saveMemberCardType
            },
            'MemberCardTypeEdit combobox[name=AllowRecharge]': {
                change: me.onAllowRechargeChange,
            },
            'MemberCardThemeGroupEdit box[action=deleteImg]': {
                // click: me.deleteImg,
            },
            'MemberCardTheme button[action=add]': {
                //click: me.addTheme
                click: me.addMemberCardThemeGroup
            },
            'MemberCardTheme button[action=edit]': {
                //click: me.editTheme
                click: me.editMemberCardThemeGroup
            },
            'MemberCardTheme button[action=refresh]': {
                click: me.refreshTheme
            },
            'MemberCardTheme button[action=cardthemegroupmanager]': {
                click: me.cardThemeGroupManager
            },
            'MemberCardTheme button[action=delete]': {
                click: me.deleteMemberCardThemeGroup
            },
            'MemberCardTheme grid': {
                // itemdblclick: me.editThemeAction
                itemdblclick: me.editThemeGroupAction
            },
            'MemberCardThemeEdit button[action=uploadthemepic]': {
                click: me.uploadthemepic
            },
            'MemberCardThemeGroupEdit button[action=uploadthemepic]': {
                click: me.uploadthemepic
            },
            'MemberCardThemeEdit button[action=save]': {
                click: me.saveMemberTheme
            },
            'MenberChoseDepartmentEdit button[action=save]':
            {
                click: me.saveChooseDeaprtment
            },
            'MemberCardTheme': {
                escActionClick: me.escActionClick,
                descActionClick: me.descActionClick,
                deleteActionClick: me.deleteActionClick,
                enableThemeActionClick: me.enableThemeActionClick,
                disableThemeActionClick: me.disableThemeActionClick,
            },
            'MemberCardThemeGroup button[action=add]': {
                click: me.addMemberCardThemeGroup
            },
            'MemberCardThemeGroup button[action=edit]': {
                click: me.editMemberCardThemeGroup
            },

            'MemberCardThemeGroup button[action=delete]': {
                click: me.deleteMemberCardThemeGroup
            },
            'MemberCardThemeGroup grid': {
                itemdblclick: me.editThemeGroupAction
            },
            'MemberCardThemeGroupEdit button[action=save]': {
                click: me.saveMemberCardThemeGroup
            },
            'MemberCardTheme treelist[name=cardcategorytree]': {
                itemclick: me.cardCategoryTreeItemClick
            },
            'MemberCardThemeGroupEdit radiogroup[name=EffectiveDate]': {
                change: me.effectiveDateRadioGroupChange
            },
        });
    },
    effectiveDateRadioGroupChange: function (radiogroup, newValue, oldValue, eOpts) {
        var me = this;
        var memberCardThemeGroupEdit = me.getMemberCardThemeGroupEdit();
        if (newValue == 0) {
            memberCardThemeGroupEdit.down('container[name=CustomDateTypeCon]').hide();
            memberCardThemeGroupEdit.down('container[name=FixationDateTypeCon]').show();
            memberCardThemeGroupEdit.down('datefield[name=GiftCardStartTime]').allowBlank = false;
            memberCardThemeGroupEdit.down('datefield[name=GiftCardEndTime]').allowBlank = false;
            memberCardThemeGroupEdit.down('numberfield[name=EffectiveDaysOfAfterBuy]').allowBlank = true;
            memberCardThemeGroupEdit.down('numberfield[name=EffectiveDays]').allowBlank = true;
            memberCardThemeGroupEdit.down('numberfield[name=EffectiveDaysOfAfterBuy]').setValue();
            memberCardThemeGroupEdit.down('numberfield[name=EffectiveDays]').setValue();
        } else {
            memberCardThemeGroupEdit.down('container[name=FixationDateTypeCon]').hide();
            memberCardThemeGroupEdit.down('container[name=CustomDateTypeCon]').show();
            memberCardThemeGroupEdit.down('numberfield[name=EffectiveDaysOfAfterBuy]').allowBlank = false;
            memberCardThemeGroupEdit.down('numberfield[name=EffectiveDays]').allowBlank = false;
            memberCardThemeGroupEdit.down('datefield[name=GiftCardStartTime]').allowBlank = true;
            memberCardThemeGroupEdit.down('datefield[name=GiftCardEndTime]').allowBlank = true;
            memberCardThemeGroupEdit.down('datefield[name=GiftCardStartTime]').setValue();
            memberCardThemeGroupEdit.down('datefield[name=GiftCardEndTime]').setValue();
        }
    },
    refreshTheme: function (btn) {
        var me = this;
        var themeStore = me.getMemberCardThemeGrid().getStore();
        Ext.apply(themeStore.proxy.extraParams, { CardThemeGroupID: '' });
        themeStore.currentPage = 1;
        themeStore.load();
    },
    cardCategoryTreeItemClick: function (sender, info, eOpts) {
        var me = this;
        var nodeInfo = info.node.data;
        var themeStore = me.getMemberCardThemeGrid().getStore();
        if (nodeInfo.leaf) {
            Ext.apply(themeStore.proxy.extraParams, { ID: nodeInfo.ID, CardThemeCategoryID: null });
            themeStore.currentPage = 1;
            themeStore.load();
        } else {
            Ext.apply(themeStore.proxy.extraParams, { ID: null, CardThemeCategoryID: nodeInfo.ID });
            themeStore.currentPage = 1;
            themeStore.load();
        }
    },
    MemberCardThemeGroupEditonclose: function () {
        Ext.Msg.alert('提示', '请先选中一条数据');
    },
    deleteMemberCardThemeGroup: function (btn) {
        var me = this;
        var grid = btn.up('window').down('grid');
        var selectedItems = grid.getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert('提示', '请先选中一条数据');
            return;
        }
        Ext.Msg.alert('提示', '确定要删除吗？', function (optional) {
            if (optional == "ok") {
                var store = grid.getStore();
                if (selectedItems[0].data.CardThemeCategoryID == "00000000-0000-0000-0000-000000000001") {
                    Ext.Msg.alert('提示', '推广海报不允许删除！！');
                    return;
                }
                var data = {
                    ID: selectedItems[0].data.ID,
                }
                store.DeleteGiftCardTem(data, success, failure);
                function success(response) {
                    Ext.Msg.hide();
                    response = JSON.parse(response.responseText);
                    if (!response.IsSuccessful) {
                        Ext.Msg.alert('提示', response.ErrorMessage);
                        return;
                    }
                    Ext.Msg.alert('提示', '删除成功！');
                    store.removeAll();
                    me.getMemberCardThemeCategoryTree().getStore().removeAll();
                    me.getMemberCardThemeCategoryTree().getStore().load();

                    store.load();

                };
                function failure(response) {
                    Ext.Msg.hide();
                    Ext.Msg.alert('提示', response.responseText);
                };
            }
        });
    },
    saveChooseDeaprtment: function (btn) {
        var Codes = "";
        var me = this;
        var win = btn.up('window');
        var MemberCardThemeGroupEdit = me.getMemberCardThemeGroupEdit();
        var MenberChoseDepartmentEdit = me.getMenberChoseDepartmentEdit();
        var records = MenberChoseDepartmentEdit.grid.getSelection();

        for (var i = 0; i < MenberChoseDepartmentEdit.grid.getSelection().length; i++) {
            if (i == 0) {
                Codes += MenberChoseDepartmentEdit.grid.getSelection()[i].data.Code;
            }
            else {
                Codes += ";" + MenberChoseDepartmentEdit.grid.getSelection()[i].data.Code;
            }

        }

        MemberCardThemeGroupEdit.down('textfield[name = DepartmentCode]').setValue(Codes);

        MemberCardThemeGroupEdit.down('button[name = chosedepartment]').setText('列表选择(已选' + MenberChoseDepartmentEdit.grid.getSelection().length + ')');

        win.close();

    },
    saveMemberCardThemeGroup: function (btn) {
        var me = this;
        var MemberCardThemeGroupEdit = me.getMemberCardThemeGroupEdit();
        var win = btn.up('window');
        var form = win.form.getForm();
        //var weekValue = MemberCardThemeGroupEdit.down('checkboxgroup[name = weeks]').getChecked();
        var RuleDescription = MemberCardThemeGroupEdit.down('htmleditor').value;  //EXT富文本内容
        //var TimeNum = MemberCardThemeGroupEdit.down('container[name=AllShowTimecontainer]').items.length;
        var CardThemeGroupUseTimeitems = [];
        var UrlValue = [];
        var checkIndex = win.down('numberfield[name=Index]').getValue();
        if (checkIndex < 1) {
            Ext.Msg.alert('错误', '序号不能小于1');
            return;
        }
        //var CheckTimeSlot = win.down('radiogroup[name=TimeSlot]').getValue();
        //if (CheckTimeSlot == false) {
        //    var checkWeek = weekValue;
        //    if (checkWeek == null || checkWeek == "") {
        //        Ext.Msg.alert('提示', '部分时间段必须选择星期');
        //        return;
        //    }
        //}
        var DepartmentCodes = win.down('textfield[name=DepartmentCode]').getValue();
        if (DepartmentCodes == null || DepartmentCodes == "") {
            Ext.Msg.alert('提示', '必须选择至少一个适用门店');
            return;
        }
        //时间子表数据
        //for (var i = 1; i < TimeNum + 1; i++) {
        //    var ChooesStarTime = MemberCardThemeGroupEdit.down('container[name=AllShowTimecontainer]').down('container[name=timecontainer' + i + ']').down('textfield[name=ChooesStarTime' + i + ']').value;
        //    var ChooesEenTime = MemberCardThemeGroupEdit.down('container[name=AllShowTimecontainer]').down('container[name=timecontainer' + i + ']').down('textfield[name=ChooesEenTime' + i + ']').value;
        //    var cst = (ChooesStarTime + ":00").match(/^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/);;
        //    var ced = (ChooesEenTime + ":00").match(/^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/);;
        //    if (cst == null || ced == null) {
        //        Ext.Msg.alert('提示', '输入的参数不是时间格式');
        //        return;
        //    }
        //    if (cst[1] > 24 || cst[3] > 60 || cst[4] > 60 || ced[1] > 24 || ced[3] > 60 || ced[4] > 60) {
        //        Ext.Msg.alert('提示', '时间格式不对');
        //        return;
        //    }
        //    CardThemeGroupUseTimeitems.push({
        //        'BeginTime': ChooesStarTime,
        //        'EndTime': ChooesEenTime,
        //    });
        //}
        for (var i = 1; i < MemberCardThemeGroupEdit.down('container[name = imgcontainer]').items.items.length; i++) {
            UrlValue.push({
                'index': i + 1,
                'CardTypeID': '00000000-0000-0000-0000-000000000003',
                'Name': '礼品卡主题',
                'ImgUrl': MemberCardThemeGroupEdit.down('container[name = imgcontainer]').items.items[i].autoEl.src,
            });
        }
        var CardThemeCategoryIDS = win.down('combobox[name=CardThemeCategoryID]').getValue();
        if (CardThemeCategoryIDS == "00000000-0000-0000-0000-000000000001" && MemberCardThemeGroupEdit.down('container[name = imgcontainer]').items.items.length > 2) {
            Ext.Msg.alert('提示', '推荐海报只能有一个主题图片');
            return;
        }
        if (CardThemeCategoryIDS == "00000000-0000-0000-0000-000000000001" && MemberCardThemeGroupEdit.down('container[name = imgcontainer]').items.items.length < 2) {
            Ext.Msg.alert('提示', '推荐海报必须有主题图片');
            return;
        }
        //var weekitem = "";
        //Ext.Array.each(weekValue, function (item) {
        //    weekitem += item.inputValue + ";";
        //});
        //weekitem = weekitem.substr(0, weekitem.length - 1);
        var formValues = form.getValues();
        var store = me.getMemberCardTheme().down('grid').getStore();
        function success(response) {
            Ext.Msg.hide();
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert('提示', response.ErrorMessage);
                return;
            }
            Ext.Msg.alert('提示', '修改成功！');
            store.load();
            win.close();
            store.rejectChanges();
            me.getMemberCardThemeCategoryTree().getStore().removeAll();
            me.getMemberCardThemeCategoryTree().getStore().load();
        };
        function successAdd(response) {
            Ext.Msg.hide();
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert('提示', response.ErrorMessage);
                return;
            }
            Ext.Msg.alert('提示', '添加成功！');
            store.load();
            win.close();
            store.rejectChanges();
            me.getMemberCardThemeCategoryTree().getStore().removeAll();
            me.getMemberCardThemeCategoryTree().getStore().load();
        };
        function failure(response) {
            Ext.Msg.hide();
            Ext.Msg.alert('提示', response.responseText);
        };
        if (form.isValid()) {
            var store = me.getMemberCardTheme().down('grid').getStore();
            var MemberCardThemeGroupEdit = me.getMemberCardThemeGroupEdit();
            var AllShowTimecontainer = MemberCardThemeGroupEdit.down('container[name = AllShowTimecontainer]');
            if (form.actionMethod == 'POST') {
                var data = {
                    Index: win.down('numberfield[name=Index]').getValue(),
                    Name: win.down('textfield[name=Name]').getValue(),
                    CardThemeCategoryID: win.down('combobox[name=CardThemeCategoryID]').getValue(),
                    RecommendGroupID: win.down('combobox[name=RecommendGroupID]').getValue(),
                    DepartmentCode: win.down('textfield[name=DepartmentCode]').getValue(),
                    GiftCardStartTime: win.down('textfield[name=GiftCardStartTime]').getValue(),
                    GiftCardEndTime: win.down('datefield[name=GiftCardEndTime]').getValue(),
                    TimeSlot: true,//win.down('radiogroup[name=TimeSlot]').getValue(),
                    week: '',//weekitem,
                    RuleDescription: RuleDescription,
                    UseTimeList: CardThemeGroupUseTimeitems,
                    MemberCardThemeList: UrlValue,
                    EffectiveDaysOfAfterBuy: win.down('numberfield[name=EffectiveDaysOfAfterBuy]').getValue(),
                    EffectiveDays: win.down('numberfield[name=EffectiveDays]').getValue(),
                    IsNotFixationDate: win.down('radiogroup[name=EffectiveDate]').getValue() == 1,
                };
                store.newAdd(data, successAdd, failure);
            } else {
                if (!form.isDirty()) {
                    win.close();
                    return;
                }
                var data = {
                    ID: formValues.ID,
                    Index: win.down('numberfield[name=Index]').getValue(),
                    Name: win.down('textfield[name=Name]').getValue(),
                    CardThemeCategoryID: win.down('combobox[name=CardThemeCategoryID]').getValue(),
                    RecommendGroupID: win.down('combobox[name=RecommendGroupID]').getValue(),
                    DepartmentCode: win.down('textfield[name=DepartmentCode]').getValue(),
                    GiftCardStartTime: win.down('textfield[name=GiftCardStartTime]').getValue(),
                    GiftCardEndTime: win.down('datefield[name=GiftCardEndTime]').getValue(),
                    TimeSlot: true,//win.down('radiogroup[name=TimeSlot]').getValue(),
                    week: '',//weekitem,
                    RuleDescription: RuleDescription,
                    MemberCardThemeList: UrlValue,
                    UseTimeList: CardThemeGroupUseTimeitems,
                    EffectiveDaysOfAfterBuy: win.down('numberfield[name=EffectiveDaysOfAfterBuy]').getValue(),
                    EffectiveDays: win.down('numberfield[name=EffectiveDays]').getValue(),
                    IsNotFixationDate: win.down('radiogroup[name=EffectiveDate]').getValue() == 1,
                };
                store.newUpp(data, success, failure);
            }
        }
    },
    chosedepartment: function (btn) {
        var me = btn.up('window');
        var mes = this;
        var MemberCardThemeGroupEdit = mes.getMemberCardThemeGroupEdit();
        var DepartmentCode = MemberCardThemeGroupEdit.down('textfield[name = DepartmentCode]').value;
        var DepartmentCodeArray = DepartmentCode.split(";");
        var win = Ext.widget('MenberChoseDepartmentEdit');
        var MenberChoseDepartmentEdit = mes.getMenberChoseDepartmentEdit();
        var deptGrid = MenberChoseDepartmentEdit.down('grid');
        var deptStore = deptGrid.getStore();
        var selectedDept = [];
        for (var j = 0; j < DepartmentCodeArray.length; j++) {
            DepartmentCodeArrayData = DepartmentCodeArray[j];
        }
        deptGrid.getStore().on('load', function (records, operation, success) {
            for (var i = 0; i < records.data.items.length; i++) {

                for (var j = 0; j < DepartmentCodeArray.length; j++) {
                    DepartmentCodeArrayData = DepartmentCodeArray[j];
                    if (records.data.items[i].data.Code == DepartmentCodeArrayData)
                        selectedDept.push(records.data.items[i]);
                    deptGrid.getSelectionModel().select(selectedDept);
                }
            }
        });
        win.show();
    },
    addMemberCardThemeGroup: function (btn) {
        var win = Ext.widget('MemberCardThemeGroupEdit');
        win.down('radiogroup[name=EffectiveDate]').setValue(0);
        win.form.getForm().actionMethod = 'POST';
        win.setTitle('添加卡片主题');
        //Ext.getCmp('TimeSlotsALL').setValue(true);
        win.show();
    },
    editMemberCardThemeGroup: function (btn) {
        var me = this;
        var grid = btn.up('window').down('grid');
        var selectedItems = grid.getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert('提示', '请先选中一条数据');
            return;
        }
        me.editThemeGroupAction(grid, selectedItems[0]);
    },
    editThemeGroupAction: function (grid, record) {
        var me = this;
        var win = Ext.widget('MemberCardThemeGroupEdit');
        win.form.loadRecord(record);
        win.down('radiogroup[name=EffectiveDate]').setValue(record.data.IsNotFixationDate ? 1 : 0);
        win.form.getForm().acrecordtionMethod = "PUT";
        var ID = record.data.ID;
        var DepartmentCode = record.data.DepartmentCode;
        var DepartmentCodeArray = DepartmentCode.split(";");
        var week = record.data.week;
        var weekArray = week.split(";");
        var weeknym = weekArray.length;
        var checkValues = {};
        //for (var i = 0; i < weeknym; i++) {
        //    weekArray[i];
        //    var checkitems = win.down('checkboxgroup[name = weeks]').config.items;
        //    for (var j = 0; j < checkitems.length; j++) {
        //        if (checkitems[j].inputValue == weekArray[i]) {
        //            switch (weekArray[i]) {
        //                case '1':
        //                    checkValues.week1 = true;
        //                    break;
        //                case '2':
        //                    checkValues.week2 = true;
        //                    break;
        //                case '3':
        //                    checkValues.week3 = true;
        //                    break;
        //                case '4':
        //                    checkValues.week4 = true;
        //                    break;
        //                case '5':
        //                    checkValues.week5 = true;
        //                    break;
        //                case '6':
        //                    checkValues.week6 = true;
        //                    break;
        //                case '7':
        //                    checkValues.week7 = true;
        //                    break;
        //            }
        //        }
        //    }
        //}
        //console.log(checkValues);
        //win.down('checkboxgroup[name = weeks]').setValue(checkValues);
        //var TimeSlotData = record.data.TimeSlot;
        //if (TimeSlotData == "true") {
        //    var checkValuess = {};
        //    checkValuess.TimeSlotsALL = true;
        //    Ext.getCmp('TimeSlotsALL').setValue(true);
        //    me.TimeSlot(win, true, false);
        //}
        //else {
        //    var checkValuess = {};
        //    checkValuess.TimeSlotsSome = true;
        //    Ext.getCmp('TimeSlotsSome').setValue(true);
        //    me.TimeSlot(win, false, true);
        //}
        win.setTitle("编辑卡片主题");
        win.down('button[name = chosedepartment]').setText('列表选择(已经' + DepartmentCodeArray.length + ')');
        dataSearchTime = { ID: record.data.ID }
        var store = me.getMemberCardTheme().down('grid').getStore();
        store.SearchTime(dataSearchTime, success, failure)
        function success(response) {
            Ext.Msg.hide();
            response = JSON.parse(response.responseText);
            var datas = response.Data;
            //Ext.Msg.alert('数据是', datas);
            //for (var i = 0; i < datas.length; i++) {
            //    win.down('label[name=timelabel]').show();
            //    var num = win.down('container[name=AllShowTimecontainer]').items.length;
            //    num++;
            //    var AddNewTimeCon = win.down('container[name=AllShowTimecontainer]');
            //    var newPanle = {
            //        xtype: "container",
            //        margin: "0 0 10 10",
            //        width: "100%",
            //        name: 'timecontainer' + num,
            //        layout: {
            //            type: "hbox"
            //        },
            //        items: [{
            //            xtype: 'textfield',
            //            value: datas[i].BeginTime,
            //            name: 'ChooesStarTime' + num,
            //            allowBlank: false,
            //        }, {
            //            xtype: "label",
            //            margin: "5 20 0 20",
            //            text: "至:",
            //        }, {
            //            xtype: 'textfield',
            //            value: datas[i].EndTime,
            //            name: 'ChooesEenTime' + num,
            //            allowBlank: false,
            //        }, {
            //            xtype: 'button',
            //            name: 'Deletetimes',
            //            action: 'Deletetimes',
            //            text: "删除时间段",
            //            margin: "0 0 0 20",
            //        }]
            //    };
            //    AddNewTimeCon.add(newPanle);
            //}
            if (record.data.CardThemeCategoryID == "00000000-0000-0000-0000-000000000001") {
                win.down('combobox[name = CardThemeCategoryID]').disable();
            }
            store.load();
        };
        function failure(response) {
            Ext.Msg.hide();
            Ext.Msg.alert('提示', response.responseText);
        };
        //读取生成图片
        var dataSearchImg = { ID: record.data.ID }
        store.SearchImg(dataSearchTime, successImg, failure)
        function successImg(response) {
            Ext.Msg.hide();
            response = JSON.parse(response.responseText);
            var datas = response.Data;
            var UrlItem = "";
            for (var i = 0; i < datas.length; i++) {
                var num = win.down('container[name=imgcontainer]').items.length;
                num++;
                var AddNewTimeCon = win.down('container[name=imgcontainer]');
                var newPanle = {
                    xtype: 'box',
                    name: 'ThemeShow' + num,
                    width: 83,
                    height: 50,
                    margin: '0 0 10 10',
                    action: 'deleteImg',
                    autoEl: { tag: 'img', src: datas[i].ImgUrl, },
                    listeners: {
                        dblclick: {
                            element: 'el',
                            fn: function (box) {

                                var removecontainer = this.component;
                                var memberCardThemeGroupEdit = me.getMemberCardThemeGroupEdit();
                                var reNewTimeCon = memberCardThemeGroupEdit.down('container[name=imgcontainer]');
                                Ext.Msg.alert('提示', '确定要删除吗？', function (optional) {
                                    if (optional == "ok") {
                                        reNewTimeCon.remove(removecontainer);
                                        Ext.Msg.alert('提示', '删除图片成功');
                                    }
                                });
                            }
                        }
                    }
                }
                if (i == 0) {
                    UrlItem += datas[i].ImgUrl;
                }
                else {
                    UrlItem += ";" + datas[i].ImgUrl;
                }
                var memberCardThemeGroupEdit = me.getMemberCardThemeGroupEdit();
                memberCardThemeGroupEdit.down('textfield[name=ImgUrl]').setValue(UrlItem);
                AddNewTimeCon.add(newPanle);
            };
        }
        win.show();
        // this.initUE();
    },
    cardThemeGroupManager: function (btn) {
        var me = this;
        var win = Ext.widget('MemberCardThemeGroup');
        win.show();
    },
    enableThemeActionClick: function (grid, record) {
        var store = grid.getStore();
        var data = {
            ID: record.data.ID,
        }
        store.setAvailable(data, success, failure);
        function success(response) {
            Ext.Msg.hide();
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert('提示', response.ErrorMessage);
                return;
            }
            Ext.Msg.alert('提示', '启用成功！');
            store.load();
        };
        function failure(response) {
            Ext.Msg.hide();
            Ext.Msg.alert('提示', response.responseText);
        };
    },
    disableThemeActionClick: function (grid, record) {
        var store = grid.getStore();
        var data = {
            ID: record.data.ID,
        }
        store.UpAvailable(data, success, failure);
        function success(response) {
            Ext.Msg.hide();
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert('提示', response.ErrorMessage);
                return;
            }
            Ext.Msg.alert('提示', '禁用成功！');
            store.load();
        };
        function failure(response) {
            Ext.Msg.hide();
            Ext.Msg.alert('提示', response.responseText);
        };
    },
    escActionClick: function (grid, record) {
        var store = grid.getStore();
        var data = {
            ID: record.data.ID,
            CardThemeCategoryID: record.data.CardThemeCategoryID,
            Index: record.data.Index,
        }
        store.setIndex(data, success, failure);
        function success(response) {
            Ext.Msg.hide();
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert('提示', response.ErrorMessage);
                return;
            }
            Ext.Msg.alert('提示', '升序成功！');
            store.load();
        };
        function failure(response) {
            Ext.Msg.hide();
            Ext.Msg.alert('提示', response.responseText);
        };
    },
    descActionClick: function (grid, record) {
        var store = grid.getStore();
        var data = {
            ID: record.data.ID,
            CardThemeCategoryID: record.data.CardThemeCategoryID,
            Index: record.data.Index,
        }
        store.UpIndex(data, success, failure);
        function success(response) {
            Ext.Msg.hide();
            response = JSON.parse(response.responseText);
            if (!response.IsSuccessful) {
                Ext.Msg.alert('提示', response.ErrorMessage);
                return;
            }
            Ext.Msg.alert('提示', '降序成功！');
            store.load();
        };
        function failure(response) {
            Ext.Msg.hide();
            Ext.Msg.alert('提示', response.responseText);
        };
    },
    deleteActionClick: function (grid, record) {
        Ext.Msg.alert('提示', '确定要删除吗？', function (optional) {
            if (optional == "ok") {
                Ext.Msg.wait('正在处理数据...', '提示');
                var store = grid.getStore();
                store.remove(record);
                store.sync({
                    callback: function (batch, options) {
                        Ext.Msg.hide();
                        if (!batch.hasException()) {
                            Ext.Msg.alert('提示', '删除成功');
                        } else {
                            Ext.Msg.alert('提示', batch.operations[0].error);
                            store.rejectChanges();
                        }
                    }
                });
            }
        });
    },
    editTheme: function (btn) {
        var me = this;
        var grid = me.getMemberCardThemeGrid();
        var selectedItems = grid.getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert('提示', '请先选中一条数据');
            return;
        }
        me.editThemeAction(grid, selectedItems[0]);
    },
    editThemeAction: function (grid, record) {
        var win = Ext.widget('MemberCardThemeEdit');
        win.form.loadRecord(record);
        win.down('box[name=ThemeShow]').autoEl.src = record.data.ImgUrl;
        win.form.getForm().actionMethod = "PUT";
        win.setTitle("编辑卡片主题");
        win.show();
    },
    saveMemberTheme: function (btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
            if (formValues.ImgUrl == "") {
                Ext.Msg.alert('提示', '请先上传图片');
                return;
            }
            var store = me.getMemberCardThemeGrid().getStore();
            if (form.actionMethod == 'POST') {
                store.create(formValues, {
                    callback: function (records, operation, success) {
                        if (!success) {
                            Ext.Msg.alert('提示', operation.error);
                            return;
                        } else {
                            Ext.Msg.alert('提示', '新增成功');
                            store.load();
                            win.close();
                        }
                    }
                });
            } else {
                if (!form.isDirty()) {
                    win.close();
                    return;
                }
                form.updateRecord();
                store.update({
                    callback: function (records, operation, success) {
                        if (!success) {
                            Ext.MessageBox.alert('提示', operation.error);
                            return;
                        } else {
                            Ext.MessageBox.alert('提示', '更新成功');
                            store.load();
                            win.close();
                        }
                    }
                });
            }
        }
    },
    deleteImg: function (btn) {

        var me = this;
        var removecontainer = this.up();
        var memberCardThemeGroupEdit = this.getMemberCardThemeGroupEdit();
        var reNewTimeCon = memberCardThemeGroupEdit.down('container[name=imgcontainer]');
        Ext.Msg.alert('提示', '确定要删除吗？', function (optional) {
            if (optional == "ok") {

                reNewTimeCon.remove(removecontainer);
                Ext.Msg.alert('提示', '删除图片成功');
            }
        });
        var me = this;
        var removecontainer = this.up();
        var memberCardThemeGroupEdit = this.getMemberCardThemeGroupEdit();
        var reNewTimeCon = memberCardThemeGroupEdit.down('container[name=imgcontainer]');
        reNewTimeCon.remove(removecontainer);
        Ext.Msg.alert('提示', '删除图片成功');
    },
    uploadthemepic: function (btn) {
        var form = btn.up('form').getForm();
        var me = btn.up('window');
        var controller = this;
        var AddNewTimeCon = me.down('container[name = imgcontainer]')
        var num = me.down('container[name=imgcontainer]').items.length;
        num++;
        if (num == 13) {
            me.down('button[name = uploadthemepic]').disable();
        };
        if (form.isValid()) {
            form.submit({
                url: '/api/UploadFile/UploadCardTheme',
                waitMsg: '正在上传...',
                success: function (fp, o) {
                    if (o.result.success) {
                        Ext.Msg.alert('提示', '上传成功！');

                        var newvalue = "";

                        var oldvauleimg = me.down('textfield[name = ImgUrl]').getValue();

                        var newPanle = {

                            xtype: 'box',
                            name: 'ThemeShow' + num,
                            width: 83,
                            height: 50,
                            margin: '0 0 10 10',
                            action: 'deleteImg',
                            autoEl: { tag: 'img', src: o.result.FileUrl, },
                            listeners: {
                                dblclick: {
                                    element: 'el',
                                    fn: function (box) {
                                        var removecontainer = this.component;
                                        var memberCardThemeGroupEdit = controller.getMemberCardThemeGroupEdit();
                                        var reNewTimeCon = memberCardThemeGroupEdit.down('container[name=imgcontainer]');
                                        Ext.Msg.alert('提示', '确定要删除吗？', function (optional) {
                                            if (optional == "ok") {
                                                reNewTimeCon.remove(removecontainer);
                                                Ext.Msg.alert('提示', '删除图片成功');

                                            }
                                        });
                                    }
                                }
                            }
                        }
                        if (oldvauleimg == "") {
                            newvalue += o.result.FileUrl;
                        }
                        else {
                            newvalue += (oldvauleimg + ';' + o.result.FileUrl)
                        }
                        AddNewTimeCon.add(newPanle);
                        btn.up('window').down('textfield[name=ImgUrl]').setValue(newvalue);

                    } else {
                        Ext.Msg.alert('提示', o.result.errorMessage);
                    }
                },
                failure: function (fp, o) {
                    Ext.Msg.alert('提示', o.result.errorMessage);
                }
            });
        }
    },
    addTheme: function (btn) {
        var win = Ext.widget('MemberCardThemeEdit');
        win.form.getForm().actionMethod = 'POST';
        win.setTitle("新增卡片主题");

        var parentForm = btn.up('window').form.getForm();
        var cardTypeID = parentForm.getValues().ID;
        win.down('textfield[name=CardTypeID]').setValue(cardTypeID);
        win.show();
    },
    theme: function (btn) {
        var selectedItems = this.getMemberCardTypeList().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert('提示', '请先选中一个卡片类型');
            return;
        }
        var win = Ext.widget('MemberCardTheme');
        var form = win.form.getForm();
        form.loadRecord(selectedItems[0]);

        var store = win.down('grid').getStore();
        Ext.apply(store.proxy.extraParams, { CardTypeID: selectedItems[0].data.ID });
        store.load();

        win.show();
    },
    Deletetimes: function (button) {

        var removecontainer = button.up('container');
        var me = this;
        var memberCardThemeGroupEdit = me.getMemberCardThemeGroupEdit();
        var reNewTimeCon = memberCardThemeGroupEdit.down('container[name=AllShowTimecontainer]');
        reNewTimeCon.remove(removecontainer);
        var num = memberCardThemeGroupEdit.down('container[name=AllShowTimecontainer]').items.length;
        num--;
        if (num < 0) {
            memberCardThemeGroupEdit.down('label[name=timelabel]').hide();
        };
    },
    AddNewTime: function (button) {
        var me = this;
        var memberCardThemeGroupEdit = me.getMemberCardThemeGroupEdit();
        memberCardThemeGroupEdit.down('label[name=timelabel]').show();
        var num = memberCardThemeGroupEdit.down('container[name=AllShowTimecontainer]').items.length;
        num++;
        var AddNewTimeCon = memberCardThemeGroupEdit.down('container[name=AllShowTimecontainer]');
        var newPanle = {
            xtype: "container",
            margin: "0 0 10 10",
            width: "100%",
            name: 'timecontainer' + num,
            layout: {
                type: "hbox"
            },
            items: [{
                xtype: 'textfield', name: 'ChooesStarTime' + num, allowBlank: false,
            }, {
                xtype: "label",
                margin: "5 20 0 20",
                text: "至:",
            }, {
                xtype: 'textfield', name: 'ChooesEenTime' + num, allowBlank: false,
            }, {
                xtype: 'button',
                name: 'Deletetimes',
                action: 'Deletetimes',
                text: "删除时间段",
                margin: "0 0 0 20",
            }]
        };
        AddNewTimeCon.add(newPanle);
    },
    JudgeGrade: function (obj, newValue, oldValue, eOpts) {
        var me = this;
        var memberCardThemeGroupEdit = me.getMemberCardThemeGroupEdit();
        var RecommendGroupID = memberCardThemeGroupEdit.down('combobox[name = RecommendGroupID]');
        if (newValue != '00000000-0000-0000-0000-000000000001') {
            RecommendGroupID.clearValue();
            RecommendGroupID.readOnly = true;
        }
        else {
            RecommendGroupID.readOnly = false;
        }
    },
    TimeSlot: function (g, newValue, oldValue) {
        var me = this;
        if (newValue == 1) {
            var field = Ext.getCmp('weekcontainer');
            field.hide();

            var addTimeCon = Ext.getCmp('addTimeCon');
            addTimeCon.hide();

            var AddtimeLab = Ext.getCmp('AddtimeLab');
            AddtimeLab.hide();

            var shoumessage = Ext.getCmp('shoumessage');
            shoumessage.hide();

        }
        if (newValue == 0) {

            Ext.getCmp('addTimeCon').allowBlank = false;
            var field = Ext.getCmp('weekcontainer');
            field.show();
            var addTimeCon = Ext.getCmp('addTimeCon');
            addTimeCon.show();
            var AddtimeLab = Ext.getCmp('AddtimeLab');
            AddtimeLab.show();
            var shoumessage = Ext.getCmp('shoumessage');
            shoumessage.show();
            var memberCardThemeGroupEdit = me.getMemberCardThemeGroupEdit();
            var timecontainer = memberCardThemeGroupEdit.down('container[name=timecontainer]');
        }
    },
    addMemberCardType: function (button) {
        var win = Ext.widget("MemberCardTypeEdit");
        win.form.getForm().actionMethod = 'POST';
        win.setTitle('新增卡片类型');
        win.show();
    },
    editMemberCardType: function () {
        var selectedItems = this.getMemberCardTypeList().getSelectionModel().getSelection();
        if (selectedItems.length < 1) {
            Ext.Msg.alert("提示", "请先选择数据");
            return;
        }
        this.showEdit(null, selectedItems[0]);
    },
    showEdit: function (grid, record) {
        var win = Ext.widget("MemberCardTypeEdit");
        win.form.loadRecord(record);
        win.form.getForm().actionMethod = 'PUT';
        win.setTitle('编辑卡片类型');
        win.down('[name=Name]').setReadOnly(true);
        var txtMaxRecharge = this.getTxtMaxRecharge();
        if (record.data.AllowRecharge === true) {
            txtMaxRecharge.setReadOnly(false);
        } else {
            txtMaxRecharge.setReadOnly(true);
        }
        win.show();
    },
    saveMemberCardType: function (btn) {
        var me = this;
        var win = me.getMemberCardTypeEdit();
        var form = win.form.getForm();
        var formValues = form.getValues();
        if (form.isValid()) {
            var myStore = this.getMemberCardTypeList().getStore();
            if (form.actionMethod == 'POST') {
                myStore.create(formValues, {
                    callback: function (records, operation, success) {
                        if (!success) {
                            Ext.MessageBox.alert("提示", operation.error);
                            return;
                        } else {
                            myStore.add(records[0].data);
                            myStore.commitChanges();
                            Ext.MessageBox.alert("提示", "新增成功");
                            win.close();
                        }
                    }
                });
            } else {
                if (!form.isDirty()) {
                    win.close();
                    return;
                }
                form.updateRecord();
                myStore.update({
                    callback: function (records, operation, success) {
                        if (!success) {
                            Ext.MessageBox.alert("提示", operation.error);
                            return;
                        } else {
                            Ext.MessageBox.alert("提示", "更新成功");
                            win.close();
                        }
                    }
                });
            }
        }
    },
    searchData: function (btn) {
        var myStore = this.getMemberCardTypeList().getStore();
        var queryValues = btn.up('form').getValues();
        if (queryValues != null) {
            queryValues.All = true;
            myStore.load({ params: queryValues });
        } else {
            Ext.MessageBox.alert("系统提示", "请输入过滤条件！");
        }
    },
    onAllowRechargeChange: function (combo, newValue, oldValue, eOpts) {
        var txtMaxRecharge = this.getTxtMaxRecharge();
        if (newValue === true) {
            txtMaxRecharge.setReadOnly(false);
        } else {
            txtMaxRecharge.setReadOnly(true);
            txtMaxRecharge.setValue('');
        }
    }
});
