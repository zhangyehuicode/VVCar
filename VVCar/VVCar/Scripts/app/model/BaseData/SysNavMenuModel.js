Ext.define('WX.model.BaseData.SysNavMenuModel', {
    extend: 'Ext.data.Model',
    idProperty: "ID",
    fields: [
        { name: 'ID' },
        { name: 'ParentID' },
        { name: 'Name' },
        { name: "leaf" },
        { name: "expanded" },
        { name: "SysMenuUrl" },
        { name: "Type", type: "number" },
        { name: "Component" },
        { name: "Index", type: "number" },
    ]
});