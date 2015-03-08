using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using SomeBasicOrigoDbApp.Core;
using System.Dynamic;

namespace SomeBasicOrigoDbApp.Tests
{
    public class XmlImport
    {
        private readonly XNamespace _ns;
        private readonly XDocument xDocument;
        public XmlImport(XDocument xDocument, XNamespace ns)
        {
            _ns = ns;
            this.xDocument = xDocument;
        }

        public IEnumerable<dynamic> Parse(string name, Action<dynamic> onParsedEntity = null)
        {
            var ns = _ns;
            var db = xDocument.Root;
            var elements = db.Elements(ns + name);
            var list = new List<dynamic>();
            foreach (var element in elements)
            {
                dynamic secondValue = new Entity( element, _ns);
                if (null != onParsedEntity) onParsedEntity( secondValue);
                list.Add((object)secondValue);
            }
            return list;
        }

        private class Entity: DynamicObject
        {
            private readonly XElement _element;
            private readonly XNamespace _ns;
            public Entity( XElement element, XNamespace ns)
            {
                _element = element;
                _ns = ns;
            }

            public override IEnumerable<string> GetDynamicMemberNames()
            {
                return _element.Elements().Select(e => e.Name.LocalName);
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                var name = binder.Name;
                var type = binder.ReturnType;
                var el = _element.Element(_ns + name);
                result = Convert.ChangeType(el.Value, type);
                if ( result != null )return true;
                result = null;
                return false;
            }
        }
    }
}
