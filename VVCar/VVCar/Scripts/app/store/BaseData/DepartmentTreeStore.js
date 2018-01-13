Ext.define('WX.store.BaseData.DepartmentTreeStore', {
    extend: 'Ext.data.TreeStore',
    autoLoad: false,
    fields: ['ID', 'Text', 'ParentId'],
    nodeParam: 'ParentId',
    defaultRootId: '',
    proxy: {
        type: 'rest',
        api: {
            read: Ext.GlobalConfig.ApiDomainUrl + 'api/Department/GetTree',
        },
        reader: {
            type: 'json',
            rootProperty: 'Children',
            successProperty: 'IsSuccessful',
            messageProperty: 'ErrorMessage',
        }
    },
    root: {
        expanded: true,//此设置为true会自动触发一次请求。
    }
});