using Dockpad.Helpers;
using Dockpad.Models;
using Dockpad.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Dockpad.ViewModels
{
    public class ActivityPageViewModel : BaseViewModel
    {

        public ObservableCollection<Activity> Activities { get; set; } = new ObservableCollection<Activity>();

        private Activity _selectedActivity;

        private IAPIManager _apiManager;

        public DelegateCommand AddActivityCommand { get; set; }

        public DelegateCommand RefreshCommand { get; set; }

        public bool IsRefreshing { get; set; }

        private IPageDialogService _pageDialog;
        public Activity SelectedActivity
        {
            get => _selectedActivity;
            set
            {
                _selectedActivity = value;
                if (_selectedActivity == null)
                    return;
                ExecuteSelectActivity(_selectedActivity);
                SelectedActivity = null;
            }
        }
        public ActivityPageViewModel(INavigationService navigationService, IAPIManager apiManager, IPageDialogService pageDialog) : base(navigationService)
        {
            _apiManager = apiManager;
            _pageDialog = pageDialog;
            AddActivityCommand = new DelegateCommand(ExecuteAddActivity);
            RefreshCommand = new DelegateCommand(ExecuteRefresh);
            LoadActivities();
        }

        private async void LoadActivities()
        {
            var response = await _apiManager.GetActivities(Config.Token);
            if (response.IsSuccessStatusCode)
            {
                Activities.Clear();
                var json = await response.Content.ReadAsStringAsync();
                PaginatedResponse<Activity> activities = await Task.Run(() => JsonConvert.DeserializeObject<PaginatedResponse<Activity>>(json));
                foreach (Activity act in activities.Results)
                {
                    Activities.Add(act);
                }
            }
        }

        public async void ExecuteSelectActivity(Activity activity)
        {
            string message = $"Name: {activity.Name}\nOwner: {activity.Owner}\nDescription: {activity.Description}\nIs active? {activity.IsActive}\nLast time: {activity.LastTime}\n";

            var more = await _pageDialog.DisplayAlertAsync("Contact details", message, "More", "Ok");
            if (more)
            {
                string action = await _pageDialog.DisplayActionSheetAsync("What would you like to do?", "Cancel", null, "Edit", "Delete");

                if (action == "Delete")
                {
                    int idx = Activities.IndexOf(activity);
                    var res = await _apiManager.DeleteActivity(Config.Token, activity.Code);
                    if (res.IsSuccessStatusCode)
                    {
                        var json = await res.Content.ReadAsStringAsync();
                        Console.WriteLine();
                    }
                    else
                    {
                        var json = await res.Content.ReadAsStringAsync();
                        Console.WriteLine();
                    }
                    Activities.RemoveAt(idx);
                }
                else
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("activity", activity);
                    await _navigationService.NavigateAsync(new Uri(NavigationConstants.EditActivityPage, UriKind.Relative), navigationParams);
                }
            }
        }

        private async void ExecuteAddActivity()
        {
            await _navigationService.NavigateAsync(new Uri(NavigationConstants.EditActivityPage, UriKind.Relative));
        }

        public void ExecuteRefresh()
        {
            LoadActivities();
            IsRefreshing = false;
        }
    }
}
