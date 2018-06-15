Ext.define('WX.view.Main', {
    extend: 'Ext.container.Viewport',
    alias: 'widget.Main',
    layout: 'border',
    initComponent: function () {
        var me = this;
        var headerPanel = Ext.create('Ext.toolbar.Toolbar', {//Ext.container.Container
            region: 'north',
            height: 55,
            layout: {
                type: 'hbox',
                pack: 'start',
                align: 'middle',
            },
            padding: '5 20 5 10',
            items: [{
                xtype: 'displayfield',
                name: 'SystemTitle',
                //flex: 1,
                width: 232,
                value: _systemTitle,
                fieldStyle: 'font-size: 14px; font-weight: bold;',//color: white;
            }, {
                //xtype: 'tbspacer',
                //width: 5,
                //cls: 'x-tbspacer-navigation',
                hidden: true,
            }, {
                xtype: 'button',
                action: 'menucollapse',
                iconCls: 'fa fa-bars',
                border: false,
                style: 'background:#fff;',
            }, {
                xtype: 'tbspacer',
                flex: 1,
            }, {
                xtype: 'splitbutton',
                name: 'btnWelcome',
                text: '欢迎你',
                border: false,
                //style: 'background: none',
                menu: new Ext.menu.Menu({
                    border: false,
                    showSeparator: false,
                    items: [
                        { text: '修改密码', action: 'changePassword', },
                    ]
                })
            }, {
                xtype: 'button',
                action: 'exitSystem',
                text: '退出系统',
                scope: this,
                width: 80,
                iconCls: 'logout',
                margin: '0 0 0 10',
            }]
        });

        var navigationPanel = Ext.create('Ext.list.Tree', {
            name: 'navigationPanel',
            border: false,
            expanderOnly: false,
            singleExpand: false,
            ui: 'navigation',
            cls: 'menu_space .x-treelist-item-icon',
            store: Ext.create("WX.store.BaseData.SysNavMenuStore"),
            micro: false,
        });

        var centerPanel = Ext.create('Ext.tab.Panel', {
            id: 'tabPanelMain',
            name: 'centerPanel',
            region: 'center',
            activeTab: 0,
            autoScroll: true,
            defaults: { // defaults are applied to items, not the container
                autoScroll: true,
            },
            items: [Ext.create('WX.view.Index')],
        });

        var footerPanel = Ext.create('Ext.Component', {
            region: 'south',
            padding: 5,
            html: '<div class="copyright">Copyright © 车因子 2018</div>',
        });

        this.items = [headerPanel, {
            region: 'west',
            width: 250,
            header: false,
            scrollable: 'y',
            bodyStyle: 'background:#32404e;',
            headerOverCls: 'x-treelist-navigation',
            collapsedCls: 'x-panel-collapsed-navigation',
            items: [navigationPanel]
        }, centerPanel, footerPanel];

        me.callParent(arguments);
    },
    afterRender: function () {
        this.callParent(arguments);
        this.keyNav = Ext.create('Ext.util.KeyNav', this.el, {
            enter: {
                fn: function (e) {
                    //var focusEl = Ext.query(':focus');
                    //if (focusEl == null || focusEl.length == 0)
                    //    return;
                    //focusEl = Ext.fly(focusEl[0]);
                    var focusEl = Ext.get(e.target);
                    var submitBtn = null;
                    if (focusEl.hasCls('x-window')) {
                        submitBtn = focusEl.query('.submitBtn')
                    } else if (focusEl.hasCls('x-form-field')) {
                        var parentEl = focusEl.parent('.x-panel');
                        if (parentEl === null)
                            return;
                        submitBtn = parentEl.query('.submitBtn');
                        if (submitBtn.length === 0) {
                            parentEl = focusEl.parent('.x-window');
                            if (parentEl === null)
                                return;
                            submitBtn = parentEl.query('.submitBtn');
                        }
                    } else {
                        return;
                    }
                    if (submitBtn.length === 0)
                        return;
                    submitBtn = Ext.getCmp(submitBtn[0].id);
                    if (submitBtn === null || submitBtn.isVisible() === false || submitBtn.isDisabled() === true)
                        return;
                    submitBtn.fireEvent('click', submitBtn);
                },
                defaultEventAction: false
            },
            scope: this
        });
    }
});