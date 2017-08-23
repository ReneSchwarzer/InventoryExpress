using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace InventoryExpress.Model
{
    public class AttributeDateTimeValue : Attribute
    {
        private DateTime value;
        private DateTime _value;

        /// <summary>
        /// Der Wert
        /// </summary>
        public DateTime Value
        {
            get
            {
                return value;
            }
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AttributeDateTimeValue()
        {
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
