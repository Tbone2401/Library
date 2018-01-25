using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace Library.Tests
{
    [TestClass]
    public class RouteTests
    {
        private HttpContextBase CreateHttpContext(
            string targetUrl = null,
            string httpMethod = "GET")
        {
            //Create Moq Request
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath)
                .Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod)
                .Returns(httpMethod);
            //Create Moq Response
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns<string>(s => s);
            //Create the moq context, using request and response
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request)
                .Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response)
                .Returns(mockResponse.Object);
            //Return moq'ed object
            return mockContext.Object;
        }

        private void TestRouteMatch(string url,
            string controller,
            string action,
            object routeProperties = null,
            string httpMethod = "GET")
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private bool TestIncomingRouteResult(RouteData routeResult,
            string controller,
            string action,
            object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) =>
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };
            bool result = valCompare(routeResult.Values["controller"], controller)
                          && valCompare(routeResult.Values["action"], action);
            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo pi in propInfo)
                {
                    if (!routeResult.Values.ContainsKey(pi.Name) &&
                        valCompare(routeResult.Values[pi.Name],
                            pi.GetValue(propertySet, null)))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private void TestRouteFail(string url)
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            //Act
            RouteData result = routes.GetRouteData(CreateHttpContext(url));
            //Assert
            Assert.IsTrue(result == null || result.Route == null);
        }

        [TestMethod]
        public void TestIncomingRoutes()
        {
            //Check for the URL that is hoped for
            TestRouteMatch("//", "Home", "Index");
            TestRouteMatch("//Customer", "Customer", "Index");
            TestRouteMatch("//Customer/List", "Customer", "List");
            TestRouteMatch("//Customer/List/All", "Customer", "List", new { id = "All" });
            TestRouteMatch("//Customer/List/All/Delete", "Customer", "List", new { id = "All", catchall = "Delete" });
            TestRouteMatch("//Customer/List/All/Delete/Perm", "Customer", "List", new { id = "All", catchall = "Delete/Perm" });
        }
    }

}
