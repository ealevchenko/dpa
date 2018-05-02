<%@ Control Language="C#" AutoEventWireup="true" CodeFile="panelFile.ascx.cs" Inherits="panelFile" %>
<style>
    .LinkCommand {
        text-transform: uppercase;
        margin: 5px;
        font-weight: bold;
    }
</style>
<%--<script type="text/javascript">
    function Show<%=this.ID %>FileNew() {
        var objnew = document.getElementById('<%=fileUpload.ClientID%>');
        if (objnew) {
            objnew.style.display = '';
        }
        var objold = document.getElementById('<%=tbFileOld.ClientID%>');
        if (objold) {
            objold.style.display = 'none';
        }
        var objclr = document.getElementById('<%=btClr.ClientID%>');
        if (objclr) {
            objclr.style.display = 'none';
        }
        var objadd = document.getElementById('<%=btAdd.ClientID%>');
        if (objadd) {
            objadd.style.display = 'none';
        }
    }
    function ClearFile() {
        var obj = document.getElementById('<%=tbFileOld.ClientID%>');
        if (obj) {
            obj.value = '';
        }
    }
    </script>--%>
<asp:TextBox ID="tbFileOld" runat="server" Enabled="false"></asp:TextBox>
<asp:Button ID="btClr" runat="server" Text="<%$ Resources:ResourceBase, btDel %>" OnClick="btClr_Click" />
<asp:Button ID="btAdd" runat="server" Text="<%$ Resources:ResourceBase, btAdd %>" OnClick="btAdd_Click" />
<asp:FileUpload ID="fileUpload" runat="server" />
