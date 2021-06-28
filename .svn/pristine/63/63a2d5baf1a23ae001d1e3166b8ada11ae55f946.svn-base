using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

namespace ASEWCFServiceLibrary.App_Code
{
    /// <summary>
    /// clsUtil의 요약 설명입니다.
    /// 프로그램에 필요한 함수들을 담은 class.
    /// 
    /// (사용예제)
    /// clsMail oCMail = new AwBaseLib.CMail(sHost);
    /// oCMail.AddAddress(CMail.MailType.FR, sFromMailAddress);
    /// oCMail.AddAddress(CMail.MailType.TO, Mail_Address);
    /// oCMail.SetSubject("Mail_Subject");
    /// oCMail.SetBody("Mail_Body");    
    /// oCMail.Send();
    /// 
    /// </summary>
    public class clsMail
    {

        public enum MailType
        {
            FR = 0,
            TO = 1,
            CC = 2
        }
        public static string MAILNEWLINE = Environment.NewLine;
        public Encoding AwMailEncoding;
        public MailMessage AwMailMessage;
        public SmtpClient AwSmtpClient;

        private bool m_MailSent;
        private string m_MailResult;
        private string m_Host;
        private int m_Port;

        public clsMail(string psHost, int piPort)
        {
            Init(psHost, piPort);
        }

        public clsMail(string psHost)
        {
            Init(psHost, 25);
        }

        public void Send()
        {
            m_MailSent = false;
            m_MailResult = string.Empty;
            AwSmtpClient = new SmtpClient(m_Host, m_Port);            
            /* without this the connection is idle too long. 
             * to tell the socket stack not re-use the socket 
             * http://community.pmail.com/forums/thread/21855.aspx             
             */
            AwSmtpClient.ServicePoint.MaxIdleTime = 1;
            AwSmtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            
            try
            {

                AwSmtpClient.Send(AwMailMessage); // for release
                AwMailMessage.Dispose();

                m_MailSent = true;
                m_MailResult = "Message Sent.";
            }
            catch (Exception ex)
            {
                m_MailSent = false;
                m_MailResult = ex.Message;
            }

            
        }

        public void AddAddress(MailType piType, string psMailAddress)
        {
            char[] cAddressSplit = { ',', ';' };
            string[] sAddresses = psMailAddress.Split(cAddressSplit);

            for (int i = 0; i <= sAddresses.Length - 1; i++)
            {

                char[] cNameSplit = { '=' };
                string[] sNames = sAddresses[i].Split(cNameSplit);

                MailAddress oAddress;

                if (sNames.Length == 2)
                {
                    oAddress = new MailAddress(sNames[0], sNames[1], AwMailEncoding);
                }
                else
                {
                    oAddress = new MailAddress(sNames[0], sNames[0], AwMailEncoding);
                }

                switch (piType)
                {
                    case MailType.FR:
                        AwMailMessage.From = oAddress;
                        break;
                    case MailType.TO:
                        AwMailMessage.To.Add(oAddress);
                        break;
                    case MailType.CC:
                        AwMailMessage.CC.Add(oAddress);
                        break;
                }
            }
        }

        public void SetSubject(string psSubject)
        {
            AwMailMessage.Subject = psSubject;
            AwMailMessage.SubjectEncoding = AwMailEncoding;
        }

        public void SetBody(string psBody)
        {
            AwMailMessage.Body = psBody;
            AwMailMessage.BodyEncoding = AwMailEncoding;
        }

        public bool GetMailSent()
        {
            return m_MailSent;
        }

        public string GetMailResult()
        {
            return m_MailResult;
        }
        
        public void Init(string psHost, int piPort)
        {
            m_MailSent = false;
            m_MailResult = string.Empty;
            m_Host = psHost;
            m_Port = piPort;
            AwMailEncoding = System.Text.Encoding.UTF8;
            AwMailMessage = new MailMessage();
        }
        
    }

}