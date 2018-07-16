using System;

namespace WindowsFormsApp1
{
    internal class CommonOpenFileDialog
    {
        public string InitialDirectory { get; internal set; }
        public bool IsFolderPicker { get; internal set; }
        public string FileName { get; internal set; }

        internal object ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}