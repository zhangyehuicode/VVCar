Ext.define('WX.view.MemberCard.MemberCard', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.MemberCard',
    title: '会员管理',
    store: Ext.create('WX.store.BaseData.MemberCardStore'),
    stripeRows: true,
    loadMask: true,
    closable: true,
    selType: 'checkboxmodel',
    initComponent: function () {
        var me = this;
        var cardStatus = Ext.getStore('DataDict.MemberCardStatusStore');
        var memberGroupStore = Ext.create('WX.store.BaseData.MemberGroupStore');
        var memberCardTypeStore = Ext.create('WX.store.BaseData.MemberCardTypeStore');
        var memberGradeStore = Ext.create('WX.store.BaseData.MemberGradeStore');
        Ext.apply(memberGradeStore.proxy.extraParams, { Start: null, Limit: null, Status: null });
        memberGradeStore.load();
        memberCardTypeStore.load();
        memberGroupStore.load();
        //me.selModel = Ext.create('Ext.selection.CheckboxModel', { mode: "SIMPLE" });
        //me.rowEditing = Ext.create('Ext.grid.plugin.RowEditing', {
        //    saveBtnText: '保存',
        //    cancelBtnText: "取消",
        //    autoCancel: false,
        //    listeners: {
        //        cancelEdit: function (rowEditing, context) {
        //            //如果是新增的数据，则删除
        //            if (context.record.phantom) {
        //                dictValueStore.remove(context.record);
        //            }
        //        },
        //        beforeedit: function (editor, context, eOpts) {
        //            if (!Ext.validatePermission('MemberCard.AdjustBalance')) {
        //                Ext.Msg.alert('提示', '没有权限');
        //                return false;
        //            }
        //            if (context.record.data.CardBalance != 0) {
        //                return false;
        //            }
        //            if (context.record.data.Status != 0) {
        //                return false;
        //            }
        //            if (editor.editing == true)
        //                return false;
        //        },
        //    }
        //});
        //me.plugins = [me.rowEditing];
        me.tbar = [
            {
                action: 'showGenerateMemberCard',
                xtype: 'button',
                text: '制作卡片',
                scope: me,
                iconCls: 'fa fa-plus-circle'
            },
            {
                xtype: 'form',
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
                items: [{
                    xtype: 'textfield',
                    name: 'BatchCode',
                    fieldLabel: '批次代码',
                    width: 170,
                    labelWidth: 60,
                    margin: '0 0 0 5'
                }, {
                    xtype: 'textfield',
                    name: 'Code',
                    fieldLabel: '卡片编号',
                    width: 170,
                    labelWidth: 60,
                    margin: '0 0 0 5'
                }, {
                    xtype: 'combobox',
                    name: 'CardStatus',
                    fieldLabel: '卡片状态',
                    labelWidth: 60,
                    width: 170,
                    store: cardStatus,
                    displayField: 'DictName',
                    valueField: 'DictValue',
                    margin: '0 0 0 5'
                }, {
                    xtype: 'combobox',
                    fieldLabel: '会员分组',
                    name: 'MemberGroupID',
                    displayField: 'Name',
                    valueField: 'ID',
                    store: memberGroupStore,
                    queryMode: 'local',
                    labelWidth: 60,
                    width: 170,
                    margin: '0 0 0 5',
                    autoLoad: false
                }, {
                    xtype: 'combobox',
                    store: memberGradeStore,
                    displayField: 'Name',
                    valueField: 'ID',
                    name: 'MemberGradeID',
                    fieldLabel: '会员等级',
                    width: 170,
                    labelWidth: 60,
                    margin: '0 0 0 5',
                    editable: true
                }, {
                    xtype: 'combobox',
                    fieldLabel: '卡片类型',
                    name: 'CardTypeID',
                    displayField: 'Name',
                    valueField: 'ID',
                    store: memberCardTypeStore,
                    labelWidth: 60,
                    width: 170,
                    margin: '0 0 0 5',
                }, {
                    action: 'search',
                    xtype: 'button',
                    text: '搜索',
                    iconCls: 'fa fa-search',
                    cls: 'submitBtn',
                    margin: '0 0 0 10'
                }, {
                    action: 'export',
                    xtype: 'button',
                    text: '导出excel',
                    iconCls: '',
                    cls: '',
                    margin: '0 0 0 10'
                }, {
                    action: 'batchmodifyremark',
                    xtype: 'button',
                    text: '修改备注',
                    iconCls: '',
                    cls: '',
                    margin: '0 0 0 10'
                }]
            }];
        me.columns = [
            { header: '卡片编号', dataIndex: 'Code', flex: 1 },
            {
                header: '卡片类型', dataIndex: 'CardTypeID', flex: 1,
                renderer: function (value) {
                    var record = memberCardTypeStore.findRecord("ID", value);
                    if (record != null)
                        return record.data.Name;
                }
            },
            {
                header: '卡片状态', dataIndex: 'Status', flex: 1,
                renderer: function (value) {
                    var index = cardStatus.find("DictValue", value);
                    if (index === -1)
                        return "";
                    return cardStatus.getAt(index).data.DictName;
                }
            },
            {
                header: '会员分组', dataIndex: 'MemberGroupID', flex: 1,
                renderer: function (value) {
                    if (value != null && value != '') {
                        var record = memberGroupStore.findRecord("ID", value);
                        if (record != null) {
                            return record.data.Name;
                        }
                        return "普通会员";
                    }
                    return "普通会员";
                }
            },
            {
                header: '会员等级', dataIndex: 'MemberGradeID', flex: 1,
                renderer: function (value) {
                    if (value != null && value != '') {
                        var record = memberGradeStore.findRecord("ID", value);
                        if (record != null) {
                            return record.data.Name;
                        }
                    }
                }
            },
            { header: '验证码', dataIndex: 'VerifyCode', flex: 1 },
            { header: '余额', dataIndex: 'CardBalance', xtype: "numbercolumn", format: '0.00', flex: 1 },
            { header: '批次代码', dataIndex: 'BatchCode', flex: 1 },
            { header: '生成日期', dataIndex: 'CreatedDate', flex: 1 },
            //{ header: '生效日期', dataIndex: 'EffectiveDate', xtype: 'datecolumn', format: 'Y-m-d', flex: 1 },
            //{ header: '截止日期', dataIndex: 'ExpiredDate', xtype: 'datecolumn', format: 'Y-m-d', flex: 1 },
            { header: '备注', dataIndex: 'Remark', flex: 1 },
        ];
        me.dockedItems = [{
            xtype: 'pagingtoolbar',
            store: me.store,
            dock: 'bottom',
            displayInfo: true
        }];
        this.callParent();
    },

    afterRender: function () {
        this.callParent(arguments);
        this.getStore().proxy.extraParams = {};
        this.getStore().load();
    }
});