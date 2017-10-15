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
    /// Interakční logika pro EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        public object obj;
        public int ID;
        public DateTime birthdate;
        public EditPage()
        {
            InitializeComponent();
        }

        public EditPage(Person person)
        {
            InitializeComponent();
            obj = person;
            ID = person.Id;
            Name.Text = person.Name;
            SurName.Text = person.Surname;
            RodneCislo1.Text = person.BirthNumber1;
            RodneCislo2.Text = person.BirthNumber2;
            BirthDate.SelectedDate = person.BirthDate;
            birthdate = person.BirthDate;
            if (person.Gender == "0")
            {
                Gender.Content = "Muž";
            }
            else
            {
                Gender.Content = "Žena";
            }
            //Gender.Content = person.Gender;
            //Added.Content = person.Added;
            //Edited.Content = person.Edited;
            Age.Content = person.Age;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //ListViewItem item = sender as ListViewItem;
            //object obj = item.Content;
            MessageBoxResult result = MessageBox.Show("Smazat záznam?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    //await Database.DeleteItemAsync(obj as TodoItem);
                    string url = "https://student.sps-prosek.cz/~horejvi14/api/";
                    var client = new RestClient(url);
                    var request = new RestRequest(Method.DELETE);
                    request.AddParameter("Id",ID);
                    var response = client.Execute(request);
                    MessageBox.Show(response.Content, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainPage Page1 = new MainPage();
                    this.NavigationService.Navigate(new MainPage());
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            /*TodoItem item = new TodoItem();
            item.ID = ID;
            item.Name = Name.Text;
            item.SurName = SurName.Text;
            item.RodneCislo = RodneCislo.Content.ToString();
            //item.BirthDate = new DateTime(birthdate.);
            item.Gender = Gender.Content.ToString();
            //item.Edited = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            //item.Added = ;
            Database.SaveItemAsync(item);*/

            string url = "https://student.sps-prosek.cz/~horejvi14/api/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.PUT);
            request.AddParameter("Id", ID);
            request.AddParameter("Name", Name.Text);
            request.AddParameter("Surname", SurName.Text);
            request.AddParameter("BirthNumber1", RodneCislo1.Text);
            request.AddParameter("BirthNumber2", RodneCislo2.Text);
            request.AddParameter("BirthDate", BirthDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"));
            //request.AddParameter("Gender", Gender.Content);
            if (Gender.Content.ToString() == "Muž")
            {
                request.AddParameter("Gender", 0);
            }
            else
            {
                request.AddParameter("Gender", 1);
            }
            
            var response = client.Execute(request);
            MessageBox.Show(response.Content, "Info", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage Page1 = new MainPage();
            this.NavigationService.Navigate(new MainPage());
        }
    }
}
