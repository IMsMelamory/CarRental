using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDesktop.ViewModel
{
    public class ObservableCollectionEx<TValue> : ObservableCollection<TValue>
    {

        public ObservableCollectionEx() : base() { }
        public ObservableCollectionEx(IEnumerable<TValue> values)
           : base(values)
        {
            this.EnsureEventWiring();
        }
        public ObservableCollectionEx(List<TValue> list)
           : base(list)
        {
            this.EnsureEventWiring();
        }

        public event EventHandler<ItemChangedEventArgs> ItemChanged;

        protected override void InsertItem(int index, TValue item)
        {
            base.InsertItem(index, item);

            var npc = item as INotifyPropertyChanged;
            if (npc != null)
                npc.PropertyChanged += this.HandleItemPropertyChanged;
        }

        protected override void RemoveItem(int index)
        {
            var item = this[index];

            base.RemoveItem(index);

            var npc = item as INotifyPropertyChanged;
            if (npc != null)
                npc.PropertyChanged -= this.HandleItemPropertyChanged;
        }

        protected override void SetItem(int index, TValue item)
        {
            var oldItem = this[index];

            base.SetItem(index, item);

            var npcOld = item as INotifyPropertyChanged;
            if (npcOld != null)
                npcOld.PropertyChanged -= this.HandleItemPropertyChanged;

            var npcNew = item as INotifyPropertyChanged;
            if (npcNew != null)
                npcNew.PropertyChanged += this.HandleItemPropertyChanged;
        }

        protected override void ClearItems()
        {
            var items = this.Items.ToList();

            base.ClearItems();

            foreach (var npc in items.OfType<INotifyPropertyChanged>().Cast<INotifyPropertyChanged>())
                npc.PropertyChanged -= this.HandleItemPropertyChanged;
        }


        private void HandleItemPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (typeof(TValue).IsAssignableFrom(sender.GetType()))
            {
                var item = (TValue)sender;
                var pos = this.IndexOf(item);
                this.OnItemChanged(item, pos, args.PropertyName);
            }
        }

        protected virtual void OnItemChanged(TValue item, int index, string propertyName)
        {
            if (this.ItemChanged != null)
                this.ItemChanged(this, new ItemChangedEventArgs(item, index, propertyName));
        }

        private void EnsureEventWiring()
        {
            foreach (var npc in this.Items.OfType<INotifyPropertyChanged>().Cast<INotifyPropertyChanged>())
            {
                npc.PropertyChanged -= this.HandleItemPropertyChanged;
                npc.PropertyChanged += this.HandleItemPropertyChanged;
            }
        }

        public class ItemChangedEventArgs : EventArgs
        {
            public ItemChangedEventArgs(TValue item, int index, string propertyName)
            {
                this.Item = item;
                this.Index = index;
                this.PropertyName = propertyName;
            }

            public int Index { get; private set; }
            public TValue Item { get; private set; }
            public string PropertyName { get; private set; }
        }
    }
}

