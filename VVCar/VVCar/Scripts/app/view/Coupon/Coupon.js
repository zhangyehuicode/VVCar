Ext.define('WX.view.Coupon.Coupon', {
    extend: 'Ext.container.Container',
    alias: 'widget.Coupon',
    title: '创建优惠券',
    loadMask: true,
    closable: true,
    initComponent: function () {
        var me = this;
        var url = window.location.origin;
        me.items = [{
            border: false,
            html: '<iframe width="100%" height="2000" frameborder="0" src="' + url + '/CouponAdmin/Index"></iframe>'
        }];
        this.callParent();
    }
});
