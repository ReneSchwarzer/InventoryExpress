using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryExpress.Model
{
    public class AttributeTextValue : Attribute
    {
        private string value;
        private string _value;
        
        /// <summary>
        /// Der Wert
        /// </summary>
        public string Value
        {
            get
            {
                return value.ToString();
            }
            set
            {
                if (!this.value.ToString().Equals(value))
                {
                    this.value = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AttributeTextValue()
        {
            value = string.Empty;
            _value = string.Empty;
        }

        /// <summary>
        /// Übernimmt die geänderten Daten
        /// </summary>
        public override void Commit()
        {
            base.Commit();

            _value = value;
        }

        /// <summary>
        /// Verwirft die geänderten Daten
        /// </summary>
        public override void Rollback()
        {
            base.Rollback();

            value = _value;
        }
    }
}
