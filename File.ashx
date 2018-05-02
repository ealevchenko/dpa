<%@ WebHandler Language="C#" Class="FileHandlerH" %>
using System;
using System.Web;
//using Core;
public class FileHandlerH : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        // Получаем идентификатор нужного файла
        // Для простоты я не делаю особых проверок
        // Но как минимум — перевести строку в число необходимо
        int id = int.Parse(HttpContext.Current.Request["ID"]);
        classFiles cfiles = new classFiles();
        FileContent fc = cfiles.GetFile(id);

        // Читаем контент в соответствии с id.
        // Как это делать я опишу дальше.
        string contentType = fc.FileContent;
        // Читаем данные
        byte[] data = fc.FileImage;
        // Выводим в поток
        HttpContext.Current.Response.ContentType = contentType;
        HttpContext.Current.Response.AppendHeader("Content-Length",
        data.Length.ToString());
        HttpContext.Current.Response.BinaryWrite(data);
        // Сбрасываем весь буфер
        //HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
    public bool IsReusable
    {
        get { return true; }
    }
}