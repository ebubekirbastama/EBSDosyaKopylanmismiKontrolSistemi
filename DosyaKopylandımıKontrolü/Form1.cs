using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace DosyaKopylandımıKontrolü
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        Thread th;

        private void logkontrol()
        {
            // Güvenlik günlüğündeki olayları izleyeceğiz
            string logName = "Security";

            // Belirli bir olay tipini filtrelemek için kullanılacak anahtar kelime
            string eventKeyword = "4663"; // Bu örnekte, dosya/folder erişim olaylarını temsil eder

            // EventLog örneği oluşturun
            EventLog eventLog = new EventLog(logName);

            // Olayları filtreleme
            EventLogEntryType entryType = EventLogEntryType.SuccessAudit; // Başarılı denetim olayları
            EventLogEntryCollection entries = eventLog.Entries;
            int sayac = 1;

            // Güvenlik günlüğündeki yeni olayları kontrol etme
            foreach (EventLogEntry entry in entries)
            {

                // Sadece belirli olay tipini ve anahtar kelimeyi içeren olayları işle
                if (entry.EntryType == entryType && entry.Message.Contains(eventKeyword))
                {
                    listBox1.Items.Add($"Dosya kopyalama olayı: {entry.TimeGenerated} - {entry.Message}");
                }
                label1.Text = "Analiz edilen : " + sayac.ToString();
                sayac++;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            th = new Thread(logkontrol); th.Start();
        }
    }
}
