using S7NetWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSpecificTools.PlcConnectivity
{
    public class TagGroup
    {
        private string m_Name;
        private Dictionary<string, EmbedTag> dictionary_EmbedTags;
        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }
        public Dictionary<string, EmbedTag> EmbedTags
        {
            get
            {
                return this.dictionary_EmbedTags;
            }
            set
            {
                this.dictionary_EmbedTags = value;
            }
        }
        public TagGroup(string Name) : base()
        {
            this.dictionary_EmbedTags = new Dictionary<string, EmbedTag>();
            this.m_Name = Name;
        }
    }

    public class EmbedTag
    {
        private string m_Name;
        private string accessaddress;
        private string dataType;
        private string desc;
        private string accessType;
        private Tag _tagCell;
        private List<Tag> _tagList = new List<Tag>();

        public List<Tag> tagList
        {
            get
            {
                return _tagList;
            }
        }

        public Tag tagCell
        {
            get
            {
                return _tagCell;
            }
        }

        public string AccessType
        {
            get
            {
                return this.accessType;
            }
            set
            {
                this.accessType = value;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        public string Accessaddress
        {
            get
            {
                return this.accessaddress;
            }
            set
            {
                this.accessaddress = value;
            }
        }
        public string DataType
        {
            get
            {
                return this.dataType;
            }
            set
            {
                this.dataType = value;
            }
        }
        public string Desc
        {
            get
            {
                return this.desc;
            }
            set
            {
                this.desc = value;
            }
        }

        public EmbedTag(string Name, string AccessType, string AccessAddress, string dataType, string Desc) : base()
        {
            this.m_Name = Name;
            this.accessType = AccessType;
            this.accessaddress = AccessAddress;
            this.dataType = dataType;
            this.desc = Desc;
            _tagCell = new Tag(AccessAddress, 0, AccessAddress);
            _tagList.Add(_tagCell);
        }

    }
}
