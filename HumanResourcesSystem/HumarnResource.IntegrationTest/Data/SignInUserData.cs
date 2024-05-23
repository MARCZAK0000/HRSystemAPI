using System.Collections;

namespace HumarnResource.IntegrationTest.Data
{
    public class SignInUserData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "testtest@test.com", "testtest123" };
            yield return new object[] { "testtest3@test.com", "testtest123" };
            yield return new object[] { "testtest2@test.com", "testtest124" };
            yield return new object[] { "testtest4@test.com", "testtest124" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
