using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlMenu
{
    [Serializable, XmlRoot("menu")]
    public class Menu
    {
        [XmlElement(ElementName = "id")]
        public string ID { get; set; }

        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    public class SubMenu
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        [XmlElement(ElementName = "displayName")]
        public string DisplayName { get; set; }

        [XmlElement(ElementName = "path")]
        public Path Path { get; set; }

        [XmlElement(ElementName = "subMenu")]
        public List<SubMenu> SubMenus { get; set; }
    }

    public class Path
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}