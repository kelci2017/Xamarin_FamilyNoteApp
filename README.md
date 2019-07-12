# Xamarin_FamilyNoteApp

**Mobile app with Xamarin for both iOS & Android**

**Ideas**

* iOS & Android apps shared the same code base with Xamarin
* The images should be put in both iOS and Android resources, and the images in iOS "Build Action" shoule be BundleResource
* The customed UI cell should be created for both iOS and Android
* When debuging the Android app, the startup project should be selected as Android and then app can be installed in the connected device
* For iOS, it's a little complicated, a mac has to be connected if you are using a windows laptop. Then the starup project should be iOS and all available devices will be shown in the menue.
* Xamarin use the MVVM design, data is placed in the Model folder, and all view controllers and views are in the View folder, viewmodels and in the ViewModel folder.
* MVVM uses MessagingCenter.Send() when there is data update and viewmodels or views use MessagingCenter.Subscribe() to observe the data change and update the binded data in views
* Xamarin forms use the data binding, the binded data could be from views or viewmodel
* Fetching data from server is multi-threading using Task, so that the data task is processed in the background, will not affect the UI thread
* Please check the backend code from repos https://github.com/kelci2017/Authentication-server & https://github.com/kelci2017/App-server
