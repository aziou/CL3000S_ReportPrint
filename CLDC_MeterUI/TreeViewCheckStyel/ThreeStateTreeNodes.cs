using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.TreeViewCheckStyel
{

    public class ThreeStateTreeNode : TreeNode
    {
        #region Constructors

        public ThreeStateTreeNode()
            : base()
        {
            this.CommonConstructor();
        }

        public ThreeStateTreeNode(string text)
            : base(text)
        {
            this.CommonConstructor();
        }

        public ThreeStateTreeNode(string text, ThreeStateTreeNode[] children)
            : base(text, children)
        {
            this.CommonConstructor();
        }

        public ThreeStateTreeNode(string text, int imageIndex, int selectedImageIndex)
            : base(text, imageIndex, selectedImageIndex)
        {
            this.CommonConstructor();
        }

        public ThreeStateTreeNode(string text, int imageIndex, int selectedImageIndex, ThreeStateTreeNode[] children)
            : base(text, imageIndex, selectedImageIndex, children)
        {
            this.CommonConstructor();
        }
        #endregion

        #region Initialization

        private void CommonConstructor()
        {
        }
        #endregion

        #region Properties

        private Enumerations.CheckBoxState mState = Enumerations.CheckBoxState.Unchecked;
        [Category("Ê÷×´ÁÐ±íÑ¡Ôñ¿ò"),
        Description("The current state of the node's checkbox, Unchecked, Checked, or Indeterminate"),
        DefaultValue(Enumerations.CheckBoxState.Unchecked),
        TypeConverter(typeof(Enumerations.CheckBoxState)),
        Editor("Ascentium.Research.Windows.Components.CheckBoxState", typeof(Enumerations.CheckBoxState))]
        public Enumerations.CheckBoxState State
        {
            get { return this.mState; }
            set
            {
                if (this.mState != value)
                {
                    this.mState = value;

                    if ((this.TreeView != null) && (this.TreeView.CheckBoxes))
                        this.Checked = (this.mState == Enumerations.CheckBoxState.Checked);

                }
            }
        }

        private Enumerations.CheckBoxState SiblingsState
        {
            get
            {
                if ((this.Parent == null) || (this.Parent.Nodes.Count == 1))
                    return this.State;
                Enumerations.CheckBoxState state = 0;
                foreach (TreeNode node in this.Parent.Nodes)
                {
                    ThreeStateTreeNode child = node as ThreeStateTreeNode;
                    if (child != null)
                        state |= child.State;
                    if (state == Enumerations.CheckBoxState.Indeterminate)
                        break;
                }

                return (state == 0) ? Enumerations.CheckBoxState.Unchecked : state;
            }
        }
        #endregion

        #region Methods

        public void Toggle(Enumerations.CheckBoxState fromState)
        {
            switch (fromState)
            {
                case Enumerations.CheckBoxState.Unchecked:
                    {
                        this.State = Enumerations.CheckBoxState.Checked;
                        break;
                    }
                case Enumerations.CheckBoxState.Checked:
                case Enumerations.CheckBoxState.Indeterminate:
                default:
                    {
                        this.State = Enumerations.CheckBoxState.Unchecked;
                        break;
                    }
            }

            this.UpdateStateOfRelatedNodes();
        }

        public new void Toggle()
        {
            this.Toggle(this.State);
        }

        public void UpdateStateOfRelatedNodes()
        {
            ThreeStateTreeView tv = this.TreeView as ThreeStateTreeView;
            if ((tv != null) && tv.CheckBoxes && tv.UseThreeStateCheckBoxes)
            {
                tv.BeginUpdate();

                if (this.State != Enumerations.CheckBoxState.Indeterminate)
                    this.UpdateChildNodeState();

                this.UpdateParentNodeState(true);

                tv.EndUpdate();
            }
        }

        private void UpdateChildNodeState()
        {
            ThreeStateTreeNode child;
            foreach (TreeNode node in this.Nodes)
            {
                if (node is ThreeStateTreeNode)
                {
                    child = node as ThreeStateTreeNode;
                    child.State = this.State;
                    child.Checked = (this.State != Enumerations.CheckBoxState.Unchecked);
                    child.UpdateChildNodeState();
                }
            }
        }

        private void UpdateParentNodeState(bool isStartingPoint)
        {

            ThreeStateTreeNode parent = this.Parent as ThreeStateTreeNode;
            if (parent != null)
            {
                Enumerations.CheckBoxState state = Enumerations.CheckBoxState.Unchecked;

                if (!isStartingPoint && (this.State == Enumerations.CheckBoxState.Indeterminate))
                    state = Enumerations.CheckBoxState.Indeterminate;
                else
                    state = this.SiblingsState;
                if (parent.State != state)
                {
                    parent.State = state;
                    parent.Checked = (state != Enumerations.CheckBoxState.Unchecked);
                    parent.UpdateParentNodeState(false);
                }
            }
        }
        #endregion
    }
}