<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlRules.ascx.cs" Inherits="controlRules" %>
<style type="text/css">
    div.rulesSection {
        float:left;
        width:auto
    }
    div.rulesRules1 {
        float:left;
        width:auto
    }
    table.tabRules {
    border: 1px solid #999999;
    color: #000000;
    border-radius: 3px;
    background-color: #F7F6F3;
    font-family: 'Arial Narrow';
    width: 300px;
    margin-top: 20px;
    font-size: 12px;
    margin-bottom: 20px;
}
.tabRules th {
    padding: 5px 10px 5px 5px;
    border: 1px solid #999999;
    color: #5B5B5B;
    font-weight: bold;
    word-wrap: hyphenate;
    text-align: left;
    text-transform: uppercase;
    background-color: #F7F6F3;
}
.tabRules td {
    padding: 5px;
    border: 1px solid #999999;
    color: #000000;
    background-color: #FFFFFF;
    /*text-align: center;*/
}
    .tabRules td.object {
    width: 200px;
    font-weight: bold;
    text-transform: uppercase;
    background-color: #F7F6F3;
    text-align:right;
}
</style>
<div class="rulesSection">
<asp:PlaceHolder ID="phSection" runat="server"></asp:PlaceHolder>
</div>
<div class="rulesRules1">
<asp:PlaceHolder ID="phRules1" runat="server"></asp:PlaceHolder>
</div>

