namespace MessageLogger.UnitTests
{
    public class MessageTest
    {
        [Fact]
        public void Message_Constructor_CreatesMessage()
        {
            var testMessage = new Message("abcdefg");
            var DT = DateTime.Now;

            Assert.Equal("abcdefg", testMessage.Content);
            
            Assert.Equal(new DateTime(DT.Year, DT.Month, DT.Day, DT.Hour, DT.Minute, DT.Second), testMessage.CreatedAt);
        }
    }
}