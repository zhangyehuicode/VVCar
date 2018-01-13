Ext.define('WX.model.BaseData.CardThemeCategoryMenuModel', {
    extend: 'Ext.data.Model',
    idProperty: "ID",
    fields: [
        { name: 'ID' },
        { name: 'ParentID' },
        { name: 'text' },
        { name: "leaf" },
        { name: "expanded" },
    ]
});