﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;

namespace FindShelter.Web.Test
{
    [TestClass]
    public class WebAppTest
    {
        [TestMethod]
        public async Task ApiValuesDefaultReturnsString()
        {
            await RunWithTestServer("/api/Facilities", HttpStatusCode.OK, async response =>
            {
                string actual = await response.Content.ReadAsStringAsync();
                Assert.AreEqual("[\"a\",\"b\",\"c\"]", actual);
            });
        }

        private static async Task RunWithTestServer(string route, HttpStatusCode expectedStatusCode, Func<HttpResponseMessage, Task> validateResponse = null)
        {       
            using (var server = TestServer.Create(builder =>
            {
                var startup = new Startup();    
                builder.Properties.Add("Test",true);
                startup.Configuration(builder);
            }))
            {
                var response = await server.HttpClient.GetAsync(route);
                Assert.IsNotNull(response);
                Assert.AreEqual(expectedStatusCode, response.StatusCode);
                if (validateResponse != null) await validateResponse(response);
            }
        }

        [TestMethod]
        public async Task DefaultPageNotFound()
        {
            await RunWithTestServer("/", HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task CreateTestServer()
        {
            using (var server = TestServer.Create(app =>
            {
                app.UseErrorPage(); 
                app.Run(context => context.Response.WriteAsync("Hello world using OWIN TestServer"));
            }))
            {
                HttpResponseMessage response = await server.HttpClient.GetAsync("/");
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
