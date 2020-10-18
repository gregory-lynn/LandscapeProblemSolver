using Xunit;
using LandscapeProblemSolver;
using System.Runtime.InteropServices.ComTypes;
using System.Collections.Generic;
using LandscapeTests.Models;
using log4net;
using System.Security.Cryptography;
using System.Linq;

namespace LandscapeTests
{
    public class UnitTests
    {
        private Helpers _helper = new Helpers();
        private int[] _randomArray;
        private int _waterCollected;
        private List<TestObjects> _objects;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Fact]
        public void TestJsonObjects()
        {
            _objects = _helper.TestObjectsList;

            //// TODO: Either fix this code or get rid of it
            //Models.TestObjects TestObject obj = (from p in _objects where p.Name.Equals("SampleArray") select p).FirstOrDefault();
            //if (TestObject == null)
            //Assert.Collection(_objects, item => Assert.Contains("SampleArray", item.Name));
            
            foreach (TestObjects _object in _objects)
            {
                try
                { 
                    _waterCollected = Problem.Solve(_object.Data.ToArray());

                    // let's do some assertions based on our JSON object and log based on expected result
                    Assert.True(_waterCollected.Equals(_object.ExpectedAmount));

                    if (!_waterCollected.Equals(_object.ExpectedAmount)) {
                        log.Warn(string.Format("Test Object: {0} failed! Expected water collected {1} does not match actual value of {2}."
                            , _object.Name, _object.ExpectedAmount, _waterCollected));
                    } else if (_object.ExpectedResult.Equals(true)) {
                        log.Info(string.Format("Test object {0} passed.... Congratulations!!", _object.Name));
                    } else {
                        log.Info(string.Format("Test object {0} had a different result from what was expected, check the test object config.", _object.Name));
                    }
                }
                catch
                {
                    if (_object.ExpectedResult.Equals(false)) {
                        log.Info(string.Format("Test object {0} threw an exception but this may be expected, please verify the test config", _object.Name));
                    }
                }
                
            }
        }

        [Fact]
        public void RandomArrayTest()
        {
            _randomArray = _helper.RandomIntArray;
            _waterCollected = Problem.Solve(_randomArray);

            Assert.IsType<int>(_waterCollected);
            Assert.True(_waterCollected > -1);

        }
    }
}
