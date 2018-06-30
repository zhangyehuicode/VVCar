Ext.define('WX.model.BaseData.CarBitCoinProductCategoryTreeModel', {
    extend: 'WX.model.BaseData.CarBitCoinProductCategoryModel',
    idProperty: 'ID',
    fields: [
        { name: 'Text' },
        { name: 'leaf' },
        { name: 'expanded' },
        { name: 'Children' },
    ]
});