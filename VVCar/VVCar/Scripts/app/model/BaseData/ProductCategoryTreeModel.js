Ext.define('WX.model.BaseData.ProductCategoryTreeModel', {
    extend: 'WX.model.BaseData.ProductCategoryModel',
    idProperty: 'ID',
    fields: ['Text', 'leaf', 'expanded', 'Children']
});