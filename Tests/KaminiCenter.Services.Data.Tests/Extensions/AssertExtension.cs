namespace KaminiCenter.Services.Data.Tests.Extensions
{
    using Xunit;

    public static class AssertExtension
    {
        public static void EqualsWithMessage(object first, object second, string message)
        {
            Assert.True(first == second, message);
        }
    }
}
