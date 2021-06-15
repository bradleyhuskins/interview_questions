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
            ["animal"] = new List<string> {"Chuck Norris once rode a nine foot grizzly bear through an automatic car wash, instead of taking a shower."},
            ["career"] = new List<string> {"In the beginning there was nothing...then Chuck Norris Roundhouse kicked that nothing in the face and said \"Get a job\". That is the story of the universe."},
            ["celebrity"] = new List<string> {"Chuck Norris smells what the Rock is cooking... because the Rock is Chuck Norris' personal chef."},
            ["dev"] = new List<string> {"Chuck Norris went out of an infinite loop."},
            ["fashion"] = new List<string> {"Chuck Norris does not follow fashion trends, they follow him. But then he turns around and punches them. Nobody follows Chuck Norris."},
            ["food"] = new List<string> {"Chuck Norris drinks napalm to quell his heartburn."},
            ["history"] = new List<string> {"After returning from World War 2 unscrathed, Bob Dole was congratulated by Chuck Norris with a handshake. The rest is history."},
            ["money"] = new List<string> {"When Chuck Norris sends in his taxes, he sends blank forms and includes only a picture of himself, crouched and ready to attack. Chuck Norris has not had to pay taxes, ever."},
            ["movie"] = new List<string> {"Scotty in Star Trek often says \"Ye cannae change the laws of physics.\" This is untrue. Chuck Norris can change the laws of physics. With his fists."},
            ["music"] = new List<string> {"Chuck Norris shot the sheriff, but he round house kicked the deputy."},
            ["political"] = new List<string> {"July 4th is Independence day. And the day Chuck Norris was born. Coincidence? I think not."},
            ["religion"] = new List<string> {"Chuck Norris does not make new year resolutions the new year makes Chuck Norris resolutions."},
            ["science"] = new List<string> {"Newton's Third Law is wrong: Although it states that for each action, there is an equal and opposite reaction, there is no force equal in reaction to a Chuck Norris roundhouse kick."},
            ["sport"] = new List<string> {"Chuck Norris plays racquetball with a waffle iron and a bowling ball."},
            ["travel"] = new List<string> {"For Spring Break '05, Chuck Norris drove to Madagascar, riding a chariot pulled by two electric eels."}
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
