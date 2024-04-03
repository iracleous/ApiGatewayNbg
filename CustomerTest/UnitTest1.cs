using Customer2.Services;

namespace CustomerTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var a= 1;
            var b= 2;
            var expectedValue = a+b;
            IAdder adder = new Adder();
            var calculated = adder.Add(a,b) ;

            Assert.Equal(expectedValue, calculated);

            Assert.Throws<NotImplementedException>(() => adder.Add(-1,1));


        }
    }
}