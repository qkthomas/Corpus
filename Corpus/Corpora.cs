using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlSerializer_Test
{
    public class Corpora
    {
        public List<List<int>> mList = new List<List<int>>();

        public Corpora()
        {
            for (int i = 0; i < 26; i++)
            {
                List<int> lst_tmp = new List<int>();
                for (int j = 0; j < 26; j++)
                {
                    lst_tmp.Add(1);
                }
                this.mList.Add(lst_tmp);
            }
        }
    }
}
