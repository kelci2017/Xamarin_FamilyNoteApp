package mono;

import java.io.*;
import java.lang.String;
import java.util.Locale;
import java.util.HashSet;
import java.util.zip.*;
import java.util.Arrays;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.util.Log;
import mono.android.Runtime;

public class MonoPackageManager {

	static Object lock = new Object ();
	static boolean initialized;

	static android.content.Context Context;

	public static void LoadApplication (Context context, ApplicationInfo runtimePackage, String[] apks)
	{
		synchronized (lock) {
			if (context instanceof android.app.Application) {
				Context = context;
			}
			if (!initialized) {
				android.content.IntentFilter timezoneChangedFilter  = new android.content.IntentFilter (
						android.content.Intent.ACTION_TIMEZONE_CHANGED
				);
				context.registerReceiver (new mono.android.app.NotifyTimeZoneChanges (), timezoneChangedFilter);

				Locale locale       = Locale.getDefault ();
				String language     = locale.getLanguage () + "-" + locale.getCountry ();
				String filesDir     = context.getFilesDir ().getAbsolutePath ();
				String cacheDir     = context.getCacheDir ().getAbsolutePath ();
				String dataDir      = getNativeLibraryPath (context);
				ClassLoader loader  = context.getClassLoader ();
				java.io.File external0 = android.os.Environment.getExternalStorageDirectory ();
				String externalDir = new java.io.File (
							external0,
							"Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath ();
				String externalLegacyDir = new java.io.File (
							external0,
							"../legacy/Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath ();

				System.loadLibrary("monodroid");

				Runtime.init (
						language,
						apks,
						getNativeLibraryPath (runtimePackage),
						new String[]{
							filesDir,
							cacheDir,
							dataDir,
						},
						loader,
						new String[] {
							externalDir,
							externalLegacyDir
						},
						MonoPackageManager_Resources.Assemblies,
						context.getPackageName (),
						android.os.Build.VERSION.SDK_INT,
						mono.android.app.XamarinAndroidEnvironmentVariables.Variables);
				
				mono.android.app.ApplicationRegistration.registerApplications ();
				
				initialized = true;
			}
		}
	}

	public static void setContext (Context context)
	{
		// Ignore; vestigial
	}

	static String getNativeLibraryPath (Context context)
	{
	    return getNativeLibraryPath (context.getApplicationInfo ());
	}

	static String getNativeLibraryPath (ApplicationInfo ainfo)
	{
		if (android.os.Build.VERSION.SDK_INT >= 9)
			return ainfo.nativeLibraryDir;
		return ainfo.dataDir + "/lib";
	}

	public static String[] getAssemblies ()
	{
		return MonoPackageManager_Resources.Assemblies;
	}

	public static String[] getDependencies ()
	{
		return MonoPackageManager_Resources.Dependencies;
	}

	public static String getApiPackageName ()
	{
		return MonoPackageManager_Resources.ApiPackageName;
	}
}

class MonoPackageManager_Resources {
	public static final String[] Assemblies = new String[]{
		/* We need to ensure that "FamilyNoteApp.Android.dll" comes first in this list. */
		"ExifLib.dll",
		"FamilyNoteApp.dll",
		"FormsViewGroup.dll",
		"Microsoft.AspNetCore.Antiforgery.dll",
		"Microsoft.AspNetCore.Authentication.Abstractions.dll",
		"Microsoft.AspNetCore.Authentication.Core.dll",
		"Microsoft.AspNetCore.Authorization.dll",
		"Microsoft.AspNetCore.Authorization.Policy.dll",
		"Microsoft.AspNetCore.Cryptography.Internal.dll",
		"Microsoft.AspNetCore.DataProtection.Abstractions.dll",
		"Microsoft.AspNetCore.DataProtection.dll",
		"Microsoft.AspNetCore.Diagnostics.Abstractions.dll",
		"Microsoft.AspNetCore.Hosting.Abstractions.dll",
		"Microsoft.AspNetCore.Hosting.Server.Abstractions.dll",
		"Microsoft.AspNetCore.Html.Abstractions.dll",
		"Microsoft.AspNetCore.Http.Abstractions.dll",
		"Microsoft.AspNetCore.Http.dll",
		"Microsoft.AspNetCore.Http.Extensions.dll",
		"Microsoft.AspNetCore.Http.Features.dll",
		"Microsoft.AspNetCore.JsonPatch.dll",
		"Microsoft.AspNetCore.Mvc.Abstractions.dll",
		"Microsoft.AspNetCore.Mvc.Core.dll",
		"Microsoft.AspNetCore.Mvc.DataAnnotations.dll",
		"Microsoft.AspNetCore.Mvc.Formatters.Json.dll",
		"Microsoft.AspNetCore.Mvc.Razor.dll",
		"Microsoft.AspNetCore.Mvc.Razor.Extensions.dll",
		"Microsoft.AspNetCore.Mvc.RazorPages.dll",
		"Microsoft.AspNetCore.Mvc.ViewFeatures.dll",
		"Microsoft.AspNetCore.Razor.dll",
		"Microsoft.AspNetCore.Razor.Language.dll",
		"Microsoft.AspNetCore.Razor.Runtime.dll",
		"Microsoft.AspNetCore.ResponseCaching.Abstractions.dll",
		"Microsoft.AspNetCore.Routing.Abstractions.dll",
		"Microsoft.AspNetCore.Routing.dll",
		"Microsoft.AspNetCore.WebUtilities.dll",
		"Microsoft.CodeAnalysis.CSharp.dll",
		"Microsoft.CodeAnalysis.dll",
		"Microsoft.CodeAnalysis.Razor.dll",
		"Microsoft.DotNet.PlatformAbstractions.dll",
		"Microsoft.Extensions.Caching.Abstractions.dll",
		"Microsoft.Extensions.Caching.Memory.dll",
		"Microsoft.Extensions.Configuration.Abstractions.dll",
		"Microsoft.Extensions.DependencyInjection.Abstractions.dll",
		"Microsoft.Extensions.DependencyInjection.dll",
		"Microsoft.Extensions.DependencyModel.dll",
		"Microsoft.Extensions.FileProviders.Abstractions.dll",
		"Microsoft.Extensions.FileProviders.Composite.dll",
		"Microsoft.Extensions.Hosting.Abstractions.dll",
		"Microsoft.Extensions.Localization.Abstractions.dll",
		"Microsoft.Extensions.Localization.dll",
		"Microsoft.Extensions.Logging.Abstractions.dll",
		"Microsoft.Extensions.ObjectPool.dll",
		"Microsoft.Extensions.Options.dll",
		"Microsoft.Extensions.Primitives.dll",
		"Microsoft.Extensions.WebEncoders.dll",
		"Microsoft.Net.Http.Headers.dll",
		"Microsoft.Win32.Registry.dll",
		"Newtonsoft.Json.Bson.dll",
		"Newtonsoft.Json.dll",
		"System.Collections.Immutable.dll",
		"System.Diagnostics.DiagnosticSource.dll",
		"System.Reflection.Metadata.dll",
		"System.Runtime.CompilerServices.Unsafe.dll",
		"System.Security.AccessControl.dll",
		"System.Security.Cryptography.Xml.dll",
		"System.Security.Permissions.dll",
		"System.Security.Principal.Windows.dll",
		"System.Text.Encodings.Web.dll",
		"Xamarin.Android.Arch.Core.Common.dll",
		"Xamarin.Android.Arch.Core.Runtime.dll",
		"Xamarin.Android.Arch.Lifecycle.Common.dll",
		"Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll",
		"Xamarin.Android.Arch.Lifecycle.LiveData.dll",
		"Xamarin.Android.Arch.Lifecycle.Runtime.dll",
		"Xamarin.Android.Arch.Lifecycle.ViewModel.dll",
		"Xamarin.Android.Support.Animated.Vector.Drawable.dll",
		"Xamarin.Android.Support.Annotations.dll",
		"Xamarin.Android.Support.AsyncLayoutInflater.dll",
		"Xamarin.Android.Support.Collections.dll",
		"Xamarin.Android.Support.Compat.dll",
		"Xamarin.Android.Support.CoordinaterLayout.dll",
		"Xamarin.Android.Support.Core.UI.dll",
		"Xamarin.Android.Support.Core.Utils.dll",
		"Xamarin.Android.Support.CursorAdapter.dll",
		"Xamarin.Android.Support.CustomTabs.dll",
		"Xamarin.Android.Support.CustomView.dll",
		"Xamarin.Android.Support.Design.dll",
		"Xamarin.Android.Support.DocumentFile.dll",
		"Xamarin.Android.Support.DrawerLayout.dll",
		"Xamarin.Android.Support.Fragment.dll",
		"Xamarin.Android.Support.Interpolator.dll",
		"Xamarin.Android.Support.Loader.dll",
		"Xamarin.Android.Support.LocalBroadcastManager.dll",
		"Xamarin.Android.Support.Media.Compat.dll",
		"Xamarin.Android.Support.Print.dll",
		"Xamarin.Android.Support.SlidingPaneLayout.dll",
		"Xamarin.Android.Support.SwipeRefreshLayout.dll",
		"Xamarin.Android.Support.Transition.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.Android.Support.v7.AppCompat.dll",
		"Xamarin.Android.Support.v7.CardView.dll",
		"Xamarin.Android.Support.v7.MediaRouter.dll",
		"Xamarin.Android.Support.v7.Palette.dll",
		"Xamarin.Android.Support.v7.RecyclerView.dll",
		"Xamarin.Android.Support.Vector.Drawable.dll",
		"Xamarin.Android.Support.VersionedParcelable.dll",
		"Xamarin.Android.Support.ViewPager.dll",
		"Xamarin.Essentials.dll",
		"Xamarin.Forms.Core.dll",
		"Xamarin.Forms.Platform.Android.dll",
		"Xamarin.Forms.Platform.dll",
		"Xamarin.Forms.Xaml.dll",
		"XamForms.Controls.Calendar.dll",
		"XamForms.Controls.Calendar.Droid.dll",
		"XLabs.Core.dll",
		"XLabs.Forms.dll",
		"XLabs.Forms.Droid.dll",
		"XLabs.Ioc.dll",
		"XLabs.Platform.dll",
		"XLabs.Platform.Droid.dll",
		"XLabs.Serialization.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = "Mono.Android.Platform.ApiLevel_28";
}
