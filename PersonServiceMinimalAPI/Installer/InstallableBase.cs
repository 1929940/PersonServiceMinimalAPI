namespace PersonServiceMinimalAPI.Installer {
    public abstract class InstallableBase {
        protected readonly WebApplication _app;

        public InstallableBase(WebApplication app) {
            _app = app;
        }

        public abstract void Install(); 
    }
}
