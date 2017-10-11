using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RestSharp;

namespace Evidence_osob_API
{
    /// <summary>
    /// Interakční logika pro MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            string url = "https://student.sps-prosek.cz/~horejvi14/api/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<Person>>(request);
            listwiew.ItemsSource = response.Data;
        }

        public void listViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            object obj = item.Content;
            EditPage EditPage = new EditPage();
            this.NavigationService.Navigate(new EditPage(obj as Person));
        }

        private void DisplayResults()
        {
            string url = "https://student.sps-prosek.cz/~horejvi14/api/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<Person>>(request);
            listwiew.ItemsSource = response.Data;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayResults();
        }

        int x = 0;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" || Name.Text == null)
            {
                MessageBox.Show("Zadejte jméno", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Name.Text.Any(c => char.IsDigit(c)) == true)
            {
                MessageBox.Show("Neplatné jméno.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (SurName.Text == "" || SurName.Text == null)
            {
                MessageBox.Show("Zadejte příjemní.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (SurName.Text.Any(c => char.IsDigit(c)) == true)
            {
                MessageBox.Show("Neplatné příjmení.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Gender.Text == "--Vyberte--")
            {
                MessageBox.Show("Vyberte pohlaví.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Int32.TryParse(RodneCislo1.Text, out x) == false || RodneCislo1.Text.ToString().Length != 6)
            {
                MessageBox.Show("Neplatné rodné číslo.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Int32.TryParse(RodneCislo2.Text, out x) == false || RodneCislo2.Text.ToString().Length != 4)
            {
                MessageBox.Show("Neplatné rodné číslo.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (BirthDate.Text == "" || BirthDate.Text == null)
            {
                MessageBox.Show("Zadejte datum narození.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                /*Person osoba = new Person();
                osoba.Name = Name.Text;
                osoba.SurName = SurName.Text;
                osoba.BirthNumber1 = RodneCislo1.Text;
                osoba.BirthNumber2 = RodneCislo2.Text;
                osoba.Gender = Gender.Text;
                osoba.BirthDate = BirthDate.SelectedDate.Value;*/
                //osoba.Added = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                //osoba.Edited = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                //Database.SaveItemAsync(item);

                string url = "https://student.sps-prosek.cz/~horejvi14/api/";
                var client = new RestClient(url);
                var request = new RestRequest(Method.PUT);
                request.AddParameter("Name", Name.Text);
                request.AddParameter("SurName", SurName.Text);
                request.AddParameter("BirthNumber1", RodneCislo1.Text);
                request.AddParameter("BirthNumber2", RodneCislo2.Text);
                request.AddParameter("Gender", Gender.Text);
                request.AddParameter("BirthDate", BirthDate.SelectedDate.Value);

                var response = client.Execute(request);
                MessageBox.Show(response.ToString(), "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                DisplayResults();

            }
        }
    }
}
