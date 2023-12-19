using ContactsApp.Models;

namespace ContactsApp
{
    public partial class MainPage : ContentPage
    {

        public ContactItem selectedItem;
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "contacts.db");
        ContactsRepository db;
        public MainPage()
        {
            InitializeComponent();
            db = new ContactsRepository(dbPath);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            contactsListView.ItemsSource = db.GetAllContactItems();
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = e.CurrentSelection.FirstOrDefault() as ContactItem;
            if (selectedItem == null) return;
            nameEntry.Text = selectedItem.Name;
            numberEntry.Text = selectedItem.Number;
            emailEntry.Text = selectedItem.Email;
            emailEntry.IsEnabled = false;
        }

        public void RefreshCollectionView()
        {
            contactsListView.ItemsSource = null;
            contactsListView.ItemsSource = db.GetAllContactItems();
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            ContactItem newContact = new ContactItem()
            {
                Name = nameEntry.Text,
                Number = numberEntry.Text,
                Email = emailEntry.Text
            };
            db.AddContact(newContact);
            RefreshCollectionView();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            clearFields();
        }

        private void clearFields()
        {
            nameEntry.Text = "";
            numberEntry.Text = "";
            emailEntry.Text = "";
            contactsListView.SelectedItem = null;
            emailEntry.IsEnabled = true;
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            //DisplayAlert("Success", $"Idex:{selectedItem.Id}", "OK");
            selectedItem.Name = nameEntry.Text;
            selectedItem.Number = numberEntry.Text;
            selectedItem.Email = emailEntry.Text;
            db.UpdateContact(selectedItem);
            RefreshCollectionView();
            clearFields();
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            db.DeleteContact(selectedItem.Id);
            RefreshCollectionView();
            clearFields();
        }
    }
}