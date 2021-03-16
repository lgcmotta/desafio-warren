using DesafioWarren.Domain.Aggregates;
using Xunit;

namespace DesafioWarren.UnitTests.Domain
{
    public class AccountTests
    {
        [Fact]
        public void CreateNewAccount_Sucess()
        {
            var account = new Account("Foo", "foo@gmail.com", "+5551999999999", "123.456.789.10");

            Assert.NotNull(account);
        }

        [Fact]
        public void GetAccountName_Sucess()
        {
            var account = new Account("Foo", "foo@gmail.com", "+5551999999999", "123.456.789.10");

            Assert.Equal("Foo", account.GetName());
        }

        [Fact]
        public void GetAccountEmail_Sucess()
        {
            var account = new Account("Foo", "foo@gmail.com", "+5551999999999", "123.456.789.10");

            Assert.Equal("foo@gmail.com", account.GetEmail());
        }

        [Fact]
        public void GetAccountPhoneNumber_Sucess()
        {
            var account = new Account("Foo", "foo@gmail.com", "+5551999999999", "123.456.789.10");

            Assert.Equal("+5551999999999", account.GetPhoneNumber());
        }

        [Fact]
        public void ChangeAccountEmail_Succes()
        {
            var account = new Account("Foo", "foo@gmail.com", "+5551999999999", "123.456.789.10");

            account.ChangeEmail("foo2@gmail.com");

            Assert.Equal("foo2@gmail.com", account.GetEmail());

            Assert.NotEqual("foo@gmail.com", account.GetEmail());
        }

        [Fact]
        public void ChangePhoneNumber_Sucess()
        {
            var account = new Account("Foo", "foo@gmail.com", "+5551999999999", "123.456.789.10");

            account.ChangePhoneNumber("+5551888888888");

            Assert.Equal("+5551888888888", account.GetPhoneNumber());

            Assert.NotEqual("+5551999999999", account.GetPhoneNumber());
        }
    }
}