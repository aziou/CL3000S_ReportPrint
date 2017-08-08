using System;

namespace CLDC_MeterUI.TreeViewCheckStyel.Enumerations
{
    [FlagsAttribute]
    public enum CheckBoxState
    {
        Unchecked = 1,
        Checked = 2,
        Indeterminate = CheckBoxState.Unchecked | CheckBoxState.Checked
    }
}
