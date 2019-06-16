using GamerRadar.ViewModels.UserGames;
using System;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using ZXing;

namespace GamerRadar.Controllers
{
    public class QRCodeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            Session["QRCode"] = string.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string imageName)
        {
            return View();
        }

        public JsonResult HandleCapture()
        {
            string path = @"http://localhost:" + Request.Url.Port + "/TempImages/" + Session["QRCode"].ToString();

            string scannedGameName = Session["ScannedGameName"]?.ToString();

            string message;
            if (string.IsNullOrEmpty(scannedGameName))
            {
                message = "System was not able to identify scanned QR Code. Please try again.";
            }
            else
            {
                message = "QR code scanned successfully. Game identified: " + scannedGameName + ". Please click submit to proceed.";
                Session["Game"] = new UserGamesSearchViewModel(scannedGameName);
            }

            return Json(new string[] { path, message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Capture()
        {
            Session["ScannedGameName"] = string.Empty;

            var readRequestInputStream = ReadRequestInputStream();
            var imageBytes = ConvertStringToBytes(readRequestInputStream);
            InterpretQRCode(imageBytes);

            SaveScannedQRCodeToTemporaryFolder(imageBytes);

            return View("Index");
        }

        private void SaveScannedQRCodeToTemporaryFolder(byte[] imageBytes)
        {
            var directoryPath = Server.MapPath("~/TempImages/");
            var fileName = string.Format("{0}.png", DateTime.Now.ToString("yyyyMMddTHHmmss.fffffff"));
            var path = string.Format("{0}/{1}", directoryPath, fileName);
            System.IO.File.WriteAllBytes(path, imageBytes);
            Session["QRCode"] = fileName;
        }

        private string ReadRequestInputStream()
        {
            string readRequestInputStream;
            using (var streamReader = new StreamReader(Request.InputStream))
            {
                readRequestInputStream = streamReader.ReadToEnd();
            }

            return readRequestInputStream;
        }

        private void InterpretQRCode(byte[] imageBytes)
        {
            var qrCodeReader = new BarcodeReader();

            using (var memoryStream = new MemoryStream(imageBytes))
            {
                var image = Image.FromStream(memoryStream);
                var result = qrCodeReader.Decode((Bitmap)image)?.Text;
                Session["ScannedGameName"] = result;
            }
        }

        private byte[] ConvertStringToBytes(string inputString)
        {
            int bytesNumber = inputString.Length / 2;
            byte[] bytes = new byte[bytesNumber];

            int fromBase = 16;
            for (int i = 0; i < bytesNumber; ++i)
            {
                string value = inputString.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(value, fromBase);
            }

            return bytes;
        }
    }
}