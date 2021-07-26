using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace LINQInManhatten
{
    class Program
    {
        static void Main(string[] args)
        {
            RootObject data = GetData();
            Questions(data);
        }

        //Filtering through teh data and outputting data to the console.
        //<param name = "data">JSON concerted data of the neighborhood data</para>
        static void Questions(RootObject data)
        {
            // 1. Output all of the neighborhoods in thsi data list

            // total of 147 neighborhoods with 4 as "empties"

            var q1 = from neighborhood in data.features
                     select neighborhood;

            int count = 0;

            foreach (var answer in q1)
            {
                Console.WriteLine($"{++count}. {answer.properties.neighborhood}");
            }

            //2. Filter out all the neighborhoods tha do not have any names
            
            Console.WriteLine("======Q2======");

            var q2 = q1.Where(x => x.properties.neighborhood != "");
            count = 0;
            foreach (var item in q2)
            {
                Console.WriteLine($"{++count}. {item.properties.neighborhood}");
            }

            //3. Remove the Duplicates
            // gives 39 unique neighborhoods

            Console.WriteLine("======Q3======");

            var q3 = q2.GroupBy(x => x.properties.neighborhood)
                        .Select(grp => grp.First());

            //4. Rewrite the queries from above, and consolidate all into one single query.

            Console.WriteLine("======Q4======");

            var final = data.features.Where(x => x.properties.neighborhood != "")
                                        .GroupBy(x => x.properties.neighborhood)
                                        .Select(x => x.First());
            count = 0;
            foreach(var item in final)
            {
                Console.WriteLine($"{++count}. {item.properties.neighborhood}");
            }

            //5. Rewrite at least one of these questions only using the opposing method(example: Use LINQ Query statemetns instead of LINQ method calls and vice versa.

            Console.WriteLine("======Q5======");
            // rewriting 2nd question
            var rewrite = from name in q1
                          where name.properties.neighborhood != ""
                          select name;
            count = 0;
            foreach (var item in rewrite)
            {
                Console.WriteLine($"{++count}. {item.properties.neighborhood}");
            }
        }

        static RootObject GetData()
        {
            //NewtonSoft Docs
            {
                string jsonData = File.ReadAllText("../../../data.json");
                //convert JSON to classes

                // returns 147 neighborhoods
                //Note that we specify a type <RootObject> that jsonConvert will map all properties into
                // This is a class hich maps the entire JSON object.
                RootObject deserializedProduct = JsonConvert.DeserializeObject<RootObject>(jsonData);

                //Questions(deserializedProduct);
                return deserializedProduct;
            }
        }

        public class RootObject
        {
            public string type { get; set; }
            public List<Feature> features { get; set; }
        }

        public class Feature
        {
            public string type { get; set; }
            public Geometry geometry { get; set; }
            public Properties properties { get; set; }
        }

        public class Properties
        {
            public string zip { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string address { get; set; }
            public string borough { get; set; }
            public string neighborhood { get; set; }
            public string county { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public List<double> coordinates { get; set; }
        }
    }
}
