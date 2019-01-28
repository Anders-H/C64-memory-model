using System.Windows.Forms;

namespace Sprdef
{
    public class Configuration
    {
        private static InputMethod _inputMethod;

        static Configuration()
        {
            if (Application.UserAppDataRegistry == null)
                return;
            _inputMethod = (InputMethod)Application.UserAppDataRegistry.GetValue(nameof(InputMethod), InputMethod.MouseInputMethod);
        }

        public static InputMethod InputMethod
        {
            get => _inputMethod;
            set
            {
                if (value != _inputMethod && Application.UserAppDataRegistry != null)
                    Application.UserAppDataRegistry.SetValue(nameof(InputMethod), (int)value);
                _inputMethod = value;
            }
        }
    }
}