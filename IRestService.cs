using System;
using System.ComponentModel;
using System.IO;

namespace ProgrammingWeapons
{
    public interface IRestService: INotifyPropertyChanged
    {
      //  void MakeRequest<T>(string url, string method, Action<T> successAction, Action<Exception> errorAction);

        #region IsLoading bool changeble property

        event Action<bool> IsLoadingChanged;
        bool IsLoading { get; }

        #endregion



        void GeneralAsyncLoad(string url, Action<Stream> responceStreamHandler);
    }
}