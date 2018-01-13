Ext.define('WX.model.BaseData.SysMenuModel', {
    extend: 'Ext.data.Model',
    idProperty: "ID",
    fields: [
        { name: 'ID' },
        {name:"Children"},
        { name: 'ParentID' },
        { name: 'Name' },
        { name: "SysMenuUrl" },
        { name: "Component" },
        { name: "IsLeaf" },
        {name:"leaf",mapping:"IsLeaf"},
        { name: "Type", type: "number" },
        { name: "IsAvailable", type: "boolean" },
        { name: "Index", type: "number" },
    ]

},
function () {
    Ext.data.NodeInterface.decorate(this);
});

