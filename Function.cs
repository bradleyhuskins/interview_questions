// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// [START functions_helloworld_get]
using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace HelloWorld
{
    public class Function : IHttpFunction
    {
        private readonly Dictionary<string, List<string>> jokeCategories = new Dictionary<string, List<string>> {
            ["animal"] = new List<string> {"Chuck Norris' cat doesn't ask to go outside. He asks the outside to come in."},
            ["career"] = new List<string> {},
            ["celebrity"] = new List<string> {},
            ["dev"] = new List<string> {},
            ["explicit"] = new List<string> {},
            ["fashion"] = new List<string> {},
            ["food"] = new List<string> {},
            ["history"] = new List<string> {},
            ["money"] = new List<string> {},
            ["movie"] = new List<string> {},
            ["music"] = new List<string> {},
            ["political"] = new List<string> {},
            ["religion"] = new List<string> {},
            ["science"] = new List<string> {},
            ["sport"] = new List<string> {},
            ["travel"] = new List<string> {}
        };
        /*
        public async Task HandleAsync(HttpContext context)
        {
            List<String> categories = new List<string>(jokeCategories.Keys);
            await context.Response.WriteAsync(JsonSerializer.Serialize(categories));
        }
        */
        private readonly Random randomGenerator = new Random();
        private string GetRandomCategory()
        {
            List<String> categoryKeys = new List<string>(jokeCategories.Keys);
            int categoryBound = categoryKeys.Count-1;
            int randomKeyIndex = randomGenerator.Next(0, categoryBound);
            return categoryKeys[randomKeyIndex];
        }
        private string GetRandomJoke(string forCategory)
        {
            List<string> jokes = jokeCategories[forCategory];
            int jokeBound = jokes.Count-1;
            int randomIndex = randomGenerator.Next(0, jokeBound);
            return jokes[randomIndex];
        }
        public async Task HandleAsync(HttpContext context)
        {
            bool hasCategory = !String.IsNullOrEmpty(context.Request.Query["category"]);
            string requestedCategory;
            if (hasCategory)
            {
                requestedCategory = context.Request.Query["category"].ToString();
            }
            else
            {
                requestedCategory = GetRandomCategory();
            }

            String joke = GetRandomJoke(requestedCategory);

            await context.Response.WriteAsync(joke);
        }
    }
}
// [END functions_helloworld_get]
