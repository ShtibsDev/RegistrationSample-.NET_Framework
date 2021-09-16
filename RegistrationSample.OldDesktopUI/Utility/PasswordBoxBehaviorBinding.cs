using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;

namespace RegistrationSample.OldDesktopUI.Utility
{
    internal class PasswordBoxBehaviorBinding : Behavior<PasswordBox>
    {
        public SecureString SPassword
        {
            get => (SecureString)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty
            = DependencyProperty.Register(
                "SPassword",
                typeof(SecureString),
                typeof(PasswordBoxBehaviorBinding),
               new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //var binding = BindingOperations.GetBindingExpression(this, PasswordProperty);
            //if (binding != null)
            //{
            //    if (binding.ResolvedSource != null)
            //    {
            //        var property = binding.ResolvedSource.GetType()
            //            .GetProperty(binding.ParentBinding.Path.Path);

            //        if (property != null)
            //        {
            //            property.SetValue(binding.ResolvedSource, AssociatedObject.SecurePassword);
            //        }
            //    }
            //}
            SPassword = AssociatedObject.SecurePassword;
            // use debugger to verify, that the validation errors exist. Otherwise, no need for the following line of code
            var behaviorErrors = Validation.GetErrors(this);
        }
    }
}
