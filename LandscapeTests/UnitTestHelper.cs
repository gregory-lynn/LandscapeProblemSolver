using System;
using System.Collections.Generic;
using System.Linq;
using LandscapeTests.Models;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace LandscapeTests
{
    public class Helpers
    {
        #region Public Properties
        public int[] RandomIntArray { get; set; }
        public List<TestObjects> TestObjectsList { get; set; }
        #endregion

        #region Constructor
        public Helpers()
        {
            RandomIntArray = RandomIntArrayGen();
            using StreamReader file = (StreamReader)GetInputFile("TestObjects.json");
            JsonSerializer serializer = new JsonSerializer();
            TestObjectsList = (List<TestObjects>)serializer.Deserialize(file, typeof(List<TestObjects>));
        }
        #endregion

        public int[] RandomIntArrayGen()
        {
            int PositionMin = 10;
            int HeightMin = 0;
            int Max = 32000;
            Random randNum = new Random();
            return Enumerable
                .Repeat(PositionMin, Max)
                .Select(i => randNum.Next(HeightMin, Max))
                .ToArray();
        }

        public static TextReader GetInputFile(string filename)
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            string path = "LandscapeTests"; 
            return new StreamReader(thisAssembly.GetManifestResourceStream(path + "." + filename));
        }
    }
}
