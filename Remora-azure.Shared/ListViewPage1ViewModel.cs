using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using System;

namespace Remora_azure.Shared
{
	class TodoItem : ITodoItem
	{
		public string Id { get; set; }
		public byte[] Version { get; set; }
		public DateTimeOffset? CreatedAt { get; set; }
		public DateTimeOffset? UpdatedAt { get; set; }
		public bool Deleted { get; set; }
		public string Text { get; set; }
		public bool Complete { get; set; }
	}

	class ListViewPage1ViewModel : INotifyPropertyChanged
    {
		private MobileServiceClient _client = new MobileServiceClient("http://localhost:59991/");
		public ObservableCollection<TodoItem> Items { get; }
        public ObservableCollection<Grouping<string, TodoItem>> ItemsGrouped { get; private set; }

        public ListViewPage1ViewModel()
        {
			Items = new ObservableCollection<TodoItem>();
			
            

            RefreshDataCommand = new Command(
                async () => await RefreshData());
        }

        public ICommand RefreshDataCommand { get; }

        async Task RefreshData()
        {
            IsBusy = true;
			var data = await _client.GetTable<TodoItem>().ToListAsync();
			foreach (var item in data)
			{
				Items.Add(item);
			}
			var sorted = from item in Items
						 orderby item.Text
						 group item by item.Text[0].ToString() into itemGroup
						 select new Grouping<string, TodoItem>(itemGroup.Key, itemGroup);

			ItemsGrouped = new ObservableCollection<Grouping<string, TodoItem>>(sorted);

			IsBusy = false;
        }

        bool busy;
	
		public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Key { get; private set; }

            public Grouping(K key, IEnumerable<T> items)
            {
                Key = key;
                foreach (var item in items)
                    this.Items.Add(item);
            }
        }
    }
}