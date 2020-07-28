using System.Collections.Generic;

namespace Core.BaiDuAI
{
    public class BaiduAIResult
    {
        public string log_id { get; set; }
        public int words_result_num { get; set; }
        public List<WordsArray> words_result { get; set; }

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

    public class WordsArray
    {
        public string Words { get; set; }

    }
}
