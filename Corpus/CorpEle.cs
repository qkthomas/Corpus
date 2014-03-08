using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Corpus
{
    [XmlRoot("Corpora_Element")]
    public class CorpEle
    {
        [XmlIgnore]
        public char mFirstCharacter;
        [XmlIgnore]
        public char mSecondCharacter;
        [XmlIgnore]
        private readonly double mSmoothing = 0.5;
        [XmlIgnore]
        private int mFrequency = 0;

        public CorpEle(char first_char, char second_char)
        {
            this.mFirstCharacter = first_char;
            this.mSecondCharacter = second_char;
        }

        public CorpEle()
        {
        }

        [XmlElement("First_Character")]
        public string FirstCharacter
        {
            get { return this.mFirstCharacter.ToString(); }
            set { this.mFirstCharacter = value[0]; }    //To find a better way or structure rather than just put value[0]
        }

        [XmlElement("Second_Character")]
        public string SecondCharacter
        {
            get { return this.mSecondCharacter.ToString(); }
            set { this.mSecondCharacter = value[0]; }
        }

        [XmlElement("Occured_Frequency")]
        public string Frequency
        {
            get { return this.mFrequency.ToString(); }
            set { this.mFrequency = Convert.ToInt32(value); }
        }

    }
}
