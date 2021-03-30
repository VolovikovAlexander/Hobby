using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Castle.Core;
using Castle.Facilities.AspNetCore;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;

namespace ControlPoint
{
    using ControlPoint.Core;


    /// <summary>
    /// Вспомогательный класс для работы с контейнером
    /// </summary>
    public static class WindsorContainerHelper
    {
        private static readonly WindsorContainer _container;

        static WindsorContainerHelper()
        {
            _container = new WindsorContainer();

            // Регистрация всех классов у которых есть аттрибут 
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel));
            _container.Register(
                Classes.FromAssembly(typeof(ServiceAttribute).Assembly)
                    .Where(t => Attribute.IsDefined(t, typeof(ServiceAttribute)))
                    .WithService.FirstInterface().LifestyleTransient()
                );
        }

        /// <summary>
        /// Получить сборку из контейнера
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Получить ссылку на контейнер
        /// </summary>
        public static WindsorContainer Container { get => _container; }
    }
}
