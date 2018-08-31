Ext.define('WX.model.BaseData.CrowdOrderModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: [
        { name: 'ID' },
        { name: 'Name' },
        { name: 'CarBitCoinProductID' },
        { name: 'CarBitCoinProductName' },
        { name: 'PriceSale' },
        { name: 'Stock' },
        { name: 'Price' },
        { name: 'PeopleCount' },
        { name: 'IsAvailable' },
        { name: 'PutawayTime' },
        { name: 'SoleOutTime' },
        { name: 'CreatedDate' },
    ]
})