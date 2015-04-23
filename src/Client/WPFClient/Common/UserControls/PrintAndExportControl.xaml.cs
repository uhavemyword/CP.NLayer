using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RichTextBoxUI;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.FormatProviders.Pdf;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Csv;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace CP.NLayer.Client.WpfClient.Common
{
    /// <summary>
    /// Interaction logic for PrintAndExportControl.xaml
    /// </summary>
    public partial class PrintAndExportControl : UserControl
    {
        public PrintAndExportControl()
        {
            InitializeComponent();
        }
    }

    public class PrintAndExportControlViewModel : ViewModelBase
    {
        public PrintAndExportControlViewModel()
        {
            this.ExportCommand = new DelegateCommand(Export);
            this.PrintCommand = new DelegateCommand(Print);
            this.PrintPreviewCommand = new DelegateCommand(PrintPreview);
            this.HeaderBackground = (Color)ColorConverter.ConvertFromString("#FFD9D9D9");
            this.RowBackground = Colors.Transparent;
            this.GroupHeaderBackground = (Color)ColorConverter.ConvertFromString("#FFF2F2F2");
            this.SelectedExportFormat = this.ExportFormats.LastOrDefault();
        }

        public ICommand PrintCommand { get; set; }

        public ICommand PrintPreviewCommand { get; set; }

        public ICommand ExportCommand { get; set; }

        private IEnumerable<string> _exportFormats;
        public IEnumerable<string> ExportFormats
        {
            get
            {
                if (_exportFormats == null)
                {
                    _exportFormats = new string[] { "Excel", "Word", "Csv", "Pdf" };
                }

                return _exportFormats;
            }
        }

        private string _selectedExportFormat;
        public string SelectedExportFormat
        {
            get
            {
                return _selectedExportFormat;
            }
            set
            {
                if (!object.Equals(_selectedExportFormat, value))
                {
                    _selectedExportFormat = value;

                    OnPropertyChanged("SelectedExportFormat");
                }
            }
        }

        private Color _groupHeaderBackground;
        public Color GroupHeaderBackground
        {
            get
            {
                return this._groupHeaderBackground;
            }
            set
            {
                if (this._groupHeaderBackground != value)
                {
                    this._groupHeaderBackground = value;
                    OnPropertyChanged("GroupHeaderBackground");
                }
            }
        }

        private Color _headerBackground;
        public Color HeaderBackground
        {
            get
            {
                return this._headerBackground;
            }
            set
            {
                if (this._headerBackground != value)
                {
                    this._headerBackground = value;
                    OnPropertyChanged("HeaderBackground");
                }
            }
        }

        private Color _rowBackground;
        public Color RowBackground
        {
            get
            {
                return this._rowBackground;
            }
            set
            {
                if (this._rowBackground != value)
                {
                    this._rowBackground = value;
                    OnPropertyChanged("RowBackground");
                }
            }
        }

        public void Export(object parameter)
        {
            var grid = parameter as RadGridView;
            if (grid != null)
            {
                grid.Export(new ExportSettings()
                {
                    GroupHeaderBackground = this.GroupHeaderBackground,
                    HeaderBackground = this.HeaderBackground,
                    RowBackground = this.RowBackground,
                    Format = (ExportFormat)Enum.Parse(typeof(ExportFormat), SelectedExportFormat, false)
                });
            }
        }

        public void Print(object parameter)
        {
            var grid = parameter as RadGridView;
            if (grid != null)
            {
                grid.Print(new PrintSettings()
                {
                    GroupHeaderBackground = this.GroupHeaderBackground,
                    HeaderBackground = this.HeaderBackground,
                    RowBackground = this.RowBackground
                });
            }
        }

        public void PrintPreview(object parameter)
        {
            var grid = parameter as RadGridView;
            if (grid != null)
            {
                grid.PrintPreview(new PrintSettings()
                {
                    GroupHeaderBackground = this.GroupHeaderBackground,
                    HeaderBackground = this.HeaderBackground,
                    RowBackground = this.RowBackground
                });
            }
        }
    }

    #region PrintAndExportExtensions

    public enum ExportFormat
    {
        Excel,
        Word,
        Pdf,
        Csv
    }

    public class PrintSettings
    {
        public Color GroupHeaderBackground
        {
            get;
            set;
        }

        public Color HeaderBackground
        {
            get;
            set;
        }

        public Color RowBackground
        {
            get;
            set;
        }
    }

    public class ExportSettings : PrintSettings
    {
        public ExportFormat Format
        {
            get;
            set;
        }
    }

    public static class PrintAndExportExtensions
    {
        public static void Export(this RadGridView grid, ExportSettings settings)
        {
            var dialog = new SaveFileDialog();

            if (settings.Format == ExportFormat.Pdf)
            {
                dialog.DefaultExt = "*.pdf";
                dialog.Filter = "Adobe PDF Document (*.pdf)|*.pdf";
            }
            else if (settings.Format == ExportFormat.Excel)
            {
                dialog.DefaultExt = "*.xlsx";
                dialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
            }
            else if (settings.Format == ExportFormat.Word)
            {
                dialog.DefaultExt = "*.docx";
                dialog.Filter = "Word Document (*.docx)|*.docx";
            }
            else if (settings.Format == ExportFormat.Csv)
            {
                dialog.DefaultExt = "*.csv";
                dialog.Filter = "CSV (Comma delimited) (*.csv)|*.csv";
            }

            if (dialog.ShowDialog() == true)
            {
                if (settings.Format == ExportFormat.Csv)
                {
                    using (var output = dialog.OpenFile())
                    {
                        grid.Export(output, new GridViewExportOptions()
                        {
                            Format = Telerik.Windows.Controls.ExportFormat.Csv,
                            ShowColumnFooters = grid.ShowColumnFooters,
                            ShowColumnHeaders = grid.ShowColumnHeaders,
                            ShowGroupFooters = grid.ShowGroupFooters
                        });
                    }
                }
                else
                {
                    if (settings.Format == ExportFormat.Excel)
                    {
                        var workbook = CreateWorkBook(grid, settings);

                        if (workbook != null)
                        {
                            var provider = new XlsxFormatProvider();
                            using (var output = dialog.OpenFile())
                            {
                                provider.Export(workbook, output);
                            }
                        }
                    }
                    else
                    {
                        var document = CreateDocument(grid, settings);

                        if (document != null)
                        {
                            document.LayoutMode = DocumentLayoutMode.Paged;
                            document.Measure(RadDocument.MAX_DOCUMENT_SIZE);
                            document.Arrange(new RectangleF(PointF.Empty, document.DesiredSize));

                            IDocumentFormatProvider provider = null;

                            if (settings.Format == ExportFormat.Pdf)
                            {
                                provider = new PdfFormatProvider();
                            }
                            else if (settings.Format == ExportFormat.Word)
                            {
                                provider = new DocxFormatProvider();
                            }

                            using (var output = dialog.OpenFile())
                            {
                                provider.Export(document, output);
                            }
                        }
                    }
                }
            }
        }

        public static void Print(this RadGridView grid, PrintSettings settings)
        {
            var rtb = CreateRadRichTextBox(grid, settings);
            var window = new RadWindow() { Height = 0, Width = 0, Opacity = 0, Content = rtb, Owner = WpfHelper.GetActiveWindow() };
            rtb.PrintCompleted += (s, e) => { window.Close(); };
            window.Show();

            rtb.Print("MyDocument", Telerik.Windows.Documents.UI.PrintMode.Native);
        }

        public static void PrintPreview(this RadGridView grid, PrintSettings settings)
        {
            var rtb = CreateRadRichTextBox(grid, settings);
            var window = CreatePreviewWindow(rtb);
            window.ShowDialog();
        }

        private static RadWindow CreatePreviewWindow(RadRichTextBox rtb)
        {
            var printButton = new RadButton()
            {
                Content = "Print",
                Margin = new Thickness(10, 0, 10, 0),
                FontWeight = FontWeights.Bold,
                Width = 80
            };

            printButton.Click += (s, e) =>
            {
                rtb.Print("MyDocument", Telerik.Windows.Documents.UI.PrintMode.Native);
            };

            var sp = new StackPanel() { Height = 26, Orientation = Orientation.Horizontal, Margin = new Thickness(10) };
            sp.Children.Add(new RadRichTextBoxStatusBar() { AssociatedRichTextBox = rtb, Margin = new Thickness(20, 0, 10, 0) });

            sp.Children.Add(new TextBlock() { Text = "Orientation:", Margin = new Thickness(10, 0, 3, 0), VerticalAlignment = VerticalAlignment.Center });

            var radComboBoxPageOrientation = new RadComboBox()
            {
                ItemsSource = new string[] { "Portrait", "Landscape" },
                SelectedIndex = 0
            };
            sp.Children.Add(radComboBoxPageOrientation);

            radComboBoxPageOrientation.SelectionChanged += (s, e) =>
            {
                rtb.ChangeSectionPageOrientation((PageOrientation)Enum.Parse(typeof(PageOrientation),
                    radComboBoxPageOrientation.Items[radComboBoxPageOrientation.SelectedIndex].ToString(), true));
            };

            sp.Children.Add(new TextBlock() { Text = "Size:", Margin = new Thickness(10, 0, 3, 0), VerticalAlignment = VerticalAlignment.Center });

            var radComboBoxPageSize = new RadComboBox()
            {
                ItemsSource = new string[] { "A0", "A1", "A2", "A3", "A4", "A5", "Letter" },
                Height = 25,
                SelectedIndex = 4,
            };
            sp.Children.Add(radComboBoxPageSize);

            radComboBoxPageSize.SelectionChanged += (s, e) =>
            {
                rtb.ChangeSectionPageSize(PaperTypeConverter.ToSize((PaperTypes)Enum.Parse(typeof(PaperTypes),
                    radComboBoxPageSize.Items[radComboBoxPageSize.SelectedIndex].ToString(), true)));
            };

            sp.Children.Add(printButton);

            var g = new Grid();
            g.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            g.RowDefinitions.Add(new RowDefinition());
            g.Children.Add(sp);
            g.Children.Add(rtb);

            Grid.SetRow(rtb, 1);

            return new RadWindow()
            {
                Content = g,
                Width = 900,
                Height = 600,
                Header = "Print Preview",
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = WpfHelper.GetActiveWindow()
            };
        }

        private static RadRichTextBox CreateRadRichTextBox(RadGridView grid, PrintSettings settings)
        {
            return new RadRichTextBox()
            {
                IsReadOnly = true,
                LayoutMode = DocumentLayoutMode.Paged,
                IsSelectionEnabled = false,
                IsSpellCheckingEnabled = false,
                Document = CreateDocument(grid, settings)
            };
        }

        private static RadDocument CreateDocument(RadGridView grid, PrintSettings settings)
        {
            RadDocument document = null;

            using (var stream = new MemoryStream())
            {
                EventHandler<GridViewElementExportingEventArgs> elementExporting = (s, e) =>
                {
                    if (e.Element == ExportElement.Table)
                    {
                        e.Attributes["border"] = "0";
                    }
                    else if (e.Element == ExportElement.HeaderRow)
                    {
                        if (settings.HeaderBackground != null)
                        {
                            e.Styles.Add("background-color", settings.HeaderBackground.ToString().Remove(1, 2));
                        }
                    }
                    else if (e.Element == ExportElement.GroupHeaderRow)
                    {
                        if (settings.GroupHeaderBackground != null)
                        {
                            e.Styles.Add("background-color", settings.GroupHeaderBackground.ToString().Remove(1, 2));
                        }
                    }
                    else if (e.Element == ExportElement.Row)
                    {
                        if (settings.RowBackground != null)
                        {
                            e.Styles.Add("background-color", settings.RowBackground.ToString().Remove(1, 2));
                        }
                    }
                };

                grid.ElementExporting += elementExporting;

                grid.Export(stream, new GridViewExportOptions()
                {
                    Format = Telerik.Windows.Controls.ExportFormat.Html,
                    ShowColumnFooters = grid.ShowColumnFooters,
                    ShowColumnHeaders = grid.ShowColumnHeaders,
                    ShowGroupFooters = grid.ShowGroupFooters
                });

                grid.ElementExporting -= elementExporting;

                stream.Position = 0;

                document = new HtmlFormatProvider().Import(stream);
            }

            return document;
        }

        private static Workbook CreateWorkBook(RadGridView grid, PrintSettings settings)
        {
            Workbook book = null;

            using (var stream = new MemoryStream())
            {
                grid.Export(stream, new GridViewExportOptions()
                {
                    Format = Telerik.Windows.Controls.ExportFormat.Csv,
                    ShowColumnFooters = grid.ShowColumnFooters,
                    ShowColumnHeaders = grid.ShowColumnHeaders,
                    ShowGroupFooters = grid.ShowGroupFooters
                });

                stream.Position = 0;

                book = new CsvFormatProvider().Import(stream);
            }

            return book;
        }
    }

    #endregion
}