using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using ShareLibrary;
using System.Threading.Tasks;

namespace MedicalAndroidClient
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private List<string> bioValuesListVM = new List<string>();
        private MedicalWebClient medicalWebClient = new MedicalWebClient();
        private ArrayAdapter<string> bioValuesAdapter;
        private ListView lsvBioValues;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            TextView txvHost = FindViewById<TextView>(Resource.Id.txvHost);
            lsvBioValues = FindViewById<ListView>(Resource.Id.lsvBioValues);
            Button btnUpdate = FindViewById<Button>(Resource.Id.btnUpdate);

            txvHost.Text = $"服务器: {medicalWebClient.Host}";

            btnUpdate.Click += BtnUpdate_Click;

            bioValuesListVM.Add("没有数据");
            bioValuesAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, bioValuesListVM);
            lsvBioValues.Adapter = bioValuesAdapter;
        }

        private void BtnUpdate_Click(object sender, System.EventArgs e)
        {
            UpdateBioValuesAsync();
        }

        private async Task UpdateBioValuesAsync()
        {
            //从服务器获取生理参数
            var bioValuesList = await medicalWebClient.GetBioValuesAsync();

            //更新用于显示的字符串集合，不能修改字符串集合对象自身，只能修改集合内容，因为集合已经绑定到ArrayAdapter
            //bioValuesListVM.Clear();
            bioValuesAdapter.Clear();

            if (bioValuesList.Count == 0)
            {
                Toast.MakeText(this, "没有数据", ToastLength.Long).Show();

                //bioValuesListVM.Add("没有数据");
                bioValuesAdapter.Add("没有数据");
            }
            else
            {
                Toast.MakeText(this, $"有{bioValuesList.Count}个数据", ToastLength.Long).Show();

                foreach (var bioValues in bioValuesList)
                {
                    string msg = $"{bioValues.CreateTime:yyyy-MM-dd HH:mm:ss}, 心率{bioValues.HR}bpm, 血氧{bioValues.SpO2}%";
                    //bioValuesListVM.Add(msg);
                    bioValuesAdapter.Add(msg);
                }
            }

            //通知UI更新
            bioValuesAdapter.NotifyDataSetChanged();
        }
    }
}