﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Unity;
using Prism;
using Prism.Ioc;
using Dockpad.Services;
using Acr.UserDialogs;

namespace Dockpad.Droid
{
    [Activity(Label = "Dockpad", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Secrets.SyncfusionAPIKey);

            UserDialogs.Init(this);

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        static IAPIService<IPRMAPI> PrmAPI = new APIService<IPRMAPI>(Config.DomainURL);
        static IAPIManager ApiManager = new APIManager(PrmAPI);
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           containerRegistry.RegisterInstance<IAPIManager>(ApiManager);
        }
    }
}