using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Services;
using PesanMakan.Business;
using System.Web.Script.Services;
using System.Timers;
using System.Data;
using System.IO;
using PesanMakan.Data;
using Biofarma.Service.ActiveDirectory;
using Bsn = PesanMakan.Business;
using System.Configuration;

namespace PesanMakan.Handler
{
    /// <summary>
    /// Summary description for GetSetService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GetSetService : System.Web.Services.WebService
    {
        public class Notif
        {
            public string type { set; get; }
            public string notes { set; get; }
            public string head { set; get; }
            public DataTable table { set; get; }
        }

        public string username { set; get; }
        public string pass { set; get; }

        int retCode;
        int hCard;
        int hContext;
        int Protocol;
        string cardUID;
        string cardValue;
        string modifier = string.Empty;
        public bool connActive = false;

        int statWrite;
        string readername = "ACS ACR122 0";      // change depending on reader
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;
        public Card.SCARD_READERSTATE RdrState;
        public Card.SCARD_IO_REQUEST pioSendRequest;

        #region LayananMakan
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void startService()
        {
            string stat;
            SelectDevice();
            establishContext();
            if (connectCard())
            {
                cardUID = getcardUID();
                cardValue = verifyCard("5");
                stat = DataLogic.data(cardUID, cardValue, modifier);
                Session["TOKEN"] = stat;
            }
            else
            {
                stat = "Tidak Terhubung NFC";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/text";
            Context.Response.Write(stat);
            //return stat;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string startServiceTicketing()
        {
            string stat;
            SelectDevice();
            establishContext();
            if (connectCard())
            {
                cardUID = getcardUID();
                cardValue = verifyCard("5");
                stat = DataLogic.data(cardUID, cardValue, modifier);
                Session["TOKEN"] = stat;
            }
            else
            {
                stat = "Tidak Terhubung NFC";
            }

            return stat;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string startServiceDekstop()
        {
            string stat;
            SelectDevice();
            establishContext();
            if (connectCard())
            {
                stat = "1";
            }
            else
            {
                stat = "0";
            }

            return stat;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void initCardUID()
        {
            SelectDevice();
            establishContext();
            if (connectCard())
            {
                cardUID = getcardUID();
                Context.Response.Clear();
                Context.Response.ContentType = "application/text";
                Context.Response.Write(cardUID);
            }
        }

        [WebMethod]
        public string initCardUIDesktop()
        {
            SelectDevice();
            establishContext();
            if (connectCard())
            {
                cardUID = getcardUID();
                
            }
            return cardUID;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetTable(string dateReg, string dateBok, string nik, string name)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SelectDevice()
        {
            List<string> availableReaders = this.ListReaders();
            try { 
                this.RdrState = new Card.SCARD_READERSTATE();
                readername = availableReaders[0].ToString();//selecting first device
                this.RdrState.RdrName = readername;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void btnWrite(string tbValue)
        {
            statWrite = submitText(tbValue, "5"); // 5 - is the block we are writing data on the card


            Context.Response.Clear();
            Context.Response.ContentType = "application/text";
            Context.Response.Write(statWrite);
        }


        [WebMethod]
        public List<string> ListReaders()
        {
            int ReaderCount = 0;
            List<string> AvailableReaderList = new List<string>();

            //Make sure a context has been established before 
            //retrieving the list of smartcard readers.
            retCode = Card.SCardListReaders(hContext, null, null, ref ReaderCount);
            if (retCode != Card.SCARD_S_SUCCESS)
            {

                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", Card.GetScardErrMsg(retCode), true);
                //connActive = false;
            }

            byte[] ReadersList = new byte[ReaderCount];

            //Get the list of reader present again but this time add sReaderGroup, retData as 2rd & 3rd parameter respectively.
            retCode = Card.SCardListReaders(hContext, null, ReadersList, ref ReaderCount);
            if (retCode != Card.SCARD_S_SUCCESS)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", Card.GetScardErrMsg(retCode), true);
                //MessageBox.Show(Card.GetScardErrMsg(retCode));
            }

            string rName = "";
            int indx = 0;
            if (ReaderCount > 0)
            {
                // Convert reader buffer to string
                while (ReadersList[indx] != 0)
                {

                    while (ReadersList[indx] != 0)
                    {
                        rName = rName + (char)ReadersList[indx];
                        indx = indx + 1;
                    }

                    //Add reader name to list
                    AvailableReaderList.Add(rName);
                    rName = "";
                    indx = indx + 1;

                }
            }
            return AvailableReaderList;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        internal void establishContext()
        {
            retCode = Card.SCardEstablishContext(Card.SCARD_SCOPE_SYSTEM, 0, 0, ref hContext);
            if (retCode != Card.SCARD_S_SUCCESS)
            {
                //MessageBox.Show("Check your device and please restart again", "Reader not connected", MessageBoxButton.OK, MessageBoxImage.Warning);
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Check Your Device Again and Restart" + "');", true);
                connActive = false;
                return;
            }
        }

        public void establishContextDekstop()
        {
            retCode = Card.SCardEstablishContext(Card.SCARD_SCOPE_SYSTEM, 0, 0, ref hContext);
            if (retCode != Card.SCARD_S_SUCCESS)
            {
                //MessageBox.Show("Check your device and please restart again", "Reader not connected", MessageBoxButton.OK, MessageBoxImage.Warning);
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Check Your Device Again and Restart" + "');", true);
                connActive = false;
                return;
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool connectCard()
        {
            connActive = true;

            retCode = Card.SCardConnect(hContext, readername, Card.SCARD_SHARE_SHARED,
                      Card.SCARD_PROTOCOL_T0 | Card.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != Card.SCARD_S_SUCCESS)
            {

                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Card Not Available" + "');", true);

                connActive = false;
                return false;
            }
            return true;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected string getcardUID()//only for mifare 1k cards
        {
            string cardUID = "";
            byte[] receivedUID = new byte[256];
            Card.SCARD_IO_REQUEST request = new Card.SCARD_IO_REQUEST();
            request.dwProtocol = Card.SCARD_PROTOCOL_T1;
            request.cbPciLength = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Card.SCARD_IO_REQUEST));
            byte[] sendBytes = new byte[] { 0xFF, 0xCA, 0x00, 0x00, 0x00 }; //get UID command      for Mifare cards
            int outBytes = receivedUID.Length;
            int status = Card.SCardTransmit(hCard, ref request, ref sendBytes[0], sendBytes.Length, ref request, ref receivedUID[0], ref outBytes);

            if (status != Card.SCARD_S_SUCCESS)
            {
                cardUID = "Error";
            }
            else
            {
                cardUID = BitConverter.ToString(receivedUID.Take(4).ToArray()).Replace("-", string.Empty).ToLower();
            }

            return cardUID;
        }

        //submit data method
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int submitText(String Text, String Block)
        {

            String tmpStr = Text;
            int indx;
            int statSubmit = 0;
            if (authenticateBlock(Block))
            {
                ClearBuffers();
                SendBuff[0] = 0xFF;                             // CLA
                SendBuff[1] = 0xD6;                             // INS
                SendBuff[2] = 0x00;                             // P1
                SendBuff[3] = (byte)int.Parse(Block);           // P2 : Starting Block No.
                SendBuff[4] = (byte)int.Parse("16");            // P3 : Data length

                for (indx = 0; indx <= (tmpStr).Length - 1; indx++)
                {
                    SendBuff[indx + 5] = (byte)tmpStr[indx];
                }
                SendLen = SendBuff[4] + 5;
                RecvLen = 0x02;

                retCode = SendAPDUandDisplay(2);

                if (retCode != Card.SCARD_S_SUCCESS)
                {
                    statSubmit = 1;
                }
                else
                {
                    statSubmit = 0;
                }
            }
            return statSubmit;
        }

        // block authentication
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected bool authenticateBlock(String block)
        {
            ClearBuffers();
            SendBuff[0] = 0xFF;                         // CLA
            SendBuff[2] = 0x00;                         // P1: same for all source types 
            SendBuff[1] = 0x86;                         // INS: for stored key input
            SendBuff[3] = 0x00;                         // P2 : Memory location;  P2: for stored key input
            SendBuff[4] = 0x05;                         // P3: for stored key input
            SendBuff[5] = 0x01;                         // Byte 1: version number
            SendBuff[6] = 0x00;                         // Byte 2
            SendBuff[7] = (byte)int.Parse(block);       // Byte 3: sectore no. for stored key input
            SendBuff[8] = 0x60;                         // Byte 4 : Key A for stored key input
            SendBuff[9] = (byte)int.Parse("1");         // Byte 5 : Session key for non-volatile memory

            SendLen = 0x0A;
            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(0);

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                //MessageBox.Show("FAIL Authentication!");
                return false;
            }

            return true;
        }

        // clear memory buffers
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected void ClearBuffers()
        {
            long indx;

            for (indx = 0; indx <= 262; indx++)
            {
                RecvBuff[indx] = 0;
                SendBuff[indx] = 0;
            }
        }

        // send application protocol data unit : communication unit between a smart card reader and a smart card
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        protected int SendAPDUandDisplay(int reqType)
        {
            int indx;
            string tmpStr = "";

            pioSendRequest.dwProtocol = Aprotocol;
            pioSendRequest.cbPciLength = 8;

            //Display Apdu In
            for (indx = 0; indx <= SendLen - 1; indx++)
            {
                tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);
            }

            retCode = Card.SCardTransmit(hCard, ref pioSendRequest, ref SendBuff[0],
                                 SendLen, ref pioSendRequest, ref RecvBuff[0], ref RecvLen);

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                return retCode;
            }

            else
            {
                try
                {
                    tmpStr = "";
                    switch (reqType)
                    {
                        case 0:
                            for (indx = (RecvLen - 2); indx <= (RecvLen - 1); indx++)
                            {
                                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                            }

                            if ((tmpStr).Trim() != "90 00")
                            {
                                //MessageBox.Show("Return bytes are not acceptable.");
                                return -202;
                            }

                            break;

                        case 1:

                            for (indx = (RecvLen - 2); indx <= (RecvLen - 1); indx++)
                            {
                                tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);
                            }

                            if (tmpStr.Trim() != "90 00")
                            {
                                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                            }

                            else
                            {
                                tmpStr = "ATR : ";
                                for (indx = 0; indx <= (RecvLen - 3); indx++)
                                {
                                    tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                                }
                            }

                            break;

                        case 2:

                            for (indx = 0; indx <= (RecvLen - 1); indx++)
                            {
                                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                            }

                            break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    return -200;
                }
            }
            return retCode;
        }

        //disconnect card reader connection
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Close()
        {
            if (connActive)
            {
                retCode = Card.SCardDisconnect(hCard, Card.SCARD_UNPOWER_CARD);
            }
            //retCode = Card.SCardReleaseContext(hCard);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string verifyCard(String Block)
        {
            string value = "";
            if (connectCard())
            {
                value = readBlock(Block);
            }

            value = value.Split(new char[] { '\0' }, 2, StringSplitOptions.None)[0].ToString();
            return value;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string readBlock(String Block)
        {
            string tmpStr = "";
            int indx;

            if (authenticateBlock(Block))
            {
                ClearBuffers();
                SendBuff[0] = 0xFF; // CLA 
                SendBuff[1] = 0xB0;// INS
                SendBuff[2] = 0x00;// P1
                SendBuff[3] = (byte)int.Parse(Block);// P2 : Block No.
                SendBuff[4] = (byte)int.Parse("16");// Le

                SendLen = 5;
                RecvLen = SendBuff[4] + 2;

                retCode = SendAPDUandDisplay(2);

                if (retCode == -200)
                {
                    return "outofrangeexception";
                }

                if (retCode == -202)
                {
                    return "BytesNotAcceptable";
                }

                if (retCode != Card.SCARD_S_SUCCESS)
                {
                    return "FailRead";
                }

                // Display data in text format
                for (indx = 0; indx <= RecvLen - 1; indx++)
                {
                    tmpStr = tmpStr + Convert.ToChar(RecvBuff[indx]);
                }

                return (tmpStr);
            }
            else
            {
                return "FailAuthentication";
            }
        }

        [WebMethod(EnableSession=true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InputProses(string nik, string nama, string[][] array)
        {
            string modifier = Session["NIK"].ToString();
            DataLogic.inserttoDB(nik, nama, array, modifier);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InputProsesFromCalendar(List<CalendarEventObj> listObj)
        {
            string JSONString = string.Empty;
            string modifier = Session["NIK"].ToString();
            string nik = modifier;
            string nama = Session["NAMA"].ToString();

            List<string> line = new List<string>();
            List<string[]> lineObj = new List<string[]>();

            for (int i = 0; i < listObj.Count; i++)
            {
                line.Clear();
                line.Add(listObj[i].id.Replace('-', '/'));
                line.Add(string.Empty);
                line.Add(listObj[i].menu);
                line.Add(listObj[i].startHour);
                line.Add(listObj[i].endHour);
                
                lineObj.Add(line.ToArray());
            }

            string[][] lineObjArray = lineObj.ToArray();
            DataLogic.inserttoDB(nik, nama, lineObjArray, modifier);

            Notif d = new Notif();
            d.type = "success";
            d.notes = "Berhasil update booking makan";
            d.head = "Success!";

            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InputMenu(string[][] array)
        {
            DataLogic.insertMenuDB(array);
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void setMonthRecap(string bulan)
        {
            Session["PERIODRECAP"] = bulan.ToString();
        }
        
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void setMonth(string bulan)
        {
            Session["PERIODMENU"]  = bulan.ToString();
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetListOrder()
        {
            string User = HttpContext.Current.Session["NAMA"].ToString();
            string Nik = HttpContext.Current.Session["NIK"].ToString();
            DataTable d = DataLogic.GetOrderByUser(User, Nik);
            string n = string.Empty;
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
       
        public void GetMenu()
        {
            string Period = HttpContext.Current.Session["PERIODMENU"].ToString(); ;
            DataTable d = DataLogic.GetAllMenu(Period);
            string n = string.Empty;
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");

        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RecapOrder()
        {
            string periode = HttpContext.Current.Session["PERIODRECAP"].ToString(); ;
            DataTable d = DataLogic.RecapOrder(periode);
            string n = string.Empty;
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");

        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetReconcile()
        {
            
            DataTable d = DataLogic.Reconcile();
            string n = string.Empty;
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TotalMember()
        {

            int d = DataLogic.TotalMember();
            Context.Response.Clear();
            Context.Response.ContentType = "application/text";
            Context.Response.Write(d);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TotalBooking()
        {

            int d = DataLogic.TotalBooking();
            Context.Response.Clear();
            Context.Response.ContentType = "application/text";
            Context.Response.Write(d);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void TotalKonfirmasi()
        {

            int d = DataLogic.TotalKonfirmasi();
            Context.Response.Clear();
            Context.Response.ContentType = "application/text";
            Context.Response.Write(d);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CheckToken(string token)
        {

            string stat = DataLogic.CheckToken(token);
            Context.Response.Clear();
            Context.Response.ContentType = "application/text";
            Context.Response.Write(stat);

        }
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUserData()
        {


            DataTable d = DataLogic.GetAllUserData();
            string n = string.Empty;
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DataTable GetUserDataDekstop()
        {
            DataTable d = DataLogic.GetAllUserData();
            return d;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DataTable GetSearchDataDekstop(string nama)
        {
            DataTable d = DataLogic.GetSearchUserData(nama);
            return d;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DelMenu(string tgl, string menu)
        {
            DataLogic.DeleteMenu(tgl, menu);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AddMenu(string tgl, string menu, string jam)
        {
            DataLogic.TambahMenu(tgl, menu, jam);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CekMenu(string Period)
        {
            DataLogic.MenuCheck(Period);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SearchNik(string nik)
        {
            string nama = DataLogic.CariNik(nik);

            string n = string.Empty;
            string JSONString = string.Empty;
            Context.Response.Clear();
            Context.Response.ContentType = "application/text";
            Context.Response.Write(nama);


        }
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegCard(string nik, string nama, string cardid, string role)
        {
            int stat = DataLogic.RegKartu(nik, nama, cardid, role);
            Context.Response.Clear();
            Context.Response.ContentType = "application/text";
            Context.Response.Write(stat);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int RegCardDekstop(string nik, string nama, string cardid, string role)
        {
            int stat = DataLogic.RegKartu(nik, nama, cardid, role);
            return stat;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DelData(string nik, string nama, string cardid, string role)
        {
            DataLogic.HapusData(nik, nama, cardid, role);

        }
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public void DelOrder(string tgl)
        {
            string User = HttpContext.Current.Session["NAMA"].ToString();
            string Nik = HttpContext.Current.Session["NIK"].ToString();
            DataLogic.DeleteOrder(tgl,Nik,User);

        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public void loginWithCardUID()
        {
            string stat = "0";
            DateTime date = ApplicationCatalogGet.GetDateTimeServer() ?? DateTime.Now;
            string[] prSplit = date.ToString().Split('/');
            string[] year = prSplit[2].Split(' ');
            int y = date.Year;
            int m = date.Month;
            int d = date.Day;

            if (m < 12)
            {
                if (d < (DateTime.DaysInMonth(y, m)) - 7)
                {
                    m = m + 1;
                }
                else
                {
                    if (m < 11)
                    {
                        m = m + 2;
                    }
                    else
                    {
                        y = y + 1;
                        m = 01;
                    }
                }

            }
            else
            {
                if (d < (DateTime.DaysInMonth(y, m)) - 7)
                {
                    y = y + 1;
                    m = 01;
                }
                else
                {
                    y = y + 1;
                    m = 02;
                }

            }

            string years = y.ToString();
            string month = m.ToString();
            DateTime Period = Convert.ToDateTime(years + "-" + month);
            string[] prSplit2 = Period.ToString().Split('/');
            string[] year2 = prSplit2[2].Split(' ');
            string periodfix = year2[0] + "-" + prSplit2[1];

            SelectDevice();
            establishContext();
            if (connectCard())
            {

                cardUID = getcardUID();
                string[] data = new string[2];
                data = DataLogic.LoginCard(cardUID);
                if (data[0] != "")
                {
                    //Session["TEST"] = "TEST";
                    Session["NIK"] = data[0];
                    Session["NAMA"] = data[1];
                    Session["ROLE"] = data[2];
                    Session["PERIOD"] = periodfix;
                    //stat = "1 "+data[0]+ " " + data[1] + " " + data[2] + "";//id kartu ada di db
                    stat = "1";
                }
                Context.Response.Clear();
                Context.Response.ContentType = "application/text";
                Context.Response.Write(stat);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public string loginWithCardUIDDekstop()
        {
            string stat = "0";
            DateTime date = ApplicationCatalogGet.GetDateTimeServer() ?? DateTime.Now;
            string[] prSplit = date.ToString().Split('/');
            string[] year = prSplit[2].Split(' ');
            int y = date.Year;
            int m = date.Month;
            int d = date.Day;

            if (m < 12)
            {
                if (d < (DateTime.DaysInMonth(y, m)) - 7)
                {
                    m = m + 1;
                }
                else
                {
                    if (m < 11)
                    {
                        m = m + 2;
                    }
                    else
                    {
                        y = y + 1;
                        m = 01;
                    }
                }

            }
            else
            {
                if (d < (DateTime.DaysInMonth(y, m)) - 7)
                {
                    y = y + 1;
                    m = 01;
                }
                else
                {
                    y = y + 1;
                    m = 02;
                }

            }

            string years = y.ToString();
            string month = m.ToString();
            DateTime Period = Convert.ToDateTime(years + "-" + month);
            string[] prSplit2 = Period.ToString().Split('/');
            string[] year2 = prSplit2[2].Split(' ');
            string periodfix = year2[0] + "-" + prSplit2[1];

            SelectDevice();
            establishContext();
            if (connectCard())
            {

                cardUID = getcardUID();
                string[] data = new string[2];
                data = DataLogic.LoginCard(cardUID);
                if (data[0] != "")
                {
                    //Session["TEST"] = "TEST";
                    Session["NIK"] = data[0];
                    Session["NAMA"] = data[1];
                    Session["ROLE"] = data[2];
                    Session["PERIOD"] = periodfix;
                    //stat = "1 "+data[0]+ " " + data[1] + " " + data[2] + "";//id kartu ada di db
                    stat = "1";
                }
                
            }
            return stat;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public void loginWithUserName(string username, string pass)
        {
            //untuk mencari period
            DateTime date = ApplicationCatalogGet.GetDateTimeServer() ?? DateTime.Now;
            string[] prSplit = date.ToString().Split('/');
            string[] year = prSplit[2].Split(' ');
            int y = date.Year;
            int m = date.Month;
            int d = date.Day;
            if (m < 12)
            {
                if (d < (DateTime.DaysInMonth(y, m)) - 7)
                {
                    m = m + 1;
                }else
                {
                    if (m < 11)
                    {
                        m = m + 2;
                    }else
                    {
                        y = y + 1;
                        m = 01;
                    }
                }
                
            }
            else
            {
                if (d < (DateTime.DaysInMonth(y, m)) - 7)
                {
                    y = y + 1;
                    m = 01;
                }
                else
                {                   
                    y = y + 1;
                    m = 02;            
                }
               
            }

            string years = y.ToString();
            string month = m.ToString();
            DateTime Period = Convert.ToDateTime(years + "-" + month);
            string[] prSplit2 = Period.ToString().Split('/');
            string[] year2 = prSplit2[2].Split(' ');
            string periodfix = year2[0] + "-" + prSplit2[1];


            string[] data = new string[2];
            
            
            string stat = "0";
            string nik;

            //if (ActiveDirectoryManager.VerifyUsernamePassword(username, pass))
            //{
                //string email = username + "@"+ConfigurationManager.AppSettings["ActiveDirectoryDomain"];
                
                //nik = DataLogic.GetUserNumberByEmail(email);

                nik = username;
                data = DataLogic.LoginUserName(nik);
                if (data[0] != "")
                {

                    Session["NIK"] = data[0];
                    Session["NAMA"] = data[1];
                    Session["ROLE"] = data[2];
                    Session["PERIOD"] = periodfix;
                    Session["INFO"] = "Selamat datang " + data[1];

                    stat = "1";
                }


            //}
            //else
            //{
                //stat = "0";
            //}


            
            Context.Response.Clear();
            Context.Response.ContentType = "application/text";
            Context.Response.Write(stat);
            //return stat;
            
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public string loginWithUserNameDekstop(string username, string pass)
        {
            //untuk mencari period
            DateTime date = ApplicationCatalogGet.GetDateTimeServer() ?? DateTime.Now;
            string[] prSplit = date.ToString().Split('/');
            string[] year = prSplit[2].Split(' ');
            int y = date.Year;
            int m = date.Month;
            int d = date.Day;
            if (m < 12)
            {
                if (d < (DateTime.DaysInMonth(y, m)) - 7)
                {
                    m = m + 1;
                }
                else
                {
                    if (m < 11)
                    {
                        m = m + 2;
                    }
                    else
                    {
                        y = y + 1;
                        m = 01;
                    }
                }

            }
            else
            {
                if (d < (DateTime.DaysInMonth(y, m)) - 7)
                {
                    y = y + 1;
                    m = 01;
                }
                else
                {
                    y = y + 1;
                    m = 02;
                }

            }

            string years = y.ToString();
            string month = m.ToString();
            DateTime Period = Convert.ToDateTime(years + "-" + month);
            string[] prSplit2 = Period.ToString().Split('/');
            string[] year2 = prSplit2[2].Split(' ');
            string periodfix = year2[0] + "-" + prSplit2[1];


            string[] data = new string[2];


            string stat = "0";
            string nik;

            //if (ActiveDirectoryManager.VerifyUsernamePassword(username, pass))
            //{
            //string email = username + "@"+ConfigurationManager.AppSettings["ActiveDirectoryDomain"];

            //nik = DataLogic.GetUserNumberByEmail(email);

            nik = username;
            data = DataLogic.LoginUserName(nik);
            if (data[0] != "")
            {

                Session["NIK"] = data[0];
                Session["NAMA"] = data[1];
                Session["ROLE"] = data[2];
                Session["PERIOD"] = periodfix;
                Session["INFO"] = "Selamat datang " + data[1];

                stat = "1";
            }


            //}
            //else
            //{
            //stat = "0";
            //}



            return stat;
        }


        [WebMethod(EnableSession=true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetMenuBooking()
        {
            string pernr = Session["NIK"].ToString();
            DataTable d = DataLogic.GetAllMenuBooking(pernr);
            string n = string.Empty;
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");

        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetMenuBookingCalendar()
        {
            string pernr = Session["NIK"].ToString();

            List<CalendarEventObj> d = DataLogic.GetCalendarMenuBooking(pernr);

            string n = string.Empty;
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetNikNama()
        {

            DataTable d = DataLogic.GetDataNikNama();
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");

        }

        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DataTable GetNikNamaDekstop()
        {

            DataTable d = DataLogic.GetDataNikNama();

            return d;
            //string JSONString = string.Empty;
            //JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
           
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetNikNamaAct()
        {

            DataTable d = DataLogic.GetDataNikNamaAct();
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetResetData()
        {

            DataTable d = DataLogic.GetResetData();
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");

        }

        [WebMethod]
        public DataTable GetNikNamaActDesktop()
        {

            DataTable d = DataLogic.GetDataNikNamaAct();
            return d;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RegMember(string nik, string nama, string cardID)
        {
            DataLogic.RegAnggota(nik, nama);
            if (cardID != string.Empty) {
                int stat = DataLogic.RegKartu(nik, nama, cardID, "USER"); //default role
            }
        }
        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ResetData(string nik)
        {
            DataLogic.ResetData(nik);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUserByNIK(string pernr)
        {
            DataTable d = Bsn.User.GetUserDataByNIK(pernr);
            string JSONString = string.Empty;
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClearSessionInfo()
        {
            if (!string.IsNullOrEmpty(Session["INFO"] as string))
            {
                Session["INFO"] = string.Empty;
            }
            string JSONString = string.Empty;

            Notif d = new Notif();
            d.type = "success";
            d.notes = "Session telah dihapus";
            d.head = "Success!";

            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClearAllSession()
        {
            Session.Clear();
            string JSONString = string.Empty;

            Notif d = new Notif();
            d.type = "success";
            d.notes = "Semua session telah dihapus";
            d.head = "Success!";

            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetTokenTicketing()
        {
            string token = Session["TOKEN"] == null ? string.Empty : Session["TOKEN"].ToString();
            string JSONString = string.Empty;
            DateTime now = ApplicationCatalogGet.GetDateTimeServer() ?? DateTime.Now;
            string d = token;
            
            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTokenTicketingDekstop()
        {
            string token = Session["TOKEN"] == null ? string.Empty : Session["TOKEN"].ToString();
            string JSONString = string.Empty;
            DateTime now = ApplicationCatalogGet.GetDateTimeServer() ?? DateTime.Now;
            string d = token;
            return d;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetDateTimeServer()
        {
            string JSONString = string.Empty;
            DateTime date = AppConfig.GetDateTimeServer();
            string d = date.ToString("yyyy/MM/dd HH:mm:ss.ff");

            JSONString = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write("{\"data\": " + JSONString + "}");
        }

    }
}