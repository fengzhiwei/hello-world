using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace PipeLine.ChildWindow
{
    public partial class TempLayer : DevExpress.XtraEditors.XtraForm
    {
        private AxMapControl m_axMapControl;
        private ITOCControl m_TOCControl;
        public List<ILayer> lLayer = new List<ILayer>();
        public TempLayer(AxMapControl axMapControl,ITOCControl mTOCControl)
        {
            InitializeComponent();
            m_axMapControl = axMapControl;
            m_TOCControl = mTOCControl;
        }

        private void TempLayer_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            ILayer layer;
            for (int i = 0; i < m_axMapControl.LayerCount; i++)
            {
                layer = m_axMapControl.Map.get_Layer(i);
                lLayer.Add(layer);
                IFeatureLayer featurelayer = layer as IFeatureLayer;
                IFeatureClass featureclass = featurelayer.FeatureClass;
                IFields fields = featureclass.Fields;
                int count = fields.FieldCount;
                IField pfield;
                listBox1.Items.Add(featurelayer.Name);
            }
        }

        private void ToTop_bt_Click(object sender, EventArgs e)
        {
            ILayer mTopLayer;
            string layerName = this.listBox1.SelectedItem.ToString();
            //MessageBox.Show(layerName);
            
            for (int i = this.listBox1.SelectedIndex; i > 0; i--)
            {
                string aa = listBox1.SelectedItem.ToString();
                string uptest = this.listBox1.Items[i - 1].ToString();
                //把当前选择行的值与上一行互换 并将选择索引减1
                listBox1.Items[i - 1] = aa;
                listBox1.Items[i] = uptest;
                listBox1.SelectedIndex = i - 1;
            }
            for (int k = 0; k < m_axMapControl.Map.LayerCount; k++)
            {
                if (m_axMapControl.Map.get_Layer(k).Name == layerName)
                {
                    mTopLayer = m_axMapControl.Map.get_Layer(k);
                    m_axMapControl.Map.MoveLayer(mTopLayer, 0);
                    m_axMapControl.ActiveView.Refresh();
                    m_TOCControl.Update();
                }
            }
            //MessageBox.Show(m_axMapControl.Map.get_Layer(0).Name);
            
        }

        private void ToBottom_bt_Click(object sender, EventArgs e)
        {
            ILayer mBottomLayer;
            string BottomlayerName = this.listBox1.SelectedItem.ToString();
            for (int i = this.listBox1.SelectedIndex; i  < this.listBox1.Items.Count -1; i++)
            {
                string aa = listBox1.SelectedItem.ToString();
                string uptest = this.listBox1.Items[i + 1].ToString();
                //把当前选择行的值与上一行互换 并将选择索引减1
                listBox1.Items[i + 1] = aa;
                listBox1.Items[i] = uptest;
                listBox1.SelectedIndex = i + 1;
            }
            for (int k = 0; k < m_axMapControl.Map.LayerCount; k++)
            {
                if (m_axMapControl.Map.get_Layer(k).Name == BottomlayerName)
                {
                    mBottomLayer = m_axMapControl.Map.get_Layer(k);
                    m_axMapControl.Map.MoveLayer(mBottomLayer, m_axMapControl.Map.LayerCount -1);
                    m_axMapControl.ActiveView.Refresh();
                    m_TOCControl.Update();
                }
            }
        }
        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void ToUp_bt_Click(object sender, EventArgs e)
        {
            ILayer upLayer;
            int indexLayer;
            int index = this.listBox1.SelectedIndex;
            if (index <= 0)
            {
                MessageBox.Show("已经是第一层了！");
            }
            else
            {
                string indexStr = (string)this.listBox1.Items[index];
                int upindex = index - 1;
                string upindexStr = (string)this.listBox1.Items[upindex];
                //MessageBox.Show(indexStr + upindexStr);
                this.listBox1.Items[index] = upindexStr;
                this.listBox1.Items[upindex] = indexStr;
                this.listBox1.SelectedIndex = upindex;
            }
            string upLayerName = this.listBox1.SelectedItem.ToString();
            for (int k = 0; k < m_axMapControl.Map.LayerCount; k++)
            {
                if (m_axMapControl.Map.get_Layer(k).Name == upLayerName)
                {
                    indexLayer = k;
                    if (k == 1)
                    {
                        return;
                    }
                    else
                    {
                        upLayer = m_axMapControl.Map.get_Layer(k);
                        m_axMapControl.Map.MoveLayer(upLayer, k - 1);
                        m_axMapControl.ActiveView.Refresh();
                        m_TOCControl.Update();
                    }
                    
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToDown_bt_Click(object sender, EventArgs e)
        {
            ILayer downLayer;
            int indexLayer;
            int index = this.listBox1.SelectedIndex;
            if (index >= this.listBox1.Items.Count - 1)
            {
                MessageBox.Show("已经是最后层了！");
            }
            else
            {
                string indexStr = (string)this.listBox1.Items[index];
                int downindex = index + 1;
                string downindexStr = (string)this.listBox1.Items[downindex];
                //MessageBox.Show(indexStr + upindexStr);
                this.listBox1.Items[index] = downindexStr;
                this.listBox1.Items[downindex] = indexStr;
                this.listBox1.SelectedIndex = downindex;
            }
            string downLayerName = this.listBox1.SelectedItem.ToString();
            for (int k = 0; k < m_axMapControl.Map.LayerCount; k++)
            {
                if (m_axMapControl.Map.get_Layer(k).Name == downLayerName)
                {
                    indexLayer = k;
                    if (k == m_axMapControl.Map.LayerCount -1)
                    {
                        return;
                    }
                    else
                    {
                        downLayer = m_axMapControl.Map.get_Layer(k);
                        m_axMapControl.Map.MoveLayer(downLayer, k + 1);
                        m_axMapControl.ActiveView.Refresh();
                        m_TOCControl.Update();
                    }

                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < m_axMapControl.Map.LayerCount; i++)
            {
                this.listBox1.Items[i] = lLayer[i].Name;
                if (i == 0)
                {
                    waiting.Show(this);
                }
                if (i == m_axMapControl.Map.LayerCount -1)
                {
                    waiting.Hide(this);
                }
                if (m_axMapControl.Map.get_Layer(i).Name == lLayer[i].Name)
                {
                    continue;
                }
                else
                {
                    for (int k = 0; k < m_axMapControl.Map.LayerCount; k++)
                    {
                        if (lLayer[i].Name == m_axMapControl.Map.get_Layer(k).Name)
                        {
                            m_axMapControl.Map.MoveLayer(m_axMapControl.Map.get_Layer(k), i);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                Application.DoEvents();
                
            }

        }



    }
}