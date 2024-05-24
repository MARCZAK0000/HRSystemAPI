using System.Collections;

namespace HumarnResource.IntegrationTest.Data
{
    public class SignInUserData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "testtest1@test.com", "testtest"};
            yield return new object[] { "testtest2@test.com", "testtest"};
            yield return new object[] { "testtest3@test.com", "testtest"};
            yield return new object[] { "testtest4@test.com", "testtest"};

        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
