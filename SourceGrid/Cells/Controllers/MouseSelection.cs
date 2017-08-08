using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SourceGrid.Cells.Controllers
{
	/// <summary>
	/// A cell controller used to handle mouse selection
	/// </summary>
	public class MouseSelection : ControllerBase
	{
		private static log4net.ILog m_log = log4net.LogManager.GetLogger(typeof(MouseSelection));
		public static MouseSelection Default = new MouseSelection();

		public override void OnMouseDown(CellContext sender, System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(sender, e);

			if (e.Button != MouseButtons.Left)
			{
				m_log.DebugFormat("not processing OnMouseDown event, since button left was not pressed");
				return;
			}
			m_log.DebugFormat("processing OnMouseDown event, button left was pressed");
			
			GridVirtual grid = sender.Grid;

			//Check the control and shift key status
			bool controlPress = ((Control.ModifierKeys & Keys.Control) == Keys.Control &&
			                     (grid.SpecialKeys & GridSpecialKeys.Control) == GridSpecialKeys.Control);

			bool shiftPress = ((Control.ModifierKeys & Keys.Shift) == Keys.Shift &&
			                   (grid.SpecialKeys & GridSpecialKeys.Shift) == GridSpecialKeys.Shift);

			//Default click handler
			if (shiftPress == false ||
			    grid.Selection.EnableMultiSelection == false)
			{
				//Handle Control key
				bool mantainSelection = grid.Selection.EnableMultiSelection && controlPress;

				//If the cell is already selected and the user has the ctrl key pressed then deselect the cell
				if (controlPress && grid.Selection.IsSelectedCell(sender.Position) && grid.Selection.ActivePosition != sender.Position)
					grid.Selection.SelectCell(sender.Position, false);
				else
					grid.Selection.Focus(sender.Position, !mantainSelection);
			}
			else //handle shift key
			{
				grid.Selection.ResetSelection(true);

				Range rangeToSelect = new Range(grid.Selection.ActivePosition, sender.Position);
				grid.Selection.SelectRange(rangeToSelect, true);
			}

			BeginScrollTracking(grid);
		}

		public override void OnMouseUp(CellContext sender, MouseEventArgs e)
		{
			base.OnMouseUp(sender, e);

			if (e.Button != MouseButtons.Left)
			{
				m_log.DebugFormat("not processing OnMouseUp event, since button left was not pressed");
				return;
			}
			m_log.DebugFormat("processing OnMouseUp event, button left was pressed");
			
			sender.Grid.MouseSelectionFinish();

			EndScrollTracking(sender.Grid);
		}

		/// <summary>
		/// Used for mouse multi selection and mouse scrolling
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnMouseMove(CellContext sender, MouseEventArgs e)
		{
			base.OnMouseMove(sender, e);

			//First check if the multi selection is enabled and the active position is valid
			if (sender.Grid.Selection.EnableMultiSelection == false ||
			    sender.Grid.MouseDownPosition.IsEmpty() ||
			    sender.Grid.MouseDownPosition != sender.Grid.Selection.ActivePosition)
				return;

			//Check if the mouse position is valid
			Position mousePosition = sender.Grid.PositionAtPoint(new Point(e.X, e.Y));
			if (mousePosition.IsEmpty())
				return;

			//If the position type is different I don't continue
			// bacause this can cause problem for example when selection the fixed rows when the scroll is on a position > 0
			// that cause all the rows to be selected
			if (sender.Grid.GetPositionType(mousePosition) !=
			    sender.Grid.GetPositionType(sender.Grid.Selection.ActivePosition))
				return;

			sender.Grid.ChangeMouseSelectionCorner(mousePosition);
		}

		/// <summary>
		/// Ends scroll tracking on double click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnDoubleClick(CellContext sender, EventArgs e)
		{
			base.OnDoubleClick(sender, e);
			
			m_log.DebugFormat("processing OnDoubleClick event");
			
			EndScrollTracking(sender.Grid);
		}
		
		private Timer mScrollTimer;
		private GridVirtual mCapturedGrid;

		/// <summary>
		/// Start the timer to scroll the visible area
		/// </summary>
		/// <param name="grid"></param>
		private void BeginScrollTracking(GridVirtual grid)
		{
			//grid.Capture = true;
			mCapturedGrid = grid;

			if (mScrollTimer == null)
			{
				mScrollTimer = new Timer();
				mScrollTimer.Interval = 100;
				mScrollTimer.Tick += this.mScrollTimer_Tick;
			}
			mScrollTimer.Enabled = true;
		}
		/// <summary>
		/// Stop the timer
		/// </summary>
		/// <param name="grid"></param>
		private void EndScrollTracking(Control grid)
		{
			//grid.Capture = false;
			mScrollTimer.Enabled = false;
			mCapturedGrid = null;
		}

		private void mScrollTimer_Tick(object sender, EventArgs e)
		{
			if (mCapturedGrid == null)
				return;

			//Scroll the view if required
			Point mousePoint = mCapturedGrid.PointToClient(Control.MousePosition);
			mCapturedGrid.ScrollOnPoint(mousePoint);
		}
	}
}
