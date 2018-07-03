Ext.define('WX.model.BaseData.CarBitCoinMemberModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: [
        { name: 'ID' },
        { name: 'Name' },
        { name: 'MobilePhoneNo' },
        { name: 'Sex' },
        { name: 'Horsepower' },
        { name: 'CarBitCoin' },
        { name: 'FrozenCoin' },
        { name: 'CreatedDate' },
    ]
});