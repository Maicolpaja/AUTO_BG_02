using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//NameSpaces 

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Windows.Media.Imaging;

namespace app_pickInformation
{
    class App : IExternalApplication
    {
        /// <summary>
        /// Evento que se desencadena cuando revit se cierra
        /// </summary>
        /// <param name="application"> Aplicacion </param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        /// <summary>
        /// Evento que se desencadena cuando revit inicia
        /// </summary>
        /// <param name="application"> Aplicacion</param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            CreateRibbonItems(application);
            return Result.Succeeded;
        }
        private void CreateRibbonItems(UIControlledApplication app)
        {
            const string tabName = "Auto. 02";
            const string pnName = "Panel 01";

            app.CreateRibbonTab(tabName);

            RibbonPanel pnAutomatizacion = app.CreateRibbonPanel(tabName, pnName);
            createAddButton(pnAutomatizacion);
        }

        private void createAddButton(RibbonPanel panel)
        {
            //BUTTON 01
            PushButtonData pbd = new PushButtonData("Button 01", "Button 01 text", assemblyPath, "app_pickInformation.cmd_pickInformation");
            pbd.LongDescription = "Este es un button para probar una external application";


            PushButton pb = panel.AddItem(pbd) as PushButton;
            SetIconsForPushButton(pb, Properties.Resources.btn_icon);

            //BUTTON 02
            PushButtonData pbd_01 = new PushButtonData("Button 02", "Button 02 text", assemblyPath, "app_pickInformation.cmd_setParameters");
            pbd_01.LongDescription = "Este es un button para probar una external application";


            PushButton pb_01 = panel.AddItem(pbd_01) as PushButton;
            SetIconsForPushButton(pb_01, Properties.Resources.IMAGEN);

            //BUTTON 03
            PushButtonData pbd_03 = new PushButtonData("Button 03", "Button 03 text", assemblyPath, "app_pickInformation.cmd_ExportExcel");
            pbd_03.LongDescription = "Este es un button para probar una external application";


            PushButton pb_03 = panel.AddItem(pbd_03) as PushButton;
            SetIconsForPushButton(pb_03, Properties.Resources.ms_excel);
        }

        /// <summary>
        /// Setear imagens para el aplicativo (Pequeñas y estandar)
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="icon"></param>
        public void SetIconsForPushButton( PushButton btn, System.Drawing.Icon icon)
        {
            btn.LargeImage = GetStandarIcon(icon);
            btn.Image = GetSmallIcon(icon);
        }

        /// <summary>
        /// Setear imagenes estandar del aplicativo
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        private static BitmapSource GetStandarIcon(System.Drawing.Icon icon)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        private static BitmapSource GetSmallIcon( System.Drawing.Icon icon)
        {
            System.Drawing.Icon smallIcon = new System.Drawing.Icon(icon, new System.Drawing.Size(16, 16));
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                smallIcon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }



        static String assemblyPath = typeof(App).Assembly.Location;
    }
}
