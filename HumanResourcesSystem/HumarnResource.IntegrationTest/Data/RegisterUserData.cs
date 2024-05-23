namespace HumarnResource.IntegrationTest.Data
{
    public class RegisterUserData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "testtest@test.com", "testtest123", "testtest123", "737410216" };
            yield return new object[] { "testtest2@test.com", "testtest124", "testtest124", "737410217" };
            yield return new object[] { "testtest3@test.com", "testtest123", "testtest123", "737410211" };
            yield return new object[] { "testtest4@test.com", "testtest124", "testtest124", "737410218" };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
       

    }
}
