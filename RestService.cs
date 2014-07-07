using System;
using System.IO;
using System.Net;

namespace ProgrammingWeapons
{
    public class RestService : PropertyChangedBase, IRestService
    {
        #region IsLoading bool changeble property
        public event Action<bool> IsLoadingChanged;

        private bool _isLoading;
        [Magic] public bool IsLoading {
            get { return _isLoading; }
            set {
                _isLoading = value;
                IsLoadingChanged.Raise(_isLoading);
            }
        }
        #endregion
        
        

        public virtual void GeneralAsyncLoad(string url, Action<Stream> responceStreamHandler) {
            try {
                IsLoading = true;
                var request = WebRequest.Create(url);
                request.BeginGetResponse((result) => generalProcessResponce(request, result, responceStreamHandler), null);
            }
            catch (Exception e) {
                //todo: log
                IsLoading = false;
#if DEBUG
                throw;
#endif
            }
        }


        protected virtual void generalProcessResponce(WebRequest request, IAsyncResult result, Action<Stream> responceStreamHandler) {
            IsLoading = false;
            try {
                var responce = request.EndGetResponse(result);
                using (var stream = responce.GetResponseStream()) {
                    responceStreamHandler(stream);
                }
            }
            catch (Exception e) {
                //todo: log
#if DEBUG
                throw;
#endif
            }
        }
        
    }
}