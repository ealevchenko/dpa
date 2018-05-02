using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using Strategic;
using System.Globalization;
using System.Threading;
using WebBase;

namespace WebReports
{
    /// <summary>
    /// Сводное описание для classPDF
    /// </summary>
    public class classPDFReports
    {
        private classProject cp = new classProject();
        private classSection cs = new classSection();
        private classOrder co = new classOrder();
        private classTemplatesSteps cts = new classTemplatesSteps();

        public classPDFReports()
        {
            //
            // TODO: добавьте логику конструктора
            //
        }

        #region МЕТОДЫ
        /// <summary>
        /// Создаем PDF-документ из HTML-документа
        /// </summary>
        public void CreatePDFDocument(string fileName, string strHtml, Rectangle rectangle)
        {
            // Добавляем поддержку русских символов
            FontFactory.Register("C:\\WINDOWS\\Fonts\\arial.ttf"); // путь к файлу шрифта
                // Создаем документ
            Document document = new Document();
            //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetPageSize(rectangle);
            // Создаем PDF-документ
            PdfWriter.GetInstance(document,
            new FileStream(fileName, FileMode.Create));
            StringReader se = new StringReader(strHtml);
            HTMLWorker obj = new HTMLWorker(document);
            document.Open();
            // Создаем PDF на основе HTML
            obj.Parse(se);
            // Сохраняем файл
            document.Close();
        }
        /// <summary>
        /// Создать PDF документ в стиле отчета по внедрению программы
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ci"></param>
        /// <param name="collproject"></param>
        public void CreatePDFDocumentStatusProgramm(string fileName, CultureInfo ci, ProjectCollection<ProjectStatus> collproject, string NameReport)
        {
            string currentLanguage = Thread.CurrentThread.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ci.Name);
            // Шрифты
            BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font h1 = new iTextSharp.text.Font(baseFont, 14f, iTextSharp.text.Font.BOLDITALIC);
            iTextSharp.text.Font hd = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
            iTextSharp.text.Font hsection = new iTextSharp.text.Font(baseFont, 9f, iTextSharp.text.Font.BOLDITALIC);
            iTextSharp.text.Font flink = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.UNDERLINE, BaseColor.BLUE);
            iTextSharp.text.Font flinkproject = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.UNDERLINE, BaseColor.BLUE);

            iTextSharp.text.Font dname = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.ITALIC);
            iTextSharp.text.Font dconstruction = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font afYes = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.NORMAL, BaseColor.GREEN);
            iTextSharp.text.Font afNo = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.NORMAL, BaseColor.RED);
            iTextSharp.text.Font dbudget = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font ddatecontractor = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font dcontractor = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.NORMAL);

            iTextSharp.text.Font dstatus_detali = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font dcoment_detali = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font dcomentstop_detali = new iTextSharp.text.Font(baseFont, 8f, iTextSharp.text.Font.NORMAL);

            //Цвет
            BaseColor bc_project = new BaseColor(83, 141, 213);
            BaseColor bc_cost = new BaseColor(230, 184, 183);
            BaseColor bc_status = new BaseColor(118, 147, 60);
            BaseColor bc_contract = new BaseColor(250, 191, 143);

            BaseColor bc_section = new BaseColor(255, 235, 156);

            BaseColor bc_cell = new BaseColor(247, 246, 243);

            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            // Создаем PDF-документ
            PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            document.Open();

            // текстовые сообщения
            //String phrase = "Статус выполнения весовой программы по состоянию на " + DateTime.Now.Date.ToString("dd-MM-yyyy") + "\n";
            // заголовки таблицы
            string s_name = cp.GetStringStrategicResource("ppNameProject");
            string s_order = cp.GetStringStrategicResource("ppOrderProject");
            string s_construction = cp.GetStringStrategicResource("ppTypeConstruction");
            string s_cost = cp.GetStringStrategicResource("ppCost");
            string s_budget = cp.GetStringStrategicResource("ppBudget");
            string s_status = cp.GetStringStrategicResource("ppStatus");
            string s_coment = cp.GetStringStrategicResource("ppComent");
            string s_comentstop = cp.GetStringStrategicResource("ppComentStop");
            string s_datecontractor = cp.GetStringStrategicResource("ppDateContractor");
            string s_contractor = cp.GetStringStrategicResource("ppContractor");

            document.Add(new Paragraph(NameReport, h1));
            // Фразы
            Phrase h_name = new Phrase(s_name.ToUpper(), hd);
            Phrase h_order = new Phrase(s_order.ToUpper(), hd);
            Phrase h_construction = new Phrase(s_construction.ToUpper(), hd);
            Phrase h_cost = new Phrase(s_cost.ToUpper(), hd);
            Phrase h_budget = new Phrase(s_budget.ToUpper(), hd);
            Phrase h_status = new Phrase(s_status.ToUpper(), hd);
            Phrase h_coment = new Phrase(s_coment.ToUpper(), hd);
            Phrase h_comentstop = new Phrase(s_comentstop.ToUpper(), hd);
            Phrase h_datecontractor = new Phrase(s_datecontractor.ToUpper(), hd);
            Phrase h_contractor = new Phrase(s_contractor.ToUpper(), hd);
            //Таблица
            PdfPTable tabstatus = new PdfPTable(10);
            //tabstatus.PaddingTop = 50f;
            //actual width of table in points
            tabstatus.TotalWidth = 750f;
            //fix the absolute width of the table
            tabstatus.LockedWidth = true;
            tabstatus.SetWidths(new float[] { 200f, 50f, 50f, 50f, 50f, 50f, 100f, 50f, 50f, 100f });
            tabstatus.HorizontalAlignment = 0;
            //Ячейки заголовок таблицы
            PdfPCell cell_name = new PdfPCell(h_name);
            cell_name.BackgroundColor = bc_project;
            cell_name.HorizontalAlignment = 1;
            cell_name.VerticalAlignment = 1;
            tabstatus.AddCell(cell_name);
            PdfPCell cell_order = new PdfPCell(h_order);
            cell_order.BackgroundColor = bc_project;
            cell_order.HorizontalAlignment = 1;
            cell_order.VerticalAlignment = 1;
            tabstatus.AddCell(cell_order);
            PdfPCell cell_construction = new PdfPCell(h_construction);
            cell_construction.BackgroundColor = bc_project;
            cell_construction.HorizontalAlignment = 1;
            cell_construction.VerticalAlignment = 1;
            tabstatus.AddCell(cell_construction);
            PdfPCell cell_cost = new PdfPCell(h_cost);
            cell_cost.BackgroundColor = bc_cost;
            cell_cost.HorizontalAlignment = 1;
            cell_cost.VerticalAlignment = 1;
            tabstatus.AddCell(cell_cost);
            PdfPCell cell_budget = new PdfPCell(h_budget);
            cell_budget.BackgroundColor = bc_cost;
            cell_budget.HorizontalAlignment = 1;
            cell_budget.VerticalAlignment = 1;
            tabstatus.AddCell(cell_budget);
            PdfPCell cell_status = new PdfPCell(h_status);
            cell_status.BackgroundColor = bc_status;
            cell_status.HorizontalAlignment = 1;
            cell_status.VerticalAlignment = 1;
            tabstatus.AddCell(cell_status);
            PdfPCell cell_coment = new PdfPCell(h_coment);
            cell_coment.BackgroundColor = bc_status;
            cell_coment.HorizontalAlignment = 1;
            cell_coment.VerticalAlignment = 1;
            tabstatus.AddCell(cell_coment);
            PdfPCell cell_comentstop = new PdfPCell(h_comentstop);
            cell_comentstop.BackgroundColor = bc_status;
            cell_comentstop.HorizontalAlignment = 1;
            cell_comentstop.VerticalAlignment = 1;
            tabstatus.AddCell(cell_comentstop);
            PdfPCell cell_datecontractor = new PdfPCell(h_datecontractor);
            cell_datecontractor.BackgroundColor = bc_contract;
            cell_datecontractor.HorizontalAlignment = 1;
            cell_datecontractor.VerticalAlignment = 1;
            tabstatus.AddCell(cell_datecontractor);
            PdfPCell cell_contractor = new PdfPCell(h_contractor);
            cell_contractor.BackgroundColor = bc_contract;
            cell_contractor.HorizontalAlignment = 1;
            cell_contractor.VerticalAlignment = 1;
            tabstatus.AddCell(cell_contractor);
            List<int> sectionbuffer = new List<int>();
            foreach (ProjectStatus ps in collproject)
            {
                // Выводим подразделение только 1 раз
                if (sectionbuffer.BinarySearch(ps.project.IDSection) < 0)
                {
                    sectionbuffer.Add(ps.project.IDSection);
                    sectionbuffer.Sort();

                    Phrase h_section = new Phrase(cs.GetCultureSection(ps.project.IDSection).SectionFull, hsection);
                    PdfPCell cell_section = new PdfPCell(h_section);
                    cell_section.BackgroundColor = bc_section;
                    cell_section.Colspan = 10;
                    cell_section.HorizontalAlignment = 1;
                    cell_section.VerticalAlignment = 1;
                    tabstatus.AddCell(cell_section);
                }
                // ячейка наименование
                Anchor d_linkproject = new Anchor(ps.project.Name, flinkproject);
                d_linkproject.Reference = cp.GetLinkProject((int)ps.project.Id);
                //Phrase d_name = new Phrase(ps.project.Name, dname);
                PdfPCell cell_dname = new PdfPCell(d_linkproject);
                cell_dname.BackgroundColor = bc_cell;
                cell_dname.HorizontalAlignment = 0;
                cell_dname.VerticalAlignment = 1;
                tabstatus.AddCell(cell_dname);

                // ячейка приказ
                if (ps.project.IDOrder != null)
                {
                    Anchor d_linkorder = new Anchor(co.GetNumDateOrder(ps.project.IDOrder), flink);
                    d_linkorder.Reference = co.GetLinkOrder(ps.project.IDOrder);
                    PdfPCell cell_dlinkorder = new PdfPCell(d_linkorder);
                    cell_dlinkorder.BackgroundColor = bc_cell;
                    cell_dlinkorder.HorizontalAlignment = 1;
                    cell_dlinkorder.VerticalAlignment = 1;
                    tabstatus.AddCell(cell_dlinkorder);
                }
                else
                {
                    PdfPCell cell_dorder = new PdfPCell();
                    cell_dorder.BackgroundColor = bc_cell;
                    tabstatus.AddCell(cell_dorder);
                }
                // ячейка тип строительства
                Phrase d_construction = new Phrase(cp.GetTypeConstruction((int)ps.project.TypeConstruction), dconstruction);
                PdfPCell cell_dconstruction = new PdfPCell(d_construction);
                cell_dconstruction.BackgroundColor = bc_cell;
                cell_dconstruction.HorizontalAlignment = 1;
                cell_dconstruction.VerticalAlignment = 1;
                tabstatus.AddCell(cell_dconstruction);
                // ячейка затраты
                string sFunding = ps.project.Funding != null ? (ps.project.Funding as decimal?).Value.ToString("###,###,##0.00") + " " + ((typeCurrency)ps.project.Currency).ToString() : "";
                string sAllocationFunds = cp.GetAllocationFunds(ps.project.AllocationFunds);
                Phrase d_Funding;
                if (ps.project.AllocationFunds)
                {

                    d_Funding = new Phrase(sFunding + "\n" + sAllocationFunds, afYes);
                }
                else
                {
                    d_Funding = new Phrase(sFunding + "\n" + sAllocationFunds, afNo);
                }
                PdfPCell cell_dcost = new PdfPCell(d_Funding);
                cell_dcost.BackgroundColor = bc_cell;
                cell_dcost.HorizontalAlignment = 1;
                cell_dcost.VerticalAlignment = 1;
                tabstatus.AddCell(cell_dcost);
                // ячейка бюджет
                Phrase d_budget = new Phrase(ps.project.SAPCode, dbudget);
                PdfPCell cell_dbudget = new PdfPCell(d_budget);
                cell_dbudget.BackgroundColor = bc_cell;
                cell_dbudget.HorizontalAlignment = 1;
                cell_dbudget.VerticalAlignment = 1;
                tabstatus.AddCell(cell_dbudget);

                PdfPTable tab_ddetali = new PdfPTable(3);
                tab_ddetali.TotalWidth = 200f;
                //fix the absolute width of the table
                tab_ddetali.LockedWidth = true;
                tab_ddetali.SetWidths(new float[] { 50f, 100f, 50f });
                //PdfPTable tab_dstatus = new PdfPTable(1);
                //PdfPTable tab_dcoment = new PdfPTable(1);
                //PdfPTable tab_dcomentstop = new PdfPTable(1);

                Phrase d_status_detali;
                Phrase d_coment_detali;
                Phrase d_comentstop_detali;
                bool bit = false;
                foreach (ProjectStepDetaliContent psd in ps.stepproject)
                {

                    if ((psd.Persent > 0) & (psd.Persent < 100))
                    {
                        bit = true;
                        d_status_detali = new Phrase(cts.GetStep(psd.IDStep), dstatus_detali);
                        d_coment_detali = new Phrase(psd.Coment, dcoment_detali);
                        d_comentstop_detali = new Phrase((psd.FactStop != null ? (psd.FactStop as DateTime?).Value.ToString("dd-MM-yyyy") : ""), dcomentstop_detali);
                        PdfPCell cell_dstatus_detali = new PdfPCell(d_status_detali);
                        cell_dstatus_detali.BackgroundColor = bc_cell;
                        cell_dstatus_detali.HorizontalAlignment = 1;
                        cell_dstatus_detali.VerticalAlignment = 1;
                        tab_ddetali.AddCell(cell_dstatus_detali);
                        PdfPCell cell_dcoment_detali = new PdfPCell(d_coment_detali);
                        cell_dcoment_detali.BackgroundColor = bc_cell;
                        cell_dcoment_detali.HorizontalAlignment = 0;
                        cell_dcoment_detali.VerticalAlignment = 1;
                        tab_ddetali.AddCell(cell_dcoment_detali);
                        PdfPCell cell_dcomentstop_detali = new PdfPCell(d_comentstop_detali);
                        cell_dcomentstop_detali.BackgroundColor = bc_cell;
                        cell_dcomentstop_detali.HorizontalAlignment = 0;
                        cell_dcomentstop_detali.VerticalAlignment = 1;
                        tab_ddetali.AddCell(cell_dcomentstop_detali);
                    }
                }
                // активных шагов небыло
                if (!bit)
                {
                    d_status_detali = new Phrase(" ", dstatus_detali);
                    d_coment_detali = new Phrase(" ", dcoment_detali);
                    d_comentstop_detali = new Phrase(" ", dcomentstop_detali);
                    PdfPCell cell_dstatus_detali = new PdfPCell(d_status_detali);
                    tab_ddetali.AddCell(cell_dstatus_detali);
                    PdfPCell cell_dcoment_detali = new PdfPCell(d_coment_detali);
                    tab_ddetali.AddCell(cell_dcoment_detali);
                    PdfPCell cell_dcomentstop_detali = new PdfPCell(d_comentstop_detali);
                    tab_ddetali.AddCell(cell_dcomentstop_detali);
                }
                // ячейка детали
                PdfPCell cell_ddetaly = new PdfPCell(tab_ddetali);
                cell_ddetaly.BackgroundColor = bc_cell;
                cell_ddetaly.Colspan = 3;
                //cell_ddetaly.HorizontalAlignment = 1;
                //cell_ddetaly.VerticalAlignment = 1;
                tabstatus.AddCell(cell_ddetaly);
                //// ячейка статус
                // PdfPCell cell_dstatus = new PdfPCell(tab_dstatus);
                // cell_dstatus.BackgroundColor = hcell;
                // cell_dstatus.HorizontalAlignment = 1;
                // cell_dstatus.VerticalAlignment = 1;
                // tabstatus.AddCell(cell_dstatus);
                //// ячейка коментарий
                // PdfPCell cell_dcoment = new PdfPCell(tab_dcoment);
                // cell_dcoment.BackgroundColor = hcell;
                // cell_dcoment.HorizontalAlignment = 0;
                // cell_dcoment.VerticalAlignment = 1;
                // tabstatus.AddCell(cell_dcoment);
                //// ячейка предпологаемая дата
                // PdfPCell cell_dcomentstop = new PdfPCell(tab_dcomentstop);
                // cell_dcomentstop.BackgroundColor = hcell;
                // cell_dcomentstop.HorizontalAlignment = 1;
                // cell_dcomentstop.VerticalAlignment = 1;
                // tabstatus.AddCell(cell_dcomentstop);

                // ячейка дата контракта
                Phrase d_datecontractor = new Phrase(ps.project.DateContractor, ddatecontractor);
                PdfPCell cell_ddatecontractor = new PdfPCell(d_datecontractor);
                cell_ddatecontractor.BackgroundColor = bc_cell;
                cell_ddatecontractor.HorizontalAlignment = 1;
                cell_ddatecontractor.VerticalAlignment = 1;
                tabstatus.AddCell(cell_ddatecontractor);
                // ячейка исполнитель
                Phrase d_contractor = new Phrase(ps.project.Contractor, dcontractor);
                PdfPCell cell_dcontractor = new PdfPCell(d_contractor);
                cell_dcontractor.BackgroundColor = bc_cell;
                cell_dcontractor.HorizontalAlignment = 1;
                cell_dcontractor.VerticalAlignment = 1;
                tabstatus.AddCell(cell_dcontractor);
            }


            document.Add(tabstatus);

            // Сохраняем файл
            document.Close();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(currentLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentLanguage);
        }
        #endregion
    }
}