using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Platform.Converters;
using UIKit;

namespace NMCT.Resto.iOS.Converters
{
    public class StringToImageConverter : MvxValueConverter<String,UIImage>
    {

        protected override UIImage Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetOnlineImage(value);
        }

        private UIImage GetOnlineImage(string uri)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                {
                    return UIImage.FromBundle("No_image_available.png");
                }

                else
                {
                    if (uri == "error") return UIImage.FromBundle("error.png");

                    uri = uri.Substring(0, 4) + "s" + uri.Substring(4);
                    using (var url = new NSUrl(uri))
                    using (var data = NSData.FromUrl(url))
                        //using (var url = new NSUrl("https://books.google.com/books/content?id=i7pmAgAAQBAJ&printsec=frontcover&img=1&zoom=5&edge=curl&source=gbs_api"))
                        //using (var data = NSData.FromUrl(url))
                        return UIImage.LoadFromData(data);
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}