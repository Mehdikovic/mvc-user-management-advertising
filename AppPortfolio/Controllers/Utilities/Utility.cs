using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio {
    public static class Utility {
        public static string GetTags(string allTags) {
            string[] tags = allTags.Split(',');
            if (tags.Length > 15) {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < 15; ++i) {
                    sb.Append(tags[i]);
                    if (i != 14) 
                        sb.Append("،");
                }
                return sb.ToString();
            }
            return allTags;
        }
    }
}