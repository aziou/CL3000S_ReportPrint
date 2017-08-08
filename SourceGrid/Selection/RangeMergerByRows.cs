using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace SourceGrid.Selection
{
	/// <summary>
	/// Returns selected row indexes
	/// </summary>
	public class RangeMergerByRows
	{
		private List<Range> m_ranges = new List<Range>();
		private int m_columnStart = 0;
		private int m_columnEnd = 0;
		
		public RangeMergerByRows()
		{
		}
		
		private void SortList()
		{
			m_ranges.Sort(new RangeComparerByRows());
		}
		
		private void InternalAdd(RangeRegion rangeRegion)
		{
			foreach (Range range in rangeRegion)
			{
				m_ranges.Add(range);
			}
		}
		
		public List<Range> GetSelectedRowRegions(int startColumn, int endColumn)
		{
			if (startColumn > endColumn)
				throw new ArgumentException("end column can not be less than startColumn");
			List<Range> ranges = new List<Range>();
			foreach (Range range in m_ranges)
			{
				ranges.Add(new Range(range.Start.Row, startColumn, range.End.Row, endColumn));
			}
			return ranges;
		}
		
		/// <summary>
		/// </summary>
		/// <param name="rangeToMerge"></param>
		/// <returns></returns>
		private void Merge(Range rangeToMerge)
		{
			while (true)
			{
				rangeToMerge = MergeRecursive(rangeToMerge);
				if (rangeToMerge == Range.Empty)
					return;
			}
		}
		
		/// <summary>
		/// Merges given range with one of the intersecting range in m_ranges
		/// if no intersecting ranges is found, then rangeToMerge is
		/// added to m_ranges
		/// </summary>
		/// <param name="rangeToMerge"></param>
		/// <returns></returns>
		private Range MergeRecursive(Range rangeToMerge)
		{
			for (int i = 0; i < m_ranges.Count; i++)
			{
				Range range = m_ranges[i];
				if (range.IntersectsWith(rangeToMerge))
				{
					m_ranges.Remove(range);
					return MergeByRow(rangeToMerge, range);
				}
			}
			m_ranges.Add(rangeToMerge);
			return Range.Empty;
		}
		
		/// <summary>
		/// Returns true if at least two ranges were joined
		/// </summary>
		/// <returns></returns>
		private bool JoinAjdancedRecursive()
		{
			SortList();
			for (int i = 0; i < m_ranges.Count - 1; i++)
			{
				Range first = m_ranges[i];
				Range second = m_ranges[i+1];
				if (first.End.Row + 1 >= second.Start.Row)
				{
					Range newRange = new Range(first.Start.Row, m_columnStart,
					                           second.End.Row, m_columnEnd);
					m_ranges.Remove(first);
					m_ranges.Remove(second);
					m_ranges.Add(newRange);
					return true;
				}
			}
			return false;
		}
		
		private void JoinAdjanced()
		{
			while (JoinAjdancedRecursive())
			{
			}
		}
		
		private Range NormalizeRange(Range rangeToNormalize)
		{
			return new Range(rangeToNormalize.Start.Row, m_columnStart,
			                 rangeToNormalize.End.Row, m_columnEnd);
		}
		
		public RangeMergerByRows AddRange(Range rangeToAdd)
		{
			rangeToAdd = NormalizeRange(rangeToAdd);
			Merge(rangeToAdd);
			JoinAdjanced();
			return this;
		}
		
		/// <summary>
		/// Returns new range witch is the max of both ranges in row axis
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		private Range MergeByRow(Range first, Range second)
		{
			int x = first.Start.Row;
			if (x > second.Start.Row)
				x = second.Start.Row;
			
			int x1 = first.End.Row;
			if (x1 < second.End.Row)
				x1 = second.End.Row;
			return new Range(x, m_columnStart, x1, m_columnEnd);
		}
		
		public bool RemoveRangeRecursive(Range rangeToRemove)
		{
			rangeToRemove = NormalizeRange(rangeToRemove);
			for (int i = 0; i < m_ranges.Count; i++)
			{
				Range range = m_ranges[i];
				if (range.IntersectsWith(rangeToRemove) == false)
					continue;
				Range intersection = range.Intersect(rangeToRemove);
				m_ranges.Remove(range);
				RangeRegion excludedRanges = range.Exclude(intersection);
				InternalAdd(excludedRanges);
				return true;
			}
			return false;
		}
		
		public RangeMergerByRows RemoveRange(Range rangeToRemove)
		{
			while (RemoveRangeRecursive(rangeToRemove))
			{
				
			}
			return this;
		}
		
		/// <summary>
		/// Returns a list of indexes of rows
		/// </summary>
		/// <returns></returns>
		public IList<int> GetRowsIndex()
		{
			IList<int> rowIndex = new List<int>();
			foreach (Range range in m_ranges)
			{
				for (int r = range.Start.Row; r <= range.End.Row; r++)
				{
					rowIndex.Add(r);
				}
			}
			return rowIndex;
		}
		
		public bool IsEmpty()
		{
			return m_ranges.Count == 0;
		}
		
		public bool IsSelectedRow(int rowIndex)
		{
			Position p = new Position(rowIndex, 0);
			foreach (Range range in m_ranges)
			{
				if (range.Contains(p) == true)
					return true;
			}
			return false;
		}
		
		public RangeMergerByRows Clear()
		{
			m_ranges.Clear();
			return this;
		}
	}
}

