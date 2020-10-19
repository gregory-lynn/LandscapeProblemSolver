using Xunit;
using LandscapeProblemSolver;
using System.Collections.Generic;
using LandscapeTests.Models;
using System.Linq;
using System;
using System.Diagnostics;

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

            log4net.Config.XmlConfigurator.Configure();
            _objects = _helper.TestObjectsList;

            Models.TestObjects TestObject = (from p in _objects where p.Name.Equals("SampleArray") select p).FirstOrDefault();
            // if we don't have our test object, fail the test 
            Assert.NotNull(TestObject);
           
            foreach (TestObjects _object in _objects)
            {
                try
                { 
                    _waterCollected = Problem.Solve(_object.Data.ToArray());

                    // let's do some assertions based on our JSON object and log based on expected result
                    // these objects ExpectedAmount should match the water collected 
                    if (Convert.ToBoolean(_object.ExpectedResult).Equals(true))
                    {
                        // Now let's do some logging to give more details in case it failed the assertion
                        if (!_waterCollected.ToString().Equals(_object.ExpectedAmount))
                        {
                            log.Warn(string.Format("Test object {0} water collected <> ExpectedAmount and ExpectedResult = true: test failed since ExpectedAmount {1} does not match actual value of {2}."
                            , _object.Name, _object.ExpectedAmount, _waterCollected.ToString()));
                        } else
                        {
                            log.Info(string.Format("Test object {0} water collected = ExpectedAmount and ExpectedResult = true: test passed.", _object.Name));
                        }
                        Assert.True(_waterCollected.ToString().Equals(_object.ExpectedAmount));
                    }
                    else if (Convert.ToBoolean(_object.ExpectedResult).Equals(false)) 
                    {
                        // These tests should fail or throw exceptions
                        // Exceptions are caught elsewhere so if we make it here, assume the values should not match
                        if (!_waterCollected.ToString().Equals(_object.ExpectedAmount))
                        {
                            log.Info(string.Format("Test object {0} water collected <> ExpectedAmount and ExpectedResult = false: negative test passed.", _object.Name));
                        }
                        Assert.False(_waterCollected.ToString().Equals(_object.ExpectedAmount));
                    }
                }
                catch(IndexOutOfRangeException e)
                {
                    if (Convert.ToBoolean(_object.ExpectedResult).Equals(false))
                    {
                        // now we log more details about our expected out of range execption
                        log.Info(string.Format("Test object {0} expected result: false; Test passed with OutOfRangeException: {1}", _object.Name, e.Message.ToString()));
                        Assert.NotNull(_object); // just a simple assertion expected to pass
                    }
                }
                catch (Exception e)
                {
                    if (Convert.ToBoolean(_object.ExpectedResult).Equals(false)) {
                        // only a negative test passing in null data should pass while throwing an exception
                        if (_object.Data == null)
                        {
                            log.Info(string.Format("Test object {0} expected result: false; Test passed with Exception: {1}", _object.Name, e.Message.ToString()));
                        }
                        else
                        {
                            log.Error(string.Format("Test object {0} expected result: false; Test failed with Exception: {1}", _object.Name, e.Message.ToString()));
                        }
                        Assert.Null(_object.Data);
                    } else {
                        // unexpected exception let's make sure to fail the test and log it
                        log.Error(string.Format("Test object {0}: unexpected exception caught: {1}", _object.Name, e.Message.ToString()));
                        Assert.Equal("foo", "bar");
                    }
                }
            }
        }

        [Fact]
        public void RandomArrayTest()
        {
            _randomArray = _helper.RandomIntArray;
            var timer = new Stopwatch();
            timer.Start();
            try
            {
                _waterCollected = Problem.Solve(_randomArray);
            }
            catch (Exception e)
            {
                // well this is unexpected, let's make sure our test fails in addition to logging more info
                log.Error(string.Format("Test RandomArrayTest threw an unexpected exception: {0}", e.Message.ToString()));
                Assert.Equal("foo", "bar");
            }
            timer.Stop();
            
            // now log how long it took, in case we are trying to make our problem solve method faster
            log.Info(string.Format("Test RandomArrayTest completed in {0}ms.", 
                timer.ElapsedMilliseconds.ToString()));
            // some simple assertions here since we have nothing to compare random numbers to
            Assert.IsType<int>(_waterCollected);
            Assert.True(_waterCollected > -1);
        }
    }
}
