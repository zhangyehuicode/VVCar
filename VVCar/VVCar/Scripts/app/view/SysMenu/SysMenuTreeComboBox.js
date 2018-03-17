
(function () {

    //var store = Ext.create("WX.store.BaseData.SysMenuStore");
    var store = Ext.create("WX.store.BaseData.SysNavManageMenuStore");
    //store.load();
    Ext.define('WX.view.SysMenu.SysMenuTreeComboBox', {
        extend: 'Ext.form.field.ComboBox',
        alias: 'widget.SysMenuTreeComboBox',
        //displayField: 'Name',
        valueField: 'ID',
        store: store,
        initComponent: function () {
            Ext.apply(this, {
                editable: false,
                queryMode: 'local',
                select: Ext.emptyFn
            });

            //store.reload();
            //this.displayField = this.displayField || 'text';
            // this.valueField = this.id

            this.treeid = Ext.String.format('tree-combobox-{0}', Ext.id());
            this.tpl = Ext.String.format('<div id="{0}"></div>', this.treeid);

            var me = this;
            this.tree = Ext.create('Ext.list.Tree', {//Ext.tree.Panel
                rootVisible: true,
                //autoScroll: true,
                //height: 300,
                singleExpand: true,
                //displayField: "Name",
                store: store,
                rendered: false,
            });
            //this.tree.on('itemclick', function (view, record) {
            //    me.setValue(record);
            //    me.collapse();
            //});
            this.tree.on('selectionchange', function (treelist, record, eOpts) {
                me.setValue(record);
                me.collapse();
            });
            me.on('expand', function (field, eOpts) {
                if (!this.tree.rendered) {
                    this.tree.render(this.treeid);
                }
            });
            this.callParent(arguments);
        },
    });
}());
