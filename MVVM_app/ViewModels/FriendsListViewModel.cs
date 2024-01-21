using MVVM_app.Views;
using MVVM_app.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVM_app.ViewModels
{
    public class FriendsListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<FriendViewModel> Friends { get; set; }
        public ICommand CreateFriendCommand { get; protected set; }
        public ICommand DeleteFriendCommand { get; protected set; }
        public ICommand SaveFriendCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        FriendViewModel selectedFriend;
        public INavigation Navigation { get; set; }

        public FriendsListViewModel() 
        { 
            Friends = new ObservableCollection<FriendViewModel>();
            CreateFriendCommand = new Command(CreateFriend);
            DeleteFriendCommand = new Command(DeleteFriend);
            SaveFriendCommand = new Command(SaveFriend);
            BackCommand = new Command(Back);
        }
        public FriendViewModel SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                if (selectedFriend != value)
                {
                    FriendViewModel tempFriend=value;
                    selectedFriend = null;
                    OnPropertyChanged("SelectedFriend");
                    Navigation.PushAsync(new FriendPage(tempFriend));
                }
            }
        }
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(v)); }
        }
        private void SaveFriend(object friendobject)
        {
            FriendViewModel friend = (FriendViewModel) friendobject;
            if(friend!=null && friend.IsValid && !Friends.Contains(friend))
            {
                Friends.Add(friend);
            }
            Back();
        }

        public void Back()
        {
            Navigation.PopAsync();
        }
        private void DeleteFriend(object friendobject)
        {
            FriendViewModel friend = (FriendViewModel)friendobject;
            if (friend != null)
            {
                Friends.Remove(friend);
                Back();
            }
        }

        private void CreateFriend()
        {
            Navigation.PushAsync(new FriendPage(new FriendViewModel() { ListViewModel=this}));
        }
    }
}
