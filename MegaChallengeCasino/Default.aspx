<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MegaChallengeCasino.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:Image ID="casinoLeftImage" Height ="150px" Width="150px" runat="server" />
                <asp:Image ID="casinoMidImage" Height ="150px" Width="150px" runat="server" />
                <asp:Image ID="casinoRightImage" Height ="150px" Width="150px" runat="server" />
            </div>
            <br />
            <div>
                <asp:Label ID="betLabel" AssociatedControlID="betTextBox" Text="Your Bet: " runat="server"></asp:Label>
                <asp:TextBox ID="betTextBox" runat="server"></asp:TextBox>
            </div>
            <br />
            <div>
                <asp:Button ID="betButton" Text="Pull The Lever!" OnClick="BetButton_Click" runat="server" />
            </div>
            <br />
            <br />
            <br />
            <div>
                <asp:Label ID="resultLabel" Text="" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="playerMoneyLabel" Text="Player's Money: $100.00" runat="server"></asp:Label>
            </div>
            <div>
                <p>
                    1 Cherry - x2 Your Bet <br />
                    2 Cherries - x3 Your Bet <br />
                    3 Cherries - x4 Your Bet <br />
                    3 7's - Jackpot - x100 Your Bet <br />
                    HOWEVER - If there's even on BAR, you win nothing.
                </p>
            </div>
        </div>
    </form>
</body>
</html>
