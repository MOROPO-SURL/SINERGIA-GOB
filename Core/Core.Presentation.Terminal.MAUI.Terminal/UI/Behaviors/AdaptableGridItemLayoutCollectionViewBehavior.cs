namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Behaviors;

public sealed class AdaptableGridItemLayoutCollectionViewBehavior : Microsoft.Maui.Controls.Behavior
{
    public double ItemWidth { get; set; }

    public double ItemSpacing { get; set; }

    protected override void OnAttachedTo(Microsoft.Maui.Controls.BindableObject bindable)
    {
        base.OnAttachedTo(bindable);

        if (bindable is not Microsoft.Maui.Controls.CollectionView) return;

        var collectionView = bindable as Microsoft.Maui.Controls.CollectionView;

        if (collectionView.ItemsLayout is not Microsoft.Maui.Controls.GridItemsLayout) return;

        collectionView.SizeChanged += CollectionView_SizeChanged;
    }

    protected override void OnDetachingFrom(Microsoft.Maui.Controls.BindableObject bindable)
    {
        base.OnDetachingFrom(bindable);

        var collectionView = bindable as Microsoft.Maui.Controls.CollectionView;

        if (collectionView.ItemsLayout is not Microsoft.Maui.Controls.GridItemsLayout) return;

        collectionView.SizeChanged -= CollectionView_SizeChanged;
    }

    private void CollectionView_SizeChanged(object sender, System.EventArgs e)
    {
        var collectionView = sender as Microsoft.Maui.Controls.CollectionView;

        var reazon = collectionView.Width / (ItemWidth + ItemSpacing);

        int columnCount = System.Convert.ToInt32(System.Math.Round(reazon, System.MidpointRounding.ToEven));

        if (columnCount > reazon)
        {
            columnCount--;
        }

        if (columnCount <= 0)
        {
            columnCount = 1;
        }

        (collectionView.ItemsLayout as Microsoft.Maui.Controls.GridItemsLayout).Span = columnCount;
    }
}
