﻿#pragma checksum "..\..\..\..\Pages\PagesFuncs\giveaway.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3A02BB6E40B8AF2D49AD2EF7D3DE3612E8BC213D90F1B7229BC7F190CE513E2C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using RetopBot.Pages.PagesFuncs;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace RetopBot.Pages.PagesFuncs {
    
    
    /// <summary>
    /// giveaway
    /// </summary>
    public partial class giveaway : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox slovotxt;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border btnlol;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid STARTroul;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel parrent;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox timercb;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label timerlb;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label countmembers;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border btnlol_Copy;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid WinGrid;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label winnerlb;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RetopBot;component/pages/pagesfuncs/giveaway.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.slovotxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.btnlol = ((System.Windows.Controls.Border)(target));
            
            #line 35 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
            this.btnlol.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.clickstartroulet);
            
            #line default
            #line hidden
            return;
            case 3:
            this.STARTroul = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.parrent = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.timercb = ((System.Windows.Controls.ComboBox)(target));
            
            #line 75 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
            this.timercb.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.selectedtime);
            
            #line default
            #line hidden
            return;
            case 6:
            this.timerlb = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.countmembers = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.btnlol_Copy = ((System.Windows.Controls.Border)(target));
            
            #line 103 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
            this.btnlol_Copy.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.clearreulst);
            
            #line default
            #line hidden
            return;
            case 9:
            this.WinGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            this.winnerlb = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            
            #line 137 "..\..\..\..\Pages\PagesFuncs\giveaway.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

