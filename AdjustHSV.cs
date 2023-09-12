// Name: Adjust HSV
// Submenu:
// Author: DataMine
// Title: Adjust HSV
// Version:
// Desc: Allows adjusting the HSV of selected pixels
// Keywords:
// URL:
// Help:
#region UICode
TextboxControl HueAmount = ""; // [360] Slider 1 Description
TextboxControl SaturationAmount = ""; // [100] Slider 2 Description
TextboxControl ValueAmount = ""; // [100] Slider 3 Description
CheckboxControl SubtractHue = false; // Subtract Hue
CheckboxControl SubtractSaturation = false; // Subtract Saturation
CheckboxControl SubtractValue = false; // Subtract Value
#endregion

void Render(Surface dst, Surface src, Rectangle rect)
{
    ColorBgra currentPixel;
    
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;

        for (int x = rect.Left; x < rect.Right; x++)
        {
            currentPixel = src[x,y];

            int hueInput = string.IsNullOrEmpty(HueAmount) ? 0 : Convert.ToInt32(HueAmount);
            int satInput = string.IsNullOrEmpty(SaturationAmount) ? 0 : Convert.ToInt32(SaturationAmount);
            int valInput = string.IsNullOrEmpty(ValueAmount) ? 0 : Convert.ToInt32(ValueAmount);

            HsvColor hsv = HsvColor.FromColor(currentPixel.ToColor());
            int newH = SubtractHue ? hsv.Hue - hueInput : hsv.Hue + hueInput;
            int newS = SubtractSaturation ? hsv.Saturation - satInput : hsv.Saturation + satInput;
            int newV = SubtractValue ? hsv.Value - valInput : hsv.Value + valInput;
            byte A = currentPixel.A;

            if(newH < 0) {
                newH = 0;
            }

            if(newS < 0) {
                newS = 0;
            }

            if(newV < 0) {
                newV = 0;
            }

            currentPixel = ColorBgra.FromColor(new HsvColor(newH, newS, newV).ToColor());
            currentPixel.A = A;

            dst[x,y] = currentPixel;
        }
    }
}