using Constant;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ViewModel
{
    public class LookupVM
    {
        public long ID { get; set; }
        public string Value
        {
            get
            {
                if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower() == UserLanguage.Arabic)
                    return Name_Ar;
                else
                    return Name_En;
            }


        }
        public string CSS_Class { get; set; }

        public string Name_Ar { get; set; }

        public string Name_En { get; set; }
    }


    public class WebLookupVM
    {
        public long ID { get; set; }
        public string Value
        {
            get
            {
                if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower() == UserLanguage.Arabic)
                    return Name_Ar;
                else
                    return Name_En;
            }

            set { }
        }
        public string CSSClss { get; set; }
        [JsonIgnore]
        public string Name_Ar { get; set; }
        [JsonIgnore]
        public string Name_En { get; set; }
        public string Name_Single_Ar { get; set; }
        public string Name_Single_En { get; set; }
    }
}
