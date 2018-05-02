<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlFileBrowser.ascx.cs" Inherits="controlFileBrowser" %>
<style type="text/css">
.FileBrowser {
    border: 1px solid #000000;
    margin-top: 10px;
    background-image: url('../Image/Admin/bg.jpg');
}
.FileBrowser table {
    border: 0px;
    color: #000000; /*border-radius: 3px;
    background-color: #F7F6F3;*/
    font-family: 'Arial Narrow'; /*background-image: url('../Image/Admin/bg.jpg');*/
    width: 500px;
    margin-left: 10px;
}
.FileBrowser th {
    border: 0px;
    color: #FFFFFF;
    font-weight: bold;
    word-wrap: hyphenate;
    text-align: center; /*padding-right: 10px;*/
    text-transform: uppercase;
    background-color: #808080;
}
.FileBrowser td {
    padding: 5px;
    border: 0px;
    color: #000000;
    text-align: left;
    /*width: 300px;
    background-color: #FFFFFF;*/
}

.FileBrowser span {
    color: #000066;
}

.FileBrowser table {
    border: 1px solid #FFFFFF;
}
</style>
<asp:Panel ID="pnFileBrowser" runat="server">
    <div class="FileBrowser">
        &nbsp; &nbsp;<asp:Label ID="lblCurrentDir" runat="server" Font-Italic="True" Text="<%$ Resources:ResourceBase, lsCurrentDir %>"></asp:Label><br />
        <br />
        &nbsp; &nbsp;<asp:Button ID="cmdUp" runat="server" Width="80px" Text="<%$ Resources:ResourceBase, btDirUp %>" Height="23px" OnClick="cmdUp_Click" CausesValidation="false" />
        &nbsp; &nbsp;<asp:Button ID="cmdClose" runat="server" Width="80px" Text="<%$ Resources:ResourceBase, btDirClose %>" Height="23px" OnClick="cmdClose_Click" CausesValidation="false"/>
        <asp:GridView ID="gridDirList" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gridDirList_SelectedIndexChanged"
            Width="400px" DataKeyNames="FullName">
            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/image/wuc/folder.png" />
                    </ItemTemplate>
                    <HeaderStyle Width="20px" />
                </asp:TemplateField>
                <asp:ButtonField DataTextField="Name" CommandName="Select" HeaderText="<%$ Resources:ResourceBase, fbfnName %>">
                    <HeaderStyle Width="200px" />
                </asp:ButtonField>
                <asp:BoundField HeaderText="<%$ Resources:ResourceBase, fbfnSize %>">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="LastWriteTime" HeaderText="<%$ Resources:ResourceBase, fbfnLastModified %>" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gridFileList" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gridFileList_SelectedIndexChanged" Width="400px" DataKeyNames="FullName">
            <HeaderStyle Font-Size="1px"></HeaderStyle>
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/image/wuc/file.png" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField DataTextField="Name" CommandName="Select">
                    <HeaderStyle Width="200px"></HeaderStyle>
                </asp:ButtonField>
                <asp:BoundField DataField="Length">
                    <HeaderStyle Width="50px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="LastWriteTime"></asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Panel>
