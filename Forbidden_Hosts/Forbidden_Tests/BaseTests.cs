using NUnit.Framework;

namespace Forbidden_Tests
{
    using Forbidden_Hosts;
    using System.Linq;

    public class BaseTests
    {
      
        /// <summary>
        /// ��������� ������������ ��������� <see cref="HostItem"/>
        /// </summary>
        [Test]
        public void Check_Extensions_Creating_HostItems()
        {
            // ����������
            var hosts = this.GetHosts();

            // ��������
            var result = hosts.ToHosts();

            // ��������
            Assert.AreEqual(hosts.Count(), result.Count());
            var dictory = result.ToDictionary(x => x.UniqueCode, x => x.Host);
            Assert.AreEqual(hosts.Count(), dictory.Keys.Count());
        }

        /// <summary>
        /// ��������� ����� ��������� �����
        /// </summary>
        [Test]
        [TestCase("unlock.microvirus.md", "microvirus.md")]
        [TestCase("credit.card.us", "card.us")]
        public void Check_Extensions_GetParent(string sourceHost, string parentHost)
        {
            // ����������
            var hosts = this.GetHosts()
                            .Select(x => x.ToHost());
            var host = sourceHost.ToHost();

            // ��������
            var result = host.GetParent(hosts);

            // ��������
            Assert.AreEqual(parentHost, result.Host);
        }


        private string[] GetHosts()
            => new[] { "unlock.microvirus.md", "microvirus.md", "visitwar.com", "visitwar.de", "fruonline.co.uk", "australia.open.com", "credit.card.us", "card.us" };
    }
}