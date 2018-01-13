(function () {
    var userStore = Ext.create('WX.store.BaseData.UserStore');
    Ext.define('WX.view.User.UserList', {
        extend: 'Ext.container.Container',
        alias: 'widget.UserList',
        title: '用户管理',
        store: userStore,
        stripeRows: true,
        //frame: false,
        loadMask: true,
        closable: true,
        layout: "hbox",
        initComponent: function () {
            var me = this;
            var deptStore = Ext.create('WX.store.BaseData.DepartmentStore', { storeId: 'User_DepartmentStore' });
            deptStore.load();
            var departmentTreeStore = Ext.create('WX.store.BaseData.DepartmentTreeStore');
            me.items = [
                {
                    title: "门店",
                    name: "departmentTree",
                    xtype: "treepanel",
                    height: "100%",
                    width: 200,
                    displayField: "Text",
                    store: departmentTreeStore,
                    rootVisible: me.rootVisible,
                    animate: me.enableAnimations,
                    singleExpand: me.singleExpand,
                    hideHeaders: true
                },
                {
                    title: "用户列表",
                    name: "userList",
                    xtype: "grid",
                    height: "100%",
                    flex: 1,
                    store: userStore,
                    selModel: Ext.create('Ext.selection.CheckboxModel', { mode: "SIMPLE" }),
                    tbar: [
                        {
                            action: 'AddUser',
                            xtype: 'button',
                            text: '添加',
                            scope: me,
                            iconCls: 'x-fa fa-plus-circle'
                        },
                        {
                            action: 'EditUser',
                            xtype: 'button',
                            text: '编辑',
                            scope: me,
                            iconCls: 'x-fa fa-pencil'
                        },
                        {
                            action: 'deleteUsers',
                            xtype: 'button',
                            text: '删除',
                            scope: me,
                            iconCls: 'x-fa fa-close'
                        },
                        {
                            action: 'resetPwd',
                            xtype: 'button',
                            text: '重置密码',
                            scope: me,
                            iconCls: 'x-fa fa-undo'
                        },
                        {
                            xtype: 'form',
                            layout: 'column',
                            border: false,
                            frame: false,
                            labelAlign: 'left',
                            buttonAlign: 'right',
                            padding: 5,
                            autoWidth: true,
                            autoScroll: true,
                            fieldDefaults: {
                                labelAlign: 'left',
                                labelWidth: 40,
                                width: 170,
                                margin: '0 0 0 10',
                            },
                            items: [
                                {
                                    xtype: 'textfield',
                                    name: 'Code',
                                    fieldLabel: '代码',
                                },
                                {
                                    xtype: 'textfield',
                                    name: 'Name',
                                    fieldLabel: '名称',
                                },
                                //{
                                //    xtype: 'combobox',
                                //    name: 'Department',
                                //    fieldLabel: '所属门店',
                                //    store: deptStore,
                                //    queryMode: 'local',
                                //    displayField: 'Name',
                                //    valueField: 'ID',
                                //    labelWidth: 60,
                                //    width: 200,
                                //},
                                {
                                    action: 'search',
                                    xtype: 'button',
                                    text: '搜 索',
                                    iconCls: 'fa fa-search',
                                    cls: 'submitBtn',
                                    margin: '0 0 0 10',
                                }]
                        }],
                    columns: [
                        { header: '用户代码', dataIndex: 'Code', flex: 1 },
                        { header: '用户名称', dataIndex: 'Name', flex: 1 },
                        {
                            header: '所属门店', dataIndex: 'DepartmentID', flex: 1,
                            renderer: function (value, cellmeta, record) {
                                var record = deptStore.findRecord('ID', value);
                                if (record == null) return value;
                                else return record.data.Name;
                            }
                        },
                        { header: '手机号码', dataIndex: 'MobilePhoneNo', flex: 1 },
                        {
                            header: '是否可用', dataIndex: 'IsAvailable', width: 80, renderer: function (value) {
                                return value == false ? '<span style="color:red;">否</span>' : '<span style="color:green;">是</span>';
                            },
                        },
                        //{
                        //    header: '是否允许登录Pos', dataIndex: 'CanLoginPos', width: 120, renderer: function (value) {
                        //        return value == false ? '<span style="color:red;">否</span>' : '<span style="color:green;">是</span>';
                        //    },
                        //},
                        //{
                        //    header: '是否允许登录管理后台', dataIndex: 'CanLoginAdminPortal', width: 120, renderer: function (value) {
                        //        return value == false ? '<span style="color:red;">否</span>' : '<span style="color:green;">是</span>';
                        //    },
                        //},
                        { header: '创建人', dataIndex: 'CreatedUser', flex: 1 },
                        { header: '创建时间', dataIndex: 'CreatedDate', flex: 1 },
                    ],
                    dockedItems: [{
                        xtype: 'pagingtoolbar',
                        store: me.store,
                        dock: 'bottom',
                        displayInfo: true
                    }
                    ]
                }
            ];
            this.callParent();
        },

        afterRender: function () {
            this.callParent(arguments);
            userStore.load();
        }
    });
}());