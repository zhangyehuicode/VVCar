Ext.define('WX.view.Member.Member', {
    extend: 'Ext.container.Container',
    alias: 'widget.Member',
    title: '会员管理',
    layout: 'hbox',
    align: 'stretch',
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        //var statusStore = Ext.create('WX.store.DataDict.MemberCardStatusStore');
        var memberStore = Ext.create('WX.store.BaseData.MemberStore');
        var memberGroupStore = Ext.create('WX.store.BaseData.MemberGroupTreeStore');
        //var memberCardTypeStore = Ext.create('WX.store.BaseData.MemberCardTypeStore');
        me.items = [{
            name: 'treeMemberGroup',
            xtype: 'treepanel',
            title: '会员类别',
            height: '100%',
            width: 200,
            useArrows: true,
            animate: true,
            displayField: 'Text',
            store: memberGroupStore,
            bbar: ['->', {
                action: 'manageMemberGroup',
                xtype: 'button',
                text: '分类管理',
                tooltip: '分类管理',
                width: 100,
                margin: '0 0 2 0',
            }, '->'],
        }, {
            xtype: 'grid',
            name: 'gridMember',
            title: '会员列表',
            flex: 1,
            store: memberStore,
            stripeRows: true,
            height: '100%',
            tbar: [{
                xtype: 'form',
                layout: 'column',
                border: false,
                frame: false,
                labelAlign: 'left',
                buttonAlign: 'right',
                labelWidth: 80,
                autoWidth: true,
                autoScroll: false,
                columnWidth: 1,
                items: [{
                    xtype: 'textfield',
                    name: 'Keyword',
                    fieldLabel: '关键字',
                    labelWidth: 60,
                    width: 240,
                    emptyText: '电话/姓名',
                    margin: "0 0 0 5"
                },
                //{
                //    xtype: "combobox",
                //    name: "Status",
                //    store: statusStore,
                //    fieldLabel: "会员状态",
                //    displayField: "DictName",
                //    valueField: "DictValue",
                //    width: 170,
                //    labelWidth: 60,
                //    forceSelection: true,
                //    margin: "0 0 0 5"
                //}, {
                //    xtype: "combobox",
                //    name: "CardTypeID",
                //    store: memberCardTypeStore,
                //    fieldLabel: "卡片类型",
                //    displayField: "Name",
                //    valueField: "ID",
                //    width: 170,
                //    labelWidth: 60,
                //    forceSelection: true,
                //    margin: "0 0 0 5"
                //    //}, {
                //    //    xtype: 'combobox',
                //    //    fieldLabel: '会员等级',
                //    //    name: "MemberGradeID",
                //    //    store: Ext.create('WX.store.BaseData.MemberGradeStore'),
                //    //    queryMode: 'remote',
                //    //    displayField: "Name",
                //    //    valueField: "ID",
                //    //    width: 170,
                //    //    labelWidth: 60,
                //    //    forceSelection: true,
                //    //    margin: '0 0 0 5',
                //},
                {
                    action: 'search',
                    xtype: 'button',
                    text: '搜 索',
                    iconCls: 'fa fa-search',
                    cls: 'submitBtn',
                    margin: '0 0 0 5',
                }, {
                    action: 'manualAddMember',
                    xtype: 'button',
                    text: '新增会员',
                    iconCls: '',
                    margin: '0 0 0 25',
                    permissionCode: 'Member.Member.AddMember'
                }, {
                    action: 'deleteMember',
                    xtype: 'button',
                    text: '删除会员',
                    scope: this,
                    iconCls: 'fa fa-close',
                    margin: '0 0 0 5',
                    permissionCode: 'Member.Member.DeleteMember'
                }, {
                    action: "export",
                    xtype: "button",
                    text: "导 出",
                    cls: "submitBtn",
                    margin: "0 0 0 5",
                    //permissionCode: 'Portal.BaseDataEdit'
                }, {
                    action: 'importMemberTemplate',
                    xtype: 'button',
                    margin: "0 0 0 25",
                    text: '下载模板',
                    permissionCode: 'Member.Member.ImportMember',
                }, {
                    xtype: 'form',
                    layout: 'fit',
                    border: false,
                    frame: false,
                    width: 60,
                    permissionCode: 'Member.Member.ImportMember',
                    items: [{
                        name: 'importMember',
                        xtype: 'fileuploadfield',
                        buttonOnly: true,
                        hideLabel: true,
                        buttonText: '导入',
                        margin: "0 0 0 5",
                        allowBlank: false,
                    }]
                }]
            }],
            columns: [
                { header: "会员卡号", dataIndex: "CardNumber", width: 100 },
                { header: "余额（元）", dataIndex: "CardBalance", width: 100, xtype: "numbercolumn" },
                //{ header: "卡片类型", dataIndex: "CardTypeDesc", flex: 1 },
                {
                    header: "分组", dataIndex: "MemberGroupID", minWidth: 100, flex: 1,
                    renderer: function (value, cellmeta, record) {
                        if (value != null && value != '') {
                            var record = memberGroupStore.findRecord('ID', value);
                            if (record != null) {
                                return record.data.Name;
                            }
                            return "普通会员";
                        }
                        return "普通会员";
                    }
                },
                { header: "姓名", dataIndex: "Name", minWidth: 100, flex: 1 },
                {
                    header: "性别", dataIndex: "Sex", width: 50,
                    renderer: function (value) {
                        if (value == 1)
                            return "男";
                        else if (value == 2)
                            return "女";
                        else
                            return "未知";
                    }
                },
                { header: "手机号码", dataIndex: "MobilePhoneNo", width: 110 },
                { header: "归属地", dataIndex: "PhoneLocation", width: 80 },
                { header: "车牌号", dataIndex: "PlateList", minWidth: 100, flex: 1, permissionCode: 'Member.Member.DepartmentColumn' },
                { header: "门店名称", dataIndex: "DepartmentName", minWidth: 200, flex: 1, permissionCode: 'Member.Member.AgentColumn' },
                { header: "门店地址", dataIndex: "DepartmentAddress", minWidth: 250, flex: 1, permissionCode: 'Member.Member.AgentColumn' },
                { header: "剩余积分", dataIndex: "Point", width: 80 },
                { header: "会员状态", dataIndex: "Status", width: 80, permissionCode: 'Member.Member.DepartmentColumn' },
                { header: '保险到期时间', dataIndex: 'InsuranceExpirationDate', xtype: "datecolumn", format: 'Y-m-d H:i:s', width: 100, permissionCode: 'Member.Member.DepartmentColumn' },
                //{ header: "会员等级", dataIndex: "MemberGradeName", flex: 1 },
                //{ header: "所属门店", dataIndex: "OwnerDepartment", flex: 1 },
                { header: "注册时间", dataIndex: "CreatedDate", xtype: "datecolumn", format: "Y-m-d H:i:s", minWidth: 100 },
                //{ header: "OpenId", dataIndex: "WeChatOpenID", flex: 1, },
            ],
            bbar: [{
                action: 'resetPassword',
                xtype: 'button',
                text: '重置核销密码',
                scope: me,
                iconCls: 'reset',
                permissionCode: 'Member.Member.DataMaintainance'
            }, {
                action: 'editMember',
                xtype: 'button',
                text: '资料维护',
                scope: me,
                iconCls: 'edit',
                permissionCode: 'Member.Member.DataMaintainance'
            },
            {
                action: 'adjustBalance',
                xtype: 'button',
                text: '余额调整',
                scope: me,
                iconCls: "adjust",
                permissionCode: 'Member.Member.adjustBalance',
            },
            {
                action: 'adjustMemberPoint',
                xtype: 'button',
                text: '积分调整',
                scope: me,
                permissionCode: 'Member.Member.adjustMemberPoint',
            }, {
                xtype: 'button',
                action: 'changeGroup',
                text: '移动分组',
                scope: me,
                permissionCode: 'Portal.BaseDataEdit',
                //menu: Ext.create('Ext.menu.Menu'),
            }],
            dockedItems: [{
                xtype: "pagingtoolbar",
                store: memberStore,
                dock: "bottom",
                displayInfo: true
            }],
        }];
        //{
        //    action: 'reportLoss',
        //    xtype: 'button',
        //    text: '挂失',
        //    scope: me,
        //    iconCls: 'reportLoss',
        //    permissionCode: 'Portal.BaseDataEdit'
        //}, {
        //    action: 'cancelLoss',
        //    xtype: 'button',
        //    text: '解挂',
        //    scope: me,
        //    iconCls: 'cancelLoss',
        //    permissionCode: 'Portal.BaseDataEdit'
        //    //}, {
        //    //    action: 'changeCard',
        //    //    xtype: 'button',
        //    //    text: '换卡',
        //    //    scope: me,
        //    //    iconCls: 'switch',
        //    //    permissionCode: 'Portal.BaseDataEdit'
        //    },
        //me.memberGrid = Ext.create('Ext.grid.Panel', {
        //    name: 'gridMember',
        //    height: '100%',
        //    flex: 1,
        //    store: memberStore,
        //    selType: 'checkboxmodel',
        //    tbar: tbar,
        //    columns: columns,
        //    bbar: bbar,
        //    dockedItems: dockedItems
        //});
        //me.memberGroup = Ext.create('WX.view.MemberGroup.MemberGroup', { width: '15%', height: '100%' });
        //me.items = [me.memberGroup, {
        //    xtype: 'splitter'
        //}, me.memberGrid];//[me.memberGrid];
        this.callParent(arguments);
    },
    //afterRender: function () {
    //    this.callParent(arguments);
    //    //this.down('grid').getStore().load();
    //}
});
