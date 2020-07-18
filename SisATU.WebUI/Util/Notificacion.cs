using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace SisATU.WebUI.Util
{
    public class Notificacion
    {
        string UserCredencial = string.Empty;
        string PasswordCredencial = string.Empty;
        string mailUser = string.Empty;
        string DominioCredencial = string.Empty;
        string Host = string.Empty;
        //string asunto = string.Empty;
        public string asunto { get; set; }
        public string rutaArchivoAdjunto { get; set; }

        public string[] arrArchivosRuta { get; set; }
        public Notificacion()
        {
            UserCredencial = System.Configuration.ConfigurationManager.AppSettings["userNoti"].ToString();
            PasswordCredencial = System.Configuration.ConfigurationManager.AppSettings["passNoti"].ToString();
            DominioCredencial = System.Configuration.ConfigurationManager.AppSettings["domiNoti"].ToString();
            Host = System.Configuration.ConfigurationManager.AppSettings["host"].ToString();
            mailUser = System.Configuration.ConfigurationManager.AppSettings["mailNoti"].ToString();
        }

        public void enviar()
        {
            try
            {
                System.Net.Mail.MailMessage mailNoti = new System.Net.Mail.MailMessage();
                mailNoti.From = new MailAddress(mailUser);
                foreach (string mailTo in strmailTo)
                {
                    mailNoti.To.Add(mailTo);
                }
                foreach (string mailCC in strCC)
                {
                    mailNoti.CC.Add(mailCC);
                }
                foreach (string mailCCO in strCCO)
                {
                    mailNoti.Bcc.Add(mailCCO);
                }
                mailNoti.Subject = this.asunto;

                mailNoti.IsBodyHtml = true;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(strBody.ToString(), Encoding.UTF8, MediaTypeNames.Text.Html);
                foreach (ArchivoMail imagen in lstImagen)
                {
                    LinkedResource img = new LinkedResource(imagen.Ruta, MediaTypeNames.Image.Jpeg);
                    img.ContentId = imagen.Name;
                    htmlView.LinkedResources.Add(img);
                }
                mailNoti.AlternateViews.Add(htmlView);
                //foreach (ArchivoMail archivo in lstArchivo)
                //{
                //    Attachment adj = new Attachment(archivo.Ruta);
                //    mailNoti.Attachments.Add(adj);
                //}
                
                if(arrArchivosRuta != null) { 
                foreach (var item in arrArchivosRuta)
                {
                    if(item != null)
                    {
                        Attachment adj = new Attachment(item);
                        mailNoti.Attachments.Add(adj);
                    }
                   
                }
                }
                mailNoti.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                mailNoti.Priority = System.Net.Mail.MailPriority.High;
                SmtpClient smtpNoti = new SmtpClient();
                smtpNoti.UseDefaultCredentials = false;
                smtpNoti.Credentials = new System.Net.NetworkCredential(UserCredencial, PasswordCredencial, DominioCredencial);//, 
                //
                smtpNoti.Host = Host;
                smtpNoti.DeliveryMethod = SmtpDeliveryMethod.Network;
                //
                smtpNoti.EnableSsl = true;
                smtpNoti.TargetName = "STARTTLS/smtp.office365.com";
                smtpNoti.Port = 587;
                //
                smtpNoti.Send(mailNoti);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetBodyNotificacion(List<ParametroMail> parametros, string plantilla)
        {
            string address = string.Empty;
            //if (plantilla == "correo1")
            //{
            address = System.Configuration.ConfigurationManager.AppSettings[plantilla].ToString();
            //}
            return GetHTMLFromAddress(address, parametros);
        }

        public string GetHTMLFromAddress(string Address, List<ParametroMail> parametros)
        {
            StringBuilder strBody = new StringBuilder();

            var urlTemplate = HttpContext.Current.Server.MapPath(Address);

            var ASCII = new System.Text.UTF8Encoding();
            var netWeb = new System.Net.WebClient();
            string lsWeb = string.Empty;
            byte[] laWeb;

            try
            {
                laWeb = netWeb.DownloadData(urlTemplate);
                lsWeb = ASCII.GetString(laWeb);

                if (parametros != null)
                {
                    foreach (ParametroMail parametro in parametros)
                    {
                        lsWeb = lsWeb.Replace(parametro.Name, parametro.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message.ToString() + ex.ToString());
                throw ex;
            }
            return lsWeb;
        }

        public List<ArchivoMail> GetImagenesNotificacion(string plantilla)
        {
            List<ArchivoMail> lst = new List<ArchivoMail>();
            //if (plantilla == "correo1")
            //{
                lst.Add(new ArchivoMail() { Name = "Adjunto", Ruta = HttpContext.Current.Server.MapPath("~/"+plantilla) });
                //lst.Add(new ArchivoMail() { Name = "logo", Ruta = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["imgLogo"].ToString()) });
            //}
            return lst;
        }

        private List<string> strmailTo = new List<string>();
        private string strtitleMessage = string.Empty;
        private string strBody = string.Empty;
        private List<string> strCC = new List<string>();
        private List<string> strCCO = new List<string>();
        private string strCodigoNotificacion;
        private List<ArchivoMail> lstImagen = new List<ArchivoMail>();
        private List<ArchivoMail> lstArchivo = new List<ArchivoMail>();

        public List<ArchivoMail> ArchivosMail
        {
            get { return lstArchivo; }
            set { lstArchivo = value; }
        }

        public List<ArchivoMail> ImagenesMail
        {
            get { return lstImagen; }
            set { lstImagen = value; }
        }

        public List<string> To
        {
            get { return strmailTo; }
            set { strmailTo = value; }
        }

        public List<string> Cc
        {
            get { return strCC; }
            set { strCC = value; }
        }

        public List<string> CcO
        {
            get { return strCCO; }
            set { strCCO = value; }
        }

        public string TitleMessage
        {
            get { return strtitleMessage; }
            set { strtitleMessage = value; }
        }

        public string Body
        {
            get { return strBody; }
            set { strBody = value; }
        }

        public string CodigoNotificacion
        {
            get { return strCodigoNotificacion; }
            set { strCodigoNotificacion = value; }
        }
    }

    public class ParametroMail
    {
        private string strValue;
        private string strName;

        public string Value
        {
            get { return strValue; }
            set { strValue = value; }
        }

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }
    }

    public class ArchivoMail
    {
        private string strRuta;
        private string strName;

        public string Ruta
        {
            get { return strRuta; }
            set { strRuta = value; }
        }

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }
    }
}