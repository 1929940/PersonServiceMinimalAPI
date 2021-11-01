
namespace PersonServiceMinimalAPI.Installer {
    public class Installer {
        private readonly WebApplication _app;

        public Installer(WebApplication app) {
            _app = app;
        }

        public void Install() {
            var installables = GetInstallables();

            foreach (var installable in installables) {
                var instance = Activator.CreateInstance(installable, _app) as InstallableBase;
                instance.Install();
            }
        }

        private Type[] GetInstallables() {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(InstallableBase).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToArray();
        } 


    }
}
