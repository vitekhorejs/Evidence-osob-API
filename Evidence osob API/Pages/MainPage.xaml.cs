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
using System.Text.RegularExpressions;

namespace Evidence_osob_API
{
    /// <summary>
    /// Interakční logika pro MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public string Url = "https://student.sps-prosek.cz/~horejvi14/api/";

        public MainPage()
        {
            InitializeComponent();
            listview.ItemsSource = GetData();
        }

        public void listViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            object obj = item.Content;
            EditPage EditPage = new EditPage();
            this.NavigationService.Navigate(new EditPage(obj as Person));
        }

        private List<Person> GetData(string SearchSurname = null)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.GET);      
            if (SearchSurname != null || SearchSurname != "")
            {
                request.AddParameter("Surname", SearchSurname);
            }
            var response = client.Execute<List<Person>>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Při komunikaci se serverem došlo k chybě.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            else
            {
                return response.Data;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchText.Text != "")
            {
                if (Regex.IsMatch(SearchText.Text, @"^[a-zA-Z]+$") == false)
                {
                    MessageBox.Show("Hledaný výraz je neplatný.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
                } else
                {
                    listview.ItemsSource = GetData(SearchText.Text);
                }
            }
            else
            {
                listview.ItemsSource = GetData();
            }
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
                MessageBox.Show("Neplatné rodné číslo.\nFormát rodného čísla:\n6 číslic (obsahuje informace o datu narození a pohlaví, lomítko, 4 číslíce (pořadové číslo)", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Int32.TryParse(RodneCislo2.Text, out x) == false || RodneCislo2.Text.ToString().Length != 4)
            {
                MessageBox.Show("Neplatné rodné číslo.\nFormát rodného čísla:\n6 číslic (obsahuje informace o datu narození a pohlaví, lomítko, 4 číslíce (pořadové číslo)", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (BirthDate.Text == "" || BirthDate.Text == null)
            {
                MessageBox.Show("Zadejte datum narození.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var client = new RestClient(Url);
                var request = new RestRequest(Method.POST);
                
                request.AddParameter("Name", Name.Text);
                request.AddParameter("Surname", SurName.Text);
                request.AddParameter("BirthNumber1", RodneCislo1.Text);
                request.AddParameter("BirthNumber2", RodneCislo2.Text);
                if (Gender.Text == "Muž")
                {
                    request.AddParameter("Gender", 0);
                }
                else
                {
                    request.AddParameter("Gender", 1);
                }
                request.AddParameter("BirthDate", BirthDate.SelectedDate.Value.Date);
                var response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    listview.ItemsSource = GetData();
                    MessageBox.Show(response.Content, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Při komunikaci se serverem došlo k chybě.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                }  
            }
        }
    }
}
