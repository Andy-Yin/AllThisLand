using System;
using System.Collections.Generic;
using System.Text;

namespace ImageRunner
{
    public class BaiduAIResult
    {
        public string log_id { get; set; }
        public int words_result_num { get; set; }
        public List<Words> words_result { get; set; }

        //{
        //    "log_id": 2471272194,
        //    "words_result_num": 2,
        //    "words_result":
        //    [
        //    {"words": " TSINGTAO"},
        //    {"words": "青島睥酒"}
        //    ]
        //}
    }

    public class Words
    {
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
