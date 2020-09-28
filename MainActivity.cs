using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.Xml;
using System.Text;
using static Application1._0.Assets.Entities;
using System;
using System.Collections.Generic;

namespace Application1._0
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        string url = "http://partner.market.yandex.ru/pages/help/YML.xml";
        ListView listView;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            //Добавлние элементов в код из activity_main.xml
            ListView listView = FindViewById<ListView>(Resource.Id.listView1);

        }

        async public void Get()//Async reading url
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                Async = true
            };
            XmlReader reader = XmlReader.Create(url, settings);
            await reader.ReadAsync();

        }

        //Works
        public void ParseOfferIds()
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                Async = true,
                DtdProcessing = DtdProcessing.Parse,
                MaxCharactersFromEntities = 1024
            };
            settings.DtdProcessing = DtdProcessing.Ignore;
            XmlReader reader = XmlReader.Create(url, settings);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:

                        if (reader.Name == "offer")
                        {
                            var idSeek = reader.GetAttribute("id");
                            foreach (int id in idSeek)
                            {

                                yml_catalogShopOffer offer = new yml_catalogShopOffer
                                {
                                    id = id /*listView1.Items.Add(reader.GetAttribute("id")); 
                                     на винформах протестировано, все offerId выдает
                                     if (reader.Name == "offer")  {
                                     listView1.Items.Add(reader.GetAttribute("id"));  }  break; */
                                };

                            }
                        }
                        break;
                    case XmlNodeType.EndElement:
                        break;
                }
            }
        }

        //метод, выдавший результаты парсинга ID в Winforms
        //public void ParseIds()
        //{
        //    XmlReaderSettings settings = new XmlReaderSettings
        //    {
        //        Async = true,
        //        DtdProcessing = DtdProcessing.Parse,
        //        MaxCharactersFromEntities = 1024
        //    };
        //    settings.DtdProcessing = DtdProcessing.Ignore;
        //    XmlReader reader = XmlReader.Create(url, settings);
        //    while (reader.Read())
        //    {
        //        switch (reader.NodeType)
        //        {
        //            case XmlNodeType.Element:

        //                if (reader.Name == "offer")
        //                {
        //                    listView1.Items.Add(reader.GetAttribute("id"));
        //                }
        //                break;
        //            case XmlNodeType.EndElement:
        //                listView1.Items.Add("");
        //                break;
        //        }
        //    }
        //} 
    }
}