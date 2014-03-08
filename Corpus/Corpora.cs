using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Corpus
{
    public class Corpora
    {
        private List<List<CorpEle>> mList = new List<List<CorpEle>>();

        public Corpora()
        {
        }

        public void Init()
        {
            int first_char = (int)'a';
            for (int i = 0; i < 26; i++)
            {
                List<CorpEle> lst_tmp = new List<CorpEle>();
                int second_char = (int)'a';
                for (int j = 0; j < 26; j++)
                {
                    CorpEle cele = new CorpEle((char)first_char, (char)second_char);
                    lst_tmp.Add(cele);
                    second_char++;
                }
                this.mList.Add(lst_tmp);
                first_char++;
            }
        }

        [XmlArray("Bigram")]
        [XmlArrayItem("Bigram-row", typeof(List<CorpEle>))]
        public List<List<CorpEle>> Bigram
        {
            get { return this.mList; }
            set { }
        }
    }
}
