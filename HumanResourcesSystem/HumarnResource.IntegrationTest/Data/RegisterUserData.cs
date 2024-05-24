using System.Collections;

namespace HumarnResource.IntegrationTest.Data
{
    public class RegisterUserData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "testtest1@test.com", "testtest", "testtest", "111111111" };
            yield return new object[] { "testtest2@test.com", "testtest", "testtest", "111111111" };
            yield return new object[] { "testtest3@test.com", "testtest", "testtest", "111111111" };
            yield return new object[] { "testtest4@test.com", "testtest", "testtest", "111111111" };
            
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
