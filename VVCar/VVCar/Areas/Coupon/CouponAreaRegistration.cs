using System.Web.Mvc;

namespace VVCar.Areas.Coupon
{
    public class CouponAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Coupon";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              "Coupon_admin_default",
              "CouponAdmin/{action}/{id}",
              new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Coupon_default",
                "Coupon/{action}/{id}",
                new { controller = "Coupon", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Coupon_Activity",
                "Activity/{action}/{id}",
                new { controller = "Activity", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}