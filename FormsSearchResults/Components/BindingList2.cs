using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace TheSeeker.Forms
{
    /// <summary>
    /// An optimized binding list that binds itself to a Consumer Control as its Data Source and performs
    /// updates on the control's thread. It uses an Operation Tracker to limit the frequency with which
    /// it informs the control of changes to prevent UI from freezing
    /// </summary>
    /// <typeparam name="TListItem"></typeparam>
    public class BindingList2<TListItem> : BindingList<TListItem>
    {
        private IOperationTracker operationTracker;
        private IDataConsumerControl<TListItem> consumerControl;

        /// <summary>
        /// Automatically binds this list to Consumer Control as its data source
        /// </summary>
        /// <param name="operationTracker">An object that determines when to refresh the underlying list</param>
        /// <param name="consumerControl">A control that will use this binding list as its data source</param>
        public BindingList2(IOperationTracker operationTracker, IDataConsumerControl<TListItem> consumerControl)
        {
            if (operationTracker == null)
                throw new ArgumentNullException(nameof(operationTracker));
            if (consumerControl == null)
                throw new ArgumentNullException(nameof(consumerControl));

            this.consumerControl = consumerControl;
            consumerControl.DataSource = this;

            consumerControl.DataSourceChanged += (source, e) =>
            {
                if (consumerControl.DataSource != this)
                {
                    this.consumerControl = null;
                }
            };

            this.operationTracker = operationTracker;
            this.operationTracker.Finished += () => {
                // Refresh the list to make sure all items are displayed
                if (Items.Count > 0)
                {
                    consumerControl.Invoke(new Action(() => base.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, Items.Count - 1))));
                }
            };
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            // If it's a call to reset the list, don't wait until the tracker allows that.
            // Always check if form handle has been created, otherwise an exception will be thrown
            if ((consumerControl.IsHandleCreated == true) &&
                (e.ListChangedType == ListChangedType.Reset || operationTracker.CanAct))
            {
                consumerControl.Invoke(new Action(() => base.OnListChanged(e)));
            }
        }

        protected override void ClearItems()
        {
            base.ClearItems();
        }
    }
}