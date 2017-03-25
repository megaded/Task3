using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Model;
using System.Windows.Input;
using System.Net;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using System.Windows;
using Client.Data;

namespace Client.ViewModel
{
   public class MainViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Setting> Settings { get; set; } = new ObservableCollection<Setting>();
        public ICommand GetResult { get; set; }
        public ResponceRepository repository;
        private List<Setting> buffer;
        private  const string url = "http://mighttrust.me/testcase/api/list/getitems?searchfield=";
        private string json;
        private string requestParams;
        public string RequestParams
        {
           get { return requestParams; }
            set
            {
                if (value != requestParams)
                {
                    requestParams = value;
                    OnPropertyChanged("RequestParams");
                }

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainViewModel()
        {
          repository = new ResponceRepository();
           repository.Connect();
            json = repository.GetLastResponce();
            buffer = JsonConvert.DeserializeObject<List<Setting>>(json);
            if (buffer != null)
            {
                UpdateSettings();
            }
            GetResult = new Command(() =>
            {
                
           StringBuilder stringUrl = new StringBuilder(url + requestParams);
            Uri urlRequest = new Uri(stringUrl.ToString());
            
            var request = (HttpWebRequest)WebRequest.Create(urlRequest);
            var responce = request.GetResponse();
            using (StreamReader reader = new StreamReader(responce.GetResponseStream()))
            {
                json = reader.ReadToEnd();
                responce.Close();
            }
                repository.UpdateLastRespoonce(json);
                buffer = JsonConvert.DeserializeObject<List<Setting>>(json);
           
                UpdateSettings();
            });
        }
        private void UpdateSettings()
        {
        
            Application.Current.Dispatcher.Invoke(() =>
            {
                Settings.Clear();
                foreach (var setting in buffer)
                {
                    Settings.Add(setting);
                }
            });
        }
    }
}
