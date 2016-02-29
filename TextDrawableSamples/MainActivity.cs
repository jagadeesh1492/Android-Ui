using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using TextDrawableSamples.Util;

namespace TextDrawableSamples
{
    [Activity(Label = "TextDrawableSamples", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        public string TYPE = "TYPE";
        private DataSource mDataSource;
        private ListView _mListView;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            _mListView = FindViewById<ListView>(Resource.Id.listView);
            mDataSource = new DataSource(this);
            // mListView.Adapter=new sa
            _mListView.ItemClick += MListView_ItemClick;
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private class SampleAdapter : BaseAdapter
        {
            private DataSource _mDataSource;
            public SampleAdapter(DataSource mDataSource)
            {
                _mDataSource = mDataSource;
            }
            public override Object GetItem(int position)
            {
                throw new System.NotImplementedException();
            }

            public override long GetItemId(int position)
            {
                throw new System.NotImplementedException();
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                throw new System.NotImplementedException();
            }

            public override int Count => _mDataSource.GetCount();
        }
        private  class ViewHolder
        {

            private ImageView imageView;

            private TextView textView;

            private ViewHolder(View view)
            {
                imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
                textView = view.FindViewById<TextView>(Resource.Id.textView);
            }
        }

    }
}
    

