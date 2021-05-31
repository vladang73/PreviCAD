using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Helpers
{

    public class DroitsPrevi
    {
        public bool EstUpdatePrevi { get; set; }
        public bool EstUpdateUser { get; set; }

        public bool EstAdminPrevi { get; set; }
        public bool EstAdminUser { get; set; }

        public bool EstLecturePrevi { get; set; }
        public bool EstLectureUser { get; set; }
    }
    public class GetDroitsPrevi
    {
       public static DroitsPrevi ObtenirDroits()
        {
            var user = HttpContext.Current.User;
            var droits = new DroitsPrevi();
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                droits.EstLectureUser = true;
                droits.EstUpdateUser = false;
                droits.EstAdminUser = false;

                if (user.IsInRole(Enums.Roles.Administrateur))
                {
                    droits.EstAdminPrevi = true;
                    droits.EstLecturePrevi = true;
                    droits.EstUpdatePrevi = true;
                }
                else if (user.IsInRole(Enums.Roles.LectureEcriture))
                {
                    droits.EstAdminPrevi = false;
                    droits.EstLecturePrevi = true;
                    droits.EstUpdatePrevi = true;
                }
                else
                {
                    droits.EstAdminPrevi = false;
                    droits.EstLecturePrevi = true;
                    droits.EstUpdatePrevi = false;
                }
            }
            else
            {
                // gérer les droits avec la session
            }
            // droits.EstAdminPrevi= HttpContext.Current.Request.IsAuthenticated && HttpContext.Current.Request.is
            return droits;
        }
    }

}