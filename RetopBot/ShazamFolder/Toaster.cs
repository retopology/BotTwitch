//
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RetopBot.ShazamFolder
{

    public static class Toaster
    {

        private static readonly string TempPath = Path.GetTempPath();


        private static readonly string ShazamIconPath = Path.Combine(TempPath, "shozom_static_shazam.tmp");
        private static readonly string YouTubeIconPath = Path.Combine(TempPath, "shozom_static_youtube.tmp");
        private static readonly string CopyIconPath = Path.Combine(TempPath, "shozom_static_copy.tmp");


        private static void OnToastActivated(ToastNotificationActivatedEventArgsCompat e)
        {
            var args = ToastArguments.Parse(e.Argument);

            var thread = new Thread(() => {
                if (args.Contains("copy")) try { Clipboard.SetText(args.Get("copy")); } catch { }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        public static void ShowFailure()
        {
            new ToastContentBuilder()
                .AddText("No Result")
                .AddText("Sorry, we didn't quite catch that")
                .Show();
        }

        public static void ShowTimeout()
        {
            new ToastContentBuilder()
                .AddText("Timeout")
                .AddText("Sorry, we took longer than expected")
                .Show();
        }

        public static void ShowError(string msg)
        {
            new ToastContentBuilder()
                .AddText("Error")
                .AddText(msg)
                .Show();
        }

        private static Bitmap CreateThumbImage(Bitmap coverImage)
        {
            var thumbImage = new Bitmap(48, 48);
            thumbImage.PlaceImage(coverImage, 0, 0, 48, 48, 2);
            return thumbImage;
        }

        private static Bitmap CreateBannerImage(Bitmap coverImage)
        {
            var bannerImage = new Bitmap(364, 180);

            bannerImage.PlaceImage(coverImage, 0, -90, 364, 364);
            bannerImage.GaussianBlur(8, 8);

            var padding = 12;

            var shadowImage = new Bitmap(180, 180);
             var g = Graphics.FromImage(shadowImage);
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(padding, padding, 180 - padding * 2 + 1, 180 - padding * 2 + 1));
            shadowImage.GaussianBlur(4, 4);

            bannerImage.PlaceImage(shadowImage, 92, 0, 180, 180);
            bannerImage.PlaceImage(coverImage, 92 + padding, padding, 180 - padding * 2, 180 - padding * 2, 2);

            return bannerImage;
        }

    }

}

