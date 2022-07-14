using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Neumorphism.Avalonia.Styles;
using Neumorphism.Avalonia.Styles.Assists;

namespace Pwa.Client.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            #region Control getter and event binding
            NavDrawerSwitch = this.Get<ToggleButton>(nameof(NavDrawerSwitch));

            DrawerList = this.Get<ListBox>(nameof(DrawerList));
            DrawerList.PointerReleased += DrawerSelectionChanged;
            DrawerList.KeyUp += DrawerList_KeyUp;

            mainScroller = this.Get<ScrollViewer>(nameof(mainScroller));
            #endregion
        }
        
        private void DrawerList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
                DrawerSelectionChanged(sender, null);
        }

        public void DrawerSelectionChanged(object sender, RoutedEventArgs args)
        {
            NavDrawerSwitch.IsChecked = false;
        }

        private void TemplatedControl_OnTemplateApplied(object sender, TemplateAppliedEventArgs e)
        {
            SnackbarHost.Post("Welcome to\r\nNeumorphism.Avalonia demo !");
        }

        private void HelloButtonMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            SnackbarHost.Post("Hello user !");
        }

        private void GoodbyeButtonMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown(0);
            }
        }

        public void SwitchUITheme(object sender)
        {
            var toggleButton = sender as ToggleButton;
            if (toggleButton != null)
            {
            }

            DirtyControlsRedrawFix();
        }

        /// <summary>
        /// Dirty hack to force redraw box shadow when theme is changed !!!!
        /// </summary>
        private void DirtyControlsRedrawFix()
        {
            var controls = this.GetVisualDescendants().OfType<InputElement>();
            foreach (InputElement ctrl in controls)
            {
                if (ctrl != null)
                {
                    if (ctrl is CheckBox || ctrl is RadioButton)
                    {
                        double h = ctrl.GetValue<double>(SelectionControlAssist.SizeProperty);
                        ctrl.SetValue(SelectionControlAssist.SizeProperty, h - 1);
                        ctrl.SetValue(SelectionControlAssist.SizeProperty, h + 1);
                    }
                    
                    if (ctrl.Height > 0)
                    {
                        ctrl.Height = ctrl.Height - 1;
                        ctrl.Height = ctrl.Height + 1;
                    }
                    else if (ctrl.Height is double.NaN)
                    {
                        ctrl.Height = 0;
                        ctrl.Height = double.NaN;
                    }
                }
            }
        }
    }
}