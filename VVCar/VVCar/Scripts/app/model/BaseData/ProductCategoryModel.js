Ext.define('WX.model.BaseData.ProductCategoryModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: [
        { name: 'ID' },
        { name: 'ParentId' },
        { name: 'Index' },
        { name: 'Code' },
        { name: 'Name' },
        { name: 'IsPosUsed' },
        { name: 'IsOverOrderAlert', type: 'boolean', defaultValue: false },
        { name: 'OverOrderAlertText' },
        { name: 'IsRequiredOrderAlert' },
        { name: 'RequiredOrderAlertText' },
        { name: 'OverOrderAlertType' },
        { name: 'OverOrderAlertTypeListShow' },
        { name: 'IsForMember' },
    ]
});