using System.Windows;
using System;
using System.Windows.Controls;

namespace Tmb.Common.WpfControls
{
    /// <summary>
    /// Defines a scrollable Canvas. 
    /// Minor change over the original source found here:
    ///  http://www.java2s.com/Tutorial/CSharp/0470__Windows-Presentation-Foundation/CreateaScrollableCanvasControl.htm
    /// </summary>
    public class ScrollableCanvas : Canvas
    {
        static ScrollableCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollableCanvas), 
                new FrameworkPropertyMetadata(typeof(ScrollableCanvas)));
        }

        protected override Size MeasureOverride(Size constraint)
        {
            double bottomMost = 0d;
            double rightMost = 0d;

            foreach (object obj in Children)
            {
                FrameworkElement child = obj as FrameworkElement;

                if (child != null)
                {
                    child.Measure(constraint);
                    if (!double.IsNaN(child.DesiredSize.Height) &&
                        !double.IsNaN(child.DesiredSize.Width))
                    {
                        bottomMost = Math.Max(bottomMost, GetTop(child) + child.DesiredSize.Height);
                        rightMost = Math.Max(rightMost, GetLeft(child) + child.DesiredSize.Width);

                        if (double.IsNaN(bottomMost))
                        {
                            bottomMost = 0;
                        }
                        if (double.IsNaN(rightMost))
                        {
                            rightMost = 0;
                        }
                    }
                }
            }
            return new Size(rightMost, bottomMost);
        }
    }

}

