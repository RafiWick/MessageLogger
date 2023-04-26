namespace MessageLogger.UnitTests
{
    public class MessageTest
    {
        [Fact]
        public void Message_Constructor_CreatesMessage()
        {
            var testMessage = new Message("abcdefg", null);
            var DT = DateTime.Now;

            Assert.Equal("abcdefg", testMessage.Content);
            
            Assert.Equal(new DateTime(DT.Year, DT.Month, DT.Day, DT.Hour, DT.Minute, DT.Second), testMessage.CreatedAt);
        }
        [Fact]
        public void Message_Edit_ChangesContentsAndSetsEdited()
        {
            var testMessage = new Message("abcdefg", null);
            testMessage.Edit("abcde");

            Assert.Equal("abcde", testMessage.Content);
            Assert.True(testMessage.Edited);
        }
    }
}