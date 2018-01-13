/// <reference path="../../ext/ext-all-dev.js" />
Ext.define('WX.store.DataDict.PermissionTypeStore', {
    extend: 'Ext.data.Store',
    fields: ['DictValue', 'DictName'],
    data: [
        { "DictValue": 0, "DictName": "管理后台菜单" },
        { "DictValue": 1, "DictName": "管理后台功能" }
    ]
});
