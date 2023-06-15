using ClassesModule;
using DataBaseModule;
using System;
using System.ComponentModel;
using Variables;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestConnection()
        {
            string connection = "null";
            ValuesProject.ConnectionString = connection;
            ConnectData connect = new ConnectData();
            var end = connect.Connection("SELEC * FROM token");
            string token = "";
            while (end.Read())
            {
                token = end.GetValue(2).ToString();
            }
            Assert.Equal(ValuesProject.ActualUser.token, token);
        }

        [Fact]
        public void TestCommand()
        {
            CommandsClass expectedCommand = new CommandsClass { id = 0,
            header = "testHeader", hotkey = null, streamerNick = "ba4ebar",
            tag = "testTag", text = "testText", type = "On"};
            ConnectData connect = new ConnectData();
            connect.InstertSql(expectedCommand);
            connect.FillCommands();
            CommandsClass actualCommand = ValuesProject.Commands[ValuesProject.Commands.Count - 1];
            Assert.Equal(expectedCommand, actualCommand);
        }
    }
}