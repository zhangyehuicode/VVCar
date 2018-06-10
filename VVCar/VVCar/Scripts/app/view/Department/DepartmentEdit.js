Ext.define('WX.view.Department.DepartmentEdit', {
    extend: 'Ext.window.Window',
    alias: 'widget.DepartmentEdit',
    title: '门店编辑',
    layout: 'fit',
    width: 450,
    bodyPadding: 5,
    modal: true,
    initComponent: function () {
        var me = this;
        var districtRegions = Ext.getStore('DistrictRegionDictStore');
        var adminRegions = Ext.getStore('AdministrationRegionDictStore');
        me.form = Ext.create('Ext.form.Panel', {
            border: false,
            trackResetOnLoad: true,
            fieldDefaults: {
                labelAlign: 'left',
                labelWidth: 90,
                anchor: '100%',
            },
            items: [{
                xtype: 'textfield',
                name: 'Code',
                fieldLabel: '门店代码',
                maxLength: 20,
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: '门店名称',
                maxLength: 20,
                allowBlank: false,
            }, {
                xtype: 'textfield',
                name: 'ContactPerson',
                fieldLabel: '联系人',
                maxLength: 10,
            }, {
                xtype: 'textfield',
                name: 'ContactPhoneNo',
                fieldLabel: '联系电话',
                maxLength: 30,
                vtype: 'phone',
            }, {
                xtype: 'textfield',
                name: 'MobilePhoneNo',
                fieldLabel: '联系手机',
                maxLength: 30,
                vtype: 'mobilephone',
            }, {
                xtype: 'textfield',
                name: 'EmailAddress',
                fieldLabel: '电子邮件',
                vtype: 'email',
            }, {
                xtype: 'textfield',
                name: 'Address',
                fieldLabel: '门店地址'
            }, {
                xtype: 'combobox',
                name: 'DistrictRegion',
                fieldLabel: '地区分区',
                store: districtRegions,
                queryMode: 'local',
                displayField: 'DictName',
                valueField: 'DictValue',
                emptyText: '请选择...',
                allowBlank: false,
                forceSelection: true,
            }, {
                xtype: 'combobox',
                name: 'AdministrationRegion',
                fieldLabel: '管理分区',
                store: adminRegions,
                queryMode: 'local',
                displayField: 'DictName',
                valueField: 'DictValue',
                emptyText: '请选择...',
                allowBlank: false,
                forceSelection: true,
            }, {
                xtype: 'textareafield',
                name: 'Remark',
                fieldLabel: '备注'
            }]
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