﻿#pragma checksum "..\..\..\..\Views\StockView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8E11EC52897ADF4665C921F83BAD93937E1C9B2B"
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
        
        
        #line 84 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.BusyIndicator busy;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid myDataGrid;
        
        #line default
        #line hidden
        
        
        #line 235 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.WatermarkTextBox SearchInOther;
        
        #line default
        #line hidden
        
        
        #line 258 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbFamilies;
        
        #line default
        #line hidden
        
        
        #line 262 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SearchByFamily;
        
        #line default
        #line hidden
        
        
        #line 271 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbCategories;
        
        #line default
        #line hidden
        
        
        #line 275 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SearchByCategory;
        
        #line default
        #line hidden
        
        
        #line 284 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSubCategories;
        
        #line default
        #line hidden
        
        
        #line 288 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SearchBySubCategory;
        
        #line default
        #line hidden
        
        
        #line 297 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbBrands;
        
        #line default
        #line hidden
        
        
        #line 301 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SearchByBrand;
        
        #line default
        #line hidden
        
        
        #line 310 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbModels;
        
        #line default
        #line hidden
        
        
        #line 314 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SearchByModel;
        
        #line default
        #line hidden
        
        
        #line 323 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSizes;
        
        #line default
        #line hidden
        
        
        #line 327 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SearchBySize;
        
        #line default
        #line hidden
        
        
        #line 336 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSuppliers;
        
        #line default
        #line hidden
        
        
        #line 340 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SearchBySupplier;
        
        #line default
        #line hidden
        
        
        #line 354 "..\..\..\..\Views\StockView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.WatermarkTextBox search;
        
        #line default
        #line hidden
        
        
        #line 371 "..\..\..\..\Views\StockView.xaml"
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
            
            #line 201 "..\..\..\..\Views\StockView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SearchInOther = ((Microsoft.Windows.Controls.WatermarkTextBox)(target));
            return;
            case 5:
            this.cbFamilies = ((System.Windows.Controls.ComboBox)(target));
            
            #line 260 "..\..\..\..\Views\StockView.xaml"
            this.cbFamilies.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbFamilies_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SearchByFamily = ((System.Windows.Controls.CheckBox)(target));
            
            #line 263 "..\..\..\..\Views\StockView.xaml"
            this.SearchByFamily.Checked += new System.Windows.RoutedEventHandler(this.SearchByFamily_Checked);
            
            #line default
            #line hidden
            
            #line 264 "..\..\..\..\Views\StockView.xaml"
            this.SearchByFamily.Unchecked += new System.Windows.RoutedEventHandler(this.SearchByFamily_Checked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cbCategories = ((System.Windows.Controls.ComboBox)(target));
            
            #line 273 "..\..\..\..\Views\StockView.xaml"
            this.cbCategories.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbCategories_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.SearchByCategory = ((System.Windows.Controls.CheckBox)(target));
            
            #line 276 "..\..\..\..\Views\StockView.xaml"
            this.SearchByCategory.Checked += new System.Windows.RoutedEventHandler(this.SearchByCategory_Checked);
            
            #line default
            #line hidden
            
            #line 277 "..\..\..\..\Views\StockView.xaml"
            this.SearchByCategory.Unchecked += new System.Windows.RoutedEventHandler(this.SearchByCategory_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cbSubCategories = ((System.Windows.Controls.ComboBox)(target));
            
            #line 286 "..\..\..\..\Views\StockView.xaml"
            this.cbSubCategories.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbSubCategories_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.SearchBySubCategory = ((System.Windows.Controls.CheckBox)(target));
            
            #line 289 "..\..\..\..\Views\StockView.xaml"
            this.SearchBySubCategory.Checked += new System.Windows.RoutedEventHandler(this.SearchBySubCategory_Checked);
            
            #line default
            #line hidden
            
            #line 290 "..\..\..\..\Views\StockView.xaml"
            this.SearchBySubCategory.Unchecked += new System.Windows.RoutedEventHandler(this.SearchBySubCategory_Checked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.cbBrands = ((System.Windows.Controls.ComboBox)(target));
            
            #line 299 "..\..\..\..\Views\StockView.xaml"
            this.cbBrands.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbBrands_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.SearchByBrand = ((System.Windows.Controls.CheckBox)(target));
            
            #line 302 "..\..\..\..\Views\StockView.xaml"
            this.SearchByBrand.Checked += new System.Windows.RoutedEventHandler(this.SearchByBrand_Checked);
            
            #line default
            #line hidden
            
            #line 303 "..\..\..\..\Views\StockView.xaml"
            this.SearchByBrand.Unchecked += new System.Windows.RoutedEventHandler(this.SearchByBrand_Checked);
            
            #line default
            #line hidden
            return;
            case 13:
            this.cbModels = ((System.Windows.Controls.ComboBox)(target));
            
            #line 312 "..\..\..\..\Views\StockView.xaml"
            this.cbModels.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbModels_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.SearchByModel = ((System.Windows.Controls.CheckBox)(target));
            
            #line 315 "..\..\..\..\Views\StockView.xaml"
            this.SearchByModel.Checked += new System.Windows.RoutedEventHandler(this.SearchByModel_Checked);
            
            #line default
            #line hidden
            
            #line 316 "..\..\..\..\Views\StockView.xaml"
            this.SearchByModel.Unchecked += new System.Windows.RoutedEventHandler(this.SearchByModel_Checked);
            
            #line default
            #line hidden
            return;
            case 15:
            this.cbSizes = ((System.Windows.Controls.ComboBox)(target));
            
            #line 325 "..\..\..\..\Views\StockView.xaml"
            this.cbSizes.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbSizes_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 16:
            this.SearchBySize = ((System.Windows.Controls.CheckBox)(target));
            
            #line 328 "..\..\..\..\Views\StockView.xaml"
            this.SearchBySize.Checked += new System.Windows.RoutedEventHandler(this.SearchBySize_Checked);
            
            #line default
            #line hidden
            
            #line 329 "..\..\..\..\Views\StockView.xaml"
            this.SearchBySize.Unchecked += new System.Windows.RoutedEventHandler(this.SearchBySize_Checked);
            
            #line default
            #line hidden
            return;
            case 17:
            this.cbSuppliers = ((System.Windows.Controls.ComboBox)(target));
            
            #line 338 "..\..\..\..\Views\StockView.xaml"
            this.cbSuppliers.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbSuppliers_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 18:
            this.SearchBySupplier = ((System.Windows.Controls.CheckBox)(target));
            
            #line 341 "..\..\..\..\Views\StockView.xaml"
            this.SearchBySupplier.Checked += new System.Windows.RoutedEventHandler(this.SearchBySupplier_Checked);
            
            #line default
            #line hidden
            
            #line 342 "..\..\..\..\Views\StockView.xaml"
            this.SearchBySupplier.Unchecked += new System.Windows.RoutedEventHandler(this.SearchBySupplier_Checked);
            
            #line default
            #line hidden
            return;
            case 19:
            this.search = ((Microsoft.Windows.Controls.WatermarkTextBox)(target));
            
            #line 355 "..\..\..\..\Views\StockView.xaml"
            this.search.KeyDown += new System.Windows.Input.KeyEventHandler(this.search_KeyDown);
            
            #line default
            #line hidden
            
            #line 356 "..\..\..\..\Views\StockView.xaml"
            this.search.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.WatermarkTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 360 "..\..\..\..\Views\StockView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 366 "..\..\..\..\Views\StockView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 22:
            this.btnVerifier = ((System.Windows.Controls.Button)(target));
            
            #line 371 "..\..\..\..\Views\StockView.xaml"
            this.btnVerifier.Click += new System.Windows.RoutedEventHandler(this.btnVerifier_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

