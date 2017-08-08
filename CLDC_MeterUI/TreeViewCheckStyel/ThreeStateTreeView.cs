using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.TreeViewCheckStyel
{

    public partial class ThreeStateTreeView : TreeView
    {
        #region Constructors

        public ThreeStateTreeView()
            : base()
        {
        }
        #endregion

        #region Public Properties
 
        private bool mUseThreeStateCheckBoxes = true;
        [Category("Ê÷×´ÁÐ±íÑ¡Ôñ¿ò"),
        Description("."),
        DefaultValue(true),
        TypeConverter(typeof(bool)),
        Editor("System.Boolean", typeof(bool))]
        public bool UseThreeStateCheckBoxes
        {
            get { return this.mUseThreeStateCheckBoxes; }
            set { this.mUseThreeStateCheckBoxes = value; }
        }
        #endregion

        #region Overrides
        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            base.OnAfterCheck(e);

            if (this.UseThreeStateCheckBoxes)
            {
                switch (e.Action)
                {
                    case TreeViewAction.ByKeyboard:
                    case TreeViewAction.ByMouse:
                        {
                            if (e.Node is ThreeStateTreeNode)
                            {
                                ThreeStateTreeNode tn = e.Node as ThreeStateTreeNode;
                                tn.Toggle();
                            }

                            break;
                        }
                    case TreeViewAction.Collapse:
                    case TreeViewAction.Expand:
                    case TreeViewAction.Unknown:
                    default:
                        {
                            break;
                        }
                }
            }
        }
        #endregion
    }
}

