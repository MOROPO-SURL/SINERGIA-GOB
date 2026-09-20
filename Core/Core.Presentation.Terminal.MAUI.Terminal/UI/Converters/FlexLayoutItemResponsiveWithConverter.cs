namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Converters
{
    public class FlexLayoutItemResponsiveWithConverter : Microsoft.Maui.Controls.IValueConverter
    {
        public double WrapSpacing { get; set; }

        public double SingleSpacing { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not double) return -1;

            var containerWidth = (double)value;

            if (containerWidth == -1) return -1;

            int column = 0;

            if (parameter is not Models.ResponsiveColumnConfigurationModel || parameter == null)
            {
                column = 12;
            }
            else
            {
                var columnConfiguration = (Models.ResponsiveColumnConfigurationModel)parameter;

                if (containerWidth >= 1200)
                {
                    if (columnConfiguration.UltraLargeContainerColumn != null) column = columnConfiguration.UltraLargeContainerColumn.Value;

                    if (column == 0 && columnConfiguration.ExtraLargeContainerColumn != null) column = columnConfiguration.ExtraLargeContainerColumn.Value;

                    if (column == 0 && columnConfiguration.LargeContainerColumn != null) column = columnConfiguration.LargeContainerColumn.Value;

                    if (column == 0 && columnConfiguration.MediumContainerColumn != null) column = columnConfiguration.MediumContainerColumn.Value;

                    if (column == 0 && columnConfiguration.SmallContainerColumn != null) column = columnConfiguration.SmallContainerColumn.Value;
                }
                else if (containerWidth >= 992)
                {
                    if (columnConfiguration.ExtraLargeContainerColumn != null) column = columnConfiguration.ExtraLargeContainerColumn.Value;

                    if (column == 0 && columnConfiguration.LargeContainerColumn != null) column = columnConfiguration.LargeContainerColumn.Value;

                    if (column == 0 && columnConfiguration.MediumContainerColumn != null) column = columnConfiguration.MediumContainerColumn.Value;

                    if (column == 0 && columnConfiguration.SmallContainerColumn != null) column = columnConfiguration.SmallContainerColumn.Value;
                }
                else if (containerWidth >= 768)
                {
                    if (columnConfiguration.LargeContainerColumn != null) column = columnConfiguration.LargeContainerColumn.Value;

                    if (column == 0 && columnConfiguration.MediumContainerColumn != null) column = columnConfiguration.MediumContainerColumn.Value;

                    if (column == 0 && columnConfiguration.SmallContainerColumn != null) column = columnConfiguration.SmallContainerColumn.Value;
                }
                else if (containerWidth >= 576)
                {
                    if (columnConfiguration.MediumContainerColumn != null) column = columnConfiguration.MediumContainerColumn.Value;

                    if (column == 0 && columnConfiguration.SmallContainerColumn != null) column = columnConfiguration.SmallContainerColumn.Value;
                }
                else if (containerWidth >= 420)
                {
                    if (columnConfiguration.SmallContainerColumn != null) column = columnConfiguration.SmallContainerColumn.Value;
                }

                if (column == 0) column = 12;
            }

            var factor = column / 12d;

            //if (factor == 1)
            //{
            //    return containerWidth * factor - Spacing * 2;
            //}

            //System.Diagnostics.Debug.WriteLine($"------------------------------------------");
            //System.Diagnostics.Debug.WriteLine($"????? ContainerWidth: {containerWidth}");
            //System.Diagnostics.Debug.WriteLine($"????? Factor: {factor}");
            //System.Diagnostics.Debug.WriteLine($"????? WrapSpacing: {WrapSpacing}");
            //System.Diagnostics.Debug.WriteLine($"????? SingleSpacing: {SingleSpacing}");
            //System.Diagnostics.Debug.WriteLine($"????? Result: {containerWidth * factor - WrapSpacing - SingleSpacing * 2}");

            return containerWidth * factor - WrapSpacing - SingleSpacing * 2d;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
