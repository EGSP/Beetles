using System.Collections.Generic;
using Egsp.Core;

namespace Game.Extensions
{
    public class PropertyBlock
    {
        private Dictionary<PropertyId, Property> _properties = new Dictionary<PropertyId, Property>();

        /// <summary>
        /// Если свойство уже было добавлено, то ничего не произойдет.
        /// </summary>
        public PropertyBlock Set(Option<Property> propertyO)
        {
            if (propertyO.IsNone)
                return this;
            
            var property = propertyO.Value;
            // Свойство уже установлено.
            if (_properties.ContainsKey(property.Id))
            {
                return this;
            }
            // Свойство не установлено.
            else
            {
                _properties.Add(property.Id, property);
                return this;
            }
        }

        public PropertyBlock Remove(PropertyId propertyId)
        {
            _properties.Remove(propertyId);
            return this;
        }
    }
}