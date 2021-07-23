using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LINQInManhatten
{
  class Program
  {
    static void Main(string[] args)
    {
      int count = 0;

      IList<JToken> featureList = ParseJSON()["features"].ToList();

      IEnumerable<JToken> neighborhoods =
      from feature in featureList
      where ((string)feature.SelectToken("properties.neighborhood") != "")
      select feature;

      Console.WriteLine(count);

      foreach(JToken feat in featureList)
      {
        count++;
      }
      Console.WriteLine(count);
    }

    public static JObject ParseJSON()
    {
      using(StreamReader reader = File.OpenText("../../../data.json"))
      {
        JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
        return o;
      }
    }


  }
}
