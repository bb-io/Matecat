﻿using Apps.Matecat.Actions;
using Newtonsoft.Json;
using Tests.Matecat.Base;

namespace Tests.Matecat
{
    [TestClass]
    public class JobTests : TestBase
    {
        [TestMethod]
        public async Task DownloadJobXliff_IsSuccess()
        {
            var actions = new JobActions(InvocationContext, FileManager);
            var result = await actions.DownloadXliff("11251251/5b70f608a46b");

            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            Console.WriteLine("Returned project object:\n" + json);
            Assert.IsNotNull(result.File);
        }

        [TestMethod]
        public async Task DownloadJobTranslatedFiles_IsSuccess()
        {
            var actions = new JobActions(InvocationContext, FileManager);
            var result = await actions.DownloadTranslations("11368472/64f4c59aa93e");

            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            Console.WriteLine("Returned project object:\n" + json);
            Assert.IsNotNull(result);
        }
    }
}
