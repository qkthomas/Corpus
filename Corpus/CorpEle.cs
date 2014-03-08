using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlSerializer_Test
{
    [XmlRoot("Corpora_Element")]
    public class CorpEle
    {
        [XmlIgnore]
        public readonly char mFirstCharacter;
        [XmlIgnore]
        public readonly char mSecondCharacter;
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
            set { }
        }

        [XmlElement("Second_Character")]
        public string SecondCharacter
        {
            get { return this.mSecondCharacter.ToString(); }
            set { }
        }

        [XmlElement("Occured_Frequency")]
        public string Frequency
        {
            get { return this.mFrequency.ToString(); }
            set { }
        }

    }
}
