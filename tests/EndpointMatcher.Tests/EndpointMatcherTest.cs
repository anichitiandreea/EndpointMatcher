using System.Collections.Generic;
using Xunit;

namespace EndpointMatcher.Tests
{
    public class EndpointMatcherTest
    {
        [Fact]
        public void GivenEndpointMatcherWhenSingleRouteExistsThenReturnMatchedRoute()
        {
            // Arrange
            var routes = new Dictionary<string, List<string>>
            {
                {
                    "users",
                    new List<string>()
                    {
                        "users/{name}", "users/{id}/applications"
                    }
                }
            };

            var endpointMatcher = new EndpointMatcher(routes);

            // Act
            var result = endpointMatcher.Match("users/Andreea");

            // Assert
            Assert.Equal("users/{name}", result);
        }

        [Fact]
        public void GivenEndpointMatcherWhenLongRouteExistsThenReturnMatchedRoute()
        {
            // Arrange
            var routes = new Dictionary<string, List<string>>
            {
                {
                    "users",
                    new List<string>()
                    {
                        "users/{name}",
                        "users/{id}/applications",
                        "users/{id:number}"
                    }
                },
                {
                    "categories",
                    new List<string>()
                    {
                        "categories/{id}/apps/{appId}"
                    }
                },
                {
                    "pages",
                    new List<string>()
                    {
                        "pages/{id:number}/sections/myPages",
                        "pages/{id}/sections"
                    }
                }
            };

            var endpointMatcher = new EndpointMatcher(routes);

            // Act
            var result = endpointMatcher.Match("pages/22/sections/myPages");

            // Assert
            Assert.Equal("pages/{id:number}/sections/myPages", result);
        }

        [Fact]
        public void GivenEndpointMatcherWhenManyPossibleRoutesExistThenReturnMatchedRoute()
        {
            // Arrange
            var routes = new Dictionary<string, List<string>>
            {
                {
                    "users",
                    new List<string>()
                    {
                        "users/{name:string}",
                        "users/{id:number}",
                        "users/signout"
                    }
                },
                {
                    "categories",
                    new List<string>()
                    {
                        "categories/{id}/apps/{appId}"
                    }
                },
                {
                    "pages",
                    new List<string>()
                    {
                        "pages/{id:number}/sections/myPages",
                        "pages/{id}/sections"
                    }
                }
            };

            var endpointMatcher = new EndpointMatcher(routes);

            // Act
            var result = endpointMatcher.Match("users/457633");

            // Assert
            Assert.Equal("users/{id:number}", result);
        }

        [Fact]
        public void GivenEndpointMatcherWhenStraightRouteExistsThenReturnMatchedRoute()
        {
            // Arrange
            var routes = new Dictionary<string, List<string>>
            {
                {
                    "users",
                    new List<string>()
                    {
                        "users/signout/pages/{name:string}",
                        "users/{id:number}",
                        "users/{name:string}",
                        "users/signout/pages/applications"
                    }
                }
            };

            var endpointMatcher = new EndpointMatcher(routes);

            // Act
            var result = endpointMatcher.Match("users/signout/pages/applications");

            // Assert
            Assert.Equal("users/signout/pages/applications", result);
        }

        [Fact]
        public void GivenEndpointMatcherWhenManySpecificRoutesExistThenReturnMatchedRoute()
        {
            // Arrange
            var routes = new Dictionary<string, List<string>>
            {
                {
                    "users",
                    new List<string>()
                    {
                        "users/signout/pages/{name:string}",
                        "users/{name:string}",
                        "users/{id:number}/pages/applications/{applicationId}",
                        "users/{id:number}/pages/applications/{applicationId:number}"
                    }
                }
            };

            var endpointMatcher = new EndpointMatcher(routes);

            // Act
            var result = endpointMatcher.Match("users/22/pages/applications/3");

            // Assert
            Assert.Equal("users/{id:number}/pages/applications/{applicationId:number}", result);
        }
    }
}
