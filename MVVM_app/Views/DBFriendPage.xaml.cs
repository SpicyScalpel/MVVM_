using MVVM_app.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Messaging;
using System;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.ComponentModel;
using System.Collections.Generic;
using SQLite;

namespace MVVM_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DBFriendPage : ContentPage, INotifyPropertyChanged
    {
        private string _photoPath;
        StackLayout stackLayout;
        Button selectButton;

        private readonly Dictionary<string, string> zodiacImages = new Dictionary<string, string>
        {
            { "Aries", "drawable/Aries.jpg" },
            { "Taurus", "drawable/Taurus.jpg" },
            { "Gemini", "drawable/Gemini.jpg" },
            { "Cancer", "drawable/Cancer.jpg" },
            { "Leo", "drawable/Lion.jpg" },
            { "Virgo", "drawable/Virgo.jpg" },
            { "Libra", "drawable/Libra.jpg" },
            { "Scorpio", "drawable/Scorpio.jpg" },
            { "Sagittarius", "drawable/Sagittarius.jpg" },
            { "Capricorn", "drawable/Capricorn.jpg" },
            { "Aquarius", "drawable/Aquarius.jpg" },
            { "Pisces", "drawable/Pisces.jpg" }
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public string PhotoPath
        {
            get { return _photoPath; }
            set
            {
                if (_photoPath != value)
                {
                    _photoPath = value;
                    OnPropertyChanged(nameof(PhotoPath));
                }
            }
        }

        public DBFriendPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Пример, как вы можете получить объект Friend из BindingContext
            if (BindingContext is Friend friend)
            {
                // Восстанавливаем путь к изображению при загрузке формы
                if (!string.IsNullOrEmpty(friend.PhotoPath))
                {
                    selectedImage.Source = ImageSource.FromFile(friend.PhotoPath);
                    PhotoPath = friend.PhotoPath; // Обновляем PhotoPath
                }
            }

            // Добавляем элементы в созданный ранее Picker
            foreach (var zodiacSign in zodiacImages.Keys)
            {
                OnePicker.Items.Add(zodiacSign);
            }
        }

        private void ChoosePhoto_Clicked(object sender, EventArgs e)
        {
            // Проверяем, созданы ли элементы, и создаем их, если они еще не существуют
            if (selectButton == null)
            {
                selectButton = new Button
                {
                    Text = "Kinnita",
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                selectButton.Clicked += OnSelectButtonClicked;
            }

            if (stackLayout == null)
            {
                stackLayout = new StackLayout
                {
                    Children =
                    {
                        OnePicker,
                        selectButton,
                        selectedImage
                    }
                };
            }

            // Обновляем Content страницы существующими элементами
            Content = stackLayout;
        }

        private void OnSelectButtonClicked(object sender, EventArgs e)
        {
            var selectedZodiac = OnePicker.SelectedItem?.ToString();
            if (selectedZodiac != null && zodiacImages.ContainsKey(selectedZodiac))
            {
                var photoPath = zodiacImages[selectedZodiac];
                selectedImage.Source = ImageSource.FromFile(photoPath);

                // Сохраняем путь к изображению внутри объекта Friend
                if (BindingContext is Friend friend)
                {
                    friend.PhotoPath = photoPath;
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // Когда свойство объекта изменяется, вызов метода OnPropertyChanged генерирует событие PropertyChanged, и система привязки данных автоматически обновляет интерфейс пользователя(UI) для отображения новых значений.
        }

        private void Salvesta_Clicked(object sender, EventArgs e)
        {
            Friend friend = (Friend)BindingContext;
            if (!string.IsNullOrEmpty(friend.Name))
            {
                App.Database.SaveItem(friend);
            }

            Navigation.PopAsync();
        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            Friend friend = (Friend)BindingContext;
            App.Database.DeleteItem(friend.ID);
            Navigation.PopAsync();
        }

        private void Loobu_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void EditButton_Clicked(object sender, EventArgs e)
        {
            // Enable editing controls
            EnableEditing(true);
        }

        private void EnableEditing(bool enable)
        {
            EntryName.IsEnabled = enable;
            DatePickerBirthday.IsEnabled = enable;
            EntryEmail.IsEnabled = enable;
            EntryPhoneNumber.IsEnabled = enable;
        }

        private async void sms_btn_Clicked(object sender, EventArgs e)
        {
            var sms = CrossMessaging.Current.SmsMessenger;
            if (sms.CanSendSms) 
            {
                sms.SendSms(EntryPhoneNumber.Text, tel_nr.Text);
                // Открываем страницу с сообщениями
                await Navigation.PushAsync(new MessagesPage());
            }

        }

    }
}
