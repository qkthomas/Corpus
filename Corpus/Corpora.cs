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
        private int mNumberOfOccuredBigrams = 0;
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

        public void IrrigateBigram(char first_character, char second_character)
        {
            int base_index = (int)'a';
            int x_index = (int)first_character - base_index;
            int y_index = (int)second_character - base_index;
            this.mList[x_index][y_index]++;
            this.mNumberOfOccuredBigrams++;
        }

        [XmlArray("Bigram")]
        [XmlArrayItem("Bigram-row", typeof(List<CorpEle>))]
        public List<List<CorpEle>> Bigram
        {
            get { return this.mList; }
            set { }
        }

        [XmlElement("Total-number-of-bigrams")]
        public int NumberOfOccuredBigrams
        {
            get { return this.mNumberOfOccuredBigrams; }
            set { this.mNumberOfOccuredBigrams = value; }
        }
    }
}
