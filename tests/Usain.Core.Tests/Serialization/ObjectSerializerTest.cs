namespace Usain.Core.Tests.Serialization
{
    using Core.Serialization;
    using Xunit;

    public class ObjectSerializerTest
    {
        [Fact]
        public void ToString_Returns_Expected_Json()
        {
            const string expected =
                "{\"property1\":\"default\",\"property3\":2}";
            var actual = ObjectSerializer.ToString(
                new TestObject
                {
                    Property3 = 2,
                });

            Assert.Equal(
                expected,
                actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void FromString_Returns_Default_When_Value_Is_NullOrEmpty(
            string value)
        {
            Assert.Null(ObjectSerializer.FromString<string>(value));
            Assert.Null(ObjectSerializer.FromString<TestObject>(value));
            Assert.Equal(
                0,
                ObjectSerializer.FromString<int>(value));
        }

        [Fact]
        public void FromString_Returns_Expected_Object()
        {
            var expected = new TestObject
            {
                Property1 = "value",
                Property2 = "value",
                Property3 = 0,
            };

            var actual = ObjectSerializer.FromString<TestObject>(
                "{\"property1\":\"value\",\"property2\":\"value\"}");

            Assert.Equal(
                expected.Property1,
                actual.Property1);
            Assert.Equal(
                expected.Property2,
                actual.Property2);
            Assert.Equal(
                expected.Property3,
                actual.Property3);
        }

        private class TestObject
        {
            public string Property1 { get; set; } = "default";
            public string Property2 { get; set; }
            public int Property3 { get; set; }
        }
    }
}
