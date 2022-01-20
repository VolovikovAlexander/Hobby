using NUnit.Framework;

namespace Forbidden_Tests
{
    using Forbidden_Hosts;
    using System.Linq;

    public class BaseTests
    {
      
        /// <summary>
        /// Проверить формирование структуры <see cref="HostItem"/>
        /// </summary>
        [Test]
        public void Check_Extensions_Creating_HostItems()
        {
            // Подготовка
            var hosts = this.GetHosts();

            // Действия
            var result = hosts.ToHosts();

            // Проверки
            Assert.AreEqual(hosts.Count(), result.Count());
            var dictory = result.ToDictionary(x => x.UniqueCode, x => x.Host);
            Assert.AreEqual(hosts.Count(), dictory.Keys.Count());
        }

        /// <summary>
        /// Проверить поиск владельца хоста
        /// </summary>
        [Test]
        [TestCase("unlock.microvirus.md", "microvirus.md")]
        [TestCase("credit.card.us", "card.us")]
        public void Check_Extensions_GetParent(string sourceHost, string parentHost)
        {
            // Подготовка
            var hosts = this.GetHosts()
                            .Select(x => x.ToHost());
            var host = sourceHost.ToHost();

            // Действия
            var result = host.GetParent(hosts);

            // Проверки
            Assert.AreEqual(parentHost, result.Host);
        }


        private string[] GetHosts()
            => new[] { "unlock.microvirus.md", "microvirus.md", "visitwar.com", "visitwar.de", "fruonline.co.uk", "australia.open.com", "credit.card.us", "card.us" };
    }
}