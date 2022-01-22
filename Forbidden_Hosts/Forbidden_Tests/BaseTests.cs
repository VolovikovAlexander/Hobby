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
            var result = hosts.ToHosts().ToList();

            // ��������
            Assert.AreEqual(hosts.Count(), result.Count());
            var dictory = result.ToDictionary(x => x.UniqueCode, x => x.Host);
            Assert.AreEqual(hosts.Count(), dictory.Keys.Count());
        }

        /// <summary>
        /// ��������� ����� ��������� �����
        /// </summary>
        [Test]
        [TestCase("unlock.unlock.microvirus.md", "microvirus.md")]
        [TestCase("unlock.microvirus.md", "microvirus.md")]
        [TestCase("credit.card.us", "card.us")]
        public void Check_Extensions_GetParent(string sourceHost, string parentHost)
        {
            // ����������
            var hosts = this.GetHosts()
                            .Select(x => x.ToHost())
                            .ToList();
            var host = hosts.Where(x => x.Host == sourceHost).FirstOrDefault();
            if (host is null)
                Assert.Pass("������������ ������� ��� ������� ���������!");

            // ��������
            var result = host.GetParent(hosts);

            // ��������
            Assert.AreEqual(parentHost, result.Host);
        }


        private string[] GetHosts()
            => new[] { "unlock.unlock.microvirus.ru", "unlock.microvirus.ru", "unlock1.microvirus.md","unlock.microvirus.md", "microvirus.md", "visitwar.com", "visitwar.de", "fruonline.co.uk", "australia.open.com", "credit.card.us", "card.us" };
    }
}