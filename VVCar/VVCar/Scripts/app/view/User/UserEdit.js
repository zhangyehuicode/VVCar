Ext.define('WX.view.User.UserEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.UserEdit',
    title: '用户编辑',
    layout: 'fit',
    width: 450,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        var deptStore = Ext.getStore('User_DepartmentStore');
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%',
            },
            items: [
                {
                    xtype: 'textfield',
                    name: 'Code',
                    fieldLabel: '用户代码',
                    maxLength: 20,
                    allowBlank: false,

                },
                {
                    xtype: 'textfield',
                    name: 'Name',
                    fieldLabel: '用户名称',
                    maxLength: 20,
                    allowBlank: false,
                },
                {
                    xtype: 'combobox',
                    name: 'DepartmentID',
                    fieldLabel: '所属门店',
                    store: deptStore,
                    queryMode: 'local',
                    displayField: 'Name',
                    valueField: 'ID',
                    emptyText: '请选择...',
                    allowBlank: false,
                    forceSelection: true,
                },
                {
                    xtype: 'textfield',
                    name: 'Password',
                    fieldLabel: '登录密码',
                    inputType: 'password',
                    allowBlank: false,
                },
                {
                    xtype: 'radiogroup',
                    fieldLabel: '性别',
                    items: [
                        { boxLabel: '男', name: 'Sex', inputValue: 1, checked: true },
                        { boxLabel: '女', name: 'Sex', inputValue: 2 },
                    ]
                },
                {
                    xtype: 'textfield',
                    name: 'PhoneNo',
                    fieldLabel: '电话号码',
                    vtype: 'phone',
                    maxLength: 15,
                },
                {
                    xtype: 'textfield',
                    name: 'MobilePhoneNo',
                    fieldLabel: '手机号码',
                    vtype: 'mobilephone',
                    maxLength: 15,
                    allowBlank: false,
                },
                {
                    xtype: 'textfield',
                    name: 'EmailAddress',
                    fieldLabel: '电子邮件',
                    vtype: 'email',
                    maxLength: 30,
                },
                {
                    xtype: 'datefield',
                    name: 'DutyTime',
                    fieldLabel: '入职时间',
                    format: 'Y-m-d',
                    allowBlank: true,
                },
                {
                    xtype: 'numberfield',
                    name: 'Age',
                    fieldLabel: '年龄',
                    minValue: 1,
                },
                {
                    xtype: 'textfield',
                    name: 'AuthorityCard',
                    fieldLabel: '权限卡',
                    maxLength: 50,
                    hidden: true,
                },
                {
                    xtype: 'fieldcontainer',
                    fieldLabel: '',
                    hideEmptyLabel: false,
                    layout: 'hbox',
                    items: [
                        {
                            xtype: 'checkboxfield',
                            name: 'IsAvailable',
                            boxLabel: '是否可用',
                            flex: 1,
                            checked: true,
                            inputValue: true,
                        },
                        //{
                        //    xtype: 'checkboxfield',
                        //    name: 'CanLoginPos',
                        //    boxLabel: '可登录Pos',
                        //    flex: 1,
                        //    checked: true,
                        //    inputValue: true,
                        //},
                        {
                            xtype: 'checkboxfield',
                            name: 'CanLoginAdminPortal',
                            boxLabel: '可登录管理后台',
                            flex: 1,
                            checked: true,
                            inputValue: true,
                        }
                    ]
                }
            ]
        });

        me.items = [me.form];
        me.buttons = [
            {
                text: '保存',
                action: 'save',
                cls: 'submitBtn',
                scope: me,
            },
            {
                text: '取消',
                scope: me,
                handler: me.close
            }
        ];
        me.callParent(arguments);
    }
});