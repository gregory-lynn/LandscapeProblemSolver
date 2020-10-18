using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LandscapeTests.Models
{
    public class TestObjects
    {
        #region Private Properties
        private string _name;
        private string _description;
        private string _expectedamount;
        private string _expectedresult;
        private List<int> _data;
        #endregion

        [JsonProperty("name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [JsonProperty("description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        [JsonProperty("expected_amount")]
        public string ExpectedAmount
        {
            get { return _expectedamount; }
            set { _expectedamount = value; }
        }
        [JsonProperty("expected_result")]
        public string ExpectedResult
        {
            get { return _expectedresult; }
            set { _expectedresult = value; }
        }
        [JsonProperty("values")]
        public List<int> Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}
