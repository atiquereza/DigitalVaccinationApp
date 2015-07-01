using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ManiacProject.Views.Classes
{
    public class PaginationView
    {
        public static string GetPaginationViewHtml(List<dynamic> PageList,dynamic FirstPage,dynamic NextPage,dynamic EndPage, string Action, Dictionary<string,string> QueryStringData, string LimitParameter)
        {
            string data = string.Empty;
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            
            List<string> queryStringAll = new List<string>();

            foreach (dynamic page in PageList)
            {
                string queryString = string.Empty;
                foreach (KeyValuePair<string, string> keyValuePair in QueryStringData)
                {
                    if(keyValuePair.Key == LimitParameter)
                    {
                        queryString += keyValuePair.Key + "=" + page.PageLimit + "&";
                    }
                    else
                    {
                        queryString += keyValuePair.Key + "=" + keyValuePair.Value + "&";
                    }
                    
                }
                queryString = queryString.TrimEnd('&');
                queryStringAll.Add(queryString);
            }

            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (string.IsNullOrWhiteSpace(appUrl)) appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
            int lastIndex = baseUrl.LastIndexOf("/");
            if(lastIndex != (baseUrl.Length -1))
            {
                baseUrl += "/";
            }


            var routeValueDictionary = urlHelper.RequestContext.RouteData.Values;
            string controller = routeValueDictionary["controller"].ToString().Trim();

            string path = baseUrl + controller;//HttpContext.Current.Request.Url.AbsolutePath;

            
            
            if (PageList.Count > 0)
            {
                int nextPageIndex = 0;
                string pageString = FirstPage.PageText;
                data +=
                    "<ul><li style=\"display: inline-block; background-color: #efefef; padding: 5px 5px; border: 1px solid lightgray;\"><a href=\"" + path + "/" + Action + "?" + queryStringAll[0] + "\"> " + pageString + "</a></li>";//@Html.ActionLink(pageString, "ViewResutls", new { limit = FirstPage.PageLimit })
                
                int currentPageNumber = NextPage.PageNumber - 1;
                int queryStringIndex = 0;
                foreach (dynamic pageData in PageList)
                {
                    
                    int pageNumber = pageData.PageNumber;

                    if (currentPageNumber < 6 && pageNumber < 10)
                    {
                        string bgColor = "#efefef";
                        if (pageNumber == currentPageNumber)
                        {
                            nextPageIndex = pageNumber; 
                            bgColor = "#efefaa";
                        }
                        data +=
                            "<li style=\"width: 13px;display: inline-block; background-color: " + bgColor + "; padding: 5px 5px; border: 1px solid lightgray;\"><a href=\"" + path + "/" + Action + "?" + queryStringAll[queryStringIndex] + "\"> " + pageNumber + "</a></li>";//@Html.ActionLink(pageNumber.ToString(), "ViewResutls", new { limit = pageData.PageLimit }) +
                    }

                    else
                    {
                        if (pageNumber > (NextPage.PageNumber - 6) && pageNumber < (NextPage.PageNumber + 3))
                        {
                            string bgColor = "#efefef";
                            if (pageNumber == currentPageNumber)
                            {
                                nextPageIndex = pageNumber; 
                                bgColor = "#efefaa";
                            }
                            data += "<li style=\"width: 13px;display: inline-block; background-color: " + bgColor +
                                    "; padding: 5px 5px; border: 1px solid lightgray;\"><a href=\"" + path + "/" + Action + "?" + queryStringAll[queryStringIndex] + "\"> " + pageNumber + "</a></li>";//@Html.ActionLink(pageNumber.ToString(), "ViewResutls", new { limit = pageData.PageLimit }) +

                        }
                    }

                    queryStringIndex++;

                }

                int endPageNumber = EndPage.PageNumber;
                if (currentPageNumber != endPageNumber)
                {
                    pageString = NextPage.PageText;
                    data +=
                        "<li style=\"display: inline-block; background-color: #efefef; padding: 5px 5px; border: 1px solid lightgray;\"><a href=\"" + path + "/" + Action + "?" +queryStringAll[nextPageIndex] + "\"> " + pageString + "</a></li>";//@Html.ActionLink(pageString, "ViewResutls", new { limit = NextPage.PageLimit }) + 
                    pageString = EndPage.PageText;
                    data +=
                        "<li style=\"display: inline-block; background-color: #efefef; padding: 5px 5px; border: 1px solid lightgray;\"><a href=\"" + path + "/" + Action + "?" + queryStringAll.Last()+ "\"> " + pageString + "</a></li>";//@Html.ActionLink(pageString, "ViewResutls", new { limit = EndPage.PageLimit }) + 

                }

                data += "</ul>";
            }
            return data;
        }
    }
}