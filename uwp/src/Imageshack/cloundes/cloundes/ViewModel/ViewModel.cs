﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using BitStamp.Model;
using cloundes.Model;

namespace cloundes.ViewModel
{
    public class ViewModel : NotifyProperty
    {
        public ViewModel()
        {
            Accound = AppId.Accound;
        }

        public CloundesAccound Accound
        {
            set;
            get;
        }

        public string Address
        {
            set
            {
                _address = value;
                OnPropertyChanged();
            }
            get
            {
                return _address;
            }

        }

        public async void UploadFile()
        {
            FileOpenPicker pick = new FileOpenPicker();
            pick.FileTypeFilter.Add(".jpg");
            StorageFile file=await pick.PickSingleFileAsync();
            QnUploadImage uploadImage=new QnUploadImage(file);
            uploadImage.OnUploaded += (s, e) =>
            {
                if (e)
                {
                    Address = uploadImage.Url;
                }
            };
            uploadImage.UploadImage();
        }

        private string _address;
    }
}