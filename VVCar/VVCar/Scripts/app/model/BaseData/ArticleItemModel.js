Ext.define('WX.model.BaseData.ArticleItemModel', {
	extend: 'Ext.data.Model',
	idProperty: 'ID',
	fields: [
		{ name: 'ID' },
		{ name: 'ArticleID' },
		{ name: 'Title' },
		{ name: 'ThumbMediaID' },
		{ name: 'Author' },
		{ name: 'Digest' },
		{ name: 'IsShowCoverPic' },
		{ name: 'Content' },
		{ name: 'ContentSourceUrl'},
		{ name: 'CreatedDate' },
	]
})