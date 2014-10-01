using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace XamarinRemotingTest
{
	[Activity (Label = "XamarinRemotingTest", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
				var uri = Android.Net.Uri.Parse ("tel:1112223333");
				var intent = new Intent (Intent.ActionView, uri); 
				StartActivity (intent); 
			};

			MyTests ();
		}

		private void MyTests()
		{
			var knownTypes = new List<Type>{ typeof(TestDataB), typeof(object[]), typeof(int), typeof(string) };
			DataContractSerializer xmlSerializer = new DataContractSerializer (typeof(TestDataA), knownTypes);

			//var jsonSettings = new DataContractJsonSerializerSettings ();
			//jsonSettings.RootName = "root";
			//jsonSettings.KnownTypes = knownTypes;
			//jsonSettings.EmitTypeInformation = EmitTypeInformation.Always;
			//DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(TestDataA), jsonSettings);	// this crashes during serialization
			//jsonSerializer = new DataContractJsonSerializer (typeof(TestDataA), knownTypes);	// this doesn't give type information

			TestDataA data = new TestDataB ();
			Object[] dataArray = new Object[] {28, "jakis tekst", new TestDataA(), new TestDataB()};

			using (MemoryStream stream = new MemoryStream ())
			{
				xmlSerializer.WriteObject (stream, data);
				string encoded = Encoding.UTF8.GetString(stream.ToArray());

				stream.Position = 0;
				Object deserialized = xmlSerializer.ReadObject (stream);
				string deserializedTypeName = deserialized.GetType().Name;

				deserializedTypeName.ToString ();
			}


			using (MemoryStream stream = new MemoryStream ())
			{
				xmlSerializer.WriteObject (stream, dataArray);
				string encoded = Encoding.UTF8.GetString(stream.ToArray());

				stream.Position = 0;
				Object deserialized = xmlSerializer.ReadObject (stream);
				string deserializedTypeName = deserialized.GetType().Name;

				deserializedTypeName.ToString ();
			}

			// binary serialization
			IFormatter formatter = new BinaryFormatter ();
			using (MemoryStream stream = new MemoryStream ())
			{
				formatter.Serialize (stream, data);

				stream.Position = 0;
				Object deserialized = formatter.Deserialize (stream);
				string deserializedTypeName = deserialized.GetType().Name;
			}

			using (MemoryStream stream = new MemoryStream ())
			{
				formatter.Serialize (stream, dataArray);

				stream.Position = 0;
				Object deserialized = formatter.Deserialize (stream);
				string deserializedTypeName = deserialized.GetType().Name;
			}

//			using (MemoryStream stream = new MemoryStream ())
//			{
//				jsonSerializer.WriteObject (stream, data);
//				string encoded = Encoding.UTF8.GetString(stream.ToArray());
//
//				stream.Position = 0;
//				Object deserialized = jsonSerializer.ReadObject (stream);
//				string deserializedTypeName = deserialized.GetType().Name;
//
//				deserializedTypeName.ToString ();
//			}



//			// binary compatibility with normal .NET 4.5
//			string hex = "00-01-00-00-00-FF-FF-FF-FF-01-00-00-00-00-00-00-00-10-01-00-00-00-04-00-00-00-08-08-1C-00-00-00-06-02-00-00-00-0B-6A-61-6B-69-73-20-74-65-6B-73-74-09-03-00-00-00-09-04-00-00-00-0C-05-00-00-00-41-43-53-68-61-72-70-54-65-73-74-2C-20-56-65-72-73-69-6F-6E-3D-31-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-6E-75-6C-6C-05-03-00-00-00-14-43-53-68-61-72-70-54-65-73-74-2E-54-65-73-74-44-61-74-61-41-02-00-00-00-07-6E-75-6D-62-65-72-41-07-73-74-72-69-6E-67-41-00-01-08-05-00-00-00-0D-00-00-00-06-06-00-00-00-08-64-6F-6D-79-73-6C-6E-79-05-04-00-00-00-14-43-53-68-61-72-70-54-65-73-74-2E-54-65-73-74-44-61-74-61-42-03-00-00-00-07-6E-75-6D-62-65-72-42-07-6E-75-6D-62-65-72-41-07-73-74-72-69-6E-67-41-00-00-01-06-08-05-00-00-00-00-00-00-00-00-00-15-40-0D-00-00-00-09-06-00-00-00-0B";
//			// .NET3.5
//			string hey = "00-01-00-00-00-FF-FF-FF-FF-01-00-00-00-00-00-00-00-10-01-00-00-00-04-00-00-00-08-08-1C-00-00-00-06-02-00-00-00-0B-6A-61-6B-69-73-20-74-65-6B-73-74-09-03-00-00-00-09-04-00-00-00-0C-05-00-00-00-43-43-53-68-61-72-70-54-65-73-74-33-35-2C-20-56-65-72-73-69-6F-6E-3D-31-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-6E-75-6C-6C-05-03-00-00-00-16-43-53-68-61-72-70-54-65-73-74-33-35-2E-54-65-73-74-44-61-74-61-41-02-00-00-00-07-6E-75-6D-62-65-72-41-07-73-74-72-69-6E-67-41-00-01-08-05-00-00-00-0D-00-00-00-06-06-00-00-00-08-64-6F-6D-79-73-6C-6E-79-05-04-00-00-00-16-43-53-68-61-72-70-54-65-73-74-33-35-2E-54-65-73-74-44-61-74-61-42-03-00-00-00-07-6E-75-6D-62-65-72-42-07-6E-75-6D-62-65-72-41-07-73-74-72-69-6E-67-41-00-00-01-06-08-05-00-00-00-00-00-00-00-00-00-15-40-0D-00-00-00-09-06-00-00-00-0B";
//			byte[] bytes = StringToByteArray(hey);
//
//			MemoryStream memStr = new MemoryStream(bytes);
//			Object deserialized2 = formatter.Deserialize(memStr);
//			string deserializedTypeName2 = deserialized2.GetType().Name;
		}

		public byte[] StringToByteArray(string hex)
		{
			hex = hex.Replace("-", "");

			return Enumerable.Range(0, hex.Length)
				.Where(x => x % 2 == 0)
				.Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
				.ToArray();
		}
	}

	[Serializable]
	public class TestDataA
	{
		public int numberA = 13;
		public string stringA = "domyslny";
	}

	[Serializable]
	public class TestDataB : TestDataA
	{
		public double numberB = 5.25;
	}
}
