    // In the XAML code, add the following lines:
    /*
    <UserControl.Resources>
        <vm:ListEnumDescriptionConverter x:Key="ListEnumDescriptionConverter" />
        <vm:MarkerEnumDescriptionConverter x:Key="MarkerEnumDescriptionConverter" />
    </UserControl.Resources>
    */
    public class ListEnumDescriptionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> list = new List<string>();
            var valueList = (value as IEnumerable<MarkerModel>);//.Cast<MarkerModel>().ToList();

            if (value != null)
            {
                if (valueList.Count() > 0)
                {
                    foreach (var item in valueList)
                    {
                        Enum myEnum = (Enum)item;
                        string description = myEnum.GetDisplayAttribute();
                        //string description = myEnum.GetDescription();
                        list.Add(description);
                    }
                }
            }
            //Enum myEnum = (Enum)value;
            //string description = myEnum.GetDisplayAttribute();
            //string description = myEnum.GetDescription();
            return list;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MarkerEnumDescriptionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Enum myEnum = (Enum)value;
            string description = myEnum.GetDisplayAttribute();
            //string description = myEnum.GetDescription();
            return description;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return source.GetType().GetMember(source.ToString())
            //             .FirstOrDefault()?
            //             .GetCustomAttribute<DisplayAttribute>()
            //             .GetDescription() ?? "unknown";
            object ret = MarkerModel.WAYPOINT_ANCHOR_2_CT;
            if(value == null) { return ret; }

            FieldInfo[] fi = targetType.GetFields();
            for(int i =0; i < fi.Length; i++)
            {
                DisplayAttribute[] attr = (DisplayAttribute[])fi[i].GetCustomAttributes(
                    typeof(DisplayAttribute), false);
                if(attr != null && attr.Length > 0)
                {
                    if(value.Equals(attr[0].GetDescription()))
                    {
                        ret = targetType.GetField(fi[i].Name);
                        break;
                    }
                }
            }
            return ret;
            //throw new NotImplementedException();
        }
    }
