﻿#pragma checksum "..\..\..\..\Views\StockView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8E9B84A9F4E4768D53BFFE213AC8AB3DE4A177EE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.PropertyGrid;
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
using System.Windows.Interactivity;
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
using aAppli.ViewModels;


namespace aAppli.Views {
    
    
    /// <summary>
    /// StockView
    /// </summary>
    public partial class StockView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 101 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.BusyIndicator busy;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid myDataGrid;
        
        #line default
        #line hidden
        
        
        #line 270 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.WatermarkTextBox SearchInOther;
        
        #line default
        #line hidden
        
        
        #line 283 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.WatermarkTextBox search;
        
        #line default
        #line hidden
        
        
        #line 300 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnVerifier;
        
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
            System.Uri resourceLocater = new System.Uri("/aAppli;component/views/stockview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\StockView.xaml"
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
            this.busy = ((Microsoft.Windows.Controls.BusyIndicator)(target));
            return;
            case 2:
            this.myDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            
            #line 236 "..\..\..\..\Views\StockView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SearchInOther = ((Microsoft.Windows.Controls.WatermarkTextBox)(target));
            return;
            case 5:
            this.search = ((Microsoft.Windows.Controls.WatermarkTextBox)(target));
            
            #line 284 "..\..\..\..\Views\StockView.xaml"
            this.search.KeyDown += new System.Windows.Input.KeyEventHandler(this.search_KeyDown);
            
            #line default
            #line hidden
            
            #line 285 "..\..\..\..\Views\StockView.xaml"
            this.search.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.WatermarkTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 289 "..\..\..\..\Views\StockView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 295 "..\..\..\..\Views\StockView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnVerifier = ((System.Windows.Controls.Button)(target));
            
            #line 300 "..\..\..\..\Views\StockView.xaml"
            this.btnVerifier.Click += new System.Windows.RoutedEventHandler(this.btnVerifier_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

