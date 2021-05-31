using Kendo.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Previgesst.Helpers
{
    public static class DataSourceRequestExtensions
    {
        /// <summary>
        /// Finds a filter or sort with the <paramref name="memberName"/> and renames it with <paramref name="newMemberName"/>.
        /// </summary>
        /// <param name="request">The DataSourceRequest instance. <see cref="Kendo.Mvc.UI.DataSourceRequest"/></param>
        /// <param name="memberName">The Name of the Filter to be renamed.</param>
        /// <param name="newMemberName">The New Name of the Filter.</param>
        /// 


        private static void setMember(string memberName, string newMemberName, object MyFilter)
        {
            if (MyFilter is CompositeFilterDescriptor)
            {
                var theFilter = (MyFilter as CompositeFilterDescriptor);
                foreach (var x in theFilter.FilterDescriptors)
                    setMember(memberName, newMemberName, x);


            }
            else if (MyFilter is FilterDescriptor)
            {
                var theFilter = (MyFilter as FilterDescriptor);
                if (theFilter.Member.Equals(memberName))
                {
                    theFilter.Member = newMemberName;
                }

            }


        }


        private static Tuple<bool, string> checkFilter(string memberName, object MyFilter, Boolean Remove)
        {
            Boolean trouve = false;
            string valeur = "";

            if (MyFilter is CompositeFilterDescriptor)
            {
                var theFilter = (MyFilter as CompositeFilterDescriptor);
                foreach (var x in theFilter.FilterDescriptors)
                {
                    var resultat = checkFilter(memberName, x, Remove);
                    if (resultat.Item1 == true)
                    {

                        if (Remove)
                            theFilter.FilterDescriptors.Remove(x);
                        return resultat;

                    }

                }

            }
            else if (MyFilter is FilterDescriptor)
            {
                var theFilter = (MyFilter as FilterDescriptor);
                if (theFilter.Member.Equals(memberName))
                {
                    trouve = true;
                    valeur = theFilter.Value.ToString();




                }
            }
            return new Tuple<bool, string>(trouve, valeur);

        }

        public static Tuple<bool, string> isFilterMember(this DataSourceRequest request, string memberName, Boolean Remove)
        {
            Boolean trouve = false;
            var valeur = "";
            for (int i = 0; i < request.Filters.Count; i++)
            {
                var filter = request.Filters[i];
                var resultat = checkFilter(memberName, filter, Remove);
                if (resultat.Item1 == true)
                {
                    if (filter is FilterDescriptor)
                    {
                        request.Filters.Remove(filter);
                    }


                    return resultat;
                }

            }

            var reponse = new Tuple<bool, string>(trouve, valeur);

            return reponse;

        }

        public static Tuple<bool, string> isSortMember(this DataSourceRequest request, string memberName, Boolean Remove)
        {
            Boolean trouve = false;
            var valeur = "";
            int NoSort = request.Sorts.Count;
            for (int noSort = request.Sorts.Count - 1; noSort >= 0; noSort--)
            {
                var sort = request.Sorts[noSort];
                if (sort.Member.Equals(memberName))
                {

                    trouve = true;
                    valeur = sort.SortDirection == System.ComponentModel.ListSortDirection.Ascending ? "A" : "D";
                    if (Remove)
                        request.Sorts.Remove(sort);
                }
            }

            var reponse = new Tuple<bool, string>(trouve, valeur);

            return reponse;
        }


        public static void RenameMember(this DataSourceRequest request, string memberName, string newMemberName)
        {
            for (int i = 0; i < request.Filters.Count; i++)
            {
                var filter = request.Filters[i];
                setMember(memberName, newMemberName, filter);
            }

            for (int noSort = 0; noSort < request.Sorts.Count; noSort++)
            {
                var sort = request.Sorts[noSort];
                if (sort.Member.Equals(memberName))
                {
                    sort.Member = newMemberName;
                }
            }
        }
    }
}