using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace BookApp.iOS.TableViewSources
{
    public class BestsellersTableViewSource : MvxTableViewSource
    {
        public BestsellersTableViewSource(UITableView tableView) : base(tableView)
        {
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            try
            {
                var cell = (BestsellersTableCell)tableView.DequeueReusableCell(BestsellersTableCell.Identifier, indexPath);
                return cell;
            } catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}